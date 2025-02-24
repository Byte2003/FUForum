using FUForum.BackendServer.Data.Entities;
using FUForum.BackendServer.Helpers;
using FUForum.ViewModels;
using FUForum.ViewModels.Contents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FUForum.BackendServer.Controllers;

public partial class KnowledgeBasesController
{
    #region Reports

    [HttpGet("{knowledgeBaseId}/reports/filter")]
    public async Task<IActionResult> GetReportsPaging(int knowledgeBaseId, string filter, int pageIndex,
        int pageSize)
    {
        var query = _context.Reports.Where(x => x.KnowledgeBaseId == knowledgeBaseId).AsQueryable();
        if (!string.IsNullOrEmpty(filter))
        {
            query = query.Where(x => x.Content.Contains(filter));
        }

        var totalRecords = await query.CountAsync();
        var items = await query.Skip((pageIndex - 1 * pageSize))
            .Take(pageSize)
            .Select(c => new ReportVM()
            {
                Id = c.Id,
                Content = c.Content,
                CreateDate = c.CreateDate,
                KnowledgeBaseId = c.KnowledgeBaseId,
                LastModifiedDate = c.LastModifiedDate,
                IsProcessed = false,
                ReportUserId = c.ReportUserId
            })
            .ToListAsync();

        var pagination = new Pagination<ReportVM>
        {
            Items = items,
            TotalRecords = totalRecords,
        };
        return Ok(pagination);
    }

    [HttpGet("{knowledgeBaseId}/reports/{reportId}")]
    public async Task<IActionResult> GetReportDetail(int knowledgeBaseId, int reportId)
    {
        var report = await _context.Reports.FindAsync(reportId);
        if (report == null)
            return NotFound(new ApiNotFoundResponse("Cannot found report"));

        var reportVm = new ReportVM()
        {
            Id = report.Id,
            Content = report.Content,
            CreateDate = report.CreateDate,
            KnowledgeBaseId = report.KnowledgeBaseId,
            LastModifiedDate = report.LastModifiedDate,
            IsProcessed = report.IsProcessed,
            ReportUserId = report.ReportUserId
        };

        return Ok(reportVm);
    }

    [HttpPost("{knowledgeBaseId}/reports")]
    public async Task<IActionResult> PostReport(int knowledgeBaseId, [FromBody] ReportCreateRequest request)
    {
        var report = new Report()
        {
            Content = request.Content,
            KnowledgeBaseId = knowledgeBaseId,
            ReportUserId = request.ReportUserId,
            IsProcessed = false
        };
        _context.Reports.Add(report);

        var knowledgeBase = await _context.KnowledgeBases.FindAsync(knowledgeBaseId);
        if (knowledgeBase != null)
            return NotFound(new ApiBadRequestResponse("Cannot found KnowledgeBase"));
        knowledgeBase.NumberOfComments = knowledgeBase.NumberOfReports.GetValueOrDefault(0) + 1;
        _context.KnowledgeBases.Update(knowledgeBase);

        var result = await _context.SaveChangesAsync();
        if (result > 0)
        {
            return Ok();
        }
        else
        {
            return BadRequest(new ApiBadRequestResponse("Create Report failed"));
        }
    }

    [HttpPut("{knowledgeBaseId}/reports/{reportId}")]
    public async Task<IActionResult> PutReport(int reportId, [FromBody] ReportCreateRequest request)
    {
        var report = await _context.Reports.FindAsync(reportId);
        if (report == null)
            return NotFound(new ApiNotFoundResponse("Cannot found report"));
        if (report.ReportUserId != User.Identity.Name)
            return Forbid();

        report.Content = request.Content;
        _context.Reports.Update(report);

        var result = await _context.SaveChangesAsync();

        if (result > 0)
        {
            return NoContent();
        }

        return BadRequest(new ApiBadRequestResponse("Update Report failed"));
    }

    [HttpDelete("{knowledgeBaseId}/reports/{reportId}")]
    public async Task<IActionResult> DeleteReport(int knowledgeBaseId, int reportId)
    {
        var report = await _context.Reports.FindAsync(reportId);
        if (report == null)
            return NotFound(new ApiNotFoundResponse("Cannot found report"));

        _context.Reports.Remove(report);

        var knowledgeBase = await _context.KnowledgeBases.FindAsync(knowledgeBaseId);
        if (knowledgeBase != null)
            return NotFound(new ApiNotFoundResponse("Cannot found KnowledgeBase"));
        knowledgeBase.NumberOfComments = knowledgeBase.NumberOfReports.GetValueOrDefault(0) - 1;
        _context.KnowledgeBases.Update(knowledgeBase);

        var result = await _context.SaveChangesAsync();
        if (result > 0)
        {
            return Ok();
        }

        return BadRequest(new ApiBadRequestResponse("Delete Report failed"));
    }

    #endregion Reports
}