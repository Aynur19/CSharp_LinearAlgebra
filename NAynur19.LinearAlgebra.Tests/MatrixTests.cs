using System;
using System.Collections.Generic;

using Xunit;

namespace NAynur19.LinearAlgebra.Tests
{
	/// <summary>
	/// Класс тестирования матрицы.
	/// </summary>
	public class MatrixTests
	{

		#region Matrix Multiplication. Scalar
		/// <summary>
		/// Тестирование метода Multiplication.
		/// При умножении матрицы на скаляр элементы матрицы должны быть правильно высчитаны в пределах точности 10^(-12).
		/// </summary>
		/// <param name="matrix">Исходная матрица.</param>
		/// <param name="scalar">Скаляр.</param>
		/// <param name="expectedMatrix">Ожидаемая матрица.</param>
		[Theory]
		[MemberData(nameof(Multiplication_SpecialScalar_Data))]
		public void Multiplication_SpecialScalar_CorrectWork(double[,] matrix, double scalar, double[,] expectedMatrix)
		{
			var actualMatrix = new Matrix(matrix).MultiplicationByNumber(scalar);
			var eps = Math.Pow(10, -12);

			for(int row = 0; row < actualMatrix.Rows; row++)
			{
				for(int column = 0; column < actualMatrix.Columns; column++)
				{
					var delta = actualMatrix[row, column] - expectedMatrix[row, column];

					if(double.IsNaN(delta))
					{
						Assert.True(double.IsNaN(actualMatrix[row, column]) && double.IsNaN(expectedMatrix[row, column])
							|| double.IsInfinity(actualMatrix[row, column]) && double.IsInfinity(expectedMatrix[row, column]));
					}
					else if(double.IsInfinity(delta))
					{
						Assert.True(double.IsInfinity(actualMatrix[row, column]) && double.IsInfinity(expectedMatrix[row, column]));
					}
					else
					{
						Assert.True((eps >= Math.Abs(actualMatrix[row, column] - expectedMatrix[row, column])));
					}
				}
			}
		}

