using HtmlAgilityPack;
using htmlscraperapi.Data;
using System.Net.WebSockets;

namespace htmlscraperapi
{
    public interface IHtmlScraperRepository
    {
        
            Task<object> ScrapeHtmlFile(IFormFile htmlFile);
        
    }
    public class HtmlScraperRepository : IHtmlScraperRepository
    {
        private readonly AppDbContext _dbContext;

        public HtmlScraperRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
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

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred: {ex.Message}");
            }
        }

        //private List<string> ExtractTableData(HtmlDocument htmlDocument)
        //{
        //    // Example: Extracting data from the first table
        //    var tableData = new List<string>();

        //    var firstTable = htmlDocument.DocumentNode.SelectSingleNode("//table");
        //    if (firstTable != null)
        //    {
        //        var rows = firstTable.SelectNodes("tr");
        //        if (rows != null)
        //        {
        //            foreach (var row in rows)
        //            {
        //                var cells = row.SelectNodes("td");
        //                if (cells != null)
        //                {
        //                    foreach (var cell in cells)
        //                    {
        //                        tableData.Add(cell.InnerText);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return tableData;
        //}

        //private List<List<string>> ExtractTableData(HtmlDocument htmlDocument)
        //{
        //    var tableData = new List<List<string>>();

        //    // Select all tables in the HTML document
        //    var tables = htmlDocument.DocumentNode.SelectNodes("//table");

        //    if (tables != null)
        //    {
        //        foreach (var table in tables)
        //        {
        //            var rows = table.SelectNodes("tr");

        //            if (rows != null && rows.Count > 0)
        //            {
        //                var tableRows = new List<string>();

        //                // Extract header row data
        //                var headerCells = rows[0].SelectNodes("th");
        //                if (headerCells != null)
        //                {
        //                    tableRows.AddRange(headerCells.Select(cell => cell.InnerText.Trim()));
        //                }

        //                tableData.Add(tableRows);

        //                // Extract data rows
        //                for (int i = 1; i < rows.Count; i++)
        //                {
        //                    var dataCells = rows[i].SelectNodes("td");

        //                    if (dataCells != null)
        //                    {
        //                        var rowData = dataCells.Select(cell => cell.InnerText.Trim()).ToList();
        //                        tableData.Add(rowData);
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return tableData;
        //}
        //private List<string> ExtractTableData(HtmlDocument htmlDocument)
        //{
        //    var tableData = new List<string>();

        //    // Select all tables in the HTML document
        //    var tables = htmlDocument.DocumentNode.SelectNodes("//table");
        //    var metricsData = tables[0];



        //    if (tables != null)
        //    {
        //        foreach (var table in tables)
        //        {
        //            var rows = table.SelectNodes("tr");
        //            var firstRow = table.SelectNodes("//tbody/tr").First();

        //            if (rows != null && rows.Count > 0)
        //            {
        //                // Extract header row data
        //                var headerCells = rows[0].SelectNodes("td");
        //                if (headerCells != null)
        //                {
        //                    tableData.AddRange(headerCells.Select(cell => cell.InnerText.Trim()));
        //                }

        //                // Extract data rows
        //                for (int i = 1; i < rows.Count; i++)
        //                {
        //                    var dataCells = rows[i].SelectNodes("td");

        //                    if (dataCells != null)
        //                    {
        //                        tableData.AddRange(dataCells.Select(cell => cell.InnerText.Trim()));
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return tableData;
        //}

        //private List<string> ExtractTableData(HtmlDocument htmlDocument)
        //{
        //    var tableData = new List<string>();

        //    // Select all tables in the HTML document
        //    // var tables = htmlDocument.DocumentNode.SelectNodes("//table");
        //    var table = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='tabs']//div[@class-'tab']");
        //   // var metricsData = tables[0];



        //    if (tables != null)
        //    {
        //        foreach (var table in table)
        //        {
        //            var rows = table.SelectNodes("tr");
        //            var firstRow = table.SelectNodes("//tbody/tr").First();

        //            if (rows != null && rows.Count > 0)
        //            {
        //                // Extract header row data
        //                var headerCells = rows[0].SelectNodes("td");
        //                if (headerCells != null)
        //                {
        //                    tableData.AddRange(headerCells.Select(cell => cell.InnerText.Trim()));
        //                }

        //                // Extract data rows
        //                for (int i = 1; i < rows.Count; i++)
        //                {
        //                    var dataCells = rows[i].SelectNodes("td");

        //                    if (dataCells != null)
        //                    {
        //                        tableData.AddRange(dataCells.Select(cell => cell.InnerText.Trim()));
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return tableData;
        //}

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

