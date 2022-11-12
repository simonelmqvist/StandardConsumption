using CalcTest.Models;

namespace CalcTest
{
    public class MeaseurementNormalizer
    {
        public ResultModel GetThreshold(double[] xValues, double[] yValues)
        {
            var result = new ResultModel();
            double minimalDifference = double.MaxValue;

            var xMax = (int)Math.Ceiling(xValues[xValues.Length - 1]);
            var xMin = (int)Math.Floor(xValues[0]);
            for (int i = xMin + 2; i <= xMax - 2; i++) // regression must contain at least two numbers
            {
                var regressionXValues = xValues.Where(x => x <= i).ToArray();
                var regressionYValues = yValues.Take(regressionXValues.Length).ToArray();

                // y = intercept + slope*x
                double rSquared, intercept, slope;
                RegressionHelper.LinearRegression(regressionXValues, regressionYValues, out rSquared, out intercept, out slope);

                // remaining straight line
                var remainingYValues = yValues.Skip(regressionYValues.Length).ToArray();
                var averageRemainingYValue = remainingYValues.Average();

                // calculate difference in current scope
                var differences = new double[xValues.Length];                
                for (int j = 0; j < xValues.Length; j++)
                {
                    double difference = 0;
                    if (j < regressionXValues.Length)
                        difference += intercept + slope * xValues[j] - yValues[j];
                    else
                        difference += averageRemainingYValue - yValues[j];
                    
                    differences[j] = Math.Abs(difference);
                }

                var averageDifference = differences.Average();
                if (averageDifference < minimalDifference)
                {
                    minimalDifference = averageDifference;
                    result.Threshold = i;
                    result.RSquared = rSquared;
                    result.Intercept = intercept;
                    result.Slope = slope;
                    result.MValue = averageRemainingYValue;
                }                    
            }

            return result;
        }
    }
}