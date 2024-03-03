using SplineInterpolationSolver.View;

namespace SplineInterpolationSolver.Input
{
    // Клас для вводу даних
    public class DataInput
    {
        // Введення даних вручну
        public static (double[], double[]) InputPointsManually()
        {
            Console.WriteLine("Введіть загальну кількість точок:");
            int n = DataInputValidator.ValidateIntInput(Console.ReadLine());

            double[] x = new double[n];
            double[] y = new double[n];

            for (int i = 0; i < n; i++)
            {
                TextViewer.ChangeColor($"\nТочка № {i + 1} - (x[{i}], y[{i}]):", "blue");

                Console.Write("\n\tx[{0}] = ", i);
                double xCoordinate = DataInputValidator.ValidateDoubleInput(Console.ReadLine());
                x[i] = xCoordinate;

                Console.Write("\ty[{0}] = ", i);
                double yCoordinate = DataInputValidator.ValidateDoubleInput(Console.ReadLine());
                y[i] = yCoordinate;
            }

            return (x, y);
        }

        // Введення з файлу
        public static string InputFileName()
        {
            Console.WriteLine("Введіть щлях до файлу:");
            return Console.ReadLine();
        }

        // Зчитування точок з файлу
        public static (double[], double[]) ReadPointsFromFile(string filename)
        {
            List<double> x = new();
            List<double> y = new();

            using (StreamReader reader = new(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 2)
                    {
                        if (double.TryParse(parts[0], out double xValue) && double.TryParse(parts[1], out double yValue))
                        {
                            x.Add(xValue);
                            y.Add(yValue);
                        }
                    }
                }
            }

            return (x.ToArray(), y.ToArray());
        }
    }
}
