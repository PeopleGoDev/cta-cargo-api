using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Batch.Services.Interface;

internal interface IFileService
{
    bool Exists();
    bool FileExist(string fileSource);
    List<DirectoryItemFile> ListFiles(string filter = "");
    bool MoveFile(string fileSource, string fileTarget);
    bool MoveToImportFolder(string fileSource);
    bool MoveToErrorFolder(string fileSource);
    Stream ReadFile(string fileName);
    bool RemoveFile(string fileSource);
}

public struct DirectoryItemFile
{
    public string? DateCreated;
    public long? FileSize;
    public string Name;
}