		/// <summary>
		/// Данные для тестового метода Multiplication_SpecialScalar_CorrectWork.
		/// </summary>
		public static IEnumerable<object[]> Multiplication_SpecialScalar_Data =>
			new List<object[]>
			{
				new object[]
				{
					new double[,] 
					{ 
						{ -12345.6789, 0, 0.00000000001 },
						{ 98765.4321, double.NaN, double.NegativeInfinity },
						{ double.MinValue, double.MaxValue, double.Epsilon } 
					},
					double.Epsilon,
					new double[,] 
					{ 
						{ -12345.6789 * double.Epsilon, 0, 0.00000000001 * double.Epsilon },
						{ 98765.4321 * double.Epsilon, double.NaN, double.NegativeInfinity * double.Epsilon },
						{ double.MinValue * double.Epsilon, double.MaxValue * double.Epsilon, double.Epsilon * double.Epsilon } 
					}
				},
				new object[]
				{
					new double[,]
					{
						{ -12345.6789, 0, 0.00000000001 },
						{ 98765.4321, double.NaN, double.NegativeInfinity },
						{ double.MinValue, double.MaxValue, double.Epsilon }
					},
					double.MaxValue,
					new double[,]
					{
						{ -12345.6789 * double.MaxValue, 0, 0.00000000001 * double.MaxValue },
						{ 98765.4321 * double.MaxValue, double.NaN, double.NegativeInfinity * double.MaxValue },
						{ double.MinValue * double.MaxValue, double.MaxValue * double.MaxValue, double.Epsilon * double.MaxValue }
					}
				},
				new object[]
				{
					new double[,]
					{
						{ -12345.6789, 0, 0.00000000001 },
						{ 98765.4321, double.NaN, double.NegativeInfinity },
						{ double.MinValue, double.MaxValue, double.Epsilon }
					},
					double.MinValue,
					new double[,]
					{
						{ -12345.6789 * double.MinValue, 0, 0.00000000001 * double.MinValue },
						{ 98765.4321 * double.MinValue, double.NaN, double.NegativeInfinity * double.MinValue },
						{ double.MinValue * double.MinValue, double.MaxValue * double.MinValue, double.Epsilon * double.MinValue }
					}
				},
				new object[]
				{
					new double[,]
					{
						{ -12345.6789, 0, 0.00000000001 },
						{ 98765.4321, double.NaN, double.NegativeInfinity },
						{ double.MinValue, double.MaxValue, double.Epsilon }
					},
					double.NaN,
					new double[,]
					{
						{ double.NaN, double.NaN, double.NaN },
						{ double.NaN, double.NaN, double.NaN },
						{ double.NaN, double.NaN, double.NaN }
					}
				},
				new object[]
				{
					new double[,]
					{
						{ -12345.6789, 0, 0.00000000001 },
						{ 98765.4321, double.NaN, double.NegativeInfinity },
						{ double.MinValue, double.MaxValue, double.Epsilon }
					},
					double.NegativeInfinity,
					new double[,]
					{
						{ -12345.6789 * double.NegativeInfinity, 0 * double.NegativeInfinity, 0.00000000001 * double.NegativeInfinity },
						{ 98765.4321 * double.NegativeInfinity, double.NaN * double.NegativeInfinity, double.NegativeInfinity * double.NegativeInfinity },
						{ double.MinValue * double.NegativeInfinity, double.MaxValue * double.NegativeInfinity, double.Epsilon * double.NegativeInfinity }
					}
				},
				new object[]
				{
					new double[,]
					{
						{ -12345.6789, 0, 0.00000000001 },
						{ 98765.4321, double.NaN, double.NegativeInfinity },
						{ double.MinValue, double.MaxValue, double.Epsilon }
					},
					double.PositiveInfinity,
					new double[,]
					{
						{ -12345.6789 * double.PositiveInfinity, 0 * double.PositiveInfinity, 0.00000000001 * double.PositiveInfinity },
						{ 98765.4321 * double.PositiveInfinity, double.NaN * double.PositiveInfinity, double.NegativeInfinity * double.PositiveInfinity },
						{ double.MinValue * double.PositiveInfinity, double.MaxValue * double.PositiveInfinity, double.Epsilon * double.PositiveInfinity }
					}
				}
			};

		/// <summary>
		/// Тестирование метода Multiplication.
		/// При умножении матрицы на скаляр элементы матрицы должны быть правильно высчитаны в пределах точности 10^(-12).
		/// </summary>
		/// <param name="matrix">Исходная матрица.</param>
		/// <param name="scalar">Скаляр.</param>
		/// <param name="expectedMatrix">Ожидаемая матрица.</param>
		[Theory]
		[MemberData(nameof(Multiplication_Scalar_Data))]
		public void Multiplication_Scalar_CorrectWork(double[,] matrix, double scalar, double[,] expectedMatrix)
		{
			var actualMatrix = new Matrix(matrix).MultiplicationByNumber(scalar);
			var eps = Math.Pow(10, -12);

			for(int row = 0; row < actualMatrix.Rows; row++)
			{
				for(int column = 0; column < actualMatrix.Columns; column++)
				{
					var delta = actualMatrix[row, column] - expectedMatrix[row, column];

					if(double.IsNaN(delta))
					{
						Assert.True(double.IsInfinity(actualMatrix[row, column]) && double.IsInfinity(expectedMatrix[row, column]) 
							|| double.IsNaN(actualMatrix[row, column]) && double.IsNaN(expectedMatrix[row, column]));
					}
					else
					{
						Assert.True((eps >= Math.Abs(actualMatrix[row, column] - expectedMatrix[row, column])));
					}
				}
			}
		}

