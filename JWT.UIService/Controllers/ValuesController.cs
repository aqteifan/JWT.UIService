using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace JWT.UIService.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public string Get()
        {
            HttpClient client = new HttpClient();
            var byteArray = Encoding.ASCII.GetBytes("Admin:pass");
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var responseMessage = client.PostAsync("http://localhost:11790/api/auth/token", new StringContent(""));

            var token = responseMessage.Result.Content.ReadAsStringAsync().Result;
            JwtSecurityToken jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var validToken = jwtToken.ValidTo > DateTime.UtcNow;

            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var result = client.GetAsync("http://localhost:10493/api/values");
            var stringResult = result.Result.Content.ReadAsStringAsync().Result;
            return stringResult;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
