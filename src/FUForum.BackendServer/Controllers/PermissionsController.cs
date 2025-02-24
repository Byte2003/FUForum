using System.Data;
using Dapper;
using FUForum.BackendServer.Authorization;
using FUForum.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace FUForum.BackendServer.Controllers
{
    public class PermissionsController : BaseController
    {
        private readonly IConfiguration _configuration;

        public PermissionsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Show list function with corressponding action included in each functions
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ClaimRequirement(FunctionCode.SYSTEM_PERMISSION, CommandCode.VIEW)]
        public async Task<IActionResult> GetCommandsView()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString(("DefaultConnection"))))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    await connection.OpenAsync();
                }

                var sql = @"SELECT f.Id,
	                       f.Name,
	                       f.ParentId,
	                       sum(case when c.Id = 'CREATE' then 1 else 0 end) as HasCreate,
	                       sum(case when c.Id = 'UPDATE' then 1 else 0 end) as HasUpdate,
	                       sum(case when c.Id = 'DELETE' then 1 else 0 end) as HasDelete,
	                       sum(case when c.Id = 'VIEW' then 1 else 0 end) as HasView,
	                       sum(case when c.Id = 'APPROVE' then 1 else 0 end) as HasApprove
                        from Functions f join CommandInFunctions cif on f.Id = cif.FunctionId
		                    left join Commands c on cif.CommandId = c.Id
                        GROUP BY f.Id,f.Name, f.ParentId
                        order BY f.ParentId";

                var result = await connection.QueryAsync<PermissionScreenVM>(sql, null, null, 120, CommandType.Text);
                return Ok(result.ToList());
            }
        }
    }
}