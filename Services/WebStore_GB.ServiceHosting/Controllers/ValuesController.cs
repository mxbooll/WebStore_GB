using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace WebStore_GB.ServiceHosting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static readonly List<string> _values = Enumerable
            .Range(1, 10)
            .Select(i => $"Value {i}")
            .ToList();

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get() => _values;

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            if (id >= _values.Count)
            {
                return NotFound();
            }

            return _values[id];
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, string value)
        {
            if (id < 0 || id >= _values.Count)
            {
                return BadRequest();
            }

            _values[id] = value;

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            if (id >= _values.Count)
            {
                return NotFound();
            }

            _values.RemoveAt(id); 

            return Ok();
        }
    }
}