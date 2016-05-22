using MastermindVanHackathon.Configuration;
using MastermindVanHackathon.Data;
using MastermindVanHackathon.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MastermindVanHackathon.Controllers
{
    public class MastermindController : ApiController
    {
        private readonly Game _game;
        private readonly IMastermindRepository _mastermindRepository;
        public MastermindController(IMastermindRepository mastermindRepository, Game game)
        {
            _game = game;
            _mastermindRepository = mastermindRepository;
            SetupDatabase();
        }

        private void SetupDatabase()
        {
            if (!_mastermindRepository.HasCollection())
            {
                _mastermindRepository.CreateCollection();
            }
        }
        //public HttpResponseMessage NewGame([FromBody] string user)
        //{
        //    //To Do
        //}
        public async Task<HttpResponseMessage> NewGame([FromBody]Player player)
        {
            _game.SetupNewGame();
            _game.AddPlayer(player);
            _game.GenerateCode();

            _mastermindRepository.Insert(_game);

            var values = new { _game.Colors, _game.CodeLength, _game.Gamekey, _game.NumGuesses, _game.PastResults, _game.Solved };

            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {

                return Request.CreateResponse(HttpStatusCode.OK, values);
            });
        }

        public async Task<HttpResponseMessage> Guess([FromBody] Guess guess)
        {
            HttpResponseMessage response = null;

            var currentGame = _mastermindRepository.GetGamebyGamekey(guess.GameKey);

            currentGame.SetTry();
            currentGame.SetGuess(guess.Code);
            currentGame.MatchCode();
            currentGame.SetResult();

            _mastermindRepository.Replace(currentGame);



            if (currentGame.IsSolved())
            {
                var ret = new
                {
                    currentGame.CodeLength,
                    FurtherInstructions = "Solve the challenge to see this!",
                    currentGame.Colors,
                    currentGame.Gamekey,
                    currentGame.Guess,
                    currentGame.NumGuesses,
                    currentGame.PastResults,
                    Result = "You win!",
                    currentGame.Solved,
                    TimeTaken = currentGame.UpdatedAt.Subtract(currentGame.CreatedAt).Seconds,
                    currentGame.Players.First().User
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
                    currentGame.Guess,
                    currentGame.NumGuesses,
                    currentGame.PastResults,
                    currentGame.Result,
                    currentGame.Solved
                };

                response = Request.CreateResponse(HttpStatusCode.OK, ret);
            }



            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {

                return response;
            });
        }
    }
}
