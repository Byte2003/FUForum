using FUForum.BackendServer.Data;
using FUForum.BackendServer.Helpers;
using FUForum.ViewModels.Contents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FUForum.BackendServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelsController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public LabelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(string id)
        {
            var label = await _context.Labels.FindAsync(id);
            if (label == null)
                return NotFound(new ApiNotFoundResponse($"Label with id: {id} is not found"));

            var labelVm = new LabelVM()
            {
                Id = label.Id,
                Name = label.Name
            };

            return Ok(labelVm);
        }

        [HttpGet("popular/{take:int}")]
        [AllowAnonymous]
        public async Task<List<LabelVM>> GetPopularLabels(int take)
        {
            var query = from l in _context.Labels
                        join lik in _context.LabelInKnowledgeBases on l.Id equals lik.LabelId
                        group new { l.Id, l.Name } by new { l.Id, l.Name } into g
                        select new
                        {
                            g.Key.Id,
                            g.Key.Name,
                            Count = g.Count()
                        };
            var labels = query.OrderByDescending(x => x.Count).Take(take)
                .Select(l => new LabelVM()
                {
                    Id = l.Id,
                    Name = l.Name
                });

            return labels.ToList();
        }
    }
}

