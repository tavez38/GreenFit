using GreenFet.Data;
using GreenFit.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenFit.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecensioniController : ControllerBase
    {

        private readonly AppDbContext _context;

        //get per ogni recensione presente
        // L'app Android chiama: GET https://tuo-tunnel-cloudflare.com/api/utenti
        [HttpGet("gymReview/{palestra}")]
        public async Task<ActionResult<List<Recensione>>> getAllGymRecensioni(int idPalestra)
        {
            return await _context.Recensione.Where(u => u.palestra == idPalestra).ToListAsync();
        }
        //caricare su db la recensione
        // L'app Android invia un file JSON con i dati a: POST https://tuo-tunnel-cloudflare.com/api/utenti
        [HttpPost]
        public async Task<ActionResult> CreaRecenione(Recensione nuovaRecensione)
        {
            _context.Recensione.Add(nuovaRecensione); // Aggiunge in memoria
            await _context.SaveChangesAsync(); // Salva nel database
            return Ok();
        }
    }
}
