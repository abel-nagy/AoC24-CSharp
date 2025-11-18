using System.Text;

//RunOnTestData();
await RunOnRealDataAsync();

// ==========================================

static void RunOnTestData()
{
    Console.WriteLine("Test Data analysis:");
    int[][] testInput =
    {
        [7, 6, 4, 2, 1],
        [1, 2, 7, 8, 9],
        [9, 7, 6, 2, 1],
        [1, 3, 2, 4, 5],
        [8, 6, 4, 4, 1],
        [1, 3, 6, 7, 9],
    };
    var testOutput = CalculateNumberOfSafeReports(testInput);
    Console.WriteLine($"Number of safe reports: {testOutput}");
}

static async Task RunOnRealDataAsync()
{
    Console.WriteLine("Real Data analysis:");
    var fileContent = await ReadFileAsync("../../../input.txt");
    var inputData = ConvertFileInputToData(fileContent);
    var output = CalculateNumberOfSafeReports(inputData);
    Console.WriteLine($"Number of safe reports: {output}");
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

static int CalculateNumberOfSafeReports(int[][] testInput)
{
    var numberOfSafeReports = 0;
    foreach (var report in testInput)
    {
        var orderType = GetOrderType(report);
        if (!IsOrderly(report, orderType)) continue;
        numberOfSafeReports++;
    }

    return numberOfSafeReports;
}

static int GetOrderType(int[] levels)
{
    var output = 0;
    for (var i = 1; i < levels.Length; i++)
    {
        if (levels[i] < levels[i - 1] && output != 1)
        {
            output = -1;
        } 
        else if (levels[i] > levels[i - 1] && output != -1)
        {
            output = 1;
        } else
        {
            return 0;
        }
    }
    return output;
}

static bool IsOrderly(int[] levels, int orderType)
{
    if(orderType == 0) { return false; }
    else if (orderType == 1)
    {
        for (var i = 1; i < levels.Length; i++)
        {
            var difference = levels[i] - levels[i - 1];
            if (difference < 1 || difference > 3) return false;
        }
    }
    else if(orderType == -1)
    {
        for (var i = 1; i < levels.Length; i++)
        {
            var difference = levels[i - 1] - levels[i];
            if (difference < 1 || difference > 3) return false;
        }
    }

    return true;
}