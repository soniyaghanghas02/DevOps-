using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TokenAuthentication.Controllers
{
    [Authorize]
    public class AccountController : ApiController
    {
        [HttpGet]
        [Route("api/account/getuser/{id}")]
        public IHttpActionResult GetUFirstUser(int id)
        {
            // Get user from dummy list
            UserService us = new UserService();
            var user = us.GetUserList().FirstOrDefault(x => x.Id == id);
            return Ok(user);
        }
    }
}
