using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLeslieModel
{
    public class Matrix
    {
        private List<List<double>> _matrix;

        public Matrix() => _matrix = new List<List<double>>();

        // кол-во строк
        public int CountRows
        {
            get
            {
                if (_matrix != null)
                    return _matrix.Count;
                else
                    return 0;
            }
            private set { }
        }

        // Кол-во столбцов
        public int CountColumns
        {
            get
            {
                if (_matrix != null)
                    return _matrix[0].Count;
                else
                    return 0;
            }
            private set { }
        }


        public Matrix(int countRows, int countColumns)
        {
            _matrix = new List<List<double>>();
            SetMatrix(countRows, countColumns);
        }

        public Matrix(List<List<double>> matrix)
        {
            _matrix = new List<List<double>>();
            SetMatrix(matrix);
        }

        public Matrix(Matrix matrix)
        {
            _matrix = new List<List<double>>();
            SetMatrix(matrix._matrix);
        }

        public void SetMatrix(int countRows, int countColumns)
        {
            _matrix.Clear();
            for (int i = 0; i < countRows; i++)
            {
                _matrix.Add(new List<double>());
                for (int j = 0; j < countColumns; j++)
                    _matrix[i].Add(0);
            }
        }

        public void SetMatrix(List<List<double>> matrix)
        {
            _matrix.Clear();
            for (int i = 0; i < matrix.Count; i++)
                _matrix.Add(new List<double>(matrix[i]));
        }

        // добавление строки в матрицу
        public void AddRow(List<double> list)
        {
            if (_matrix.Count == 0)
                _matrix.Add(list);
            else if (list.Count == _matrix[0].Count)
                _matrix.Add(list);
            else if (list.Count < _matrix[0].Count)
            {
                _matrix.Add(list);
                for (int i = list.Count; i < _matrix[0].Count; i++)
                    _matrix[_matrix.Count - 1].Add(0);
            }
            else
            {
                _matrix.Add(new());
                for (int i = list.Count; i < _matrix[0].Count; i++)
                    _matrix[_matrix.Count - 1].Add(list[i]);
            }
        }

        // добавление столбца в строку
        public void AddColumn(List<double> list)
        {
            if (_matrix.Count == 0)
                SetMatrix(list.Count, 0);
            if (list.Count == _matrix.Count)
                for (int i = 0; i < _matrix.Count; i++)
                    _matrix[i].Add(list[i]);
            else if (list.Count < _matrix.Count)
            {
                for (int i = 0; i < list.Count; i++)
                    _matrix[i].Add(list[i]);
                for (int i = list.Count; i < _matrix.Count; i++)
                    _matrix[i].Add(0);
            }
            else
                for (int i = 0; i < _matrix.Count; i++)
                    _matrix[i].Add(list[i]);
        }

        public void AddColumn(Matrix matrix, int indexColumn)
        {
            if (_matrix.Count == 0)
                SetMatrix(matrix.CountRows, 0);
            if (matrix.CountRows == CountRows)
                for (int i = 0; i < _matrix.Count; i++)
                    _matrix[i].Add(matrix[i][indexColumn]);
            else if (matrix.CountRows < CountRows)
            {
                for (int i = 0; i < matrix.CountRows; i++)
                    _matrix[i].Add(matrix[i][indexColumn]);
                for (int i = matrix.CountRows; i < _matrix.Count; i++)
                    _matrix[i].Add(0);
            }
            else
                for (int i = 0; i < _matrix.Count; i++)
                    _matrix[i].Add(matrix[i][indexColumn]);
        }




        public List<double> this[int index]
        {
            get => _matrix[index];
            set => _matrix[index] = new(value);
        }

        public static Matrix operator *(Matrix matrixOne, Matrix matrixTwo)
        {
            if (matrixOne.CountColumns == matrixTwo.CountRows)
            {
                var result = new Matrix(matrixOne.CountRows, matrixTwo.CountColumns);
                for (int i = 0; i < matrixOne.CountRows; i++)
                    for (int j = 0; j < matrixTwo.CountColumns; j++)
                        for (int k = 0; k < matrixOne.CountColumns; k++)
                            result[i][j] += matrixOne[i][k] * matrixTwo[k][j];
                return result;
            }
            else
                throw new Exception("Число столбцов матрицы matrixOne не равно числу строк матрицы matrixTwo.");

        }

        public void MultiplicationMatrix(Matrix matrix)
        {
            if (CountColumns == matrix.CountRows)
            {
                var result = new Matrix(CountRows, matrix.CountColumns);
                for (int i = 0; i < CountRows; i++)
                    for (int j = 0; j < matrix.CountColumns; j++)
                        for (int k = 0; k < CountColumns; k++)
                            result[i][j] += _matrix[i][k] * matrix[k][j];
            }
        }

        // сложение матриц
        public static Matrix operator +(Matrix matrixOne, Matrix matrixTwo)
        {
            var result = new Matrix(matrixOne);
            result.AdditionMatrix(matrixTwo);
            return result;
        }

        public void AdditionMatrix(Matrix matrix)
        {
            if (matrix.CountColumns == CountColumns && matrix.CountRows == CountRows)
            {
                for (int i = 0; i < CountRows; i++)
                    for (int j = 0; j < CountColumns; j++)
                        _matrix[i][j] += matrix[i][j];
            }
        }

        public static Matrix operator -(Matrix matrixOne, Matrix matrixTwo)
        {
            var result = new Matrix(matrixOne);
            result.SubtractionMatrix(matrixTwo);
            return result;
        }



        public void SubtractionMatrix(Matrix matrix)
        {
            if (matrix.CountColumns == CountColumns && matrix.CountRows == CountRows)
            {
                for (int i = 0; i < CountRows; i++)
                    for (int j = 0; j < CountColumns; j++)
                        _matrix[i][j] -= matrix[i][j];
            }
        }

        public void PrintConsole()
        {
            foreach (var row in _matrix)
            {
                foreach (var col in row)
                    Console.Write($"{col}\t");
                Console.WriteLine();
            }
        }

        public void Clear() => _matrix.Clear();
    }
}
