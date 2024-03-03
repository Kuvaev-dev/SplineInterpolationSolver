using System.Text;
using SplineInterpolationSolver.View;

namespace SplineInterpolationSolver.Logic
{
    // Клас, що реалізує лінійну інтерполяцію
    public class LinearInterpolation : ISplineInterpolation
    {
        public double[] Interpolate(double[] x, double[] y)
        {
            // Лінійна інтерполяція - просто з'єднуємо точки прямими лініями
            TextViewer.ChangeColor("\nРозв'язання\n", "blue");

            List<double> interpolatedPoints = new();
            StringBuilder interpolationSteps = new();

            for (int i = 0; i < x.Length - 1; i++)
            {
                double slope = (y[i + 1] - y[i]) / (x[i + 1] - x[i]);
                double intercept = y[i] - slope * x[i];

                interpolationSteps.AppendLine($"Крок {i + 1}:");
                interpolationSteps.AppendLine($"\t{i + 1}.1. Розрахунок нахилу: slope = (y[{i + 1}] - y[{i}]) / (x[{i + 1}] - x[{i}]) = ({y[i + 1]} - {y[i]}) / ({x[i + 1]} - {x[i]}) = {slope:0.##}");
                interpolationSteps.AppendLine($"\t{i + 1}.2. Розрахунок перетину з віссю ординат: intercept = y[{i}] - slope * x[{i}] = {y[i]} - {slope} * {x[i]} = {intercept:0.##}");
                interpolationSteps.AppendLine($"\t{i + 1}.3. Рівняння для сегмента {i + 1}: y = {slope:0.##}x + {intercept:0.##} для x у ({x[i]}; {x[i + 1]})");

                interpolatedPoints.Add(x[i]);
                interpolatedPoints.Add(y[i]);
                interpolatedPoints.Add(x[i + 1]);
                interpolatedPoints.Add(y[i + 1]);

                TextViewer.ChangeColor($"\nКрок {i + 1}:\n", "blue");
                TextViewer.ChangeColor($"\t{i + 1}.1. Розрахунок нахилу: \n\tslope = (y[{i + 1}] - y[{i}]) / (x[{i + 1}] - x[{i}]) = ({y[i + 1]} - {y[i]}) / ({x[i + 1]} - {x[i]}) = {slope:0.##}", "magenta");
                TextViewer.ChangeColor($"\t{i + 1}.2. Розрахунок перетину з віссю ординат: \n\tintercept = y[{i}] - slope * x[{i}] = {y[i]} - {slope} * {x[i]} = {intercept:0.##}", "magenta");
                TextViewer.ChangeColor($"\t{i + 1}.3. Рівняння для сегмента {i + 1}: \n\ty = {slope:0.##}x + {intercept:0.##} для x у ({x[i]}; {x[i + 1]})", "magenta");
            }

            Console.WriteLine();

            // Збереження проміжних результатів у текстовий файл
            string filenameTimestamp = $"Interpolation_Results_{DateTime.Now:yyyyMMddHHmmss}.txt";
            using (StreamWriter writer = new(filenameTimestamp))
            {
                writer.WriteLine("Результати інтерполяції:\n");
                writer.WriteLine(interpolationSteps.ToString());

                writer.WriteLine("Інтерпольовані точки:");
                for (int i = 0; i < interpolatedPoints.Count; i += 4)
                {
                    writer.WriteLine($"\tСегмент {i / 4 + 1}: від ({interpolatedPoints[i]}, {interpolatedPoints[i + 1]}) до ({interpolatedPoints[i + 2]}, {interpolatedPoints[i + 3]})");
                }
            }
            Console.WriteLine($"\nРезультати інтерполяції збережено до файлу {filenameTimestamp}\n");

            return interpolatedPoints.ToArray();
        }
    }
}
