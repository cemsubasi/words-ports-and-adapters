using Microsoft.AspNetCore.StaticFiles;

namespace Infra.ass.Operation;

public class FileReader {
    public async Task<(byte[], string, string)> ReadAsync(string path, CancellationToken cancellationToken) {
        var file = await System.IO.File.ReadAllBytesAsync(path, cancellationToken);

        var provider = new FileExtensionContentTypeProvider();

        var contentType = string.Empty;
        var getContentTypeResult = provider.TryGetContentType(Path.GetFileName(path), out contentType);
        if (string.IsNullOrEmpty(contentType)) {
            contentType = "application/octet-stream";
        }

        return (file, contentType, Path.GetFileName(path));
    }
}