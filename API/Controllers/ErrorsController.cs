
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]

    [Microsoft.AspNetCore.Mvc.Route("errors/{code}")]
    public class ErrorsController : BaseApiController
    {
        public IActionResult GetError(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}