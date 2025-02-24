using Microsoft.AspNetCore.Mvc;

namespace FUForum.BackendServer.Authorization;

public class ClaimRequirementAttribute : TypeFilterAttribute
{
    public ClaimRequirementAttribute(FunctionCode functionId, CommandCode commandId)
        : base(typeof(ClaimRequirementFilter))
    {
        Arguments = new object[] { functionId, commandId };
    }
}