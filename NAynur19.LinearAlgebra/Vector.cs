using System;
using System.Collections.Generic;
using System.Linq;

namespace NAynur19.LinearAlgebra
{
	/// <summary>
	/// Вектор.
	/// </summary>
	public class Vector
	{
		#region Vector Properties
		/// <summary>
		/// Количество измерений вектора.
		/// </summary>
		public int Dimension { get; private set; }

		/// <summary>
		/// Элементы вектора.
		/// </summary>
		public double[] Items { get; private set; }
		#endregion

		#region Vector Constructor
		/// <summary>
		/// Конструктор.
		/// </summary>
		/// <param name="count">Размерность вектора.</param>
		/// <param name="defValue">Значение по умолчанию.</param>
		public Vector(int dimension, double defValue = 0.0)
		{
			SetDimension(dimension);
			Items = new double[Dimension];
			if(defValue != 0.0)
			{
				SetItems(defValue);
			}
		}

		/// <summary>
		/// Конструктор.
		/// </summary>
		/// <param name="vector">Исходный вектор.</param>
		public Vector(Vector vector)
		{
			SetDimension(vector.Dimension);
			Items = new double[Dimension];
			CopyFrom(vector);
		}

		/// <summary>
		/// Конструктор.
		/// </summary>
		/// <param name="vector">Исходный массив данных.</param>
		public Vector(double[] vector)
		{
			SetDimension(vector.Length);
			Items = new double[Dimension];
			CopyFrom(vector);
		}
		#endregion

		#region Vector Copy From
		/// <summary>
		/// Копирование вектора из другого вектора.
		/// </summary>
		/// <param name="vector">Вектор, из которого копируются значения.</param>
		public void CopyFrom(Vector vector)
		{
			this.CopyFrom(vector.Items);
		}

		/// <summary>
		/// Копирование вектора из массива.
		/// </summary>
		/// <param name="vector">Массив (вектор), с которого копируются значения.</param>
		public void CopyFrom(double[] vector)
		{
			SizeIsEqual(this.Items, vector);

			for(int index = 0; index < Dimension; index++)
			{
				this[index] = vector[index];
			}
		}
		#endregion

		#region Vector Indexator
		/// <summary>
		/// Индексатор вектора.
		/// </summary>
		/// <param name="index">Индекс элемента вектора.</param>
		/// <returns>Возвращает элемент вектора по индексу.</returns>
		public double this[int index]
		{
			get
			{
				IndexIsValid(index);
				
				return Items[index];
			}
			set
			{
				if(IndexIsValid(index))
				{
					Items[index] = value;
				}
			}
		}
		#endregion

		#region Vector Get Middle Item
		/// <summary>
		/// Получение элемента вектора, находящегося в середине.
		/// </summary>
		/// <returns>Возвращает кортеж - индекс и элемент вектора, находящийся в середине.</returns>
		public (int, double) GetMiddleItem()
		{
			return GetMiddleItem(this);
		}

		/// <summary>
		/// Получение элемента вектора, находящегося в середине.
		/// </summary>
		/// <param name="vector">Вектор данных.</param>
		/// <returns>Возвращает кортеж - индекс и элемент вектора, находящийся в середине.</returns>
		public static (int, double) GetMiddleItem(Vector vector)
		{
			return GetMiddleItem(vector.Items);
		}

		/// <summary>
		/// Получение элемента вектора, находящегося в середине.
		/// </summary>
		/// <typeparam name="T">Универсальный тип данных.</typeparam>
		/// <param name="vector">Вектор данных.</param>
		/// <returns>Возвращает кортеж - индекс и элемент вектора, находящийся в середине.</returns>
		public static (int, T) GetMiddleItem<T>(T[] vector)
		{
			var middle = (int)vector.Length / 2;
			return (middle, vector[middle]);
		}
		#endregion

		#region Vector Get Subvector
		/// <summary>
		/// Получение подвектора данных из текущего вектора.
		/// </summary>
		/// <param name="start">Начальный индекс подвектора.</param>
		/// <param name="end">Конечный индекс подвектора.</param>
		/// <returns>Возварщает новый вектор данных - подвектор исходного.</returns>
		public Vector GetSubvector(int start, int end)
		{
			return GetSubvector(this, start, end);
		}

		/// <summary>
		/// Получение подвектора данных из текущего вектора.
		/// </summary>
		/// <param name="vector">Вектор данных, из которого нужно получить подвектор.</param>
		/// <param name="start">Начальный индекс подвектора.</param>
		/// <param name="end">Конечный индекс подвектора.</param>
		/// <returns>Возварщает новый вектор данных - подвектор исходного.</returns>
		public static Vector GetSubvector(Vector vector, int start, int end)
		{
			var subvector = GetSubvector(vector.Items, start, end);
			return new Vector(subvector);
		}

