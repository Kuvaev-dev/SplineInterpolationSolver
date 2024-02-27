using SplineInterpolationSolver.View;

namespace SplineInterpolationSolver.Input
{
    public class DataInputValidator
    {
        // Валідація введення цілих чисел
        public static int ValidateIntInput(string input)
        {
            int value;

            while (!int.TryParse(input, out value))
            {
                TextViewer.ChangeColor("\nПОМИЛКА: Некоректне введення даних. Значення має бути типу 'int'\n", "red");
                input = Console.ReadLine();
            }

            return value;
        }

        // Валідація введення дійсних чисел
        public static double ValidateDoubleInput(string input)
        {
            double value;

            while (!double.TryParse(input, out value))
            {
                TextViewer.ChangeColor("\nПОМИЛКА: Некоректне введення даних. Значення має бути типу 'double'\n", "red");
                input = Console.ReadLine();
            }

            return value;
        }
    }
}
