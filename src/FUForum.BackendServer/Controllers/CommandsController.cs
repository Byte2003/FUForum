using FUForum.BackendServer.Authorization;
using FUForum.BackendServer.Data;
using FUForum.BackendServer.Data.Entities;
using FUForum.ViewModels;
using FUForum.ViewModels.Contents;
using FUForum.ViewModels.Systems;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FUForum.BackendServer.Controllers;

public class CommandsController : BaseController
{
    private readonly ApplicationDbContext _context;

    public CommandsController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCommands()
    {
        var commands = _context.Commands;
        var commandVMs = await commands.Select(c => new CommandVM()
        {
            Id = c.Id,
            Name = c.Name,
        }).ToListAsync();
        return Ok(commandVMs);
    }
}