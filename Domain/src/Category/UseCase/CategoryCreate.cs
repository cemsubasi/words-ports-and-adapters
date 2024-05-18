namespace Domain.Category.UseCase;

public class CategoryCreate {
   public Guid AccountId { get; set; }
   public string Name { get; set; }

   public static CategoryCreate Build(Guid id, string name) {
      return new CategoryCreate {
         AccountId = id,
         Name = name,
      };
   }
}
