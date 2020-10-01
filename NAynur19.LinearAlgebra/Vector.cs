﻿using System;

namespace NAynur19.LinearAlgebra
{
	/// <summary>
	/// Вектор.
	/// </summary>
	public class Vector
	{
		#region Properties
		/// <summary>
		/// Количество измерений вектора.
		/// </summary>
		public int Dimension { get; private set; }

		/// <summary>
		/// Координаты вектора.
		/// </summary>
		public double[] Items { get; private set; }
		#endregion

		#region Constructor
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
		/// <param name="vector">Одномерный массив.</param>
		public Vector(double[] vector)
		{
			SetDimension(vector.Length);
			Items = new double[Dimension];
			CopyFrom(vector);
		}
		#endregion

		#region Copy Vector From
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
			if(vector.Length == Dimension)
			{
				for(int index = 0; index < Dimension; index++)
				{
					this[index] = vector[index];
				}
			}
			else
			{
				throw new LinearAlgebraException(LinearAlgebraExceptionMessage.VectorSizeNonEqual);
			}
		}
		#endregion

		#region Indexators
		/// <summary>
		/// Индексатор вектора.
		/// </summary>
		/// <param name="index">Индекс элемента вектора.</param>
		/// <returns>Возвращает элемент вектора по индексу.</returns>
		public double this[int index]
		{
			get
			{
				if(IndexIsValid(index))
				{
					return Items[index];
				}
				else
				{
					throw new LinearAlgebraException(LinearAlgebraExceptionMessage.IndexOutOfVectorSizeException);
				}
			}
			set
			{
				if(IndexIsValid(index))
				{
					Items[index] = value;
				}
				else
				{
					throw new LinearAlgebraException(LinearAlgebraExceptionMessage.IndexOutOfVectorSizeException);
				}
			}
		}
		#endregion

		#region Getters
		#region Get Maximum
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
			if(!IndexIsValid(vector, start, end))
			{
				throw new LinearAlgebraException(LinearAlgebraExceptionMessage.IndexOutOfVectorSizeException);
			}

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

		#region Get Minimum
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
			if(!IndexIsValid(vector, start, end))
			{
				throw new LinearAlgebraException(LinearAlgebraExceptionMessage.IndexOutOfVectorSizeException);
			}

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
		#endregion

