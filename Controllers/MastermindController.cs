using MastermindVanHackathon.AppServices;
using MastermindVanHackathon.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MastermindVanHackathon.Controllers
{
    public class MastermindController : ApiController
    {
        private readonly IMastermindAppService _mastermindAppService;

        public MastermindController(IMastermindAppService mastermindAppService)
        {
            _mastermindAppService = mastermindAppService;
        }

        public async Task<HttpResponseMessage> NewGame([FromBody]Player player)
        {
            var newGame = _mastermindAppService.StartGame(player);

            var values = new
            {
                newGame.Colors,
                newGame.CodeLength,
                newGame.Gamekey,
                newGame.Player1.NumGuesses,
                newGame.Player1.PastResults,
                newGame.Solved
            };

            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, values);
            });
        }

        public async Task<HttpResponseMessage> Guess([FromBody] Guess guess)
        {
            HttpResponseMessage response = null;

            string resultMassage = "";
            if (_mastermindAppService.IsFinished(guess.GameKey, out resultMassage))
                return Request.CreateResponse(HttpStatusCode.OK, new { message = resultMassage });
            else
            {
                resultMassage = "You win!";
                var currentGame = _mastermindAppService.TryGuessCode(guess);

                if (currentGame.IsSolved())
                {
                    var ret = new
                    {
                        currentGame.CodeLength,
                        FurtherInstructions = "Solve the challenge to see this!",
                        currentGame.Colors,
                        currentGame.Gamekey,
                        currentGame.Player1.Guess,
                        currentGame.Player1.NumGuesses,
                        currentGame.Player1.PastResults,
                        Result = resultMassage,
                        currentGame.Solved,
                        TimeTaken = currentGame.TimeTaken(),
                        currentGame.Player1.User
                    };
                    response = Request.CreateResponse(HttpStatusCode.OK, ret);
                }
                else
                {
                    var ret = new
                    {
                        currentGame.CodeLength,
                        currentGame.Colors,
                        currentGame.Gamekey,
                        currentGame.Player1.Guess,
                        currentGame.Player1.NumGuesses,
                        currentGame.Player1.PastResults,
                        currentGame.Result,
                        currentGame.Solved
                    };
                    response = Request.CreateResponse(HttpStatusCode.OK, ret);
                }
            }

            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return response;
            });
        }
    }
}