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
            if (rows < 0 || columns < 0) throw new ArgumentOutOfRangeException(nameof(rows), nameof(columns));
            Rows = rows;
            Columns = columns;
            Array = new double[rows, columns];
                if (rows <= 0 || columns <= 0)
                {
                    throw new ArgumentOutOfRangeException( nameof(rows), nameof(columns));
                } 
            if (Array == null) throw new MatrixException();
        }

        public Matrix(double[,] array)
        {
            if (array == null) throw new ArgumentNullException( nameof(array));
            Rows = array.GetLength(0);
            Columns = array.GetLength(1);
            if (Rows < 0 || Columns < 0) throw new ArgumentOutOfRangeException( nameof(Rows), nameof(Columns));
            Array = array;
            Array = new double[Rows, Columns];
        }

        public double this[int row, int column]
        {
            get
            {
                if (row >= Rows || column >= Columns) throw new ArgumentException(nameof(row), nameof(column));
                if (row < 0 || column < 0) throw new ArgumentException (nameof(row), nameof(column));
                return Array[row, column];  
            }
            set
            {
                if (row >= Rows || column >= Columns) throw new ArgumentException(nameof(row), nameof(column));
                if (row < 0 || column < 0) throw new ArgumentException (nameof(row), nameof(column));
                Array[row, column] = value;
            }
        } 
        public Matrix Add(Matrix matrix)
        {
            if (matrix == null) throw new ArgumentNullException( nameof(matrix));
            if (matrix.Columns != Columns || matrix.Rows != Rows) throw new MatrixException();
            if (matrix.Columns <= 0 || matrix.Rows <= 0) throw new ArgumentException (nameof(Columns), nameof(Rows));

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
            if (matrix == null) throw new ArgumentNullException( nameof(matrix));
            if (matrix.Columns != Columns || matrix.Rows != Rows) throw new MatrixException();
            if (matrix.Columns <= 0 || matrix.Rows <= 0) throw new ArgumentException (nameof(Columns), nameof(Rows));

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
            if (matrix == null) throw new ArgumentNullException( nameof(matrix));
            if (matrix.Columns != Rows || matrix.Rows != Columns)  throw new MatrixException();
            if (matrix.Columns <= 0 || matrix.Rows <= 0) throw new ArgumentException (nameof(Columns), nameof(Rows));

             Matrix matrix1 = new Matrix(matrix.Columns, Array.GetLength(1));
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