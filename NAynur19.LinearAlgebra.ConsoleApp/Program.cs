using System;
using System.Dynamic;

namespace NAynur19.LinearAlgebra.ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			TestMatrixMultiplicationOnMatrx();
			//TestMatrixAddation();
			//TestMatrixMultiplicationOnScalar();

			Console.ReadKey();
		}

		static void TestMatrixMultiplicationOnMatrx()
		{
			var matrixA = new Matrix(new double[,] { { 1, 2, 3 } });
			Console.WriteLine($"Матрица A:");
			Console.WriteLine($"{matrixA}");

			var matrixB = new Matrix(new double[,] { { 5 }, { -4 }, { 0 } });
			Console.WriteLine($"Матрица B:");
			Console.WriteLine($"{matrixB}");

			var matrixAB = matrixA.Multiplication(matrixB);
			Console.WriteLine($"Матрица AB:");
			Console.WriteLine($"{matrixAB}");

			Console.WriteLine($"-----------------------------------------------");
			matrixA = new Matrix(new double[,] { { 2, 1 }, { 0, 3 } });
			Console.WriteLine($"Матрица A:");
			Console.WriteLine($"{matrixA}");

			matrixB = new Matrix(new double[,] { { 3, 5 }, { 4, -1 } });
			Console.WriteLine($"Матрица B:");
			Console.WriteLine($"{matrixB}");

			matrixAB = matrixA.Multiplication(matrixB);
			Console.WriteLine($"Матрица AB:");
			Console.WriteLine($"{matrixAB}");
		}

		static void TestMatrixAddation()
		{
			var matrixA = new Matrix(new double[,] { { 3, 7, 1 }, { -1, 2, 0 } });
			Console.WriteLine($"Матрица А:");
			Console.WriteLine($"{matrixA}");

			var matrixB = new Matrix(new double[,] { { 6, -15, 3 }, { 4, 1, 2 } });
			Console.WriteLine($"Матрица B:");
			Console.WriteLine($"{matrixB}");

			matrixA.Addation(matrixB);
			Console.WriteLine($"Матрица А + B:");
			Console.WriteLine($"{matrixA}");
		}

		static void TestMatrixMultiplicationOnScalar()
		{
			var matrix = new Matrix(new double[,] { { 2, -3, 0 }, { 1, 4, -1 } });

			Console.WriteLine($"Матрица А:");
			Console.WriteLine($"{matrix}");

			matrix.MultiplicationByNumber(5);
			Console.WriteLine($"Матрица 5А:");
			Console.WriteLine($"{matrix}");
		}
	}
}
