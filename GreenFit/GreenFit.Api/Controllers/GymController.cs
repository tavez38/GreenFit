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
        [HttpGet("geografia")]
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
        [HttpGet("cerca/{nome}")]
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
            _context.Gym.Add(nuovaPalestra); // Aggiunge in memoria
            await _context.SaveChangesAsync(); // Salva nel database
            return Ok();
        }
    }
}
