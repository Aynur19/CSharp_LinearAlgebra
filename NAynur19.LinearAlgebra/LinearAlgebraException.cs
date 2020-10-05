using System;

namespace NAynur19.LinearAlgebra
{
	/// <summary>
	/// Класс исключений, наследуемый от базового класса исключений.
	/// </summary>
	public class LinearAlgebraException : Exception
	{
		/// <summary>
		/// Конструктор, наследующий базовую логику.
		/// </summary>
		public LinearAlgebraException() : base() { }

		/// <summary>
		/// Конструктор, наследующий базовую логику.
		/// </summary>
		/// <param name="message">Сообщение об ошибке.</param>
		public LinearAlgebraException(string message) : base(message) { }

		/// <summary>
		/// Конструктор, наследующий базовую логику.
		/// </summary>
		/// <param name="message">Сообщение об ошибке</param>
		/// <param name="innerException">Внутреннее исключение.</param>
		public LinearAlgebraException(string message, Exception innerException) : base(message, innerException) { }
	}

	/// <summary>
	/// Класс сообщений исключений.
	/// </summary>
	public static class LinearAlgebraExceptionMessage
	{
		#region Vector Exceptions
		public static string VectorSizeLessOneException => "Размер вектора не может быть меньше 1.";
		public static string IndexOutOfVectorSizeException => "Индекс находится за пределами массива.";
		public static string StartIndexOutOfVectorSizeException => "Начальный индекс находится за пределами массива.";
		public static string EndIndexOutOfVectorSizeException => "Конечный индекс находится за пределами массива.";
		public static string EndIndexLessStartIndexOfVectorException => "Конечный индекс меньше стартового индекса массива.";
		public static string VectorSizeNonEqualException => "Размеры векторов не одинаковы.";
		#endregion

		#region Matrix Exceptions
		public static string MatrixSizeLessOneException => "Размер матрицы не может быть меньше 1";
		public static string MatrixRowIndexOutSizeException => "Индекс строки находится за пределами матрицы.";
		public static string MatrixColumnIndexOutSizeException => "Индекс столбца находится за пределами матрицы.";
		public static string MatrixSizeNonEqualException => "Размеры матриц не одинаковы.";
		public static string MatrixSizeNonValidForMultiplication => "Размеры матриц не подходят для произведения.";
		#endregion
	}
}
