
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _context;
        public BuggyController(StoreContext context)
        {
            this._context = context;

        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {

            var thing = _context.Products.Find(-1);
            if (thing == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok();
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var thing = _context.Products.Find(-1);
            var result = thing.ToString();
            return Ok();
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(404));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetValidationBadRequest(int id)
        {
            return Ok();
        }

    }



}