		/// <summary>
		/// Данные для тестового метода Multiplication_Scalar_CorrectWork.
		/// </summary>
		public static IEnumerable<object[]> Multiplication_Scalar_Data =>
			new List<object[]>
			{
				new object[]
				{
					new double[,] { { 1 } },
					10000,
					new double[,] { { 10000 } }
				},
				new object[]
				{
					new double[,] { { 1, -1, 2 }, { double.MaxValue, -99.0, double.MinValue } },
					-1000,
					new double[,] { { -1000, 1000, -2000 }, { double.NegativeInfinity, 99000, double.PositiveInfinity } }
				},
				new object[]
				{
					new double[,] { { 1 }, { double.NaN }, { double.NegativeInfinity }, { double.PositiveInfinity } },
					-500,
					new double[,] { { -500 }, { double.NaN }, { double.PositiveInfinity }, { double.NegativeInfinity } }
				},
				new object[]
				{
					new double[,] { { 1, 0 }, { -9, 3 }, { 7, 7 }, { -7, -7 }, { -125, 0.255 } },
					12.34,
					new double[,] { { 12.34, 0 }, { -111.06, 37.02 }, { 86.38, 86.38 }, { -86.38, -86.38 }, { -1542.5, 3.1467 } }
				},
				new object[]
				{
					new double[,] { { } },
					1245,
					new double[,] { { } }
				},
				new object[]
				{
					new double[,]
					{
						{ double.Epsilon, double.MaxValue, double.MinValue, double.NaN, double.NegativeInfinity, double.PositiveInfinity },
						{ -99.963, 87.852, -76.741, 65.63, -54.52, 43.41 }
					},
					0.987,
					new double[,]
					{
						{ double.Epsilon, double.MaxValue * 0.987, double.MinValue * 0.987, double.NaN, double.NegativeInfinity, double.PositiveInfinity },
						{ -98.663481, 86.709924, -75.743367, 64.77681, -53.81124, 42.84567 }
					},
				},
				new object[]
				{
					new double[,] { }, 
					100,
					new double[,] { },
				}
			};
		#endregion

		#region Is Equal
		/// <summary>
		/// Тестирование метода IsEqual.
		/// Для равенства матриц необходимо, чтобы размерности совпадали и значения элементов соответственно тоже были равны.
		/// Nan = Nan!
		/// </summary>
		/// <param name="matrix">Матрица исходная.</param>
		/// <param name="isEqualMatrix">Матрица, равная исходной.</param>
		/// <param name="isNotEquaMatrix">Матрица неравная исходной.</param>
		[Theory]
		[MemberData(nameof(IsEqual_TwoDimensionArray_Data))]
		public void IsEqual_Matrix_CorreсtWork(double[,] matrix, double[,] isEqualMatrix, double[,] isNotEquaMatrix)
		{
			var actaulMatrix = new Matrix(matrix);

			Assert.False(actaulMatrix.IsEqual(new Matrix(isNotEquaMatrix)));
			Assert.True(actaulMatrix.IsEqual(new Matrix(isEqualMatrix)));
		}

		/// <summary>
		/// Тестирование метода IsEqual.
		/// Для равенства матриц необходимо, чтобы размерности совпадали и значения элементов соответственно тоже были равны.
		/// Nan = Nan!
		/// </summary>
		/// <param name="matrix">Матрица исходная.</param>
		/// <param name="isEqualMatrix">Матрица, равная исходной.</param>
		/// <param name="isNotEquaMatrix">Матрица неравная исходной.</param>
		[Theory]
		[MemberData(nameof(IsEqual_TwoDimensionArray_Data))]
		public void IsEqual_TwoDimensionArray_CorreсtWork(double[,] matrix, double[,] isEqualMatrix, double[,] isNotEquaMatrix)
		{
			var actualMatrix = new Matrix(matrix);

			Assert.False(actualMatrix.IsEqual(isNotEquaMatrix));
			Assert.True(actualMatrix.IsEqual(isEqualMatrix));
		}

