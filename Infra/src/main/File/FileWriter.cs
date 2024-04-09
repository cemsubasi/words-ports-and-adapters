namespace Infra.File;

public class FileWriter {
    private string[] PermittedExtensions => [".jpg", ".jpeg", ".png", ".pdf"];
    public async Task<string> WriteAsync(IFormFile formFile, CancellationToken cancellationToken) {
        var fileDirectory = "files";
        if (!Directory.Exists(fileDirectory)) {
            Directory.CreateDirectory(fileDirectory);
        }

        if (formFile.Length == 0) {
            throw new ArgumentException("File is empty");
        }

        if (formFile.Length > 10_000_000) {
            throw new ArgumentException("File is too large");
        }

        if (!this.PermittedExtensions.Contains(Path.GetExtension(formFile.FileName).ToLower())) {
            throw new ArgumentException("File type not permitted");
        }

        var filePath = Path.Combine(fileDirectory, Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(formFile.FileName));
        using var stream = new FileStream(filePath, FileMode.Create);
        await formFile.CopyToAsync(stream, cancellationToken);

        return filePath;
    }
}