using FUForum.BackendServer.Helpers;
using FUForum.ViewModels.Contents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FUForum.BackendServer.Controllers;

public partial class KnowledgeBasesController
{
    #region  Attachments

    [HttpGet("{knowledgeBaseId}/attachments")]
    public async Task<IActionResult> GetAttachment(int knowledgeBaseId)
    {
        var query = await _context.Attachments
            .Where(x => x.KnowledgeBaseId == knowledgeBaseId)
            .Select(c => new AttachmentVM()
            {
                Id = c.Id,
                LastModifiedDate = c.LastModifiedDate,
                CreateDate = c.CreateDate,
                FileName = c.FileName,
                FilePath = c.FilePath,
                FileSize = c.FileSize,
                FileType = c.FileType,
                KnowledgeBaseId = c.KnowledgeBaseId
            }).ToListAsync();

        return Ok(query);
    }

    [HttpDelete("{knowledgeBaseId}/attachments/{attachmentId}")]
    [ApiValidationFilter]
    public async Task<IActionResult> DeleteAttachment(int attachmentId)
    {
        var attachment = await _context.Attachments.FindAsync(attachmentId);
        if (attachment == null)
            return NotFound();

        _context.Attachments.Remove(attachment);

        var result = await _context.SaveChangesAsync();
        if (result > 0)
        {
            return Ok();
        }
        return BadRequest(new ApiBadRequestResponse("Delete Attachment failed"));
    }

    #endregion
}