		/// <summary>
		/// Данные для тестового метода IsEqual_TwoDimensionArray_CorreсtWork.
		/// </summary>
		public static IEnumerable<object[]> IsEqual_TwoDimensionArray_Data =>
			new List<object[]>
			{
				new object[]
				{
					new double[,] { { 1 } },
					new double[,] { { 1 } },
					new double[,] { { } },
				},
				new object[]
				{
					new double[,] { { 1, -1, 2 }, { double.MaxValue, -99.0, double.MinValue } },
					new double[,] { { 1, -1, 2 }, { double.MaxValue, -99.0, double.MinValue } },
					new double[,] { { 1, -1 }, { 2, double.MaxValue }, { -99.0, double.MinValue } },
				},
				new object[]
				{
					new double[,] { { 1 }, { double.NaN }, { double.NegativeInfinity }, { double.PositiveInfinity } },
					new double[,] { { 1 }, { double.NaN }, { double.NegativeInfinity }, { double.PositiveInfinity } },
					new double[,] { { 1, double.NaN, double.NegativeInfinity, double.PositiveInfinity } },
				},
				new object[]
				{
					new double[,] { { 1, 0 }, { -9, 3 }, { 7, 7 }, { -7, -7 }, { -125, 0.255 } },
					new double[,] { { 1, 0 }, { -9, 3 }, { 7, 7 }, { -7, -7 }, { -125, 0.255 } },
					new double[,] { { 1, 0, -9, 3, 7 }, { 7, -7, -7, -125, 0.255 } },
				},
				new object[]
				{
					new double[,] { { } },
					new double[,] { { } },
					new double[,] { },
				},
				new object[]
				{
					new double[,] { { double.Epsilon, double.MaxValue, double.MinValue, double.NaN, double.NegativeInfinity, double.PositiveInfinity }, { -99.963, 87.852, -76.741, 65.63, -54.52, 43.41 } },
					new double[,] { { double.Epsilon, double.MaxValue, double.MinValue, double.NaN, double.NegativeInfinity, double.PositiveInfinity }, { -99.963, 87.852, -76.741, 65.63, -54.52, 43.41 } },
					new double[,] { { double.Epsilon, double.MaxValue, double.MinValue, double.NaN, double.NegativeInfinity, double.PositiveInfinity }, { -99.93, 87.852, -76.741, 65.63, -54.52, 43.41 } },
				},
				new object[]
				{
					new double[,] { },
					new double[,] { },
					new double[,] { { } },
				}
			};
		#endregion

		#region To String
		/// <summary>
		/// Тестирование метода ToString.
		/// Корректный вывод в стрку матрицы.
		/// </summary>
		/// <param name="matrix">Исходная матрица.</param>
		/// <param name="matrixToString">Строковое представление матрицы.</param>
		[Theory]
		[MemberData(nameof(ToString_Data))]
		public void ToString_CorrectToStringMatrix(double[,] matrix, string matrixToString)
		{
			var actualMatrix = new Matrix(matrix);

			Assert.Equal(matrixToString, actualMatrix.ToString());
		}

