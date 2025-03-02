using FUForum.BackendServer.Authorization;
using FUForum.BackendServer.Data;
using FUForum.BackendServer.Data.Entities;
using FUForum.BackendServer.Helpers;
using FUForum.ViewModels;
using FUForum.ViewModels.Systems;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FUForum.BackendServer.Controllers
{
    public class UsersController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<User> userManager, ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
        }

        // URL: GET: https://localhost:7017/api/users
        [HttpGet]
        [ClaimRequirement(FunctionCode.SYSTEM_USER, CommandCode.VIEW)]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var roleVMs = users.Select(r => new UserVM()
            {
                Id = r.Id,
                UserName = r.UserName,
                Email = r.Email,
                PhoneNumber = r.PhoneNumber,
                FirstName = r.FirstName,
                LastName = r.LastName,
                Dob = r.Dob
            });
            return Ok(roleVMs);
        }

        // URL: GET: https://localhost:7017/api/users?filter={keyword}&pageIndex=1&pageSize=10
        [HttpGet]
        [Route("filter")]
        [ClaimRequirement(FunctionCode.SYSTEM_USER, CommandCode.VIEW)]
        public async Task<IActionResult> GetUsersPaging(string filter, int pageIndex, int pageSize)
        {
            var query = _userManager.Users;
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(u => u.Id.Contains(filter)
                                         || u.UserName.Contains(filter)
                                         || u.Email.Contains(filter)
                                         || u.LastName.Contains(filter)
                                         || u.FirstName.Contains(filter));
            }

            var totalRecords = await query.CountAsync();
            var items = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new UserVM()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Dob = u.Dob
                })
                .ToListAsync();
            var pagination = new Pagination<UserVM>
            {
                Items = items,
                TotalRecords = totalRecords,
            };
            return Ok(pagination);
        }

        // URL: GET: https://localhost:7017/api/users/{id}
        [HttpGet("{id}")]
        [ClaimRequirement(FunctionCode.SYSTEM_USER, CommandCode.VIEW)]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound($"Cannot find user with id: {id}");
            var userVM = new UserVM()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Dob = user.Dob
            };
            return Ok(userVM);
        }

        // URL: POST: https://localhost:7017/api/users
        [HttpPost]
        [ClaimRequirement(FunctionCode.SYSTEM_USER, CommandCode.CREATE)]
        [ApiValidationFilter]
        public async Task<IActionResult> PostUser(UserCreateRequest request)
        {
            var user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Dob = request.Dob
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(GetById), new { id = user.Id }, request);
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse(result));
            }
        }

        // URL: PUT: https://localhost:7017/api/users
        [HttpPut("{id}")]
        [ClaimRequirement(FunctionCode.SYSTEM_USER, CommandCode.UPDATE)]
        [ApiValidationFilter]
        public async Task<IActionResult> PutUser(string id, [FromBody] UserCreateRequest request)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(new ApiNotFoundResponse("Cannot find user"));
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;
            user.Dob = request.Dob;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return NoContent();
            }

            return BadRequest(result.Errors);
        }

        [HttpPut("{id}/update-password")]
        [ClaimRequirement(FunctionCode.SYSTEM_USER, CommandCode.UPDATE)]
        [ApiValidationFilter]
        public async Task<IActionResult> PutUserPassword(string id, [FromBody] UserPasswordChangRequest request)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(new ApiNotFoundResponse("Cannot find user"));
            }

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (result.Succeeded)
            {
                return NoContent();
            }

            return BadRequest(new ApiBadRequestResponse(result));
        }

        // URL: DELETE: https://localhost:7017/api/users/{id}
        [HttpDelete("{id}")]
        [ClaimRequirement(FunctionCode.SYSTEM_USER, CommandCode.DELETE)]
        [ApiValidationFilter]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(new ApiNotFoundResponse("Cannot find user"));
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                var userVM = new UserVM()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Dob = user.Dob
                };
                return Ok(userVM);
            }

            return BadRequest(new ApiBadRequestResponse(result));
        }

        [HttpGet("{userId}/menu")]
        public async Task<IActionResult> GetMenuByUserPermission(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound(new ApiNotFoundResponse($"Cannot find user with id: {userId}"));
            var roles = await _userManager.GetRolesAsync(user);
            var query = from f in _context.Functions
                join p in _context.Permissions on f.Id equals p.FunctionId
                join r in _roleManager.Roles on p.RoleId equals r.Id
                join c in _context.Commands on p.CommandId equals c.Id
                where roles.Contains(r.Name) && c.Id == "VIEW"
                select new FunctionVM()
                {
                    Id = f.Id,
                    Name = f.Name,
                    Url = f.Url,
                    ParentId = f.ParentId,
                    SortOrder = f.SortOrder
                };
            var result = await query.Distinct()
                .OrderBy(u => u.ParentId)
                .ThenBy(u => u.SortOrder)
                .ToListAsync();
            return Ok(result);
        }
    }
}