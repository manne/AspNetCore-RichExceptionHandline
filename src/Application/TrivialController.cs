using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Application
{
    [Route("api/[controller]")]
    public class TrivialController
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "nothing special" };
        }
    }
}
