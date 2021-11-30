using System;

namespace LeastCostMethod
{
    public class LeastCostMoveIterator
    {
        public bool NextMove(CostsTable costsTable)
        {
            var minActiveCell = GetMinCellIndexes(costsTable);
            if (minActiveCell == null)
                return false;

            FillCellWithHeadersValues(costsTable, minActiveCell.Item1, minActiveCell.Item2);
            return true;
        }

        private Tuple<int, int> GetMinCellIndexes(CostsTable costsTable)
        {
            Tuple<int, int> resultIndexes = null;
            
            for (int x = 0; x < costsTable.LengthX; x++)
            {
                for (int y = 0; y < costsTable.LengthY; y++)
                {
                    if(!costsTable.IsCellActiveForLeastCost(x, y))
                        continue;

                    if (resultIndexes == null)
                    {
                        resultIndexes =  new Tuple<int, int>(x, y);
                        continue;
                    }

                    if (costsTable.GetCost(x, y) < costsTable.GetCost(resultIndexes.Item1, resultIndexes.Item2))
                    {
                        resultIndexes = new Tuple<int, int>(x, y);
                    }
                }
            }

            return resultIndexes;
        }
        
        private void FillCellWithHeadersValues(CostsTable costsTable, int indexX, int indexY)
        {
            var fillCellValue = MaxValueToFillCell(costsTable, indexX, indexY);
            costsTable.TransferValueToCell(indexX, indexY, fillCellValue);
        }

        private static decimal MaxValueToFillCell(CostsTable costsTable, int indexX, int indexY)
        {
            return Math.Min(costsTable.GetHeaderXCurrentValue(indexX), costsTable.GetHeaderYCurrentValue(indexY));
        }
    }
}