		/// <summary>
		/// Данные для тестового метода ToString_CorrectToStringMatrix.
		/// </summary>
		public static IEnumerable<object[]> ToString_Data =>
			new List<object[]>
			{
				new object[]
				{
					new double[,] {	{ 1 } }, 
					$"|1|"
				},
				new object[]
				{
					new double[,] { { 1, -1, 2 }, { double.MaxValue, -99.0, double.MinValue } },
					$"|1, -1, 2|\r\n" +
					$"|{double.MaxValue}, -99, {double.MinValue}|"
				},
				new object[]
				{
					new double[,] { { 1 }, { double.NaN }, { double.NegativeInfinity }, { double.PositiveInfinity } },
					$"|1|\r\n|{double.NaN}|\r\n|{double.NegativeInfinity}|\r\n|{double.PositiveInfinity}|"
				},
				new object[] 
				{
					new double[,] { { 1, 0 }, { -9, 3 }, { 7, 7 }, { -7, -7 }, { -125, 0.255 } },
					$"|1, 0|\r\n" +
					$"|-9, 3|\r\n" +
					$"|7, 7|\r\n" +
					$"|-7, -7|\r\n" +
					$"|-125, 0,255|"
				},
				new object[]
				{
					new double[,] { { } },
					$"||"
				},
				new object[]
				{
					new double[,] 
					{ 
						{ double.Epsilon, double.MaxValue, double.MinValue, double.NaN, double.NegativeInfinity, double.PositiveInfinity }, 
						{ -99.963, 87.852, -76.741, 65.63, -54.52, 43.41 } 
					},
					$"|{double.Epsilon}, {double.MaxValue}, {double.MinValue}, {double.NaN}, {double.NegativeInfinity}, {double.PositiveInfinity}|\r\n" +
					$"|-99,963, 87,852, -76,741, 65,63, -54,52, 43,41|"
				},
				new object[]
				{
					new double[,] { },
					$"||"
				}
			};
		#endregion

		#region Matrix Indexator. Index In Matrix Size
		/// <summary>
		/// Тестирование индексатора.
		/// Выброс исключения, если в индексаторе указываются индексы выходящие за пределы матрицы.
		/// </summary>
		/// <param name="rows">Количество строк.</param>
		/// <param name="columns">Количество столбцов.</param>
		/// <param name="rowIndex">Индекс строки.</param>
		/// <param name="columnIndex">Индекс столбца.</param>
		[Theory]
		[MemberData(nameof(MatrixIndexator_IndexInMatrixSize_Data))]
		public void MatrixIndexator_IndexInMatrixSize_CorrectGetSet(int rows, int columns, int rowIndex, int columnIndex)
		{
			var matrix = new Matrix(rows, columns, -10);
			double newValue = 10;

			matrix[rowIndex, columnIndex] = newValue;

			Assert.Equal(newValue, matrix[rowIndex, columnIndex]);
		}

		/// <summary>
		/// Данные для тестового метода MatrixIndexator_IndexInMatrixSize_CorrectGetSet.
		/// </summary>
		public static IEnumerable<object[]> MatrixIndexator_IndexInMatrixSize_Data =>
			new List<object[]>
			{
				new object[] { 10, 10, 0, 9 },
				new object[] { 10, 10, 1, 8 },
				new object[] { 10, 10, 2, 7 },
				new object[] { 10, 10, 3, 6 },
				new object[] { 10, 10, 4, 5 },
				new object[] { 10, 10, 5, 6 },
				new object[] { 10, 10, 6, 7 },
				new object[] { 1, 1, 0, 0 },
			};
		#endregion

		#region Matrix Indexator. Index Out Matrix Size
		/// <summary>
		/// Тестирование индексатора.
		/// Выброс исключения, если в индексаторе указываются индексы выходящие за пределы матрицы.
		/// </summary>
		/// <param name="rows">Количество строк.</param>
		/// <param name="columns">Количество столбцов.</param>
		/// <param name="rowIndex">Индекс строки.</param>
		/// <param name="columnIndex">Индекс столбца.</param>
		[Theory]
		[MemberData(nameof(MatrixIndexator_IndexOutMatrixSize_Data))]
		public void MatrixIndexator_IndexOutMatrixSize_ThrowException(int rows, int columns, int rowIndex, int columnIndex)
		{
			var matrix = new Matrix(rows, columns);
			double currentValue = -10;
			double newValue = 10;
			
			Action getterCode = () => currentValue = matrix[rowIndex, columnIndex];
			Action setterCode = () => matrix[rowIndex, columnIndex] = newValue;

			Assert.Throws<LinearAlgebraException>(getterCode);
			Assert.Throws<LinearAlgebraException>(setterCode);
		}

