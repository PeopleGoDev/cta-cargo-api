using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Domain.Repositories;
public interface IMessageSubmitFileRepository
{
    void CreateMessageSubitFile(MessageSubmitFile messageSubmitFile);
    Task<MessageSubmitFile> GetMessageSubmitFileByIdAsync(int id);
    Task<List<MessageSubmitFile>> GetMessageSubmitFileBySourceIdAsync(FileType fileType, int sourceId);
}