using Domain.Comment.Entity;
using Domain.Comment.UseCase;
using Domain.Post.Entity;
using Domain.Post.UseCase;

namespace Domain.Post.Port;

public interface PostPort {
  Task CreateAsync(CreatePost useCase, CancellationToken cancellationToken);
  Task<PostEntity> RetrieveAsync(RetrievePost useCase, CancellationToken cancellationToken);
  Task<PostEntity[]> RetrieveAllAsync(RetrieveAllPosts useCase, CancellationToken cancellationToken);
  Task AddComment(CreateComment useCase, CancellationToken cancellationToken);

  Task<CommentEntity> RetrieveCommentsAsync(RetrieveComment useCase, CancellationToken cancellationToken);

  Task<CommentEntity[]> RetrieveCommentsAsync(RetrieveAllComments useCase, CancellationToken cancellationToken);

}