		/// <summary>
		/// Получение подвектора данных из текущего вектора.
		/// </summary>
		/// <typeparam name="T">Универсальный тип данных.</typeparam>
		/// <param name="vector">Вектор данных, из которого нужно получить подвектор.</param>
		/// <param name="start">Начальный индекс подвектора.</param>
		/// <param name="end">Конечный индекс подвектора.</param>
		/// <returns>Возварщает новый вектор данных - подвектор исходного.</returns>
		public static T[] GetSubvector<T>(T[] vector, int start, int end)
		{
			StartEndIsValid(vector, start, end);

			var result = new T[end - start + 1];
			for(int i = 0; i < result.Length; i++)
			{
				result[i] = vector[start + i];
			}

			return result;
		}
		#endregion

		#region Vector Get Subvector Large Items
		/// <summary>
		/// Получение подвектора данных из текущего вектора.
		/// Подвектор данных должен содержать элементы, значения которых больше (или равно) указанного элемента.
		/// </summary>
		/// <param name="item">Нижняя граница значений подвектора.</param>
		/// <param name="itemIsInclude">Указатель того, что указанное значение нужно включить в подвектор.</param>
		/// <returns>Возварщает новый вектор данных - подвектор исходного.</returns>
		public Vector GetSubvectorLargeItems(double item, bool itemIsInclude = true)
		{
			return GetSubvectorLargeItems(this, item, itemIsInclude);
		}

		/// <summary>
		/// Получение подвектора данных из текущего вектора.
		/// Подвектор данных должен содержать элементы, значения которых больше (или равно) указанного элемента.
		/// </summary>
		/// <param name="vector">Вектор данных, из которого нужно получить подвектор.</param>
		/// <param name="item">Нижняя граница значений подвектора.</param>
		/// <param name="itemIsInclude">Указатель того, что указанное значение нужно включить в подвектор.</param>
		/// <returns>Возварщает новый вектор данных - подвектор исходного.</returns>
		public static Vector GetSubvectorLargeItems(Vector vector, double item, bool itemIsInclude = true)
		{
			var subvector = GetSubvectorLargeItems(vector.Items, item, itemIsInclude);
			return new Vector(subvector);
		}

		/// <summary>
		/// Получение подвектора данных из текущего вектора.
		/// Подвектор данных должен содержать элементы, значения которых больше (или равно) указанного элемента.
		/// </summary>
		/// <typeparam name="T">Тип данных, реализующий интерфейс IComparable.</typeparam>
		/// <param name="vector">Вектор данных, из которого нужно получить подвектор.</param>
		/// <param name="item">Нижняя граница значений подвектора.</param>
		/// <param name="itemIsInclude">Указатель того, что указанное значение нужно включить в подвектор.</param>
		/// <returns>Возварщает новый вектор данных - подвектор исходного.</returns>
		public static T[] GetSubvectorLargeItems<T>(T[] vector, T item, bool itemIsInclude = true) where T : IComparable
		{
			var lessItems = new List<T>();

			if(itemIsInclude)
			{
				for(int i = 0; i < vector.Length; i++)
				{
					if(item.CompareTo(vector[i]) <= 0)
					{
						lessItems.Add(vector[i]);
					}
				}
			}
			else
			{
				for(int i = 0; i < vector.Length; i++)
				{
					if(item.CompareTo(vector[i]) < 0)
					{
						lessItems.Add(vector[i]);
					}
				}
			}

			return lessItems.ToArray();
		}

		/// <summary>
		/// Получение подвектора данных из текущего вектора.
		/// Подвектор данных должен содержать элементы, значения которых больше (или равно) указанного элемента.
		/// </summary>
		/// <param name="item">Кортеж состоящий из индекса и элемента - нижней границы подвектора.</param>
		/// <param name="itemIsInclude">Указатель того, что указанное значение нужно включить в подвектор.</param>
		/// <returns>Возварщает новый вектор данных - подвектор исходного.</returns>
		public Vector GetSubvectorLargeItems((int, double) item, bool itemIsInclude = true)
		{
			return GetSubvectorLargeItems(this, item, itemIsInclude);
		}

		/// <summary>
		/// Получение подвектора данных из текущего вектора.
		/// Подвектор данных должен содержать элементы, значения которых больше (или равно) указанного элемента.
		/// </summary>
		/// <param name="vector">Вектор данных, из которого нужно получить подвектор.</param>
		/// <param name="item">Кортеж, состоящий из индекса и элемента - нижней границы подвектора.</param>
		/// <param name="itemIsInclude">Указатель того, что указанное значение нужно включить в подвектор.</param>
		/// <returns>Возварщает новый вектор данных - подвектор исходного.</returns>
		public static Vector GetSubvectorLargeItems(Vector vector, (int, double) item, bool itemIsInclude = true)
		{
			var subvector = GetSubvectorLargeItems(vector.Items, item, itemIsInclude);
			return new Vector(subvector);
		}

