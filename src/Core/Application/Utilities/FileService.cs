using Application.Configurations;
using Application.Interfaces.Utilities;
using Domain.Enumeradores.Notificacao;
using Domain.Interfaces.Utilities;

namespace Application.Utilities
{
    public class FileService(INotificador _notificador) : IFileService
    {
        private readonly string _baseDirectory = AppSettings.CompanyFilePaths;

        public async Task<byte[]> GetFileAsync(string folder, string fileName)
        {
            try
            {
                string filePath = Path.Combine(_baseDirectory, folder, fileName);

                if(!File.Exists(filePath))
                {
                    _notificador.Notify(EnumTipoNotificacao.ErroCliente,
                        $"O arquivo não foi encontrado no caminho {filePath}.");

                    return null;
                }

                return await File.ReadAllBytesAsync(filePath);
            }
            catch(Exception)
            {
                _notificador.Notify(EnumTipoNotificacao.ErroInterno,
                    "Ocorreu um erro ao buscar arquivo.");

                return null;
            }
        }

        public async Task<bool> SaveFileAsync(string folder, string fileName, byte[] fileData)
        {
            try
            {
                string directoryPath = Path.Combine(_baseDirectory, folder);

                if(!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                string filePath = Path.Combine(directoryPath, fileName);

                if(File.Exists(filePath))
                {
                    _notificador.Notify(EnumTipoNotificacao.Informacao,
                        "O aquivo já existe.");

                    return true;
                }

                await File.WriteAllBytesAsync(filePath, fileData);

                return true;
            }
            catch(Exception)
            {
                _notificador.Notify(EnumTipoNotificacao.ErroInterno,
                    "Ocorreu um erro ao salvar arquivo.");

                return false;
            }
        }

        public bool DeleteFile(string folder, string fileName)
        {
            try
            {
                string filePath = Path.Combine(_baseDirectory, folder, fileName);

                if(!File.Exists(filePath))
                {
                    _notificador.Notify(EnumTipoNotificacao.ErroCliente, $"O arquivo não foi encontrado no caminho {filePath}.");
                    return false;
                }

                File.Delete(filePath);

                return true;
            }
            catch(Exception)
            {
                _notificador.Notify(EnumTipoNotificacao.ErroInterno, "Ocorreu um erro ao deletar arquivo.");
                return false;
            }
        }
    }
}
