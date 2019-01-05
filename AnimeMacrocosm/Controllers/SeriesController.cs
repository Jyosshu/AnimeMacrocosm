using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AnimeMacrocosm.Interface;

namespace AnimeMacrocosm.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly ISeriesRepository _seriesRepository;

        public SeriesController(ISeriesRepository seriesRepository)
        {
            _seriesRepository = seriesRepository;
        }

        // GET: api/Series
        [HttpGet]
        public IActionResult GetAllSeries() => Ok(_seriesRepository.GetAllSeries());

        // GET: api/Series/5
        [HttpGet]
        [Route("/[controller]/[action]/{id}")]
        public IActionResult GetSeriesById(int id) => Ok(_seriesRepository.GetSeriesById(id));

        // POST: api/Series
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT: api/Series/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
