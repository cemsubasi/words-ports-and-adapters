using Domain.Common;
using Domain.File.UseCase;

namespace Domain.File.Model;

public class RetrieveAllFilesRequest(int page, int size) : DataRequest(page, size) {
    public RetrieveAllFiles ToUseCase(Guid accountId) {
        return RetrieveAllFiles.Build(accountId, this.Page, this.Size);
    }
}