		#region Setters
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
			else
			{
				Dimension = dimension;
			}
		}

		#region Set Items
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
		/// <typeparam name="T">Тип данных, реализующий интерфейс IComporable.</typeparam>
		/// <param name="vector">Вектор данных.</param>
		/// <param name="item">Заполняемое значение.</param>
		/// <param name="start">Начальный индекс заполнения.</param>
		/// <param name="end">Конечный индекс заполнения.</param>
		public static void SetItem<T>(T[] vector, T item, int start, int end)
		{
			if(!IndexIsValid(vector, start, end))
			{
				throw new LinearAlgebraException(LinearAlgebraExceptionMessage.IndexOutOfVectorSizeException);
			}

			for(int i = start; i <= end; i++)
			{
				vector[i] = item;
			}
		}
		#endregion
		#endregion

		#region Shift Items To Left
		/// <summary>
		/// Сдвиг элементов вектора влево.
		/// </summary>
		/// <param name="item">Элемент, заполняемый на место свинутого элемента.</param>
		/// <param name="start">Начальный индекс сдвигаемого элемента.</param>
		public void ShiftItemsToLeft(double item, int start)
		{
			ShiftItemsToLeft(this, item, start);
		}

		/// <summary>
		/// Сдвиг элементов вектора влево.
		/// </summary>
		/// <param name="vector">Вектора данных.</param>
		/// <param name="item">Элемент, заполняемый на место свинутого элемента.</param>
		/// <param name="start">Начальный индекс сдвигаемого элемента.</param>
		public static void ShiftItemsToLeft(Vector vector, double item, int start)
		{
			ShiftItemsToLeft(vector.Items, item, start);
		}

		/// <summary>
		/// Сдвиг элементов вектора влево.
		/// </summary>
		/// <param name="vector">Вектора данных.</param>
		/// <param name="item">Элемент, заполняемый на место свинутого элемента.</param>
		/// <param name="start">Начальный индекс сдвигаемого элемента.</param>
		public static void ShiftItemsToLeft<T>(T[] vector, T item, int start)
		{
			if(!IndexIsValid(vector, start))
			{
				throw new LinearAlgebraException(LinearAlgebraExceptionMessage.IndexOutOfVectorSizeException);
			}

			if(start == 0)
			{
				return;
			}

			for(int i = start; i < vector.Length; i++)
			{
				vector[start - 1] = vector[start];
			}

			vector[vector.Length - 1] = item;
		}
		#endregion

		#region Validation
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
			return (index >= 0 && index < vector.Length);
		}

		/// <summary>
		/// Проверка индексов вектора.
		/// </summary>
		/// <param name="vector">Вектор данных.</param>
		/// <param name="start">Начальный индекс.</param>
		/// <param name="end">Конечный индекс.</param>
		/// <returns>Возвращает результат проверки индексов вектора.</returns>
		public bool IndexIsValid(int start, int end)
		{
			return IndexIsValid(this, start, end);
		}

		/// <summary>
		/// Проверка индексов вектора.
		/// </summary>
		/// <param name="vector">Вектор данных.</param>
		/// <param name="start">Начальный индекс.</param>
		/// <param name="end">Конечный индекс.</param>
		/// <returns>Возвращает результат проверки индексов вектора.</returns>
		public static bool IndexIsValid(Vector vector, int start, int end)
		{
			return IndexIsValid(vector.Items, start, end);
		}

		/// <summary>
		/// Проверка индексов вектора.
		/// </summary>
		/// <typeparam name="T">Универсальный тип данных.</typeparam>
		/// <param name="vector">Вектор данных.</param>
		/// <param name="start">Начальный индекс.</param>
		/// <param name="end">Конечный индекс.</param>
		/// <returns>Возвращает результат проверки индексов вектора.</returns>
		public static bool IndexIsValid<T>(T[] vector, int start, int end)
		{
			return (start >= 0 && start < vector.Length && end >= 0 && end < vector.Length && start <= end);
		}
		#endregion

		#region Size Is Equal
		/// <summary>
		/// Проверка равенства размерностей векторов.
		/// </summary>
		/// <param name="vector">Вектор, у которого проверяется размерность.</param>
		/// <returns>Возвращает истину если размерности совпадают, иначе - ложь.</returns>
		public bool SizeIsEqual(Vector vector)
		{
			return SizeIsEqual(vector.Items);
		}

		/// <summary>
		/// Проверка равенства размерностей векторов.
		/// </summary>
		/// <param name="vector">Вектор, у которого проверяется размерность.</param>
		/// <returns>Возвращает истину если размерности совпадают, иначе - ложь.</returns>
		public bool SizeIsEqual(double[] vector)
		{
			return (vector.Length == Dimension);
		}

		/// <summary>
		/// Проверка равенства размерностей векторов.
		/// </summary>
		/// <param name="vector1">Вектор 1.</param>
		/// <param name="vector2">Вектор 2.</param>
		/// <returns>Возвращает истину если размерности совпадают, иначе - ложь.</returns>
		public static bool SizeIsEqual(Vector vector1, Vector vector2)
		{
			return SizeIsEqual(vector1.Items, vector1.Items);
		}

		/// <summary>
		/// Проверка равенства размерностей векторов.
		/// </summary>
		/// <param name="vector1">Вектор 1.</param>
		/// <param name="vector2">Вектор 2.</param>
		/// <returns>Возвращает истину если размерности совпадают, иначе - ложь.</returns>
		public static bool SizeIsEqual(double[] vector1, double[] vector2)
		{
			return (vector1.Length == vector2.Length);
		}
		#endregion

		#region Vector Addation
		/// <summary>
		/// Сложение векторов. Изменяется текущий вектор.
		/// </summary>
		/// <param name="vector">Вектор.</param>
		/// <returns>Возвращает текущий вектор - результат сложения векторов.</returns>
		public Vector Addation(Vector vector)
		{
			return Addation(vector.Items);
		}

		/// <summary>
		/// Сложение векторов. Изменяется текущий вектор.
		/// </summary>
		/// <param name="vector">Вектор.</param>
		/// <returns>Возвращает текущий вектор - результат сложения векторов.</returns>
		public Vector Addation(double[] vector)
		{
			if(Dimension != vector.Length)
			{
				throw new LinearAlgebraException(LinearAlgebraExceptionMessage.VectorSizeNonEqual);
			}

			for(int i = 0; i < Dimension; i++)
			{
				this[i] += vector[i];
			}

			return this;
		}

		/// <summary>
		/// Сложение 2-х векторов.
		/// </summary>
		/// <param name="vector1">Вектор 1.</param>
		/// <param name="vector2">Вектор 2.</param>
		/// <returns>Возвращает новый вектор - результат сложения векторов.</returns>
		public static Vector Addation(Vector vector1, Vector vector2)
		{
			return Addation(vector1.Items, vector2.Items);
		}

		/// <summary>
		/// Сложение 2-х векторов.
		/// </summary>
		/// <param name="vector1">Вектор 1.</param>
		/// <param name="vector2">Вектор 2.</param>
		/// <returns>Возвращает новый вектор - результат сложения векторов.</returns>
		public static Vector Addation(double[] vector1, double[] vector2)
		{
			if(vector1.Length != vector2.Length)
			{
				throw new LinearAlgebraException(LinearAlgebraExceptionMessage.VectorSizeNonEqual);
			}

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
		/// <param name="vector">Вектор.</param>
		/// <param name="scalar">Скаляр.</param>
		/// <param name="isNew">Указатель создания нового вектора.</param>
		/// <returns>Возвращает вектор - результат умножения на скаляр.</returns>
		public static Vector MultiplicationByNumber(Vector vector, double scalar, bool isNew = false)
		{
			if(isNew)
			{
				return MultiplicationByNumber(vector.Items, scalar);
			}
			else
			{
				for(int i = 0; i < vector.Dimension; i++)
				{
					vector[i] *= scalar;
				}

				return vector;
			}
		}

		/// <summary>
		/// Умножения вектора на скаляр.
		/// </summary>
		/// <param name="vector">Вектор.</param>
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
		/// <param name="vector">Вектор.</param>
		/// <returns>Возвращает результат скалярного произведения векторов.</returns>
		public double ScalarMultiplication(Vector vector)
		{
			return ScalarMultiplication(this, vector);
		}

		/// <summary>
		/// Скалярное произведение векторов.
		/// </summary>
		/// <param name="vector">Вектор.</param>
		/// <returns>Возвращает результат скалярного произведения векторов.</returns>
		public double ScalarMultiplication(double[] vector)
		{
			return ScalarMultiplication(this.Items, vector);
		}

		/// <summary>
		/// Скалярное произведение векторов.
		/// </summary>
		/// <param name="vector1">Вектор 1.</param>
		/// <param name="vector2">Вектор 2.</param>
		/// <returns>Возвращает результат скалярного произведения векторов.</returns>
		public static double ScalarMultiplication(Vector vector1, Vector vector2)
		{
			return ScalarMultiplication(vector1.Items, vector2.Items);
		}

		/// <summary>
		/// Скалярное произведение векторов.
		/// </summary>
		/// <param name="vector1">Вектор 1.</param>
		/// <param name="vector2">Вектор 2.</param>
		/// <returns>Возвращает результат скалярного произведения векторов.</returns>
		public static double ScalarMultiplication(double[] vector1, double[] vector2)
		{
			if(!SizeIsEqual(vector1, vector2))
			{
				throw new LinearAlgebraException(LinearAlgebraExceptionMessage.VectorSizeNonEqual);
			}

			var result = 0.0;
			for(int i = 0; i < vector1.Length; i++)
			{
				result += vector1[i] * vector2[i];
			}

			return result;
		}
		#endregion

		#region Vector Subtracting
		public Vector Subtracting(Vector vector)
		{
			return Subtracting(vector.Items);
		}

		/// <summary>
		/// Разность векторов. Изменяется текущий вектор.
		/// </summary>
		/// <param name="vector">Вектор.</param>
		/// <returns>Возвращает текущий вектор - результат разности векторов.</returns>
		public Vector Subtracting(double[] vector)
		{
			if(vector.Length != Dimension)
			{
				throw new LinearAlgebraException(LinearAlgebraExceptionMessage.VectorSizeNonEqual);
			}

			for(int i = 0; i < Dimension; i++)
			{
				this[i] -= vector[i];
			}

			return this;
		}

		/// <summary>
		/// Разность 2-х векторов.
		/// </summary>
		/// <param name="vector1">Вектор 1.</param>
		/// <param name="vector2">Вектор 2.</param>
		/// <returns>Возвращает новый вектор - результат разности 2-х векторов.</returns>
		public static Vector Subtracting(Vector vector1, Vector vector2)
		{
			return Subtracting(vector1.Items, vector2.Items);
		}

		/// <summary>
		/// Разность 2-х векторов.
		/// </summary>
		/// <param name="vector1">Вектор 1.</param>
		/// <param name="vector2">Вектор 2.</param>
		/// <returns>Возвращает новый вектор - результат разности 2-х векторов.</returns>
		public static Vector Subtracting(double[] vector1, double[] vector2)
		{
			if(vector1.Length != vector2.Length)
			{
				throw new LinearAlgebraException(LinearAlgebraExceptionMessage.VectorSizeNonEqual);
			}

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
