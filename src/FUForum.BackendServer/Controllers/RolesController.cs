using FUForum.BackendServer.Authorization;
using FUForum.BackendServer.Data;
using FUForum.BackendServer.Data.Entities;
using FUForum.BackendServer.Helpers;
using FUForum.ViewModels;
using FUForum.ViewModels.Systems;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FUForum.BackendServer.Controllers
{
    public class RolesController : BaseController
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public RolesController(RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }

        // URL: GET: /api/roles
        [HttpGet]
        [ClaimRequirement(FunctionCode.SYSTEM_ROLE, CommandCode.VIEW)]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var roleVMs = roles.Select(r => new RoleVM()
            {
                Id = r.Id,
                Name = r.Name
            });
            return Ok(roleVMs);
        }

        // URL: GET: /api/roles?filter={keyword}&pageIndex=1&pageSize=10
        [HttpGet]
        [Route("filter")]
        [ClaimRequirement(FunctionCode.SYSTEM_ROLE, CommandCode.VIEW)]
        public async Task<IActionResult> GetRolesPaging(string filter, int pageIndex, int pageSize)
        {
            var query = _roleManager.Roles;
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(u => u.Id.Contains(filter) || u.Name.Contains(filter));
            }

            var totalRecords = await query.CountAsync();
            var items = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new RoleVM()
                {
                    Id = u.Id,
                    Name = u.Name,
                })
                .ToListAsync();
            var pagination = new Pagination<RoleVM>
            {
                Items = items,
                TotalRecords = totalRecords,
            };
            return Ok(pagination);
        }

        // URL: GET: /api/roles/{id}
        [HttpGet("{id}")]
        [ClaimRequirement(FunctionCode.SYSTEM_ROLE, CommandCode.VIEW)]
        public async Task<IActionResult> GetById(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound($"Cannot find role with id: {id}");
            var roleVM = new RoleVM()
            {
                Id = role.Id,
                Name = role.Name
            };
            return Ok(roleVM);
        }

        // URL: POST: /api/roles
        [HttpPost]
        [ClaimRequirement(FunctionCode.SYSTEM_ROLE, CommandCode.CREATE)]
        [ApiValidationFilter]
        public async Task<IActionResult> PostRole(RoleCreateRequest request)
        {
            var role = new IdentityRole()
            {
                Id = request.Id,
                Name = request.Name,
                NormalizedName = request.Name.ToUpper()
            };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(GetById), new { id = role.Id }, request);
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse(result));
            }
        }

        // URL: PUT: /api/roles
        [HttpPut("{id}")]
        [ClaimRequirement(FunctionCode.SYSTEM_ROLE, CommandCode.UPDATE)]
        [ApiValidationFilter]
        public async Task<IActionResult> PutRole(string id, [FromBody] RoleCreateRequest request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound(new ApiNotFoundResponse("Cannot find a role with the provided id"));
            }

            role.Name = request.Name;
            role.NormalizedName = request.Name.ToUpper();
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return NoContent();
            }

            return BadRequest(new ApiBadRequestResponse(result));
        }

        // URL: DELETE: /api/roles/{id}
        [HttpDelete("{id}")]
        [ClaimRequirement(FunctionCode.SYSTEM_ROLE, CommandCode.DELETE)]
        [ApiValidationFilter]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound(new ApiNotFoundResponse("Cannot find a role with the provided id"));
            }

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                var roleVM = new RoleVM()
                {
                    Id = role.Id,
                    Name = role.Name
                };
                return Ok(roleVM);
            }

            return BadRequest(new ApiBadRequestResponse(result));
        }

        [HttpGet("{roleId}/permissions")]
        [ClaimRequirement(FunctionCode.SYSTEM_PERMISSION, CommandCode.VIEW)]
        public async Task<IActionResult> GetPermissionsByRoleId(string roleId)
        {
            var permissions = from p in _context.Permissions
                join c in _context.Commands on p.CommandId equals c.Id
                where p.RoleId == roleId
                select new PermissionVM()
                {
                    FunctionId = p.FunctionId,
                    RoleId = p.RoleId,
                    CommandId = p.CommandId
                };
            return Ok(await permissions.ToListAsync());
        }

        [HttpPut("{roleId}/permissions")]
        [ClaimRequirement(FunctionCode.SYSTEM_ROLE, CommandCode.UPDATE)]
        [ApiValidationFilter]
        public async Task<IActionResult> PutPermissionByRoleId(string roleId,
            [FromBody] UpdatePermissionRequest request)
        {
            var newPermissions = new List<Permission>();
            foreach (var permision in request.Permissions)
            {
                newPermissions.Add(new Permission()
                {
                    FunctionId = permision.FunctionId,
                    RoleId = roleId,
                    CommandId = permision.CommandId
                });
            }
            var existingPermissions = await _context.Permissions.Where(p => p.RoleId == roleId).ToListAsync();
            _context.Permissions.RemoveRange(existingPermissions);
            _context.Permissions.AddRange(newPermissions);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return NoContent();
            }
            return BadRequest(new ApiBadRequestResponse("Update permission failed"));
        }
    }
}