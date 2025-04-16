using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrinchPhotosAPI.Data;
using TrinchPhotosAPI.Data.Models;

namespace TrinchPhotosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public PortfolioController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Portfolio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Portfolio>>> GetPortfolio()
        {
            return await _context.Portfolio.ToListAsync();
        }

        // GET: api/Portfolio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Portfolio>> GetPortfolio(int id)
        {
            var portfolio = await _context.Portfolio.FindAsync(id);

            if (portfolio == null)
            {
                return NotFound();
            }

            return portfolio;
        }
        private bool PortfolioExists(int id)
        {
            return _context.Portfolio.Any(e => e.Id == id);
        }
    }
}
