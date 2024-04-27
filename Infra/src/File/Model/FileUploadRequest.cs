using System.Text.Json.Serialization;
using Domain.File.UseCase;

namespace Infra.File.Model;

public class FileUploadRequest {
    public IFormFile File { get; set; }

    public bool IsVisible { get; set; }

    public CreateFile ToUseCase(Guid accountId, string path) {
        return CreateFile.Build(
            accountId: accountId,
            name: Path.GetFileNameWithoutExtension(path),
            path: path,
            extension: Path.GetExtension(this.File.FileName),
            contentType: this.File.ContentType,
            size: this.File.Length,
            isVisible: this.IsVisible);
    }
}