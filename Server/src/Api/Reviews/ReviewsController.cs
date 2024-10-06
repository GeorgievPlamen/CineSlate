using Api.Controllers.Common;
using Microsoft.AspNetCore.Mvc;

namespace Api.Reviews;

public class ReviewsController : BaseController
{
    [HttpPost("Add")]
    public IActionResult AddReview(string review)
    {
        return Ok(review);
    }
}