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
        //public HttpResponseMessage NewGame([FromBody] string user)
        //{
        //    //To Do
        //}

        public HttpResponseMessage Get()
        {

            return Request.CreateResponse(HttpStatusCode.OK, "teste");
        }
    }
}
