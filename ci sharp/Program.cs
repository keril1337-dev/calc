using System;

namespace MatrixCalculator
{
    class Matrix
    {
        public double[,] Data { get; }
        public int Rows { get; }
        public int Cols { get; }

        public Matrix(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            Data = new double[rows, cols];
        }

        public void FillFromConsole()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    Console.Write($"Элемент [{i + 1},{j + 1}]: ");
                    Data[i, j] = double.Parse(Console.ReadLine());
                }
            }
        }

        public void Print()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    Console.Write($"{Data[i, j],8} ");
                }
                Console.WriteLine();
            }
        }

        public static Matrix Add(Matrix a, Matrix b)
        {
            if (a.Rows != b.Rows || a.Cols != b.Cols)
            {
                Console.WriteLine("Ошибка ввода");
                return null;
            }

            Matrix result = new Matrix(a.Rows, a.Cols);
            Console.WriteLine("\nПромежуточные действия (сложение):");
            for (int i = 0; i < a.Rows; i++)
            {
                for (int j = 0; j < a.Cols; j++)
                {
                    result.Data[i, j] = a.Data[i, j] + b.Data[i, j];
                    Console.WriteLine($"[{i + 1},{j + 1}]: {a.Data[i, j]} + {b.Data[i, j]} = {result.Data[i, j]}");
                }
            }
            return result;
        }

        public static Matrix Subtract(Matrix a, Matrix b)
        {
            if (a.Rows != b.Rows || a.Cols != b.Cols)
            {
                Console.WriteLine("Ошибка ввода!");
                return null;
            }

            Matrix result = new Matrix(a.Rows, a.Cols);
            Console.WriteLine("\nПромежуточные действия (вычитание):");
            for (int i = 0; i < a.Rows; i++)
            {
                for (int j = 0; j < a.Cols; j++)
                {
                    result.Data[i, j] = a.Data[i, j] - b.Data[i, j];
                    Console.WriteLine($"[{i + 1},{j + 1}]: {a.Data[i, j]} - {b.Data[i, j]} = {result.Data[i, j]}");
                }
            }
            return result;
        }

        public static Matrix Multiply(Matrix a, Matrix b)
        {
            if (a.Cols != b.Rows)
            {
                Console.WriteLine("Ошибка ввода");
                return null;
            }

            Matrix result = new Matrix(a.Rows, b.Cols);
            Console.WriteLine("\nПромежуточные действия (умножение строки на столбец):");
            for (int i = 0; i < a.Rows; i++)
            {
                for (int j = 0; j < b.Cols; j++)
                {
                    double sum = 0;
                    string steps = "";
                    for (int k = 0; k < a.Cols; k++)
                    {
                        double prod = a.Data[i, k] * b.Data[k, j];
                        sum += prod;
                        steps += $"{a.Data[i, k]}*{b.Data[k, j]}" + (k == a.Cols - 1 ? "" : " + ");
                    }
                    result.Data[i, j] = sum;
                    Console.WriteLine($"[{i + 1},{j + 1}]: {steps} = {sum}");
                }
            }
            return result;
        }

        public void Determinant()
        {
            if (Rows != Cols)
            {
                Console.WriteLine("Ошибка ввода");
                return;
            }

            double det = 0;
            if (Rows == 1)
            {
                det = Data[0, 0];
                Console.WriteLine($"Определитель 1x1: {det}");
            }
            else if (Rows == 2)
            {
                det = Data[0, 0] * Data[1, 1] - Data[0, 1] * Data[1, 0];
                Console.WriteLine($"Промежуточные действия (2x2):");
                Console.WriteLine($"({Data[0, 0]} * {Data[1, 1]}) - ({Data[0, 1]} * {Data[1, 0]}) = {det}");
            }
            else if (Rows == 3)
            {
                double p1 = Data[0, 0] * Data[1, 1] * Data[2, 2];
                double p2 = Data[0, 1] * Data[1, 2] * Data[2, 0];
                double p3 = Data[1, 0] * Data[2, 1] * Data[0, 2];

                double m1 = Data[0, 2] * Data[1, 1] * Data[2, 0];
                double m2 = Data[0, 1] * Data[1, 0] * Data[2, 2];
                double m3 = Data[1, 2] * Data[2, 1] * Data[0, 0];

                det = (p1 + p2 + p3) - (m1 + m2 + m3);

                Console.WriteLine("Промежуточные действия (метод треугольников):");
                Console.WriteLine($"Положительные: ({p1}) + ({p2}) + ({p3}) = {p1+p2+p3}");
                Console.WriteLine($"Отрицательные: ({m1}) + ({m2}) + ({m3}) = {m1+m2+m3}");
                Console.WriteLine($"Итог: {p1+p2+p3} - ({m1+m2+m3}) = {det}");
            }
            Console.WriteLine($"Определитель матрицы: {det}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Выберите операцию: 1.Сложение, 2.Вычитание, 3.Умножение, 4.Определитель");
                int choice = int.Parse(Console.ReadLine());

                Console.Write("Введите количество строк 1-й матрицы: ");
                int r1 = int.Parse(Console.ReadLine());
                Console.Write("Введите количество столбцов 1-й матрицы: ");
                int c1 = int.Parse(Console.ReadLine());
                Matrix m1 = new Matrix(r1, c1);
                m1.FillFromConsole();

                if (choice == 4)
                {
                    m1.Determinant();
                }
                else
                {
                    Console.Write("Введите количество строк 2-й матрицы: ");
                    int r2 = int.Parse(Console.ReadLine());
                    Console.Write("Введите количество столбцов 2-й матрицы: ");
                    int c2 = int.Parse(Console.ReadLine());
                    Matrix m2 = new Matrix(r2, c2);
                    m2.FillFromConsole();

                    Matrix result = null;
                    switch (choice)
                    {
                        case 1: result = Matrix.Add(m1, m2); break;
                        case 2: result = Matrix.Subtract(m1, m2); break;
                        case 3: result = Matrix.Multiply(m1, m2); break;
                        default: Console.WriteLine("Неверный выбор."); break;
                    }

                    if (result != null)
                    {
                        Console.WriteLine("Результат:");
                        result.Print();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошбика ввода");
            }
        }
    }

}
