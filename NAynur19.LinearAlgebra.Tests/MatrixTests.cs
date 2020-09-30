using System;
using System.Collections.Generic;

using Xunit;

namespace NAynur19.LinearAlgebra.Tests
{
	/// <summary>
	/// ����� ������������ �������.
	/// </summary>
	public class MatrixTests
	{

		#region Matrix Multiplication. Scalar
		/// <summary>
		/// ������������ ������ Multiplication.
		/// ��� ��������� ������� �� ������ �������� ������� ������ ���� ��������� ��������� � �������� �������� 10^(-12).
		/// </summary>
		/// <param name="matrix">�������� �������.</param>
		/// <param name="scalar">������.</param>
		/// <param name="expectedMatrix">��������� �������.</param>
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
		/// ������ ��� ��������� ������ Multiplication_SpecialScalar_CorrectWork.
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
		/// ������������ ������ Multiplication.
		/// ��� ��������� ������� �� ������ �������� ������� ������ ���� ��������� ��������� � �������� �������� 10^(-12).
		/// </summary>
		/// <param name="matrix">�������� �������.</param>
		/// <param name="scalar">������.</param>
		/// <param name="expectedMatrix">��������� �������.</param>
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
		/// ������ ��� ��������� ������ Multiplication_Scalar_CorrectWork.
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
		/// ������������ ������ IsEqual.
		/// ��� ��������� ������ ����������, ����� ����������� ��������� � �������� ��������� �������������� ���� ���� �����.
		/// Nan = Nan!
		/// </summary>
		/// <param name="matrix">������� ��������.</param>
		/// <param name="isEqualMatrix">�������, ������ ��������.</param>
		/// <param name="isNotEquaMatrix">������� �������� ��������.</param>
		[Theory]
		[MemberData(nameof(IsEqual_TwoDimensionArray_Data))]
		public void IsEqual_Matrix_Corre�tWork(double[,] matrix, double[,] isEqualMatrix, double[,] isNotEquaMatrix)
		{
			var actaulMatrix = new Matrix(matrix);

			Assert.False(actaulMatrix.IsEqual(new Matrix(isNotEquaMatrix)));
			Assert.True(actaulMatrix.IsEqual(new Matrix(isEqualMatrix)));
		}

		/// <summary>
		/// ������������ ������ IsEqual.
		/// ��� ��������� ������ ����������, ����� ����������� ��������� � �������� ��������� �������������� ���� ���� �����.
		/// Nan = Nan!
		/// </summary>
		/// <param name="matrix">������� ��������.</param>
		/// <param name="isEqualMatrix">�������, ������ ��������.</param>
		/// <param name="isNotEquaMatrix">������� �������� ��������.</param>
		[Theory]
		[MemberData(nameof(IsEqual_TwoDimensionArray_Data))]
		public void IsEqual_TwoDimensionArray_Corre�tWork(double[,] matrix, double[,] isEqualMatrix, double[,] isNotEquaMatrix)
		{
			var actualMatrix = new Matrix(matrix);

			Assert.False(actualMatrix.IsEqual(isNotEquaMatrix));
			Assert.True(actualMatrix.IsEqual(isEqualMatrix));
		}

		/// <summary>
		/// ������ ��� ��������� ������ IsEqual_TwoDimensionArray_Corre�tWork.
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
		/// ������������ ������ ToString.
		/// ���������� ����� � ����� �������.
		/// </summary>
		/// <param name="matrix">�������� �������.</param>
		/// <param name="matrixToString">��������� ������������� �������.</param>
		[Theory]
		[MemberData(nameof(ToString_Data))]
		public void ToString_CorrectToStringMatrix(double[,] matrix, string matrixToString)
		{
			var actualMatrix = new Matrix(matrix);

			Assert.Equal(matrixToString, actualMatrix.ToString());
		}

		/// <summary>
		/// ������ ��� ��������� ������ ToString_CorrectToStringMatrix.
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
		/// ������������ �����������.
		/// ������ ����������, ���� � ����������� ����������� ������� ��������� �� ������� �������.
		/// </summary>
		/// <param name="rows">���������� �����.</param>
		/// <param name="columns">���������� ��������.</param>
		/// <param name="rowIndex">������ ������.</param>
		/// <param name="columnIndex">������ �������.</param>
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
		/// ������ ��� ��������� ������ MatrixIndexator_IndexInMatrixSize_CorrectGetSet.
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
		/// ������������ �����������.
		/// ������ ����������, ���� � ����������� ����������� ������� ��������� �� ������� �������.
		/// </summary>
		/// <param name="rows">���������� �����.</param>
		/// <param name="columns">���������� ��������.</param>
		/// <param name="rowIndex">������ ������.</param>
		/// <param name="columnIndex">������ �������.</param>
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
		/// ������ ��� ��������� ������ MatrixIndexator_IndexOutMatrixSize_ThrowException.
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
		/// ������������ ������������.
		/// � ���������� ��������� ������.
		/// ���������� ����� � �������� ������ ���� ������, ��� � ���� �������.
		/// </summary>
		/// <param name="expectedMatrix">��������� �������� �������.</param>
		/// <param name="rows">���������� �����.</param>
		/// <param name="columns">���������� ��������.</param>
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
		/// ������������ ������������.
		/// � ���������� ��������� ������.
		/// ���������� ����� � �������� ������ ���� ������, ��� � ���� �������.
		/// </summary>
		/// <param name="expectedMatrix">��������� �������� �������.</param>
		/// <param name="rows">���������� ����.</param>
		/// <param name="columns">���������� ��������.</param>
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
		/// ������ ��� ��������� ������ MatrixConstructor_TwoDimensionArray_CreateMatrix.
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
		/// ������������ ������������.
		/// ��������� ��������� �� ��������� ������ ����������� ��� �������� �������.
		/// </summary>
		/// <param name="rows">���������� ����.</param>
		/// <param name="columns">���������� �������.</param>
		/// <param name="defaultValue">�������� �� ���������.</param>
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
		/// ������ ��� ��������� ������ MatrixConstructor_DefaultValue_CreateMatrix.
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
		/// ������������ ������������.
		/// ���������� �������� �������, ���� � �������� ������� ������ ����������� �����.
		/// </summary>
		/// <param name="rows">���������� �����.</param>
		/// <param name="columns">���������� ��������.</param>
		[Theory]
		[MemberData(nameof(MatrixConstructor_NaturalSize_Data))]
		public void MatrixConstructor_NaturalSize_CreateMatrix(int rows, int columns)
		{
			var matrix = new Matrix(rows, columns);

			Assert.Equal(rows, matrix.Rows);
			Assert.Equal(columns, matrix.Columns);
		}

		/// <summary>
		/// ������ ��� ��������� ������ MatrixConstructor_NaturalSize_CreateMatrix.
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
		/// ������������ ������������.
		/// ������ ����������, ���� � ���������� � �������� ���������� ����� ��� ������� ������������� �����.
		/// </summary>
		/// <param name="rows">���������� �����.</param>
		/// <param name="columns">���������� ��������.</param>
		[Theory]
		[MemberData(nameof(MatrixConstructor_NegativeSize_Data))]
		public void MatrixConstructor_NegativeSize_ThrowException(int rows, int columns)
		{
			Action testCode = () => new Matrix(rows, columns);

			Assert.Throws<LinearAlgebraException>(testCode);
		}

		/// <summary>
		/// ������ ��� ��������� ������ MatrixConstructor_NegativeSize_ThrowException.
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
