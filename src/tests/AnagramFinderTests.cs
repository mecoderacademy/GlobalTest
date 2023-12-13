using codetest;
using Microsoft.VisualStudio.TestPlatform.Utilities;

namespace tests;

public class AnagramFinderTests
{
    string currentFilePath = Directory.GetCurrentDirectory();

    [SetUp]
    public void Setup()
    {
       
    }


    [Test]
    public void ShouldReturnFileDoesntExist()
    {
        // Arrange - set up the input
        var input = @"example1232.txt";

        // Act - Run the program with arguments
        var output = TestHelper.CapturedStdOut(() =>
            TestHelper.RunConsoleApp(new string[] { input }));

        // Assert - Check what is logged to the console
        StringAssert.IsMatch(output[0], $"{input} does not exist");
    }


    
}