namespace HtmlConverter;

public class FileHandler
{
    private static string inputOutputPath = 
        Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\..\Files\"));

    public static string defaultInputHtml = 
        Path.GetFullPath(Path.Combine(inputOutputPath, @"input.html"));

    public static string defaultOutputMd =
        Path.GetFullPath(Path.Combine(inputOutputPath, @"output.md"));

    public static string GetFileInDefaultPath(string filename)
    {
        return Path.GetFullPath(Path.Combine(inputOutputPath, filename));
    }
}