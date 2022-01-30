using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ex1c.Data;
using ex1c.Models;

namespace ex1c.Controllers
{
    [Produces("application/json")]
    [Route("api/SubregionsApi")]
    [ApiController]
    public class SubregionsApiController : Controller
    {
        private readonly WideWorldContext _context;

        public SubregionsApiController(WideWorldContext context)
        {
            _context = context;
        }

        //// GET: api/CountriesApi
        //[HttpGet]
        //public IEnumerable<string> GetRegions()
        //{
        //    List<string> stringList = new List<string>();
        //    stringList = _context.Countries.Select(c => c.Region).Distinct().OrderBy(r => 1).ToList();
        //    return stringList;
        //}
        // GET: api/SubregionsApi
        [HttpGet]
        public IEnumerable<Subregion> GetSubregions()
        {
            List<string> stringList = new List<string>();
            stringList = _context.Countries.Select(c => c.Subregion).Distinct().OrderBy(r => 1).ToList();
            List<Subregion> subregionList = new List<Subregion>();
            foreach (string s in stringList)
            {
                Subregion r = new Subregion();
                r.SubregionName = s;
                subregionList.Add(r);
            }
            return subregionList;
        }
        // GET: api/SubregionsApi/Americas
        [HttpGet("{region}")]
        public async Task<IActionResult> GetSubregions([FromRoute] string region)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            List<string> stringList = new List<string>();
            stringList = await _context.Countries.Where(r => r.Region == region).Select(c => c.Subregion).Distinct().OrderBy(r => 1).ToListAsync();
            List<Subregion> subregionList = new List<Subregion>();
            foreach (string s in stringList)
            {
                Subregion r = new Subregion();
                r.SubregionName = s;
                subregionList.Add(r);
            }

            if (subregionList == null)
            {
                return NotFound();
            }

            return Ok(subregionList);
        }



        //private bool CountriesExists(int id)
        //{
        //    return _context.Countries.Any(e => e.CountryId == id);
        //}
    }
}
