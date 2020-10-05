using System;
using System.Text;

namespace NAynur19.LinearAlgebra
{
	/// <summary>
	/// Матрица.
	/// </summary>
	public class Matrix
	{
		#region Properties
		/// <summary>
		/// Количество строк.
		/// </summary>
		public int Rows { get; private set; }

		/// <summary>
		/// Количество столбцов.
		/// </summary>
		public int Columns { get; private set; }

		/// <summary>
		/// Матрица элементов.
		/// </summary>
		public double[,] Items { get; private set; }
		#endregion

		#region Indexator
		/// <summary>
		/// Индексатор.
		/// </summary>
		/// <param name="row">Индекс строки.</param>
		/// <param name="column">Индекс столбца.</param>
		/// <returns>Возвращает элемент матрицы по указанному индексу.</returns>
		public double this[int row, int column]
		{
			get
			{
				if(IndexIsValid(row, column))
				{
					return Items[row, column];
				}
				else
				{
					throw new LinearAlgebraException(LinearAlgebraExceptionMessage.IndexOutOfMatrixSizeException);
				}
			}
			set
			{
				if(IndexIsValid(row, column))
				{
					Items[row, column] = value;
				}
				else
				{
					throw new LinearAlgebraException(LinearAlgebraExceptionMessage.IndexOutOfMatrixSizeException);
				}
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Конструктор.
		/// </summary>
		/// <param name="rows">Количество строк.</param>
		/// <param name="columns">Количество столбцов.</param>
		/// <param name="defaultValue">Значение всех элементов матрицы по умолчанию.</param>
		public Matrix(int rows, int columns, double defaultValue = default)
		{
			SetRows(rows);
			SetColumns(columns);
			Items = new double[Rows, Columns];
			SetDefaultValues(defaultValue);
		}

		/// <summary>
		/// Конструктор.
		/// </summary>
		/// <param name="matrix">Двумерный массив.</param>
		public Matrix(double[,] matrix)
		{
			Rows = matrix.GetLength(0);
			Columns = matrix.GetLength(1);
			Items = new double[Rows, Columns];
			SetItems(matrix);
		}

		/// <summary>
		/// Конструктор.
		/// </summary>
		/// <param name="matrix">Исходная матрица.</param>
		public Matrix(Matrix matrix)
		{
			Rows = matrix.Rows;
			Columns = matrix.Columns;
			Items = new double[Rows, Columns];
			SetItems(matrix.Items);
		}
		#endregion

		#region Setters
		/// <summary>
		/// Установка значений из другой матрицы.
		/// </summary>
		/// <param name="matrix">Матрица, значения которой устанавливаются в текущую матрицу.</param>
		public void SetItems(Matrix matrix)
		{
			SetItems(matrix.Items);
		}

		/// <summary>
		/// Установка значений из другой матрицы.
		/// </summary>
		/// <param name="matrix">Матрица, значения которой устанавливаются в текущую матрицу.</param>
		public void SetItems(double[,] matrix)
		{
			if(SizeIsEqual(matrix))
			{
				for(int row = 0; row < Rows; row++)
				{
					for(int column = 0; column < Columns; column++)
					{
						this[row, column] = matrix[row, column];
					}
				}
			}
			else
			{
				throw new LinearAlgebraException(LinearAlgebraExceptionMessage.MatrixSizeNonEqual);
			}
		}

		/// <summary>
		/// Установка значения количества строк в матрице.
		/// </summary>
		/// <param name="rows">Количество строк.</param>
		private void SetRows(int rows)
		{
			if(rows < 0)
			{
				throw new LinearAlgebraException(LinearAlgebraExceptionMessage.MatrixSizeLessOneException);
			}
			else
			{
				Rows = rows;
			}
		}

		/// <summary>
		/// Установка значения количества столбцов в матрице.
		/// </summary>
		/// <param name="columns">Количество столбцов.</param>
		private void SetColumns(int columns)
		{
			if(columns < 0)
			{
				throw new LinearAlgebraException(LinearAlgebraExceptionMessage.MatrixSizeLessOneException);
			}
			else
			{
				Columns = columns;
			}
		}

		/// <summary>
		/// Установка значения в элементы матрицы.
		/// </summary>
		/// <param name="defaultValue">Значение по умолчанию.</param>
		public void SetDefaultValues(double defaultValue)
		{
			for(int row = 0; row < Rows; row++)
			{
				for(int column = 0; column < Columns; column++)
				{
					this[row, column] = defaultValue;
				}
			}
		}

		/// <summary>
		/// Установка вектора в строку матрицы по указанному индексу.
		/// </summary>
		/// <param name="vector">Вектор.</param>
		/// <param name="row">Индекс строки.</param>
		public void SetRow(Vector vector, int row)
		{
			SetRow(vector.Items, row);
		}

		/// <summary>
		/// Установка вектора в строку матрицы по указанному индексу.
		/// </summary>
		/// <param name="vector">Вектор.</param>
		/// <param name="row">Индекс строки.</param>
		public void SetRow(double[] vector, int row)
		{
			if(!RowIndexIsValid(this, row) || vector.Length != Columns)
			{
				throw new LinearAlgebraException(LinearAlgebraExceptionMessage.IndexOutOfMatrixSizeException);
			}

			for(int column = 0; column < vector.Length; column++)
			{
				this[row, column] = vector[column];
			}
		}

		/// <summary>
		/// Установка вектора в столбец матрицы по указанному индексу.
		/// </summary>
		/// <param name="vector">Вектор.</param>
		/// <param name="column">Индекс столбца.</param>
		public void SetColumn(Vector vector, int column)
		{
			SetColumn(vector.Items, column);
		}

		/// <summary>
		/// Установка вектора в столбец матрицы по указанному индексу.
		/// </summary>
		/// <param name="vector">Вектор.</param>
		/// <param name="column">Индекс столбца.</param>
		public void SetColumn(double[] vector, int column)
		{
			if(!ColumnIndexIsValid(this, column) || vector.Length != this.Rows)
			{
				throw new LinearAlgebraException(LinearAlgebraExceptionMessage.IndexOutOfMatrixSizeException);
			}

			for(int row = 0; row < vector.Length; row++)
			{
				this[row, column] = vector[row];
			}
		}
		#endregion

		#region Getters
		/// <summary>
		/// Получение строки матрицы по указанному индексу.
		/// </summary>
		/// <param name="row">Индекс строки.</param>
		/// <returns>Возвращает новый вектор - строку из матрицы по указанному индексу.</returns>
		public Vector GetRow(int row)
		{
			return GetRow(this, row);
		}

		/// <summary>
		/// Получение строки матрицы по указанному индексу.
		/// </summary>
		/// <param name="matrix">Матрица.</param>
		/// <param name="row">Индекс строки.</param>
		/// <returns>Возвращает новый вектор - строку из матрицы по указанному индексу.</returns>
		public static Vector GetRow(Matrix matrix, int row)
		{
			return GetRow(matrix.Items, row);
		}

		/// <summary>
		/// Получение строки матрицы по указанному индексу.
		/// </summary>
		/// <param name="matrix">Матрица.</param>
		/// <param name="row">Индекс строки.</param>
		/// <returns>Возвращает новый вектор - строку из матрицы по указанному индексу.</returns>
		public static Vector GetRow(double[,] matrix, int row)
		{
			if(!RowIndexIsValid(matrix, row))
			{
				throw new LinearAlgebraException(LinearAlgebraExceptionMessage.IndexOutOfMatrixSizeException);
			}

			var result = new Vector(matrix.GetLength(1));
			for(int column = 0; column < result.Dimension; column++)
			{
				result[column] = matrix[row, column];
			}

			return result;
		}

		/// <summary>
		/// Получение столбца матрицы по указанному индексу.
		/// </summary>
		/// <param name="column">Индекс стобца.</param>
		/// <returns>Возвращает новый вектор - столбец из матрицы по указанному индексу.</returns>
		public Vector GetColumn(int column)
		{
			return GetColumn(this, column);
		}

		/// <summary>
		/// Получение столбца матрицы по указанному индексу.
		/// </summary>
		/// <param name="matrix">Матрица.</param>
		/// <param name="column">Индекс столбца.</param>
		/// <returns>Возвращает новый вектор - стобец из матрицы по указанному индексу.</returns>
		public static Vector GetColumn(Matrix matrix, int column)
		{
			return GetColumn(matrix.Items, column);
		}

		/// <summary>
		/// Получение столбца матрицы по указанному индексу.
		/// </summary>
		/// <param name="matrix">Матрица.</param>
		/// <param name="column">Индекс столбца.</param>
		/// <returns>Возвращает новый вектор - стобец из матрицы по указанному индексу.</returns>
		public static Vector GetColumn(double[,] matrix, int column)
		{
			if(!ColumnIndexIsValid(matrix, column))
			{
				throw new LinearAlgebraException(LinearAlgebraExceptionMessage.IndexOutOfMatrixSizeException);
			}

			var result = new Vector(matrix.GetLength(0));
			for(int row = 0; row < result.Dimension; row++)
			{
				result[row] = matrix[row, column];
			}

			return result;
		}
		#endregion

		#region Matrix Invert
		/// <summary>
		/// Инвертирование матрицы.
		/// </summary>
		/// <returns>Возвращает текущую инвертированную матрицу.</returns>
		public Matrix ToInvert()
		{
			for(int row = 0; row < Rows; row++)
			{
				for(int column = 0; column < Columns; column++)
				{
					this[row, column] = -this[row, column];
				}
			}

			return this;
		}

		/// <summary>
		/// Инвертирование матрицы другой матрицы.
		/// </summary>
		/// <param name="matrix">Исходная матрица.</param>
		public static void ToInvert(Matrix matrix)
		{
			ToInvert(matrix.Items);
		}

		/// <summary>
		/// Инвертирование другой матрицы.
		/// </summary>
		/// <param name="matrix">Исходный двумерный массив.</param>
		public static void ToInvert(double[,] matrix)
		{
			for(int row = 0; row < matrix.GetLength(0); row++)
			{
				for(int column = 0; column < matrix.GetLength(1); column++)
				{
					matrix[row, column] = -matrix[row, column];
				}
			}
		}

		/// <summary>
		/// Получение инвертированной матрицы.
		/// </summary>
		/// <param name="matrix">Исходная матрица.</param>
		/// <returns>Возвращает инвертированную матрицу.</returns>
		public static Matrix GetInvertMatrix(Matrix matrix)
		{
			return GetInvertMatrix(matrix.Items);
		}

		/// <summary>
		/// Получение инвертированной матрицы.
		/// </summary>
		/// <param name="matrix">Исходная матрица.</param>
		/// <returns>Возвращает инвертированную матрицу.</returns>
		public static Matrix GetInvertMatrix(double[,] matrix)
		{
			var result = new Matrix(matrix.GetLength(0), matrix.GetLength(1));
			for(int row = 0; row < result.Rows; row++)
			{
				for(int column = 0; column < result.Columns; column++)
				{
					result[row, column] = -matrix[row, column];
				}
			}

			return result;
		}
		#endregion

		#region Matrix Addation
		/// <summary>
		/// Сложение матриц.
		/// </summary>
		/// <param name="matrix">Матрица, которую прибавляем к текущей.</param>
		/// <returns>Возвращает текущую матрицу - результат сложения матриц.</returns>
		public Matrix Addation(Matrix matrix)
		{
			return Addation(matrix.Items);
		}

		/// <summary>
		/// Сложение матриц.
		/// </summary>
		/// <param name="matrix">Матрица, которую прибавляем к текущей.</param>
		/// <returns>Возвращает текущую матрицу - результат сложения матриц.</returns>
		public Matrix Addation(double[,] matrix)
		{
			if(SizeIsEqual(matrix))
			{
				for(int row = 0; row < this.Rows; row++)
				{
					for(int column = 0; column < this.Columns; column++)
					{
						this[row, column] += matrix[row, column];
					}
				}
			}
			else
			{
				throw new LinearAlgebraException(LinearAlgebraExceptionMessage.MatrixSizeNonEqual);
			}
		
			return this;
		}

		/// <summary>
		/// Сложение матриц.
		/// </summary>
		/// <param name="matrix1">Матрица 1.</param>
		/// <param name="matrix2">Матрица 1.</param>
		/// <returns>Возвращает новую матрицу - результат сложения 2-х матриц.</returns>
		public static Matrix Addation(Matrix matrix1, Matrix matrix2)
		{
			if(!matrix1.SizeIsEqual(matrix2))
			{
				throw new LinearAlgebraException(LinearAlgebraExceptionMessage.MatrixSizeNonEqual);
			}

			var result = new Matrix(matrix1.Rows, matrix1.Columns);


			return result;
		}

		/// <summary>
		/// Сложение матриц.
		/// </summary>
		/// <param name="matrix1">Матрица 1.</param>
		/// <param name="matrix2">Матрица 1.</param>
		/// <returns>Возвращает новую матрицу - результат сложения 2-х матриц.</returns>
		public static Matrix Addation(double[,] matrix1, double[,] matrix2)
		{
			if(!SizeIsEqual(matrix1, matrix2))
			{
				throw new LinearAlgebraException(LinearAlgebraExceptionMessage.MatrixSizeNonEqual);
			}

			var result = new Matrix(matrix1.GetLength(0), matrix1.GetLength(1));
			for(int row = 0; row < result.Rows; row++)
			{
				for(int column = 0; column < result.Columns; column++)
				{
					result[row, column] = matrix1[row, column] + matrix2[row, column];
				}
			}

			return result;
		}
		#endregion

		#region Matrix Subtraction
		/// <summary>
		/// Вычитание матрицы из текущей.
		/// </summary>
		/// <param name="matrix">Матрица, которую вычитают из текущей матрицы.</param>
		/// <returns>Возвращает текущую матрицу - результат разности матриц.</returns>
		public Matrix Subtraction(Matrix matrix)
		{
			return Subtraction(matrix.Items);
		}

		/// <summary>
		/// Вычитание матрицы из текущей.
		/// </summary>
		/// <param name="matrix">Матрица, которую вычитают из текущей матрицы.</param>
		/// <returns>Возвращает текущую матрицу - результат разности матриц.</returns>
		public Matrix Subtraction(double[,] matrix)
		{
			if(!SizeIsEqual(matrix))
			{
				throw new LinearAlgebraException(LinearAlgebraExceptionMessage.MatrixSizeNonEqual);
			}

			for(int row = 0; row < Rows; row++)
			{
				for(int column = 0; column < Columns; column++)
				{
					this[row, column] -= matrix[row, column];
				}
			}

			return this;
		}

		/// <summary>
		/// Разность матриц.
		/// </summary>
		/// <param name="matrix1">Матрица 1.</param>
		/// <param name="matrix2">Матрица 2.</param>
		/// <returns>Возвращает новую матрицу - результат разности матриц.</returns>
		public static Matrix Subtration(Matrix matrix1, Matrix matrix2)
		{
			return Subtraction(matrix1.Items, matrix2.Items);
		}

		/// <summary>
		/// Разность матриц.
		/// </summary>
		/// <param name="matrix1">Матрица 1.</param>
		/// <param name="matrix2">Матрица 2.</param>
		/// <returns>Возвращает новую матрицу - результат разности матриц.</returns>
		public static Matrix Subtraction(double[,] matrix1, double[,] matrix2)
		{
			if(!SizeIsEqual(matrix1, matrix2))
			{
				throw new LinearAlgebraException(LinearAlgebraExceptionMessage.MatrixSizeNonEqual);
			}

			var result = new Matrix(matrix1.GetLength(0), matrix1.GetLength(1));
			for(int row = 0; row < result.Rows; row++)
			{
				for(int column = 0; column < result.Columns; column++)
				{
					result[row, column] = matrix1[row, column] - matrix2[row, column];
				}
			}

			return result;
		}
		#endregion

		#region Matrix Copy From
		/// <summary>
		/// Копирование матрицы из тругой матрицы.
		/// </summary>
		/// <param name="matrix">Матрица, значения которого копируются в текущую матрицу.</param>
		public void CopyFrom(Matrix matrix)
		{
			CopyFrom(matrix.Items);
		}

		/// <summary>
		/// Копирование матрицы из двумерного массива.
		/// </summary>
		/// <param name="matrix">Двумерный массив, значения которого копируются в текущую матрицу.</param>
		public void CopyFrom(double[,] matrix)
		{
			if(matrix.GetLength(0) == this.Rows && matrix.GetLength(1) == this.Columns)
			{
				for(int row = 0; row < this.Rows; row++)
				{
					for(int column = 0; column < this.Columns; column++)
					{
						this[row, column] = matrix[row, column];
					}
				}
			}
			else
			{
				throw new LinearAlgebraException(LinearAlgebraExceptionMessage.MatrixSizeNonEqual);
			}
		}
		#endregion

		#region Matrix Multiplication By Number
		/// <summary>
		/// Умножение матрицы на скаляр.
		/// </summary>
		/// <param name="scalar">Скаляр.</param>
		/// <returns>Возвращает текущую матрицу - результат умножения матрицы на скаляр.</returns>
		public Matrix MultiplicationByNumber(double scalar)
		{
			for(int row = 0; row < Rows; row++)
			{
				for(int column = 0; column < Columns; column++)
				{
					this[row, column] *= scalar;
				}
			}

			return this;
		}

		/// <summary>
		/// Умножение матрицы на скаляр.
		/// </summary>
		/// <param name="matrix">Матрица.</param>
		/// <param name="scalar">Скаляр.</param>
		/// <returns>Возвращает новую матрицу - результат умножения матрицы на скаляр.</returns>
		public static Matrix MultiplicationByScalar(Matrix matrix, double scalar)
		{
			return MultiplicationByScalar(matrix.Items, scalar);
		}

		/// <summary>
		/// Умножение матрицы на скаляр.
		/// </summary>
		/// <param name="matrix">Матрица.</param>
		/// <param name="scalar">Скаляр.</param>
		/// <returns>Возвращает новую матрицу - результат умножения матрицы на скаляр.</returns>
		public static Matrix MultiplicationByScalar(double[,] matrix, double scalar)
		{
			var result = new Matrix(matrix.GetLength(0), matrix.GetLength(1));
			for(int row = 0; row < result.Rows; row++)
			{
				for(int column = 0; column < result.Columns; column++)
				{
					result[row, column] = matrix[row, column] * scalar;
				}
			}

			return result;
		}
		#endregion

		#region matrix Multiplication on Matrix
		/// <summary>
		/// Произведение матриц.
		/// </summary>
		/// <param name="matrix">Матрица даннх.</param>
		/// <returns>возвращает новую матрицу - результат произведения 2-х матриц.</returns>
		public Matrix Multiplication(Matrix matrix)
		{
			return Multiplication(matrix.Items);
		}

		/// <summary>
		/// Произведение матриц.
		/// </summary>
		/// <param name="matrix">Матрица данных.</param>
		/// <returns>возвращает новую матрицу - результат произведения 2-х матриц.</returns>
		public Matrix Multiplication(double[,] matrix)
		{
			return Multiplication(this.Items, matrix);
		}

		/// <summary>
		/// Произведение матриц.
		/// </summary>
		/// <param name="matrix1">Матрица данных 1.</param>
		/// <param name="matrix2">Матрица данных 2.</param>
		/// <returns>Возвращает новую матрицу - результат произведения 2-х матриц.</returns>
		public static Matrix Multiplication(Matrix matrix1, Matrix matrix2)
		{
			return Multiplication(matrix1.Items, matrix2.Items);
		}

		/// <summary>
		/// Произведение матриц.
		/// </summary>
		/// <param name="matrix1">Матрица данных 1.</param>
		/// <param name="matrix2">Матрица данных 2.</param>
		/// <returns>Возвращает новую матрицу - результат произведения 2-х матриц.</returns>
		public static Matrix Multiplication(double[,] matrix1, double[,] matrix2)
		{
			Matrix.SizeIsValidForMultiplication(matrix1, matrix2, true);

			var result = new Matrix(matrix1.GetLength(0), matrix2.GetLength(1));

			for(int resultRow = 0; resultRow < result.Rows; resultRow++)
			{
				for(int resultColumn = 0; resultColumn < result.Columns; resultColumn++)
				{
					result[resultRow, resultColumn] = Vector.ScalarMultiplication(Matrix.GetRow(matrix1, resultRow), Matrix.GetColumn(matrix2, resultColumn));
				}
			}

			return result;
		}
		#endregion

		#region Matrix Is Equal
		/// <summary>
		/// Проверка равенства матриц.
		/// </summary>
		/// <param name="matrix">Матрица данных.</param>
		/// <returns>Возвращает истину если матрицы равны, иначе возвращает ложь.</returns>
		public bool IsEqual(Matrix matrix)
		{
			return IsEqual(matrix.Items);
		}

		/// <summary>
		/// Проверка равенства матриц.
		/// </summary>
		/// <param name="matrix">Матрица данных.</param>
		/// <returns>Возвращает истину если матрицы равны, иначе возвращает ложь.</returns>
		public bool IsEqual(double[,] matrix)
		{
			return IsEqual(this.Items,matrix);
		}

		/// <summary>
		/// Проверка равенства матриц.
		/// </summary>
		/// <typeparam name="T">Тип данных, реализующий интерфейс IComparable.</typeparam>
		/// <param name="matrix1">Матрица данных 1.</param>
		/// <param name="matrix2">Матрица данных 2.</param>
		/// <returns>Возвращает истину если матрицы равны, иначе возвращает ложь.</returns>
		public static bool IsEqual(Matrix matrix1, Matrix matrix2)
		{
			return IsEqual(matrix1.Items, matrix2.Items);
		}

		/// <summary>
		/// Проверка равенства матриц.
		/// </summary>
		/// <typeparam name="T">Тип данных, реализующий интерфейс IComparable.</typeparam>
		/// <param name="matrix1">Матрица данных 1.</param>
		/// <param name="matrix2">Матрица данных 2.</param>
		/// <returns>Возвращает истину если матрицы равны, иначе возвращает ложь.</returns>
		public static bool IsEqual<T>(T[,] matrix1, T[,] matrix2) where T : IComparable
		{
			var result = true;
			if(!SizeIsEqual(matrix1, matrix2))
			{
				return false;
			}

			for(int row = 0; row < matrix1.GetLength(0); row++)
			{
				for(int column = 0; column < matrix1.GetLength(1); column++)
				{
					if(matrix1[row, column].CompareTo(matrix2[row, column]) != 0)
					{
						return false;
					}
				}
			}

			return result;
		}
		#endregion

		#region Matrix Row Index Validation
		/// <summary>
		/// Проверка индекса строки на валидность.
		/// </summary>
		/// <param name="row">Индекс строки.</param>
		/// <param name="isThrowException">Указатель на выброс исключения в случае False. По умолчанию отключен.</param>
		/// <returns>Возвращает истину если индекс валидный, иначе выбрасывает соответствующее исключение.</returns>
		public bool RowIndexIsValid(int row, bool isThrowException = false)
		{
			return RowIndexIsValid(this, row, isThrowException);
		}

		/// <summary>
		/// Проверка индекса строки на валидность.
		/// </summary>
		/// <param name="matrix">Матрица данных.</param>
		/// <param name="row">Индекс строки.</param>
		/// <param name="isThrowException">Указатель на выброс исключения в случае False. По умолчанию отключен.</param>
		/// <returns>Возвращает истину если индекс валидный, иначе выбрасывает соответствующее исключение.</returns>
		public static bool RowIndexIsValid(Matrix matrix, int row, bool isThrowException = false)
		{
			return RowIndexIsValid(matrix.Items, row, isThrowException);
		}

		/// <summary>
		/// Проверка индекса строки на валидность.
		/// </summary>
		/// <typeparam name="T">Универсальный тип данных.</typeparam>
		/// <param name="row">Индекс строки.</param>
		/// <param name="matrix">Матрица данных.</param>
		/// <param name="isThrowException">Указатель на выброс исключения в случае False. По умолчанию отключен.</param>
		/// <returns>Возвращает истину если индекс валидный, иначе выбрасывает соответствующее исключение.</returns>
		public static bool RowIndexIsValid<T>(T[,] matrix, int row, bool isThrowException = false)
		{
			if(row < 0 && row >= matrix.GetLength(0))
			{
				if(isThrowException)
				{
					throw new LinearAlgebraException(LinearAlgebraExceptionMessage.MatrixRowIndexOutSizeException);
				}
				else
				{
					return false;
				}
				
			}

			return true;
		}
		#endregion

		#region Matrix Column Index Validation
		/// <summary>
		/// Проверка индекса столбца на валидность.
		/// </summary>
		/// <param name="matrix">Матрица.</param>
		/// <param name="isThrowException">Указатель на выброс исключения в случае False. По умолчанию отключен.</param>
		/// <returns>Возвращает истину если индекс валидный, иначе выбрасывает соответствующее исключение.</returns>
		public bool ColumnIndexIsValid(int column, bool isThrowException = false)
		{
			return ColumnIndexIsValid(this, column, isThrowException);
		}

		/// <summary>
		/// Проверка индекса столбца на валидность.
		/// </summary>
		/// <param name="matrix">Матрица.</param>
		/// <param name="column">Индекс столбца.</param>
		/// <param name="isThrowException">Указатель на выброс исключения в случае False. По умолчанию отключен.</param>
		/// <returns>Возвращает истину если индекс валидный, иначе выбрасывает соответствующее исключение.</returns>
		public static bool ColumnIndexIsValid(Matrix matrix, int column, bool isThrowException = false)
		{
			return ColumnIndexIsValid(matrix.Items, column, isThrowException);
		}

		/// <summary>
		/// Проверка индекса столбца на валидность.
		/// </summary>
		/// <typeparam name="T">Универсальный тип данных.</typeparam>
		/// <param name="matrix">Матрица.</param>
		/// <param name="column">Индекс столбца.</param>
		/// <param name="isThrowException">Указатель на выброс исключения в случае False. По умолчанию отключен.</param>
		/// <returns>Возвращает истину если индекс валидный, иначе выбрасывает соответствующее исключение.</returns>
		public static bool ColumnIndexIsValid<T>(T[,] matrix, int column, bool isThrowException = false)
		{
			if(column < 0 && column >= matrix.GetLength(1))
			{
				if(isThrowException)
				{
					throw new LinearAlgebraException(LinearAlgebraExceptionMessage.MatrixColumnIndexOutSizeException);
				}
				else
				{
					return false;
				}
				
			}

			return true;
		}
		#endregion

		#region Matrix Index Validation
		/// <summary>
		/// Проверка индексов на валидность.
		/// </summary>
		/// <param name="row">Индекс строки.</param>
		/// <param name="column">Индекс столбца.</param>
		/// <param name="isThrowException">Указатель на выброс исключения в случае False. По умолчанию отключен.</param>
		/// <returns>Возвращает истину если индекс валидный, иначе выбрасывает соответствующее исключение.</returns>
		public bool IndexIsValid(int row, int column, bool isThrowException = false)
		{
			return (RowIndexIsValid(row, isThrowException) && ColumnIndexIsValid(column, isThrowException));
		}

		/// <summary>
		/// Проверка индексов на валидность.
		/// </summary>
		/// <param name="matrix">Матрица.</param>
		/// <param name="row">Индекс строки.</param>
		/// <param name="column">Индекс столбца.</param>
		/// <param name="isThrowException">Указатель на выброс исключения в случае False. По умолчанию отключен.</param>
		/// <returns>Возвращает истину если индекс валидный, иначе выбрасывает соответствующее исключение.</returns>
		public bool IndexIsValid(Matrix matrix, int row, int column, bool isThrowException = false)
		{
			return (RowIndexIsValid(matrix, row, isThrowException) && ColumnIndexIsValid(matrix, column, isThrowException));
		}

		/// <summary>
		/// Проверка индексов на валидность.
		/// </summary>
		/// <typeparam name="T">Униерсальный тип данных.</typeparam>
		/// <param name="matrix">Матрица.</param>
		/// <param name="row">Индекс строки.</param>
		/// <param name="column">Индекс столбца.</param>
		/// <param name="isThrowException">Указатель на выброс исключения в случае False. По умолчанию отключен.</param>
		/// <returns>Возвращает истину если индекс валидный, иначе выбрасывает соответствующее исключение.</returns>
		public static bool IndexIsValid<T>(T[,] matrix, int row, int column, bool isThrowException = false)
		{
			return (RowIndexIsValid(matrix, row, isThrowException) && ColumnIndexIsValid(matrix, column, isThrowException));
		}
		#endregion

		#region Matrix Size Is Equal
		/// <summary>
		/// Проверка равенства размерностей матриц.
		/// </summary>
		/// <param name="matrix2">Матрица данных.</param>
		/// <param name="isThrowException">Указатель на выброс исключения в случае False. По умолчанию отключен.</param>
		/// <returns>Возвращает истину если размерности совпадают, иначе - выбрасывает соответствующее исключение.</returns>
		public bool SizeIsEqual(Matrix matrix, bool isThrowException = false)
		{
			return SizeIsEqual(this, matrix, isThrowException);
		}

		/// <summary>
		/// Проверка равенства размерностей матриц.
		/// </summary>
		/// <param name="matrix1">Матрица данных 1.</param>
		/// <param name="matrix2">Матрица данных 2.</param>
		/// <param name="isThrowException">Указатель на выброс исключения в случае False. По умолчанию отключен.</param>
		/// <returns>Возвращает истину если размерности совпадают, иначе - выбрасывает соответствующее исключение.</returns>
		public static bool SizeIsEqual(Matrix matrix1, Matrix matrix2, bool isThrowException = false)
		{
			return SizeIsEqual(matrix1.Items, matrix2.Items, isThrowException);
		}

		/// <summary>
		/// Проверка равенства размерностей матриц.
		/// </summary>
		/// <typeparam name="T">Универсальный тип данных.</typeparam>
		/// <param name="matrix1">Матрица данных 1.</param>
		/// <param name="matrix2">Матрица данных 2.</param>
		/// <param name="isThrowException">Указатель на выброс исключения в случае False. По умолчанию отключен.</param>
		/// <returns>Возвращает истину если размерности совпадают, иначе - выбрасывает соответствующее исключение.</returns>
		public static bool SizeIsEqual<T>(T[,] matrix1, T[,] matrix2, bool isThrowException = false)
		{
			if(matrix1.GetLength(0) != matrix2.GetLength(0) || matrix1.GetLength(1) != matrix2.GetLength(1))
			{
				if(isThrowException)
				{
					throw new LinearAlgebraException(LinearAlgebraExceptionMessage.MatrixSizeNonEqualException);
				}
				else
				{
					return false;
				}
			}

			return true;
		}
		#endregion

		#region Matrix Size Validation For Matrix Multiplication
		/// <summary>
		/// Проверка размерностей матриц для операции произведения указанных матриц.
		/// </summary>
		/// <param name="matrix">Матрица данных.</param>
		/// <param name="isThrowException">Указатель выброса исключения если метод должен вернуть false. По умолчанию отключен.</param>
		/// <returns>Возвращает истину если размерности совпадают, иначе возвращает ложь.</returns>
		public bool SizeIsValidForMultiplication(Matrix matrix, bool isThrowException = false)
		{
			return SizeIsValidForMultiplication(this, matrix, isThrowException);
		}

		/// <summary>
		/// Проверка размерностей матриц для операции произведения указанных матриц.
		/// </summary>
		/// <param name="matrix1">Матрица данных 1.</param>
		/// <param name="matrix2">Матрица данных 2.</param>
		/// <param name="isThrowException">Указатель выброса исключения если метод должен вернуть false. По умолчанию отключен.</param>
		/// <returns>Возвращает истину если размерности совпадают, иначе возвращает ложь.</returns>
		public static bool SizeIsValidForMultiplication(Matrix matrix1, Matrix matrix2, bool isThrowException = false)
		{
			return SizeIsValidForMultiplication(matrix1.Items, matrix2.Items, isThrowException);
		}

		/// <summary>
		/// Проверка размерностей матриц для операции произведения указанных матриц.
		/// </summary>
		/// <typeparam name="T">Универсальный тип данных.</typeparam>
		/// <param name="matrix1">Матрица данных 1.</param>
		/// <param name="matrix2">Матрица данных 2.</param>
		/// <param name="isThrowException">Указатель выброса исключения если метод должен вернуть false. По умолчанию отключен.</param>
		/// <returns>Возвращает истину если размерности совпадают, иначе возвращает ложь.</returns>
		public static bool SizeIsValidForMultiplication<T>(T[,] matrix1, T[,] matrix2, bool isThrowException = false)
		{
			if(matrix1.GetLength(0) != matrix2.GetLength(1))
			{
				if(isThrowException)
				{
					throw new LinearAlgebraException(LinearAlgebraExceptionMessage.MatrixSizeNonValidForMultiplication);
				}
				else
				{
					return false;
				}
			}

			return true;
		}
		#endregion

		#region Matrix To String
		/// <summary>
		/// Вывод строкового представления матрицы.
		/// </summary>
		/// <returns>Возвращает строковое представление текущей матрицы.</returns>
		public override string ToString()
		{
			return ToString(this);
		}

		/// <summary>
		/// Вывод строкового представления матрицы.
		/// </summary>
		/// <param name="matrix">Матрица данных.</param>
		/// <returns>Возвращает строковое представление матрицы.</returns>
		public static string ToString(Matrix matrix)
		{
			return ToString(matrix.Items);
		}

		/// <summary>
		/// Вывод строкового представления матрицы.
		/// </summary>
		/// <typeparam name="T">Универсальный тип данных.</typeparam>
		/// <param name="matrix">Матрица данных.</param>
		/// <returns>Возвращает строковое представление матрицы.</returns>
		public static string ToString<T>(T[,] matrix)
		{
			var splitter = "|\r\n|";
			StringBuilder stringMatrix = new StringBuilder($"|");
			
			if(matrix.GetLength(0) > 0 && matrix.GetLength(1) > 0)
			{
				for(int row = 0; row < matrix.GetLength(0); row++)
				{
					stringMatrix.Append($"{matrix[row, 0]}");
					for(int column = 1; column < matrix.GetLength(1); column++)
					{
						stringMatrix.Append($", {matrix[row, column]}");
					}

					stringMatrix.Append(splitter);
				}
				stringMatrix.Remove(stringMatrix.Length - splitter.Length, splitter.Length);
			}

			stringMatrix.Append($"|");

			return stringMatrix.ToString();
		}
		#endregion
	}
}