		/// <summary>
		/// Получение подвектора данных из текущего вектора.
		/// Подвектор данных должен содержать элементы, значения которых больше (или равно) указанного элемента.
		/// </summary>
		/// <typeparam name="T">Тип данных, реализующий интерфейс IComparable.</typeparam>
		/// <param name="vector">Вектор данных, из которого нужно получить подвектор.</param>
		/// <param name="item">Кортеж, состоящий из индекса и элемента - нижней границы подвектора.</param>
		/// <param name="itemIsInclude">Указатель того, что указанное значение нужно включить в подвектор.</param>
		/// <returns>Возварщает новый вектор данных - подвектор исходного.</returns>
		public static T[] GetSubvectorLargeItems<T>(T[] vector, (int, T) item, bool itemIsInclude = true) where T : IComparable
		{
			IndexIsValid(vector, item.Item1);

			var largeItems = new List<T>();

			if(itemIsInclude)
			{
				for(int i = 0; i < vector.Length; i++)
				{
					if(item.Item2.CompareTo(vector[i]) <= 0)
					{
						largeItems.Add(vector[i]);
					}
				}
			}
			else
			{
				for(int i = 0; i < vector.Length; i++)
				{
					if(item.Item2.CompareTo(vector[i]) <= 0 && item.Item1 != i)
					{
						largeItems.Add(vector[i]);
					}
				}
			}

			return largeItems.ToArray();
		}
		#endregion

		#region Vector Get Subvector Less Items
		/// <summary>
		/// Получение подвектора данных из текущего вектора.
		/// Подвектор данных должен содержать элементы, значения которых меньше (или равно) указанного элемента.
		/// </summary>
		/// <param name="item">Верхняя граница значений подвектора.</param>
		/// <param name="itemIsInclude">Указатель того, что указанное значение нужно включить в подвектор.</param>
		/// <returns>Возварщает новый вектор данных - подвектор исходного.</returns>
		public Vector GetSubvectorLessItems(double item, bool itemIsInclude = true)
		{
			return GetSubvectorLessItems(this, item, itemIsInclude);
		}

		/// <summary>
		/// Получение подвектора данных из текущего вектора.
		/// Подвектор данных должен содержать элементы, значения которых меньше (или равно) указанного элемента.
		/// </summary>
		/// <param name="vector">Вектор данных, из которого нужно получить подвектор.</param>
		/// <param name="item">Верхняя граница значений подвектора.</param>
		/// <param name="itemIsInclude">Указатель того, что указанное значение нужно включить в подвектор.</param>
		/// <returns>Возварщает новый вектор данных - подвектор исходного.</returns>
		public static Vector GetSubvectorLessItems(Vector vector, double item, bool itemIsInclude = true)
		{
			var subvector = GetSubvectorLessItems(vector.Items, item, itemIsInclude);
			return new Vector(subvector);
		}

		/// <summary>
		/// Получение подвектора данных из текущего вектора.
		/// Подвектор данных должен содержать элементы, значения которых меньше (или равно) указанного элемента.
		/// </summary>
		/// <typeparam name="T">Тип данных, реализующий интерфейс IComparable.</typeparam>
		/// <param name="vector">Вектор данных, из которого нужно получить подвектор.</param>
		/// <param name="item">Верхняя граница значений подвектора.</param>
		/// <param name="itemIsInclude">Указатель того, что указанное значение нужно включить в подвектор.</param>
		/// <returns>Возварщает новый вектор данных - подвектор исходного.</returns>
		public static T[] GetSubvectorLessItems<T>(T[] vector, T item, bool itemIsInclude = true) where T : IComparable
		{
			var lessItems = new List<T>();

			if(itemIsInclude)
			{
				for(int i = 0; i < vector.Length; i++)
				{
					if(item.CompareTo(vector[i]) >= 0)
					{
						lessItems.Add(vector[i]);
					}
				}
			}
			else
			{
				for(int i = 0; i < vector.Length; i++)
				{
					if(item.CompareTo(vector[i]) > 0)
					{
						lessItems.Add(vector[i]);
					}
				}
			}

			return lessItems.ToArray();
		}

