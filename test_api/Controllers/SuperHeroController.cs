using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_api.Data;
using test_api.Entites;

namespace test_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly ProductDbContext _context;
        public SuperHeroController(ProductDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Hero>>> GetAllHeroes()
        {
            var heroes = await _context.Heroes.ToListAsync();
            return Ok(heroes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Hero>> GetHero(int id)
        {
            var hero = await _context.Heroes.FindAsync(id);
            if (hero is null)
                return NotFound("Hero not found!");

            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<Hero>> AddHero(Hero hero)
        {
           _context.Heroes.Add(hero);
           await _context.SaveChangesAsync();

            return Ok(await _context.Heroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<Hero>> UpdateHero(Hero updatedHero)
        {
            var dbHero = await _context.Heroes.FindAsync(updatedHero.Id);
            if (dbHero is null)
                return NotFound("Hero not found!");

            dbHero.Name = updatedHero.Name;
            dbHero.FirstName = updatedHero.FirstName;
            dbHero.LastName = updatedHero.LastName;
            dbHero.Place = updatedHero.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.Heroes.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<Hero>> DeleteHero(int id)
        {
            var dbHero = await _context.Heroes.FindAsync(id);
            if (dbHero is null)
                return NotFound("Hero not found!");

            _context.Heroes.Remove(dbHero);
            await _context.SaveChangesAsync();

            return Ok(await _context.Heroes.ToListAsync());
        }
    }
}
