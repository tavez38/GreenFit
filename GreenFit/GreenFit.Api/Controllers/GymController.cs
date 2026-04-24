using GreenFet.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GreenFit.Shared.Models;

namespace GreenFit.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GymController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GymController(AppDbContext context) { _context = context; }
        //get per dati geografici palestra
        // L'app Android chiama: GET https://tuo-tunnel-cloudflare.com/api/utenti
        [HttpGet("/GymController/getCoordinate")]
        public async Task<IActionResult> GetRapido()
        {
            var dati = await _context.Gym
                .Select(g => new
                {
                    g.Id,
                    g.latitude,
                    g.longitude,
                    g.name
                })
                .ToListAsync();

            return Ok(dati);
        }
        // get per palestra con nome
        // L'app Android chiama: GET https://tuo-tunnel-cloudflare.com/api/utenti/cerca/Mario
        [HttpGet("/GymController/cerca/{nome}")]
        public async Task<ActionResult<List<Gym>>> GetPerNome(string nome)
        {
            // Cerca nel database dove il Nome è uguale a quello richiesto
            return await _context.Gym.Where(u => u.name == nome).ToListAsync();
        }
        //caricare su db la palestra
        // L'app Android invia un file JSON con i dati a: POST https://tuo-tunnel-cloudflare.com/api/utenti
        [HttpPost]
        public async Task<ActionResult> CreaPalestra(Gym nuovaPalestra)
        {
            // Controlliamo se esiste già una palestra in quelle coordinate esatte
            var esisteGia = await _context.Gym.AnyAsync(g =>
                g.latitude == nuovaPalestra.latitude &&
                g.longitude == nuovaPalestra.longitude);

            if (esisteGia)
            {
                return BadRequest("Esiste già una palestra registrata in queste coordinate geografiche.");
            }

            _context.Gym.Add(nuovaPalestra);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