		/// <summary>
		/// Получение подвектора данных из текущего вектора.
		/// Подвектор данных должен содержать элементы, значения которых меньше (или равно) указанного элемента.
		/// </summary>
		/// <param name="item">Кортеж, состоящий из индекса и элемента - верхней границы подвектора.</param>
		/// <param name="itemIsInclude">Указатель того, что указанный элемент нужно включить в подвектор.</param>
		/// <returns>Возварщает новый вектор данных - подвектор исходного.</returns>
		public Vector GetSubvectorLessItems((int, double) item, bool itemIsInclude = true)
		{
			return GetSubvectorLessItems(this, item, itemIsInclude);
		}

		/// <summary>
		/// Получение подвектора данных из текущего вектора.
		/// Подвектор данных должен содержать элементы, значения которых меньше (или равно) указанного элемента.
		/// </summary>
		/// <param name="vector">Вектор данных, из которого нужно получить подвектор.</param>
		/// <param name="item">Кортеж, состоящий из индекса и элемента - верхней границы подвектора.</param>
		/// <param name="itemIsInclude">Указатель того, что указанный элемент нужно включить в подвектор.</param>
		/// <returns>Возварщает новый вектор данных - подвектор исходного.</returns>
		public static Vector GetSubvectorLessItems(Vector vector, (int, double) item, bool itemIsInclude = true)
		{
			var subvector = GetSubvectorLessItems(vector.Items, item, itemIsInclude);
			return new Vector(subvector);
		}

		/// <summary>
		/// Получение подвектора данных из текущего вектора.
		/// Подвектор данных должен содержать элементы, значения которых меньше (или равно) указанного элемента.
		/// </summary>
		/// <typeparam name="T">Тип данных, реализующий интерфейс IComparable.</typeparam>
		/// <param name="vector">Вектор данных, из которого нужно получить подвектор.</param>
		/// <param name="item">Кортеж, состоящий из индекса и элемента - верхней границы подвектора.</param>
		/// <param name="itemIsInclude">Указатель того, что указанный элемент нужно включить в подвектор.</param>
		/// <returns>Возварщает новый вектор данных - подвектор исходного.</returns>
		public static T[] GetSubvectorLessItems<T>(T[] vector, (int, T) item, bool itemIsInclude = true) where T : IComparable
		{
			IndexIsValid(vector, item.Item1);
			
			var lessItems = new List<T>();

			if(itemIsInclude)
			{
				for(int i = 0; i < vector.Length; i++)
				{
					if(item.Item2.CompareTo(vector[i]) >= 0)
					{
						lessItems.Add(vector[i]);
					}
				}
			}
			else
			{
				for(int i = 0; i < vector.Length; i++)
				{
					if(item.Item2.CompareTo(vector[i]) >= 0 && item.Item1 != i)
					{
						lessItems.Add(vector[i]);
					}
				}
			}

			return lessItems.ToArray();
		}
		#endregion

		#region Vector Get Maximum Item
		/// <summary>
		/// Получение максимального значения и индекса данного элемента вектора.
		/// </summary>
		/// <returns>Возвращает кортеж: индекс и максимальное значение элемента текущего вектора.</returns>
		public (int, double) GetMax()
		{
			return GetMax(this);
		}

		/// <summary>
		/// Получение максимального значения и индекса данного элемента вектора.
		/// </summary>
		/// <param name="vector">Вектор данных.</param>
		/// <returns>Возвращает кортеж: индекс и максимальное значение элемента вектора.</returns>
		public static (int, double) GetMax(Vector vector)
		{
			return GetMax(vector.Items);
		}

		/// <summary>
		/// Получение максимального значения и индекса данного элемента вектора.
		/// </summary>
		/// <typeparam name="T">Тип данных, реализующий интерфейс IComparable.</typeparam>
		/// <param name="vector">Вектор данных.</param>
		/// <returns>Возвращает кортеж: индекс и максимальное значение элемента вектора.</returns>
		public static (int, T) GetMax<T>(T[] vector) where T : IComparable
		{
			return GetMax(vector, 0, vector.Length - 1);
		}

		/// <summary>
		/// Получение максимального значения и индекса данного элемента вектора из диапазона значений.
		/// </summary>
		/// <typeparam name="T">Тип данных, реализующий интерфейс IComparable.</typeparam>
		/// <param name="vector">Вектор данных.</param>
		/// <param name="start">Начальный индекс.</param>
		/// <param name="end">Конечный индекс.</param>
		/// <returns>Возвращает кортеж: индекс и максимальное значение элемента вектора.</returns>
		public static (int, T) GetMax<T>(T[] vector, int start, int end) where T : IComparable
		{
			StartEndIsValid(vector, start, end);

			(int, T) max = (start, vector[start]);
			for(int i = start; i <= end; i++)
			{
				if(max.Item2.CompareTo(vector[i]) < 0)
				{
					max = (i, vector[i]);
				}
			}

			return max;
		}
		#endregion

