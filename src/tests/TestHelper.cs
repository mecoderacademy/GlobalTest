
namespace tests;
using codetest;
internal class TestHelper
{
    public static void RunConsoleApp(string[]? arguments = default)
    {        
        var entryPoint = typeof(AnagramFinder).Assembly.EntryPoint!;
        entryPoint.Invoke(null, new object[] { arguments ?? Array.Empty<string>() });
    }

    public static string[] CapturedStdOut(Action callback)
    {
        TextWriter originalStdOut = Console.Out;

        using var newStdOut = new StringWriter();
        Console.SetOut(newStdOut);

        callback.Invoke();
        var capturedOutput = newStdOut.ToString();

        Console.SetOut(originalStdOut);

        return capturedOutput.Replace("\r", "").Split("\n", StringSplitOptions.TrimEntries|StringSplitOptions.RemoveEmptyEntries);
    }
}