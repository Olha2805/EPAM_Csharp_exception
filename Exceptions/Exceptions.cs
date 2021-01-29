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
            Array = new double[rows, columns];
                if (rows <= 0 || columns <= 0)
                {
                    throw new ArgumentOutOfRangeException();
                } 
            if (Array == null) throw new MatrixException();
        }

        public Matrix(double[,] array)
        {
            if (array == null) throw new ArgumentNullException();
            Rows = array.GetLength(0);
            Columns = array.GetLength(1);
            Array = array;
            Array = new double[Rows, Columns];
        }

        public double this[int row, int column]
        {
            get
            {
                if (row >= Rows || column >= Columns) throw new ArgumentException();
                if (row < 0 || column < 0) throw new ArgumentOutOfRangeException();
                return Array[row, column];  
            }
            set
            {
                if (row >= Rows || column >= Columns) throw new ArgumentException();
                if (row < 0 || column < 0) throw new ArgumentOutOfRangeException();
                Array[row, column] = value;
            }
        } 
        public Matrix Add(Matrix matrix)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException();
            }
            if (matrix.Columns != Columns || matrix.Rows != Rows)
            {
                throw new MatrixException();
            }
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
            if (matrix == null) throw new ArgumentNullException();
            if (matrix.Columns != Rows || matrix.Rows != Columns) throw new MatrixException();
            if (matrix.Columns <= 0 || matrix.Rows <= 0) throw new ArgumentOutOfRangeException();

            for (int i = 0; i < Array.GetLength(0); i++)
            {
                for (int j = 0; j < Array.GetLength(1); j++) matrix[i, j] -= Array[i, j];
            }
            return matrix;
        }
        public Matrix Multiply(Matrix matrix)
        {
            if (matrix == null) throw new ArgumentNullException();
            if (matrix.Columns != Rows || matrix.Rows != Columns)  throw new MatrixException();
            if (matrix.Columns <= 0 || matrix.Rows <= 0) throw new ArgumentOutOfRangeException();
            
            Matrix matrix1 = new Matrix(matrix.Columns, matrix.Rows);
            for (int i = 0; i < Array.GetLength(0); i++)
            {
                for (int j = 0; j < Array.GetLength(1); j++)
                {
                    for (int k = 0; k < matrix.Columns; k++)
                    {
                        matrix1[i, j] = matrix[i, k] * Array[k, j];
                    }
                }
            }
            return matrix1;        
         }
    }
}