		#region Vector Get Minimum Item
		/// <summary>
		/// Получение минимального значения и индекса данного элемента вектора.
		/// </summary>
		/// <returns>Возвращает кортеж: индекс и минимальное значение элемента текущего вектора.</returns>
		public (int, double) GetMin()
		{
			return GetMin(this);
		}

		/// <summary>
		/// Получение минимального значения и индекса данного элемента вектора.
		/// </summary>
		/// <param name="vector">Вектор данных.</param>
		/// <returns>Возвращает кортеж: индекс и минимальное значение элемента вектора.</returns>
		public static (int, double) GetMin(Vector vector)
		{
			return GetMin(vector.Items);
		}

		/// <summary>
		/// Получение минимального значения и индекса данного элемента вектора.
		/// </summary>
		/// <typeparam name="T">Тип данных, реализующий интерфейс IComparable.</typeparam>
		/// <param name="vector">Вектор данных.</param>
		/// <returns>Возвращает кортеж: индекс и минимальное значение элемента вектора.</returns>
		public static (int, T) GetMin<T>(T[] vector) where T : IComparable
		{
			return GetMin(vector, 0, vector.Length - 1);
		}

		/// <summary>
		/// Получение минимального значения и индекса данного элемента вектора из диапазона значений.
		/// </summary>
		/// <typeparam name="T">Тип данных, реализующий интерфейс IComparable.</typeparam>
		/// <param name="vector">Вектор данных.</param>
		/// <param name="start">Начальный индекс.</param>
		/// <param name="end">Конечный индекс.</param>
		/// <returns>Возвращает кортеж: индекс и минимальное значение элемента вектора.</returns>
		public static (int, T) GetMin<T>(T[] vector, int start, int end) where T : IComparable
		{
			StartEndIsValid(vector, start, end);

			(int, T) min = (start, vector[start]);
			for(int i = start; i <= end; i++)
			{
				if(min.Item2.CompareTo(vector[i]) > 0)
				{
					min = (i, vector[i]);
				}
			}

			return min;
		}
		#endregion

		#region Vector Set Dimension
		/// <summary>
		/// Установка размерности вектора.
		/// </summary>
		/// <param name="dimension">Размерность вектора.</param>
		private void SetDimension(int dimension)
		{
			if(dimension < 1)
			{
				throw new LinearAlgebraException(LinearAlgebraExceptionMessage.VectorSizeLessOneException);
			}
			
			Dimension = dimension;
		}
		#endregion

		#region Vector Set Items
		/// <summary>
		/// Заполнение вектора одним значением.
		/// </summary>
		/// <param name="item"></param>
		public void SetItems(double item)
		{
			SetItems(this, item, 0, Dimension - 1);
		}

		/// <summary>
		/// Заполнение вектора одним значением для указанного диапазона.
		/// </summary>
		/// <param name="item">Заполняемое значение.</param>
		/// <param name="start">Начальный индекс заполнения.</param>
		/// <param name="end">Конечный индекс заполнения.</param>
		public void SetItems(double item, int start, int end)
		{
			SetItems(this, item, start, end);
		}

		/// <summary>
		/// Заполнение вектора одним значением для указанного диапазона.
		/// </summary>
		/// <param name="vector">Вектор данных.</param>
		/// <param name="item">Заполняемое значение.</param>
		/// <param name="start">Начальный индекс заполнения.</param>
		/// <param name="end">Конечный индекс заполнения.</param>
		public static void SetItems(Vector vector, double item, int start, int end)
		{
			SetItem(vector.Items, item, start, end);
		}

		/// <summary>
		/// Заполнение вектора одним значением для указанного диапазона.
		/// </summary>
		/// <typeparam name="T">Универсальный тип данных.</typeparam>
		/// <param name="vector">Вектор данных.</param>
		/// <param name="item">Заполняемое значение.</param>
		/// <param name="start">Начальный индекс заполнения.</param>
		/// <param name="end">Конечный индекс заполнения.</param>
		public static void SetItem<T>(T[] vector, T item, int start, int end)
		{
			StartEndIsValid(vector, start, end);

			for(int i = start; i <= end; i++)
			{
				vector[i] = item;
			}
		}
		#endregion

		#region Vectors Concatination
		/// <summary>
		/// Конкатенация векторов.
		/// </summary>
		/// <param name="vectors">Массив векторов.</param>
		/// <returns>Возвращает вектор - результат конкатенации векторов.</returns>
		public static Vector VectorsConcatination(Vector[] vectors)
		{
			var result = vectors.Select(v => v.Items).ToArray();
			return new Vector(VectorsConcatination(result));
		}

