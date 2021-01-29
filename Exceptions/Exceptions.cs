using System;

namespace Exceptions
{
    public class MatrixException : Exception
    {
        public MatrixException() { }
        public MatrixException(string message) : base(message) { }
        public MatrixException(string message, Exception inner) : base(message, inner) { }
        protected MatrixException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    public class Matrix
    {
        public int Rows
            { get; }
        public int Columns
            { get; }
        public double[,] Array
            { get; }   
        public Matrix(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            if (Rows < 0 || Columns < 0) throw new ArgumentOutOfRangeException("бідапічальпомилка");
            Array = new double[rows, columns];
                if (rows <= 0 || columns <= 0)
                {
                    throw new ArgumentOutOfRangeException("бідапічальпомилка");
                } 
            if (Array == null) throw new MatrixException("бідапічальпомилка");
        }

        public Matrix(double[,] array)
        {
            if (array == null) throw new ArgumentNullException("бідапічальпомилка");
            Rows = array.GetLength(0);
            Columns = array.GetLength(1);
            if (Rows < 0 || Columns < 0) throw new ArgumentOutOfRangeException("бідапічальпомилка");
            Array = array;
            Array = new double[Rows, Columns];
        }

        public double this[int row, int column]
        {
            get
            {
                if (row >= Rows || column >= Columns) throw new ArgumentException("бідапічальпомилка");
                if (row < 0 || column < 0) throw new ArgumentOutOfRangeException("бідапічальпомилка");
                return Array[row, column];  
            }
            set
            {
                if (row >= Rows || column >= Columns) throw new ArgumentException("бідапічальпомилка");
                if (row < 0 || column < 0) throw new ArgumentOutOfRangeException("бідапічальпомилка");
                Array[row, column] = value;
            }
        } 
        public Matrix Add(Matrix matrix)
        {
            if (matrix == null) throw new ArgumentNullException("бідапічальпомилка");
            if (matrix.Columns != Columns || matrix.Rows != Rows) throw new MatrixException("бідапічальпомилка");
            if (matrix.Columns <= 0 || matrix.Rows <= 0) throw new ArgumentOutOfRangeException("бідапічальпомилка");

            for (int i = 0; i < Array.GetLength(0); i++)
            {
                for (int j = 0; j < Array.GetLength(1); j++)
                {
                    matrix[i, j] += Array[i, j];
                }
            }
            return matrix;
        }
        public Matrix Subtract(Matrix matrix)
        {
            if (matrix == null) throw new ArgumentNullException("бідапічальпомилка");
            if (matrix.Columns != Rows || matrix.Rows != Columns) throw new MatrixException("бідапічальпомилка");
            if (matrix.Columns <= 0 || matrix.Rows <= 0) throw new ArgumentOutOfRangeException("бідапічальпомилка");

            for (int i = 0; i < Array.GetLength(0); i++)
            {
                for (int j = 0; j < Array.GetLength(1); j++)
                {
                    matrix[i, j] -= Array[i, j];
                }
            }
            return matrix;
        }
        public Matrix Multiply(Matrix matrix)
        {
            if (matrix == null) throw new ArgumentNullException("бідапічальпомилка");
            if (matrix.Columns != Rows || matrix.Rows != Columns)  throw new MatrixException("бідапічальпомилка");
            if (matrix.Columns <= 0 || matrix.Rows <= 0) throw new ArgumentOutOfRangeException("бідапічальпомилка");

             Matrix matrix1 = new Matrix(matrix.Columns, matrix.Rows);
            for (int i = 0; i < matrix.Columns; i++)
            {
                for (int j = 0; j < matrix.Rows; j++)
                {
                    for (int k = 0; k < Array.GetLength(0); k++)
                    {
                        matrix1[i, j] = matrix[i, j] * Array[i, j] + matrix [i, j]* Array [i, k];
                    }
                }
            }
            return matrix1;            
        }
    }
}