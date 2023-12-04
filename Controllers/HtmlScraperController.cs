using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static htmlscraperapi.IHtmlScraperRepository;

namespace htmlscraperapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HtmlScraperController : ControllerBase
    {
        private readonly IHtmlScraperRepository _htmlScraperRepository;

        public HtmlScraperController(IHtmlScraperRepository htmlScraperRepository)
        {
            _htmlScraperRepository = htmlScraperRepository;
        }
        [HttpPost]
        public async Task<IActionResult> ScrapeHtmlFile(IFormFile htmlFile)
        {
            if (htmlFile == null || htmlFile.Length == 0)
            {
                return BadRequest("Please provide a valid HTML file.");
            }

            try
            {
                var result = await _htmlScraperRepository.ScrapeHtmlFile(htmlFile);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