		/// <summary>
		/// Конкатенация векторов.
		/// </summary>
		/// <typeparam name="T">Универсальный тип данных.</typeparam>
		/// <param name="vectors">Массив векторов.</param>
		/// <returns>Возвращает вектор - результат конкатенации векторов.</returns>
		public static T[] VectorsConcatination<T>(params T[][] vectors)  
		{
			var count = 0;
			for(int i = 0; i < vectors.GetLength(0); i++)
			{
				count += vectors[i].Length;
			}

			var result = new T[count];
			var index = 0;
			
			for(int i = 0; i < vectors.GetLength(0); i++)
			{
				for(int j = 0; j < vectors[i].Length; j++)
				{
					result[index] = vectors[i][j];
					index++;
				}
			}

			return result;
		}
		#endregion

		#region Vector Items Shift To Left
		/// <summary>
		/// Сдвиг элементов вектора влево.
		/// </summary>
		/// <param name="item">Элемент, заполняемый на место сдвинутого элемента.</param>
		/// <param name="start">Начальный индекс сдвигаемого элемента.</param>
		public void ShiftItemsToLeft(double item, int start)
		{
			ShiftItemsToLeft(this, item, start);
		}

		/// <summary>
		/// Сдвиг элементов вектора влево.
		/// </summary>
		/// <param name="vector">Вектора данных.</param>
		/// <param name="item">Элемент, заполняемый на место сдвинутого элемента.</param>
		/// <param name="start">Начальный индекс сдвигаемого элемента.</param>
		public static void ShiftItemsToLeft(Vector vector, double item, int start)
		{
			ShiftItemsToLeft(vector.Items, item, start);
		}

		/// <summary>
		/// Сдвиг элементов вектора влево.
		/// </summary>
		/// <typeparam name="T">Универсальный тип данных.</typeparam>
		/// <param name="vector">Вектора данных.</param>
		/// <param name="item">Элемент, заполняемый на место сдвинутого элемента.</param>
		/// <param name="start">Начальный индекс сдвигаемого элемента.</param>
		public static void ShiftItemsToLeft<T>(T[] vector, T item, int start)
		{
			IndexIsValid(vector, start);

			if(start == 0)
			{
				return;
			}

			for(int i = start; i < vector.Length; i++)
			{
				vector[i - 1] = vector[i];
			}

			vector[^1] = item;
		}
		#endregion

		#region Vector Index Validation
		/// <summary>
		/// Проверка индекса вектора.
		/// </summary>
		/// <param name="index">Проверяемый индекс.</param>
		/// <returns>Возвращает результат проверки индекса вектора.</returns>
		public bool IndexIsValid(int index)
		{
			return IndexIsValid(this, index);
		}

		/// <summary>
		/// Проверка индекса вектора.
		/// </summary>
		/// <param name="vector">Вектор данных.</param>
		/// <param name="index">Проверяемый индекс.</param>
		/// <returns>Возвращает результат проверки индекса вектора.</returns>
		public static bool IndexIsValid(Vector vector, int index)
		{
			return IndexIsValid(vector.Items, index);
		}

		/// <summary>
		/// Проверка индекса вектора.
		/// </summary>
		/// <typeparam name="T">Универсальный тип данных.</typeparam>
		/// <param name="vector">Вектор данных.</param>
		/// <param name="index">Проверяемый индекс.</param>
		/// <returns>Возвращает результат проверки индекса вектора.</returns>
		public static bool IndexIsValid<T>(T[] vector, int index)
		{
			if(index < 0 && index >= vector.Length)
			{
				throw new LinearAlgebraException(LinearAlgebraExceptionMessage.IndexOutOfVectorSizeException);
			}

			return true;
		}
		#endregion

		#region Vector Start and End Index Validation
		/// <summary>
		/// Проверка индексов вектора.
		/// </summary>
		/// <param name="vector">Вектор данных.</param>
		/// <param name="start">Начальный индекс.</param>
		/// <param name="end">Конечный индекс.</param>
		/// <returns>Возвращает результат проверки индексов вектора.</returns>
		public bool StartEndIsValid(int start, int end)
		{
			return StartEndIsValid(this, start, end);
		}

		/// <summary>
		/// Проверка индексов вектора.
		/// </summary>
		/// <param name="vector">Вектор данных.</param>
		/// <param name="start">Начальный индекс.</param>
		/// <param name="end">Конечный индекс.</param>
		/// <returns>Возвращает результат проверки индексов вектора.</returns>
		public static bool StartEndIsValid(Vector vector, int start, int end)
		{
			return StartEndIsValid(vector.Items, start, end);
		}

