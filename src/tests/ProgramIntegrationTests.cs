using codetest;

namespace tests;

public class ProgramIntegrationTests
{
    string currentFilePath = Directory.GetCurrentDirectory();

    [SetUp]
    public void Setup()
    {
        // Get the full path to the file
        CreateFile(currentFilePath, "empty.txt");
        CreateFile(currentFilePath, "singleCharacter.txt");
    }

    [TearDown]
    public void TearDown()
    {

        File.Delete($"{currentFilePath}/empty.txt");
        File.Delete($"{currentFilePath}/singleCharacter.txt");
    }

    [Test]
    public void ShouldReturnInputFileRequiredNoArgumentsPresent()
    {
        var output = TestHelper.CapturedStdOut(() => TestHelper.RunConsoleApp());
        Assert.IsTrue(output.Length == 1);
        StringAssert.IsMatch(output[0], "Please ensure that the input file is provided");
    }

    [Test]
    public void ShouldReturnInputFileRequiredIfMoreThan1ArgumentsPresent()
    {
        var output = TestHelper.CapturedStdOut(() => TestHelper.RunConsoleApp(new[] { "one", "two" }));
        Assert.IsTrue(output.Length == 1);
        StringAssert.IsMatch(output[0], "Please ensure that the input file is provided");
    }

    [Test]
    public void ShouldReturnInputFileDoesNotExist()
    {
        var output = TestHelper.CapturedStdOut(() => TestHelper.RunConsoleApp(new[] { "abc.txt" }));
        Assert.IsTrue(output.Length == 1);
        StringAssert.IsMatch(output[0], "abc.txt does not exist");
    }

    [Test]
    public void ShouldHandleEmptyFile()
    {
        // Arrange
        var output = TestHelper.CapturedStdOut(() =>
            TestHelper.RunConsoleApp(new string[] { @"empty.txt" }));

        // Assert
        StringAssert.IsMatch(output[0], "file is empty");
    }

    [Test]
    public void ShouldReturnExpectedAnagramsGrouped()
    {
        // Act - Runs the program with arguments
        var output = TestHelper.CapturedStdOut(() =>
            TestHelper.RunConsoleApp(new string[] { @"example1.txt" }));

        // Assert what is logged to the console
        StringAssert.IsMatch(output[0], "abc,bac,cba");
        StringAssert.IsMatch(output[1], "fun,unf");
        StringAssert.IsMatch(output[2], "hello");
    }


    [Test]
    public void ShouldHandleSingleForWordFile()
    {
        // Arrange

        File.WriteAllLines($"{currentFilePath}/singleCharacter.txt", new string[] { "hello" });

        var output = TestHelper.CapturedStdOut(() =>
            TestHelper.RunConsoleApp(new string[] { @"singleCharacter.txt" }));

        // Assert
        StringAssert.IsMatch(output[0], "hello");
    }
    private void DeleteAllFiles()
    {

    }
    private void CreateFile(string path, string fileName)
    {
        try
        {
            var fullFilePath = Path.Combine(path, fileName);

            if (!File.Exists(fullFilePath))
            {
                using (File.Create(fullFilePath))
                {
                    // Do nothing inside the using statement, it will ensure the file stream is closed
                }
            }
        }
        catch (Exception e)
        {

        }
    }


    [Test]
    public void ShouldHandleLargeNumberOfGroupsAndBeLessThanOriginalFile()
    {
        // Arrange
        var output = TestHelper.CapturedStdOut(() =>
              TestHelper.RunConsoleApp(new string[] { @"example2.txt" }));

        //    // Act
        var result = File.ReadAllLines(@"example2.txt").Length;

        //    Assert
        Assert.Less(output.Length, result);
        //}

    }

    [Test]
    public void ShouldHaveWordsWithSameLengthInEachGroup()
    {
        // Arrange
        var output = TestHelper.CapturedStdOut(() =>
              TestHelper.RunConsoleApp(new string[] { @"example2.txt" }));

        //    // Ac

        // Assert
        foreach (var group in output)
        {
            var words = group.Split(',');

            // Check if all words in the group have the same length
            Assert.IsTrue(words.All(word => word.Length == words[0].Length));
        }


    }

    [Test]
    public void ShouldHaveWordsWithSameCharactersButDifferentOrderInEachGroup()
    {
       
        // Act
        var output = TestHelper.CapturedStdOut(() =>
             TestHelper.RunConsoleApp(new string[] { @"example2.txt" }));

        // Assert
        foreach (var group in output)
        {
            var words = group.Split(',');

            // Check only if the group has more than one word
            if (words.Length > 1)
            {
                for (int i = 0; i < words.Length - 1; i++)
                {
                    var currentWord = words[i];
                    var nextWord = words[i + 1];

                    // Check if the order of characters in the current word is different from the next word

                    if(currentWord.SequenceEqual(nextWord))
                    {

                    }
                    Assert.IsFalse(currentWord.SequenceEqual(nextWord), $"In group '{group}', words '{currentWord}' and '{nextWord}' have the same order.");
                }

                // Check if the order of characters in the last word is different from the first word
                var lastWord = words[words.Length - 1];
                var firstWord = words[0];
                if (lastWord.SequenceEqual(firstWord))
                {

                }
                Assert.IsFalse(lastWord.SequenceEqual(firstWord), $"In group '{group}', words '{lastWord}' and '{firstWord}' have the same order.");
            }
        }

    }
}