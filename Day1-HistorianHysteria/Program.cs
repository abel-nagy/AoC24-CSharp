using System.Text;

//RunCalculationsOnTestData();
await RunCalculationsOnRealDataAsync();

// =========================================================================================

static void RunCalculationsOnTestData()
{
    int[] input1 = { 3, 4, 2, 1, 3, 3 };
    int[] input2 = { 4, 3, 5, 3, 9, 3 };

    Console.WriteLine("Test Data analysis");
    Console.WriteLine($"Sum of difference: {CalculateSumOfDifference(input1, input2)}");
    Console.WriteLine($"Similarity score: {CalculateSimilarityScore(input1, input2)}");
}

static async Task RunCalculationsOnRealDataAsync()
{
    Console.WriteLine("Real Data analysis:");
    var fileContent = await ReadFileAsync("../../../input.txt");
    var input = ConvertInputToIntArrays(fileContent);
    Console.WriteLine($"Sum of difference: {CalculateSumOfDifference(input[0], input[1])}");
    Console.WriteLine($"Similarity score: {CalculateSimilarityScore(input[0], input[1])}");
}

// =========================================================================================

static async Task<string> ReadFileAsync(string path)
{
    return await File.ReadAllTextAsync(path, Encoding.UTF8);
}

static int[][] ConvertInputToIntArrays(string input)
{
    var rows = input.Split(Environment.NewLine);
    int[] array1 = new int[rows.Length];
    int[] array2 = new int[rows.Length];

    for (var i = 0; i < rows.Length; i++)
    {
        var splitRow = rows[i].Split("   ");
        int.TryParse(splitRow[0], out array1[i]);
        int.TryParse(splitRow[1], out array2[i]);
    }

    return [array1, array2];
}

static int CalculateSumOfDifference(int[] array1, int[] array2)
{
    array1 = array1.Order().ToArray();
    array2 = array2.Order().ToArray();
    int[] diffs = new int[array1.Length];

    for (var i = 0; i < array1.Length; i++)
    {
        diffs[i] = Math.Max(array1[i], array2[i]) - Math.Min(array1[i], array2[i]);
    }

    return diffs.Sum();
}

static int CalculateSimilarityScore(int[] array1, int[] array2)
{
    var similarity = 0;
    for (var i = 0; i < array1.Length; i++)
    {
        similarity += array1[i] * array2.Count(number => number == array1[i]);
    }

    return similarity;
}