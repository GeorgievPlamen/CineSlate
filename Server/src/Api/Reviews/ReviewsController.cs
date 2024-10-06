using Api.Controllers.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Reviews;

[Authorize]
public class ReviewsController : BaseController
{
    [HttpPost("Add")]
    public IActionResult AddReview(string review)
    {
        return Ok(review);
    }
}