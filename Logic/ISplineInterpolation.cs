namespace SplineInterpolationSolver.Logic
{
    // Інтерфейс для визначення методів інтерполяції сплайнами
    public interface ISplineInterpolation
    {
        double[] Interpolate(double[] x, double[] y);
    }
}
