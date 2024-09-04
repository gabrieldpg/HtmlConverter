using HtmlAgilityPack;

namespace HtmlConverter;

public static class StringExtensions
{
    public static string CleanHtmlText(this string text)
    {
        // Clean the text by removing unwanted characters like &nbsp;
        string cellText = HtmlEntity.DeEntitize(text).Trim();

        // Further cleaning can be done here if needed
        // e.g., removing line breaks or multiple spaces
        return cellText
            .Replace("\n", "")
            .Replace("\r", "")
            .Replace("\t", " ")
            .Replace("  ", " ");
    }
}