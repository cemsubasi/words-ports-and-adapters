using System.Text.Json.Serialization;
using Domain.Common;

namespace Domain.File.UseCase;

public class RetrieveAllFiles : DataRequest {
    [JsonIgnore]
    public Guid AccountId { get; private set; }

    private RetrieveAllFiles(Guid accountId, int page, int size) : base(page, size) {
        this.AccountId = accountId;
    }

    public static RetrieveAllFiles Build(Guid accountId, int page, int size) {
        return new RetrieveAllFiles(accountId, page, size);
    }
}
