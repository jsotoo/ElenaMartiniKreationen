namespace ElenaMartiniKreationen.Server.Services
{
    public class FileUploader : IFileUploader
    {

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<FileUploader> _logger;

        public FileUploader(IWebHostEnvironment webHostEnvironment, ILogger<FileUploader> logger)
        {
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public async Task<string> UploadFileAsync(string? base64Image, string? file)
        {
            if (string.IsNullOrWhiteSpace(base64Image) || string.IsNullOrWhiteSpace(file))
            {
                return string.Empty;
            }

            try
            {
                var carpeta = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                var bytes = Convert.FromBase64String(base64Image);

                var rutaCompleta = Path.Combine(carpeta, file);

                await using var fileStream = new FileStream(rutaCompleta, FileMode.Create);
                await fileStream.WriteAsync(bytes, 0, bytes.Length);

                return $"/uploads/{file}";
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Error al subir el archivo {archivo}", file);
                return string.Empty;
            }
        }
    }
}
