using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Enums;
using CtaCargo.CctImportacao.Domain.Repositories;
using CtaCargo.CctImportacao.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Repository.SQL;

public class MessageSubmitFileRepository : IMessageSubmitFileRepository
{
    private readonly ApplicationDbContext _context;

    public MessageSubmitFileRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void CreateMessageSubitFile(MessageSubmitFile messageSubmitFile)
    {
        if (messageSubmitFile is null)
        {
            throw new ArgumentNullException(nameof(messageSubmitFile));
        }

        _context.MessageSubmitFiles.Add(messageSubmitFile);
    }

    public async Task<MessageSubmitFile?> GetMessageSubmitFileByIdAsync(int id)
    {
        return await _context.MessageSubmitFiles
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<MessageSubmitFile>> GetMessageSubmitFileBySourceIdAsync(
        FileType fileType,
        int sourceId)
    {
        return await _context.MessageSubmitFiles
            .Where(x => x.SourceId == sourceId && x.Type == fileType)
            .ToListAsync();
    }
}
