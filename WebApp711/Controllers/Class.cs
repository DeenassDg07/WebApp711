using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp711.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator mediator = mediator;

        [Authorize]
        [HttpPost("submit")]
        public async Task<ActionResult> SubmitComment(FeedbackToAddDTO feedback)
        {
            var command = new SubmitFeedbackCommand { Feedback = feedback, SID = User.Claims.First().Value };
            await mediator.SendAsync(command);
            return Ok();
        }

        [HttpGet("category/{categoryId}/average-rating")]
        public async Task<ActionResult<double>> GetAverageRating(int categoryId)
        {
            var command = new GetAverageRatingCommand { CategoryId = categoryId };
            return Ok(await mediator.SendAsync(command));
        }

        [Authorize]
        [HttpGet("my")]
        public async Task<ActionResult<IEnumerable<FeedbackDTO>>> GetFeedbacksOfCurrentUser()
        {
            return Ok(await mediator.SendAsync(new GetFeedbacksOfCurrentUserCommand { SID = User.Claims.First().Value }));
        }


        [Authorize]
        [HttpGet("recent")]
        public async Task<ActionResult<IEnumerable<FeedbackDTO>>> GetRecentFeedbacks()
        {


            return Ok(await mediator.SendAsync(new GetFeedbacksOfCurrentUserCommand { SID = User.Claims.First().Value }));
        }
    }
}
