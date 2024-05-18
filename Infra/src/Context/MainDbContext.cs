using Domain.Account.Entity;
using Domain.Answer.Entity;
using Domain.Category.Entity;
using Domain.File.Entity;
using Domain.Identity.Entity;
using Domain.Question.Entity;
using Domain.SuperAccount.Entity;
using Infra.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;

namespace Infra.Context;

public class MainDbContext : DbContext {
  private readonly string connectionString;
  public MainDbContext() {
  }

  public MainDbContext(IOptions<DBOptions> options) : base() {
    this.connectionString = options.Value.MainDb;
    /* Console.WriteLine("Connection string is {0}", this.connectionString); */

    /* Console.WriteLine(configuration["ConnectionStrings:MainDb"]); */
  }

  public MainDbContext(string connectionString) : base() {
    this.connectionString = connectionString;
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

    _ = builder.Entity<IdentityEntity>()
        .Property(x => x.Id).ValueGeneratedOnAdd();

    _ = builder.Entity<AccountEntity>()
      .HasOne(x => x.Creator)
      .WithMany()
      .HasForeignKey(x => x.CreatedBy);
  }

  public virtual DbSet<AccountEntity> Accounts { get; set; }
  public virtual DbSet<SuperAccountEntity> SuperAccounts { get; set; }
  public DbSet<CategoryEntity> Categories { get; set; }
  public DbSet<QuestionEntity> Questions { get; set; }
  public DbSet<AnswerEntity> Answers { get; set; }
  public virtual DbSet<FileEntity> Files { get; set; }
  public virtual DbSet<IdentityEntity> Identities { get; set; }
}