		/// <summary>
		/// Проверка индексов вектора.
		/// </summary>
		/// <typeparam name="T">Универсальный тип данных.</typeparam>
		/// <param name="vector">Вектор данных.</param>
		/// <param name="start">Начальный индекс.</param>
		/// <param name="end">Конечный индекс.</param>
		/// <returns>Возвращает результат проверки индексов вектора.</returns>
		public static bool StartEndIsValid<T>(T[] vector, int start, int end)
		{
			if(start < 0 || start >= vector.Length)
			{
				throw new LinearAlgebraException(LinearAlgebraExceptionMessage.StartIndexOutOfVectorSizeException);
			}

			if(end < 0 || end >= vector.Length)
			{
				throw new LinearAlgebraException(LinearAlgebraExceptionMessage.EndIndexOutOfVectorSizeException);
			}

			if(start > end)
			{
				throw new LinearAlgebraException(LinearAlgebraExceptionMessage.EndIndexLessStartIndexOfVectorException);
			}

			return true;
		}
		#endregion

		#region Vectors Size Is Equal
		/// <summary>
		/// Проверка равенства размерностей векторов.
		/// </summary>
		/// <param name="vector">Вектор данных.</param>
		/// <returns>Возвращает истину если размерности совпадают, иначе выбрасывает исключение.</returns>
		public bool SizeIsEqual(Vector vector)
		{
			return SizeIsEqual(this, vector);
		}

		/// <summary>
		/// Проверка равенства размерностей векторов.
		/// </summary>
		/// <param name="vector1">Вектор данных 1.</param>
		/// <param name="vector2">Вектор данных 2.</param>
		/// <returns>Возвращает истину если размерности совпадают, иначе выбрасывает исключение.</returns>
		public static bool SizeIsEqual(Vector vector1, Vector vector2)
		{
			return SizeIsEqual(vector1.Items, vector1.Items);
		}

		/// <summary>
		/// Проверка равенства размерностей векторов.
		/// </summary>
		/// <typeparam name="T">Универсальный тип данных.</typeparam>
		/// <param name="vector1">Вектор данных 1.</param>
		/// <param name="vector2">Вектор данных 2.</param>
		/// <returns>Возвращает истину если размерности совпадают, иначе выбрасывает исключение.</returns>
		public static bool SizeIsEqual<T>(T[] vector1, T[] vector2)
		{
			if(vector1.Length != vector2.Length)
			{
				throw new LinearAlgebraException(LinearAlgebraExceptionMessage.VectorSizeNonEqualException);
			}

			return true;
		}
		#endregion

		#region Vector Addation
		/// <summary>
		/// Сложение векторов. Изменяется текущий вектор.
		/// </summary>
		/// <param name="vector">Вектор данных.</param>
		/// <returns>Возвращает текущий вектор - результат сложения векторов.</returns>
		public Vector Addation(Vector vector)
		{
			return Addation(vector.Items);
		}

		/// <summary>
		/// Сложение векторов. Изменяется текущий вектор.
		/// </summary>
		/// <param name="vector">Вектор данных.</param>
		/// <returns>Возвращает текущий вектор - результат сложения векторов.</returns>
		public Vector Addation(double[] vector)
		{
			SizeIsEqual(this.Items, vector);

			for(int i = 0; i < Dimension; i++)
			{
				this[i] += vector[i];
			}

			return this;
		}

		/// <summary>
		/// Сложение 2-х векторов.
		/// </summary>
		/// <param name="vector1">Вектор данных 1.</param>
		/// <param name="vector2">Вектор данных 2.</param>
		/// <returns>Возвращает новый вектор - результат сложения векторов.</returns>
		public static Vector Addation(Vector vector1, Vector vector2)
		{
			return Addation(vector1.Items, vector2.Items);
		}

		/// <summary>
		/// Сложение 2-х векторов.
		/// </summary>
		/// <param name="vector1">Вектор данных 1.</param>
		/// <param name="vector2">Вектор данных 2.</param>
		/// <returns>Возвращает новый вектор - результат сложения векторов.</returns>
		public static Vector Addation(double[] vector1, double[] vector2)
		{
			SizeIsEqual(vector1, vector2);

			var result = new Vector(vector1.Length);
			for(int i = 0; i < result.Dimension; i++)
			{
				result[i] = vector1[i] + vector2[2];
			}

			return result;
		}
		#endregion

		#region Vector Miltiplication By Number
		/// <summary>
		/// Умножение вектора на скаляр. Изменяется текущий вектор.
		/// </summary>
		/// <param name="scalar">Скаляр</param>
		/// <returns>Возвращает текущий вектор - результат умножения на скаляр.</returns>
		public Vector MultiplicationByNumber(double scalar)
		{
			for(int i = 0; i < Dimension; i++)
			{
				this[i] *= scalar;
			}

			return this;
		}

