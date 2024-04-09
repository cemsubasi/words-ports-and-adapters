namespace Domain.File.UseCase;

public class RetrieveFile {
    public Guid Id { get; private set; }
    public Guid AccountId { get; private set; }
    public RetrieveFile(Guid id, Guid accountId) {
        this.Id = id;
        this.AccountId = accountId;
    }

    public static RetrieveFile Build(Guid id, Guid accountId) {
        return new RetrieveFile(id, accountId);
    }
}