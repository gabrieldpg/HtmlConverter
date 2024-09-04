using NLog;

namespace HtmlConverter;

public class Program
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    static void Main(string[] args)
    {
        try
        {
            var html = File.ReadAllText(FileHandler.defaultInputHtml);

            var markdown = TableConverter.HtmlTableToMarkdown(html);
            Console.WriteLine(markdown);

            File.WriteAllText(FileHandler.defaultOutputMd, markdown);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

}