		/// <summary>
		/// Умножения вектора на скаляр.
		/// </summary>
		/// <param name="vector">Вектор данных.</param>
		/// <param name="scalar">Скаляр.</param>
		/// <returns>Возвращает вектор - результат умножения на скаляр.</returns>
		public static Vector MultiplicationByNumber(Vector vector, double scalar)
		{
			return MultiplicationByNumber(vector.Items, scalar);
		}

		/// <summary>
		/// Умножения вектора на скаляр.
		/// </summary>
		/// <param name="vector">Вектор данных.</param>
		/// <param name="scalar">Скаляр.</param>
		/// <returns>Возвращает новый вектор - результат умножения на скаляр.</returns>
		public static Vector MultiplicationByNumber(double[] vector, double scalar)
		{
			var result = new Vector(vector.Length);

			for(int i = 0; i < result.Dimension; i++)
			{
				result[i] = vector[i] * scalar;
			}

			return result;
		}
		#endregion

		#region Vector Scalar Multiplication
		/// <summary>
		/// Скалярное произведение векторов.
		/// </summary>
		/// <param name="vector">Вектор данных.</param>
		/// <returns>Возвращает результат скалярного произведения векторов.</returns>
		public double ScalarMultiplication(Vector vector)
		{
			return ScalarMultiplication(this, vector);
		}

		/// <summary>
		/// Скалярное произведение векторов.
		/// </summary>
		/// <param name="vector">Вектор данных.</param>
		/// <returns>Возвращает результат скалярного произведения векторов.</returns>
		public double ScalarMultiplication(double[] vector)
		{
			return ScalarMultiplication(this.Items, vector);
		}

		/// <summary>
		/// Скалярное произведение векторов.
		/// </summary>
		/// <param name="vector1">Вектор данных 1.</param>
		/// <param name="vector2">Вектор данных 2.</param>
		/// <returns>Возвращает результат скалярного произведения векторов.</returns>
		public static double ScalarMultiplication(Vector vector1, Vector vector2)
		{
			return ScalarMultiplication(vector1.Items, vector2.Items);
		}

		/// <summary>
		/// Скалярное произведение векторов.
		/// </summary>
		/// <param name="vector1">Вектор данных 1.</param>
		/// <param name="vector2">Вектор данных 2.</param>
		/// <returns>Возвращает результат скалярного произведения векторов.</returns>
		public static double ScalarMultiplication(double[] vector1, double[] vector2)
		{
			SizeIsEqual(vector1, vector2);

			var result = 0.0;
			for(int i = 0; i < vector1.Length; i++)
			{
				result += vector1[i] * vector2[i];
			}

			return result;
		}
		#endregion

		#region Vector Subtracting
		/// <summary>
		/// Разность векторов. Изменяется текущий вектор.
		/// </summary>
		/// <param name="vector">Вектор данных.</param>
		/// <returns>Возвращает текущий вектор - результат разности векторов.</returns>
		public Vector Subtracting(Vector vector)
		{
			return Subtracting(vector.Items);
		}

		/// <summary>
		/// Разность векторов. Изменяется текущий вектор.
		/// </summary>
		/// <param name="vector">Вектор данных.</param>
		/// <returns>Возвращает текущий вектор - результат разности векторов.</returns>
		public Vector Subtracting(double[] vector)
		{
			SizeIsEqual(this.Items, vector);

			for(int i = 0; i < Dimension; i++)
			{
				this[i] -= vector[i];
			}

			return this;
		}

		/// <summary>
		/// Разность 2-х векторов.
		/// </summary>
		/// <param name="vector1">Вектор данных 1.</param>
		/// <param name="vector2">Вектор данных 2.</param>
		/// <returns>Возвращает новый вектор - результат разности 2-х векторов.</returns>
		public static Vector Subtracting(Vector vector1, Vector vector2)
		{
			return Subtracting(vector1.Items, vector2.Items);
		}

		/// <summary>
		/// Разность 2-х векторов.
		/// </summary>
		/// <param name="vector1">Вектор данных 1.</param>
		/// <param name="vector2">Вектор данных 2.</param>
		/// <returns>Возвращает новый вектор - результат разности 2-х векторов.</returns>
		public static Vector Subtracting(double[] vector1, double[] vector2)
		{
			SizeIsEqual(vector1, vector2);

			var result = new Vector(vector1.Length);
			for(int i = 0; i < result.Dimension; i++)
			{
				result[i] = vector1[i] - vector2[i];
			}

			return result;
		}
		#endregion
	}
}
