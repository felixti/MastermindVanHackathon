using MastermindVanHackathon.AppServices;
using MastermindVanHackathon.Models;
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
    public class MastermindMultiplayerController : ApiController
    {
        private readonly IMastermindMultiplayerAppService _mastermindMultiplayerAppService;
        public MastermindMultiplayerController(IMastermindMultiplayerAppService mastermindMultiplayerAppService)
        {
            _mastermindMultiplayerAppService = mastermindMultiplayerAppService;
        }

        public async Task<HttpResponseMessage> Join([FromBody] JoinViewModel join)
        {
            Player newPlayer = new Player(join.Name);
            newPlayer.SetRole(join.Role);
            var result = _mastermindMultiplayerAppService.Join(newPlayer, join.RoomId);

            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { RoomdId = result.RoomId, Message = result.Message });
            });
        }


        public async Task<HttpResponseMessage> SetSecretCode([FromBody] SecretCodeViewModel secretCode)
        {

            dynamic result = _mastermindMultiplayerAppService.SetSecretCode(secretCode.UserName, secretCode.RoomId, secretCode.Code);

            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { Message = result.Message });
            });
        }
    }
}
