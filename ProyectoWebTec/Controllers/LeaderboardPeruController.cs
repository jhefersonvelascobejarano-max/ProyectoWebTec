using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoWebTec.Data;
using ProyectoWebTec.Models;

namespace ProyectoWebTec.Controllers
{
    [ApiController]
    [Route("api/leaderboard/peru")]
    public class LeaderboardPeruController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LeaderboardPeruController(AppDbContext context)
        {
            _context = context;
        }

        // 1. GET lista: /api/leaderboard/peru
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaderboardPeru>>> GetAll()
        {
            var scores = await _context.LeaderboardPeru
                .OrderByDescending(x => x.Score)
                .ToListAsync();
            return Ok(scores);
        }

        // 2. GET por ID: /api/leaderboard/peru/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaderboardPeru>> GetById(int id)
        {
            var entry = await _context.LeaderboardPeru.FindAsync(id);
            if (entry == null)
                return NotFound(new { message = $"Registro con ID {id} no encontrado." });

            return Ok(entry);
        }

        // 3. POST: /api/leaderboard/peru
        [HttpPost]
        public async Task<ActionResult<LeaderboardPeru>> Create([FromBody] LeaderboardPeru newEntry)
        {
            newEntry.CreatedAt = DateTime.UtcNow;
            _context.LeaderboardPeru.Add(newEntry);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = newEntry.Id }, newEntry);
        }

        // 4. PUT: /api/leaderboard/peru/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LeaderboardPeru updatedEntry)
        {
            var existing = await _context.LeaderboardPeru.FindAsync(id);
            if (existing == null)
                return NotFound(new { message = $"Registro con ID {id} no encontrado." });

            existing.PlayerName = updatedEntry.PlayerName;
            existing.Score = updatedEntry.Score;

            await _context.SaveChangesAsync();
            return Ok(existing);
        }

        // 5. DELETE: /api/leaderboard/peru/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _context.LeaderboardPeru.FindAsync(id);
            if (existing == null)
                return NotFound(new { message = $"Registro con ID {id} no encontrado." });

            _context.LeaderboardPeru.Remove(existing);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Registro con ID {id} eliminado correctamente." });
        }
    }
}
