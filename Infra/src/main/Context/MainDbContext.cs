using Domain.Account.Entity;
using Domain.Category.Entity;
using Domain.Comment.Entity;
using Domain.File.Entity;
using Domain.Post.Entity;
using Domain.SuperAccount.Entity;
using Infra.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;

namespace Infra.Context;

public class MainDbContext : DbContext {
  private readonly string connectionString;
  private readonly ILogger<MainDbContext> logger;
  public MainDbContext() {
  }

  public MainDbContext(ILogger<MainDbContext> logger, IConfiguration configuration, IOptions<DBOptions> options) : base() {
    this.connectionString = options.Value.MainDb;
    /* Console.WriteLine("Connection string is {0}", this.connectionString); */

    /* Console.WriteLine(configuration["ConnectionStrings:MainDb"]); */
    this.logger = logger;
  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
    _ = optionsBuilder.UseMySql(this.connectionString, new MySqlServerVersion(new Version(8, 0, 32)));
    /* _ = optionsBuilder.LogTo(message => this.logger.LogInformation(message), LogLevel.Information); */
  }

  public override ValueTask<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken = default) {
    var property = entity.GetType().GetProperties().Where(x => x.Name == "CreatedAt").FirstOrDefault();
    if (property is not null) {
      property.SetValue(entity, DateTimeOffset.UtcNow);
    }

    return base.AddAsync(entity, cancellationToken);
  }

  protected override void OnModelCreating(ModelBuilder builder) {
    _ = builder.Entity<AccountEntity>()
        .ToTable("Accounts");

    _ = builder.Entity<SuperAccountEntity>()
        .ToTable("SuperAccounts");

    _ = builder.Entity<AccountEntity>().HasIndex(x => x.Email).IsUnique();
    _ = builder.Entity<AccountEntity>().Property(x => x.Email).IsRequired();
    _ = builder.Entity<AccountEntity>().Property(x => x.Name).IsRequired();
    _ = builder.Entity<AccountEntity>().Property(x => x.Password).IsRequired();
    _ = builder.Entity<AccountEntity>().Property(x => x.PasswordSalt).IsRequired();

    _ = builder.Entity<SuperAccountEntity>().HasIndex(x => x.Email).IsUnique();
    _ = builder.Entity<SuperAccountEntity>().Property(x => x.Email).IsRequired();
    _ = builder.Entity<SuperAccountEntity>().Property(x => x.Name).IsRequired();
    _ = builder.Entity<SuperAccountEntity>().Property(x => x.Password).IsRequired();
    _ = builder.Entity<SuperAccountEntity>().Property(x => x.PasswordSalt).IsRequired();

    _ = builder.Entity<CommentEntity>()
      .HasOne(x => x.Creator)
      .WithMany()
      .HasForeignKey(x => x.CreatedBy);

    _ = builder.Entity<AccountEntity>()
      .HasOne(x => x.Creator)
      .WithMany()
      .HasForeignKey(x => x.CreatedBy);

    _ = builder.Entity<PostEntity>()
      .HasOne(x => x.Category)
      .WithMany(x => x.Posts)
      .HasForeignKey(x => x.CategoryId);

    _ = builder.Entity<PostEntity>()
      .HasOne(x => x.Account)
      .WithMany(x => x.Posts)
      .HasForeignKey(x => x.AccountId);

    _ = builder.Entity<PostEntity>()
      .HasMany(x => x.Comments)
      .WithOne(x => x.Post)
      .HasForeignKey(x => x.PostId);

    _ = builder.Entity<CommentEntity>()
      .HasOne(x => x.ParentComment)
      .WithMany(x => x.SubComments)
      .HasForeignKey(x => x.ParentCommentId);
  }

  public virtual DbSet<AccountEntity> Accounts { get; set; }
  public virtual DbSet<SuperAccountEntity> SuperAccounts { get; set; }
  public virtual DbSet<PostEntity> Posts { get; set; }
  public virtual DbSet<CommentEntity> Comments { get; set; }
  public virtual DbSet<CategoryEntity> Categories { get; set; }
  public virtual DbSet<FileEntity> Files { get; set; }
}
