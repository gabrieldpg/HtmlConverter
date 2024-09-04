using HtmlAgilityPack;
using System.Text;

namespace HtmlConverter;

public static class TableConverter
{
    public static string HtmlTableToMarkdown(string html)
    {
        var columnWidths = new List<int>();
        var rowData = new List<List<string>>();

        CalculateRawDataAndWidths(Rows(html), columnWidths, rowData);

        return FormatMarkdownTable(columnWidths, rowData);
    }

    private static HtmlNodeCollection Rows(string html)
    {
        // Load HTML
        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        // Retrieve table
        var table = doc.DocumentNode.SelectSingleNode("//table");
        if (table == null)
            throw new ArgumentException("No table found in the provided HTML.");

        // Retrieve rows, ignoring colgroup and other elements
        var rows = table.SelectNodes(".//tr");
        if (rows == null)
            throw new ArgumentException("No table rows found in the provided HTML.");

        return rows;
    }

    private static void CalculateRawDataAndWidths(
        HtmlNodeCollection rows, 
        List<int> columnWidths,
        List<List<string>> rowData)
    {
        foreach (var row in rows)
        {
            // Get cells
            var cells = row.SelectNodes("th|td");
            if (cells == null) continue;

            // Format text for each cell
            var rowText = cells
                .Select(cell => cell.InnerText.CleanHtmlText())
                .ToList();

            rowData.Add(rowText);

            // Calculate maximum width for this column
            for (int i = 0; i < rowText.Count; i++)
            {
                if (i >= columnWidths.Count)
                    columnWidths.Add(rowText[i].Length);
                else
                    columnWidths[i] = Math.Min(Constants.MaxColumnWidth, Math.Max(columnWidths[i], rowText[i].Length));
            }
        }
    }

    private static string FormatMarkdownTable(
        List<int> columnWidths,
        List<List<string>> rowData)
    {
        var markdown = new StringBuilder();

        foreach (var row in rowData)
        {
            // Append row data
            for (int i = 0; i < row.Count; i++)
            {
                markdown.Append("|").Append(row[i].PadRight(columnWidths[i]));
            }
            markdown.AppendLine("|");

            // Append header separator after first row
            if (rowData.IndexOf(row) == 0)
            {
                for (int i = 0; i < row.Count; i++)
                {
                    markdown.Append("|").Append(new string('-', columnWidths[i]));
                }
                markdown.AppendLine("|");
            }
        }

        return markdown.ToString();
    }
}