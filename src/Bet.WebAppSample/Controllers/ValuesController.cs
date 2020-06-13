using System.Collections.Generic;
using System.Web.Http;

using Bet.WebAppSample.Services;

namespace Bet.WebAppSample.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly FeedService _feedService;

        public ValuesController(FeedService feedService)
        {
            _feedService = feedService;
        }

        // GET: api/Values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2", _feedService.GetValue() };
        }

        // GET: api/Values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Values
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Values/5
        public void Delete(int id)
        {
        }
    }
}
