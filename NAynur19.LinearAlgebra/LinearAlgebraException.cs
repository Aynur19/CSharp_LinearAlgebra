using System;

namespace NAynur19.LinearAlgebra
{
	public class LinearAlgebraException : Exception
	{
		public LinearAlgebraException() : base() { }
		public LinearAlgebraException(string message) : base(message) { }
		public LinearAlgebraException(string message, Exception innerException) : base(message, innerException) { }
	}

	public static class LinearAlgebraExceptionMessage
	{
		public static string MatrixSizeLessOneException => "Размер матрицы не может быть меньше 1";
		public static string VectorSizeLessOneException => "Размер вектора не может быть меньше 1";
		public static string IndexOutOfMatrixSizeException => "Индекс находится за пределами массива.";
		public static string IndexOutOfVectorSizeException => "Индекс находится за пределами массива.";
		public static string MatrixSizeNonEqual => "Размеры матриц не одинаковы";
		public static string VectorSizeNonEqual => "Размеры векторов не одинаковы";
		public static string MatrixSizeNonValidForMultiplication => "Размеры матриц не подходят для произведения";
	}
}
