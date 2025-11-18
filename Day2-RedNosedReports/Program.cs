using System.Text;

RunOnTestData();
Console.WriteLine();
await RunOnRealDataAsync();

// ==========================================

static void RunOnTestData()
{
    Console.WriteLine("Test Data analysis:");
    int[][] input =
    {
        [7, 6, 4, 2, 1],
        [1, 2, 7, 8, 9],
        [9, 7, 6, 2, 1],
        [1, 3, 2, 4, 5],
        [8, 6, 4, 4, 1],
        [1, 3, 6, 7, 9],
    };
    var output = CalculateNumberOfSafeReports(input);
    Console.WriteLine($"Number of safe reports: {output}");

    var outputWithProblemDampeners = CalculateNumberOfSafeReports(input, true);
    Console.WriteLine($"Number of safe reports with Problem Dampeners on: {outputWithProblemDampeners}");
}

static async Task RunOnRealDataAsync()
{
    Console.WriteLine("Real Data analysis:");
    var fileContent = await ReadFileAsync("../../../input.txt");
    var input = ConvertFileInputToData(fileContent);

    var output = CalculateNumberOfSafeReports(input);
    Console.WriteLine($"Number of safe reports: {output}");

    var outputWithProblemDampeners = CalculateNumberOfSafeReports(input, true);
    Console.WriteLine($"Number of safe reports with Problem Dampeners on: {outputWithProblemDampeners}");
}

// ==========================================

static async Task<string> ReadFileAsync(string path)
{
    return await File.ReadAllTextAsync(path, Encoding.UTF8);
}

static int[][] ConvertFileInputToData(string input)
{
    var rows = input.Split(Environment.NewLine);
    int[][] inputData = new int[rows.Length][];
    for (var i = 0; i < rows.Length; i++)
    {
        var splitRow = rows[i].Split(" ");
        int[] rowData = new int[splitRow.Length];
        for (var j = 0; j < splitRow.Length; j++)
        {
            int.TryParse(splitRow[j], out rowData[j]);
        }
        inputData[i] = rowData;
    }

    return inputData;
}

// ==========================================

static int CalculateNumberOfSafeReports(int[][] testInput, bool isDampenerOn = false)
{
    var numberOfSafeReports = 0;
    foreach (var report in testInput)
    {
        if (IsReportSafe(report))
        {
            numberOfSafeReports++;
            continue;
        }

        if (isDampenerOn)
        {
            for (var i = 0; i < report.Length; i++)
            {
                var modifiedReport = report.ToList();
                modifiedReport.RemoveAt(i);
                if (IsReportSafe(modifiedReport.ToArray()))
                {
                    numberOfSafeReports++;
                    break;
                }
            }
        }
    }

    return numberOfSafeReports;
}

static bool IsReportSafe(int[] levels)
{
    var orderDirection = 0;
    var orderSet = false;

    for (var i = 0; i < levels.Length - 1; i++)
    {
        var current = levels[i];
        var next = levels[i + 1];
        var difference = next - current;

        if (!orderSet)
        {
            orderSet = true;
            if (difference < 0) orderDirection = -1;
            else if (difference > 0) orderDirection = 1;
        }
        else
        {
            if ((difference < 0 && orderDirection == 1) || (difference > 0 && orderDirection == -1)) return false;
        }

        difference = Math.Abs(difference);

        if (difference < 1 || difference > 3) return false;
    }

    return true;
}