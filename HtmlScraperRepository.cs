﻿using HtmlAgilityPack;
using htmlscraperapi.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.WebSockets;

namespace htmlscraperapi
{
    public interface IHtmlScraperRepository
    {
        ScrapedContent DeleteScrapedContent(int id);
        IEnumerable<ScrapedContent> GetAllScrapedContent();
       //Task<ScrapedContent> GetById(int id);
        ScrapedContent GetScrapedContentById(int id);
        Task<object> ScrapeHtmlFile(IFormFile htmlFile);
        
    }
    public class HtmlScraperRepository : IHtmlScraperRepository
    {
        private readonly AppDbContext _dbContext;

        public HtmlScraperRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ScrapedContent GetScrapedContentById(int id)
        {
            return _dbContext.ScrapedContents.Find(id);
        }
        //public  Task<ScrapedContent> GetById(int id)
        //{
        //    return _dbContext.ScrapedContents.FirstOrDefaultAsync(x=> x.Id == id);
        //}
        public IEnumerable<ScrapedContent> GetAllScrapedContent()
        {
            return _dbContext.ScrapedContents.ToList();
        }
        public ScrapedContent DeleteScrapedContent(int id)
        {
            var reportToDelete = _dbContext.ScrapedContents.Find(id);

            if (reportToDelete == null)
            {
                return null; // Indicate that the report with the given ID was not found
            }

            _dbContext.ScrapedContents.Remove(reportToDelete);
            _dbContext.SaveChanges();

            return reportToDelete;
        }

        public async Task<object> ScrapeHtmlFile(IFormFile htmlFile)
        {
            try
            {
                // Read HTML content from the uploaded file
                using (var streamReader = new StreamReader(htmlFile.OpenReadStream()))
                {
                    var html = await streamReader.ReadToEndAsync();

                    // Load HTML content into HtmlDocument
                    var htmlDocument = new HtmlDocument();
                    htmlDocument.LoadHtml(html);

                    // Extract specific information from HTML (e.g., title and table data)
                    var title = htmlDocument.DocumentNode.SelectSingleNode("//title")?.InnerText;
                    var tableData = ExtractTableData(htmlDocument);

                    // Save the scraped content to the database
                    var scrapedContent = new ScrapedContent
                    {
                        Title = title,
                        TableData = tableData
                    };

                    _dbContext.ScrapedContents.Add(scrapedContent);
                    await _dbContext.SaveChangesAsync();

                    // Create a simple response object
                    var result = new
                    {
                        Title = title,
                        TableData = tableData
                    };
                    // Convert the result object to JSON format
                    var jsonResult = JsonConvert.SerializeObject(result);

                    // Save the JSON result to a file (optional)
                    // File.WriteAllText("result.json", jsonResult);

                    return jsonResult;

                   // return result;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred: {ex.Message}");
            }
        }


        private List<string> ExtractTableData(HtmlDocument htmlDocument)
        {
            var tableData = new List<string>();

            // Select the div with class 'tabs' and its descendant div with class 'tab'
            var tabDiv = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='tabs']//div[@class='tab']");

            if (tabDiv != null)
            {
                // Select all <tr> elements within the 'tab' div
                var trNodes = tabDiv.SelectNodes(".//tbody/tr");

                if (trNodes != null)
                {
                    foreach (var trNode in trNodes)
                    {
                        // Select all <td> elements within the current <tr>
                        var tdNodes = trNode.SelectNodes("td");

                        if (tdNodes != null)
                        {
                            tableData.AddRange(tdNodes.Select(td => td.InnerText.Trim()));
                        }
                    }
                }
            }

            return tableData;
        }

    }
}

