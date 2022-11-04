using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicTacDough.Shared.Domain;

namespace TicTacDough.Server.Controllers {
    
    // specify route path
    [Route("api/[controller]")]

    // indicate that this controller is for HTTP API responses
    [ApiController]

    public class ScoresController : ControllerBase {

        // members
        //private readonly IRepository.IUnitOfWork unitOfWork;

        // constructor
        /*public ScoresController(IRepository.IUnitOfWork unitOfWork) {
            this.unitOfWork = unitOfWork;
        }*/

        // GET: /api/Scores/
        /*[HttpGet]
        public async Task<IActionResult> GetScores() {
            var scores = await this.unitOfWork.Scores.GetAll(
                //includes: q => q.Include(x => x.username).Include(x => x.score)
                );
            return Ok(scores);
        }*/

        [HttpGet]
        public List<TicTacDough.Shared.Domain.Score> Index() {
            List<Score> scores = new List<Score>();
            scores.Add(new Score(10, "Amy"));
            scores.Add(new Score(3, "Shawn"));

            return scores;
        }
    }
}