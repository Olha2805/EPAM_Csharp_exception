using System;

namespace Exceptions
{

    [Serializable]
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
        {
            get;
        }
        public int Columns
        {
            get ;
        }

        public double[,] Array
        {
            get;
        }
        public Matrix(int rows, int columns)
        {
            
            if (rows <= 0 || columns <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rows), nameof(columns));
            }
            Rows = rows;
            Columns = columns;
            Array = new double[rows, columns];
            if (Array == null)
            {
                throw new MatrixException();
            }
        }

        
        public Matrix(double[,] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            Rows = array.GetLength(0);
            Columns = array.GetLength(1);
            if (Rows < 0 || Columns < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(array));
            }
            Array = new double[Rows, Columns];
            Array = array;


        }
        public double this[int row, int column]
        {
            
            get
            {
                if (row > Rows || column > Columns)
                {
                    throw new ArgumentException(nameof(row),nameof(column));
                }
                if (row < 0 || column < 0)
                {
                    throw new ArgumentException(nameof(row), nameof(column));
                }
                return Array[row, column];
            }
            set
            {
                if (row > Rows || column > Columns)
                {
                    throw new ArgumentException(nameof(row), nameof(column));
                }
                if (row < 0 || column < 0)
                {
                    throw new ArgumentException(nameof(row), nameof(column));
                }
                Array[row, column] = value;
            }
            
        }
        public Matrix Add(Matrix matrix)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }
            if (matrix.Columns != Columns|| matrix.Rows!=Rows)
            {
                throw new MatrixException();
            }
            if (matrix.Rows <= 0 || matrix.Columns <= 0)
            {
                throw new ArgumentException(nameof(matrix));
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
            
            if (matrix == null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }
            if (matrix.Columns != Columns || matrix.Rows != Rows)
            {
                throw new MatrixException();
            }
            if (matrix.Rows <= 0 || matrix.Columns <= 0)
            {
                throw new ArgumentException(nameof(matrix));
            }
            for (int i = 0; i < Array.GetLength(0); i++)
            {
                for (int j = 0; j < Array.GetLength(1); j++)
                {
                    Array[i, j] -= matrix[i, j];
                }
            }
            Matrix result = new Matrix(Array);
            return result;
        }
        public Matrix Multiply(Matrix matrix)
        {
            Matrix matrix2 = new Matrix(Array);
            if (matrix == null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }
            if (matrix.Rows != matrix2.Columns)
            {
                throw new MatrixException();
            }
            if (matrix.Rows != matrix2.Columns)
            {
                throw new MatrixException();
            }
            if (matrix.Rows < 0 || matrix.Columns < 0)
            {
                throw new ArgumentException(nameof(matrix));
            }
            if (matrix.Rows == 0 || matrix.Columns == 0)
            {
                return matrix;
            }

            Matrix matrix1 = new Matrix(matrix2.Rows, matrix.Columns);
            for (int i = 0; i < matrix1.Rows; i++)
            {
                for (int j = 0; j < matrix1.Columns; j++)
                {
                    matrix1[i, j] = 0;
                    for (int k = 0; k < matrix2.Columns; k++)
                    {
                        matrix1[i, j] += matrix2[i, k] * matrix[k, j];
                    }
                }
            }
            return matrix1;
        }
    }
}
