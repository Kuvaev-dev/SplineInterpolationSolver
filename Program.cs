using System.Text;
using SplineInterpolationSolver.Input;
using SplineInterpolationSolver.Logic;
using SplineInterpolationSolver.View;

namespace SplineInterpolationSolver
{
    // Головний клас програми
    class Program
    {
        // Приклад точок у файлі:
        // 0,1 0.2
        // 0,3 0,4
        // 0,5 0,6
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.OutputEncoding = Encoding.Unicode;

            bool exit = false;
            while (!exit)
            {
                double[] x = null, y = null;

                Console.WriteLine("Оберіть спосіб введення даних:");
                TextViewer.ChangeColor("\t1. Вручну.", "blue");
                TextViewer.ChangeColor("\t2. З текстового файлу.", "yellow");
                TextViewer.ChangeColor("\t3. Вихід.", "red");
                Console.WriteLine("Ваш вибір:");
                int choice = DataInputValidator.ValidateIntInput(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        (x, y) = DataInput.InputPointsManually();
                        break;
                    case 2:
                        Console.Clear();
                        string filename = DataInput.InputFileName();
                        (x, y) = DataInput.ReadPointsFromFile(filename);

                        for (int i = 0; i < x.Length; i++)
                        {
                            TextViewer.ChangeColor($"\nТочка № {i + 1} - (x[{i}], y[{i}]):", "blue");
                            Console.WriteLine($"\n\tx[{i}] = {x[i]}");
                            Console.WriteLine($"\ty[{i}] = {y[i]}");
                        }
                        break;
                    case 3:
                        Console.WriteLine("Натисніть будь-яку клавішу для виходу...");
                        Console.Clear();
                        exit = true;
                        break;
                    default:
                        TextViewer.ChangeColor("ПОМИЛКА: Некоректний вибір. Використовується введення вручну.", "red");
                        (x, y) = DataInput.InputPointsManually();
                        break;
                }

                if (exit)
                    continue;

                // Вибір методу інтерполяції
                Console.WriteLine("\nОберіть метод інтерполяції:");
                TextViewer.ChangeColor("\t1. Лінійна.", "blue");
                Console.WriteLine("Ваш вибір:");
                int methodChoice = DataInputValidator.ValidateIntInput(Console.ReadLine());

                ISplineInterpolation interpolationMethod;

                switch (methodChoice)
                {
                    case 1:
                        interpolationMethod = new LinearInterpolation();
                        break;
                    default:
                        TextViewer.ChangeColor("ПОМИЛКА: Некоректний вибір. Використовується метод за замовчуванням.", "red");
                        interpolationMethod = new LinearInterpolation();
                        break;
                }

                // Інтерполяція
                double[] interpolatedPoints = interpolationMethod.Interpolate(x, y);

                // Виведення інтерпольованих точок
                TextViewer.ChangeColor("Інтерпольовані точки:", "blue");
                for (int i = 0; i < interpolatedPoints.Length; i += 4)
                {
                    TextViewer.ChangeColor($"\n\tСегмент {i / 4 + 1}: від ({interpolatedPoints[i]}, {interpolatedPoints[i + 1]}) до ({interpolatedPoints[i + 2]}, {interpolatedPoints[i + 3]})", "yellow");
                }

                Console.WriteLine("\nЧи бажаєте продовжити обчислення? (Y/N)");
                string continueChoice = Console.ReadLine();
                exit = !(continueChoice.Equals("Y", StringComparison.OrdinalIgnoreCase));
                Console.Clear();
            }
        }
    }
}
