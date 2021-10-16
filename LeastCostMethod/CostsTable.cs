using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace LeastCostMethod
{
    public class CostsTable
    {
        private readonly decimal[] _headerValuesX;
        private readonly decimal[] _headerValuesY;
        private readonly decimal[,] _costMatrix;
        private readonly decimal[,] _resultsMatrix;

        public CostsTable(decimal[] headerValuesX, decimal[] headerValuesY, decimal[,] costMatrix)
        {
            if (!IsValidTable(headerValuesX, headerValuesY, costMatrix))
            {
                throw new ArgumentException();
            }
            
            _headerValuesX = headerValuesX;
            _headerValuesY = headerValuesY;
            _costMatrix = costMatrix;
            _resultsMatrix = CreateZeroMatrix(_headerValuesX.Length, _headerValuesY.Length);
        }

        public decimal ResultSum
        {
            get
            {
                decimal result = 0;

                for (int x = 0; x < LengthX; x++)
                {
                    for (int y = 0; y < LengthY; y++)
                    {
                        result += _resultsMatrix[x, y] * _costMatrix[x, y];
                    }
                }

                return result;
            }
        }
        
        private static bool IsValidTable(decimal[] headerValuesX, decimal[] headerValuesY, decimal[,] costMatrix)
        {
            return headerValuesX.Length == costMatrix.GetLength(0) 
                   && headerValuesY.Length == costMatrix.GetLength(1);
        }

        public IReadOnlyList<decimal> HeadersX => _headerValuesX;
        public IReadOnlyList<decimal> HeadersY => _headerValuesY;

        public IEnumerable<decimal> GetCostsColumn(int x)
        {
            for (int y = 0; y < LengthY; y++)
            {
                yield return _costMatrix[x, y];
            }
        }
        
        public IEnumerable<decimal> GetValuesColumn(int x)
        {
            for (int y = 0; y < LengthY; y++)
            {
                yield return _resultsMatrix[x, y];
            }
        }
        
        public IEnumerable<decimal> GetCostsRow(int y)
        {
            for (int x = 0; x < LengthX; x++)
            {
                yield return _costMatrix[x, y];
            }
        }
        
        public IEnumerable<decimal> GetValuesRow(int y)
        {
            for (int x = 0; x < LengthX; x++)
            {
                yield return _resultsMatrix[x, y];
            }
        }

        public int LengthX => _headerValuesX.Length;
        public int LengthY => _headerValuesY.Length;

        public decimal GetCost(int indexX, int indexY)
        {
            return _costMatrix[indexX, indexY];
        }
        
        public decimal GetValue(int indexX, int indexY)
        {
            return _resultsMatrix[indexX, indexY];
        }
        
        public decimal GetHeaderXCurrentValue(int indexX)
        {
            return _headerValuesX[indexX];
        }
        
        public decimal GetHeaderYCurrentValue(int indexY)
        {
            return _headerValuesY[indexY];
        }

        public void TransferValueToCell(int indexX, int indexY, decimal value)
        {
            _resultsMatrix[indexX, indexY] += value;
            
            _headerValuesX[indexX] -= value;
            _headerValuesY[indexY] -= value;
        }
        
        private static decimal[,] CreateZeroMatrix(int sizeX, int sizeY)
        {
            return new decimal[sizeX, sizeY];
        }
    }
}