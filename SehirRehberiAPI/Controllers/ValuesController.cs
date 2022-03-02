using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SehirRehberiAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SehirRehberiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private DataContext _context;

        public ValuesController(DataContext context)
        {
            _context = context;
        }


        [HttpGet("getlist")]
        public async Task< ActionResult> GetList()
        {
            var result = await _context.Values.ToListAsync();
            return Ok(result);
        }


        [HttpGet("get")]
        public async Task<ActionResult> Get(int Id)
        {
            var result = await _context.Values.FirstOrDefaultAsync(v => v.Id == Id);
            if (result!=null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Böyle bir data yoktur");
            }
        }
    }
}
