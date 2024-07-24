using Azure.Storage.Files.Shares.Models;
using Azure.Storage.Files.Shares;
using CtaCargo.CctImportacao.Batch.Services.Interface;

namespace CtaCargo.CctImportacao.Batch.Services;

public class AzureSharedFolderService : IFileService
{

    private readonly string _sharedFolder;
    private readonly string _connectionString;
    private readonly string _folder;
    private readonly string _archiveFolder;
    private readonly ShareClient _shareClient;

    public AzureSharedFolderService(string connectionString, string sharedFolder, string? folder)
    {
        _sharedFolder = sharedFolder;
        _connectionString = connectionString;
        _folder = folder != null? folder.Split(";")[0]: "/";
        _archiveFolder = folder != null ? folder.Split(";")[1] : "/";
        _shareClient = new ShareClient(_connectionString, _sharedFolder);
    }

    public bool Exists()
    {
        throw new NotImplementedException();
    }

    public bool FileExist(string fileSource)
    {
        throw new NotImplementedException();
    }

    public List<DirectoryItemFile> ListFiles(string filter = "")
    {
        var folders = new List<DirectoryItemFile>();

        var remaining = new Queue<ShareDirectoryClient>();

        remaining.Enqueue(_shareClient.GetDirectoryClient(_folder));

        while (remaining.Count > 0)
        {
            ShareDirectoryClient dir = remaining.Dequeue();
            foreach (ShareFileItem item in dir.GetFilesAndDirectories())
            {
                if( item.IsDirectory ) { continue; }

                folders.Add(new DirectoryItemFile
                {
                    Name = item.Name,
                    DateCreated = item.Properties.CreatedOn?.ToString(),
                    FileSize = item.FileSize
                });
            }
        }
        return folders;
    }

    public bool MoveFile(string fileSource, string fileTarget)
    {
        ShareDirectoryClient _directory = _shareClient.GetDirectoryClient(_folder);

        ShareFileClient myFile = _directory.GetFileClient(fileSource);

        myFile.Rename($"{_archiveFolder}/{fileTarget}");

        return true;
    }

    public bool MoveToErrorFolder(string fileSource)
    {
        return MoveFile(fileSource, $"{fileSource}.ERROR");
    }

    public bool MoveToImportFolder(string fileSource)
    {
        return MoveFile(fileSource, $"{fileSource}.IMPORTED");
    }

    public Stream ReadFile(string fileName)
    {
        ShareClient share = new ShareClient(_connectionString, _sharedFolder);

        ShareDirectoryClient directory = share.GetDirectoryClient(_folder);
        
        ShareFileClient file = directory.GetFileClient(fileName);
        
        ShareFileDownloadInfo download = file.Download().Value;
        
        return download.Content;
    }

    public bool RemoveFile(string fileSource)
    {
        ShareDirectoryClient _directory = _shareClient.GetDirectoryClient(_folder);

        ShareFileClient myFile = _directory.GetFileClient(fileSource);

        myFile.Delete();

        return true;
    }
}
