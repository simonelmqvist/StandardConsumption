using CalcTest;
using CalcTest.Models;

string path = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
var directory = System.IO.Path.GetDirectoryName(path); // bin folder

var xValues = new List<double>();
var yValues = new List<double>();
ExcelHelper.GetExcelValuesFromFile(directory, ref xValues, ref yValues);

var normalizer = new MeaseurementNormalizer();
ResultModel result = normalizer.GetThreshold(xValues.ToArray(), yValues.ToArray());
Console.WriteLine($"Threshold is: {result.Threshold}");
Console.WriteLine($"RSquared is: {result.RSquared}");
Console.WriteLine($"Intercept is: {result.Intercept}");
Console.WriteLine($"Slope is: {result.Slope}");
Console.WriteLine($"MValue is: {result.MValue}");
Console.ReadLine();