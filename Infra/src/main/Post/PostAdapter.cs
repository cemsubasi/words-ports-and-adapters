using Domain.Category.Entity;
using Domain.Common;
using Domain.Post.Entity;
using Domain.Post.Port;
using Domain.Post.UseCase;
using Infra.Context;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infra.Post.Adapter;

public class PostAdapter : PostPort {
  private readonly MainDbContext context;
  public PostAdapter(MainDbContext context) {
    this.context = context;
  }

  /* public async Task CreateAsync(CreatePost useCase, CancellationToken cancellationToken) { */
  /*   var entity = useCase.Adapt<PostEntity>(); */

  /*   // TODO: categoryId or categoryName must be validated */
  /*   if (!useCase.CategoryId.HasValue) { */
  /*     var category = await this.context.Categories */
  /*       .AsNoTracking() */
  /*       .SingleOrDefaultAsync(x => x.Category == useCase.CategoryName, cancellationToken); */

  /*     if (category is not null) { */
  /*       entity.CategoryId = category.Id; */
  /*     } else { */
  /*       var categoryEntity = new CategoryEntity { */
  /*         Id = Guid.NewGuid(), */
  /*         Category = useCase.CategoryName, */
  /*       }; */

  /*       entity.CategoryId = categoryEntity.Id; */
  /*       entity.Category = categoryEntity; */

  /*       _ = await this.context.Categories.AddAsync(categoryEntity, cancellationToken); */
  /*       _ = await this.context.SaveChangesAsync(cancellationToken); */
  /*     } */
  /*   } */

  /*   _ = await this.context.Posts.AddAsync(entity, cancellationToken); */
  /*   var result = await this.context.SaveChangesAsync(cancellationToken); */
  /*   if (result.Equals(0)) { */
  /*     throw new DbUpdateException("An error occoured."); */
  /*   } */
  /* } */

  public async Task CreateAsync(CreatePost useCase, CancellationToken cancellationToken) {
    var entity = useCase.Adapt<PostEntity>();

    await ValidateAndSetCategoryAsync(useCase, entity, cancellationToken);

    await this.context.Posts.AddAsync(entity, cancellationToken);
    await this.context.SaveChangesAsync(cancellationToken);
  }

  private async Task ValidateAndSetCategoryAsync(CreatePost useCase, PostEntity entity, CancellationToken cancellationToken) {
    if (!useCase.CategoryId.HasValue) {
      var category = await FindOrCreateCategoryAsync(useCase.CategoryName, cancellationToken);
      entity.CategoryId = category.Id;
    }
  }

  private async Task<CategoryEntity> FindOrCreateCategoryAsync(string categoryName, CancellationToken cancellationToken) {
    var category = await this.context.Categories
        .AsNoTracking()
        .SingleOrDefaultAsync(x => x.Category == categoryName, cancellationToken);

    if (category is null) {
      category = new CategoryEntity {
        Id = Guid.NewGuid(),
        Category = categoryName,
      };

      await this.context.Categories.AddAsync(category, cancellationToken);
      await this.context.SaveChangesAsync(cancellationToken);
    }

    return category;
  }

  public async Task<PostEntity[]> RetrieveAllAsync(RetrieveAllPosts useCase, CancellationToken cancellationToken) {
    var posts = await this.context.Posts
      .AsNoTrackingWithIdentityResolution()
      .Where(x => x.AccountId == useCase.AccountId)
      .Include(x => x.Comments)
      .ThenInclude(x => x.SubComments)
      .Include(x => x.Account)
      .Include(x => x.Category)
      .Skip(useCase.Page * useCase.Size)
      .Take(useCase.Size)
      .ToArrayAsync(cancellationToken);

    return posts;
  }

  public async Task<PostEntity> RetrieveAsync(RetrievePost useCase, CancellationToken cancellationToken) {
    var post = await this.context.Posts
      .Where(x => x.AccountId == useCase.AccountId && x.Id == useCase.Id)
      .Include(x => x.Comments)
      .SingleOrDefaultAsync(cancellationToken);

    return post;
  }
}
