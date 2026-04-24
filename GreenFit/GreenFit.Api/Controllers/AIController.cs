using GreenFit.Api.services;
using Microsoft.AspNetCore.Mvc;

namespace GreenFit.Api.Controllers
{
    public class AIController : Controller
    {
        [HttpPost("/AIController/AiRequest")]
        public async Task<IActionResult>aiRequest([FromBody]string request){
            
            GeminiServices gemini = new GeminiServices();
            string response = await gemini.InterrogaGeminiAsync(request);
            gemini = null;
            if (response.Contains("Errore"))
                return BadRequest(response);

            return Ok(new { ris = response });
            
        }
    }
}
