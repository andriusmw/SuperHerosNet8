using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHerosNet.Controllers.Data;
using SuperHerosNet.Controllers.Entities;

namespace SuperHerosNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }
        //Get all superheroes
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAllHeroes()
        {
            var heroes = await _context.SuperHeroes.ToListAsync();
         
            return Ok(heroes);
        }

        //Get a single hero by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<List<SuperHero>>> GetHero(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if(hero == null)
            {
                return NotFound("Hero not found");
            }

            return Ok(hero);
        }

        //POST / create a superhero
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
             _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        //PUT / EDIT a superhero
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero updatedHero)
        {
            var dbhero = await _context.SuperHeroes.FindAsync(updatedHero.Id);
            if (dbhero == null)
            {
                return NotFound("Hero not found");
            } else
            {
                dbhero.Name = updatedHero.Name;
                dbhero.FirstName = updatedHero.FirstName;
                dbhero.LastName = updatedHero.LastName;
                dbhero.Place = updatedHero.Place;

                await _context.SaveChangesAsync();
            }

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        //DELETE / REMOVE a superhero
        [HttpDelete]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var dbhero = await _context.SuperHeroes.FindAsync(id);
            if (dbhero == null)
            {
                return NotFound("Hero not found");
            }
            else
            {
                _context.SuperHeroes.Remove(dbhero);
                await _context.SaveChangesAsync();
            }

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

    }


}