		/// <summary>
		/// Данные для тестового метода MatrixIndexator_IndexOutMatrixSize_ThrowException.
		/// </summary>
		public static IEnumerable<object[]> MatrixIndexator_IndexOutMatrixSize_Data =>
			new List<object[]>
			{
				new object[] { 10, 10, -1, 10 },
				new object[] { 10, 10, -1, 9 },
				new object[] { 10, 10, 10, 10 },
				new object[] { 10, 10, 10, 9 },
				new object[] { 10, 10, 0, -10 },
				new object[] { 10, 10, -1, -10 },
				new object[] { 10, 10, 0, 10 },
				new object[] { 0, 0, 0, 0 },
				new object[] { 0, 1, 0, 0 },
				new object[] { 1, 0, 0, 0 },
			};
		#endregion

		#region Matrix Constructor. Two Dimension Array/Matrix
		/// <summary>
		/// Тестирование конструктора.
		/// В параметрах двумерный массив.
		/// Количество строк и столбцов должно быть верным, как и сама матрица.
		/// </summary>
		/// <param name="expectedMatrix">Двумерная исходная матрица.</param>
		/// <param name="rows">Количество строк.</param>
		/// <param name="columns">Количество столбцов.</param>
		[Theory]
		[MemberData(nameof(MatrixConstructor_TwoDimensionArray_Data))]
		public void MatrixConstructor_Matrix_CorrectMatrix(double[,] expectedMatrix, int rows, int columns)
		{
			var matrix = new Matrix(expectedMatrix);
			var actualMatrix = new Matrix(matrix);

			Assert.Equal(rows, actualMatrix.Rows);
			Assert.Equal(columns, actualMatrix.Columns);
			Assert.Equal<double[,]>(expectedMatrix, actualMatrix.Items);
		}

		/// <summary>
		/// Тестирование конструктора.
		/// В параметрах двумерный массив.
		/// Количество строк и столбцов должно быть верным, как и сама матрица.
		/// </summary>
		/// <param name="expectedMatrix">двумерная исходная матрица.</param>
		/// <param name="rows">Количество сток.</param>
		/// <param name="columns">Количество столбцов.</param>
		[Theory]
		[MemberData(nameof(MatrixConstructor_TwoDimensionArray_Data))]
		public void MatrixConstructor_TwoDimensionArray_CreateMatrix(double[,] expectedMatrix, int rows, int columns)
		{
			var actualMatrix = new Matrix(expectedMatrix);

			Assert.Equal(rows, actualMatrix.Rows);
			Assert.Equal(columns, actualMatrix.Columns);
			Assert.Equal<double[,]>(expectedMatrix, actualMatrix.Items);
		}

		/// <summary>
		/// Данные для тестового метода MatrixConstructor_TwoDimensionArray_CreateMatrix.
		/// </summary>
		public static IEnumerable<object[]> MatrixConstructor_TwoDimensionArray_Data =>
			new List<object[]>
			{
				new object[]
				{
					new double[,]
					{
						{ 1 }
					},
					1, 1
				},
				new object[]
				{
					new double[,]
					{
						{ 1, -1, 2 },
						{ double.MaxValue, -99.0, double.MinValue }
					},
					2, 3
				},
				new object[]
				{
					new double[,]
					{
						{ 1 },
						{ double.NaN },
						{ double.NegativeInfinity },
						{ double.PositiveInfinity }
					},
					4, 1
				},
				new object[]
				{
					new double[,]
					{
						{ 1, 0 },
						{ -9, 3 },
						{ 7, 7 },
						{ -7, -7 },
						{ -125, 0.255 }
					},
					5, 2
				},
				new object[]
				{
					new double[,]
					{
						{ }
					},
					1, 0
				},
				new object[]
				{
					new double[,]
					{
						{ double.Epsilon, double.MaxValue, double.MinValue, double.NaN, double.NegativeInfinity, double.PositiveInfinity },
						{ -99.963, 87.852, -76.741, 65.63, -54.52, 43.41 }
					},
					2, 6
				},
				new object[]
				{
					new double[,]
					{

					},
					0, 0
				}
			};
		#endregion

