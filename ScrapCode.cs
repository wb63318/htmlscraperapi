namespace htmlscraperapi
{
    public class ScrapCode
    {

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
    }
}
