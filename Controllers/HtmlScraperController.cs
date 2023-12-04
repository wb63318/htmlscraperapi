﻿using Microsoft.AspNetCore.Http;
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
        [HttpGet]
        public ActionResult<IEnumerable<ScrapedContent>> GetAllResults()
        {
            try
            {
                var results = _htmlScraperRepository.GetAllScrapedContent();

                if (results == null || !results.Any())
                {
                    return NoContent();
                }

                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
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
        [HttpDelete("{id}")]
        public ActionResult DeleteReport(int id)
        {
            try
            {
                var deletedReport = _htmlScraperRepository.DeleteScrapedContent(id);

                if (deletedReport == null)
                {
                    return NotFound($"Report with ID {id} not found");
                }

                return Ok($"Report with ID {id} deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}