		#region Matrix Constructor. Default Value 
		/// <summary>
		/// Тестирование конструктора.
		/// Указанным значением по умолчанию должны заполняться все элементы матрицы.
		/// </summary>
		/// <param name="rows">Количество сток.</param>
		/// <param name="columns">Количество колонок.</param>
		/// <param name="defaultValue">Значение по умолчанию.</param>
		[Theory]
		[MemberData(nameof(MatrixConstructor_DefaultValue_Data))]
		public void MatrixConstructor_DefaultValue_CreateMatrix(int rows, int columns, double defaultValue)
		{
			var matrix = new Matrix(rows, columns, defaultValue);

			for(int row = 0; row < rows; row++)
			{
				for(int column = 0; column < columns; column++)
				{
					Assert.Equal(defaultValue, matrix.Items[row, column]);
				}
			}
		}

		/// <summary>
		/// Данные для тестового метода MatrixConstructor_DefaultValue_CreateMatrix.
		/// </summary>
		public static IEnumerable<object[]> MatrixConstructor_DefaultValue_Data =>
			new List<object[]>
			{
				new object[] { 10, 10, double.Epsilon },
				new object[] { 10, 10, double.MaxValue },
				new object[] { 10, 10, double.MinValue },
				new object[] { 10, 10, double.NaN },
				new object[] { 10, 10, double.NegativeInfinity },
				new object[] { 10, 10, double.PositiveInfinity },
				new object[] { 10, 10, -100 },
				new object[] { 10, 10, -100.123 },
				new object[] { 10, 10, 0 },
				new object[] { 10, 10, 0.456 },
				new object[] { 10, 10, 100 },
				new object[] { 10, 10, 100.789 }
			};
		#endregion

		#region Matrix Constructor. Natural Size
		/// <summary>
		/// Тестирование конструктора.
		/// Корректное создание матрицы, если в размерах указаны только натуральные числа.
		/// </summary>
		/// <param name="rows">Количество строк.</param>
		/// <param name="columns">Количество столбцов.</param>
		[Theory]
		[MemberData(nameof(MatrixConstructor_NaturalSize_Data))]
		public void MatrixConstructor_NaturalSize_CreateMatrix(int rows, int columns)
		{
			var matrix = new Matrix(rows, columns);

			Assert.Equal(rows, matrix.Rows);
			Assert.Equal(columns, matrix.Columns);
		}

		/// <summary>
		/// Данные для тестового метода MatrixConstructor_NaturalSize_CreateMatrix.
		/// </summary>
		public static IEnumerable<object[]> MatrixConstructor_NaturalSize_Data =>
			new List<object[]>
			{
				new object[] { 100, 100},
				new object[] { 100, 0 },
				new object[] { 0, 100 },
			};
		#endregion

		#region Matrix Constructor. Negative Size
		/// <summary>
		/// Тестирование конструктора.
		/// Выброс исключения, если в параметрах в качестве количества строк или стобцов отрицательное число.
		/// </summary>
		/// <param name="rows">Количество строк.</param>
		/// <param name="columns">Количество столбцов.</param>
		[Theory]
		[MemberData(nameof(MatrixConstructor_NegativeSize_Data))]
		public void MatrixConstructor_NegativeSize_ThrowException(int rows, int columns)
		{
			Action testCode = () => new Matrix(rows, columns);

			Assert.Throws<LinearAlgebraException>(testCode);
		}

		/// <summary>
		/// Данные для тестового метода MatrixConstructor_NegativeSize_ThrowException.
		/// </summary>
		public static IEnumerable<object[]> MatrixConstructor_NegativeSize_Data =>
			new List<object[]>
			{
				new object[] { -100, 100 },
				new object[] { -75, 0 },
				new object[] { -50, -50 },
				new object[] { 0, -75 },
				new object[] { 100, -100 }
			};
		#endregion
	}
}
