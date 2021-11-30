using System.Globalization;
using System.Text;

namespace LeastCostMethod
{
    public static class CostsTableForLeastCostMethodExtensions
    {
        public static string LeastCostTableToString(this CostsTable costsTable)
        {
            var stringBuilder = new StringBuilder();
            
            stringBuilder.Append("  ");
            stringBuilder.Append(string.Join(' ', costsTable.HeadersY));

            stringBuilder.AppendLine();
            
            for (int x = 0; x < costsTable.LengthX; x++)
            {
                stringBuilder.Append(costsTable.GetHeaderXCurrentValue(x) + " ");

                for (int y = 0; y < costsTable.LengthY; y++)
                {
                    stringBuilder.Append(GetCellValueForShow(costsTable, x, y));
                    stringBuilder.Append(" ");
                }

                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }

        private static string GetCellValueForShow(CostsTable costsTable, int x, int y)
        {
            return IsZeroNonActiveCell(costsTable, x, y) 
                ? "x"
                : costsTable.GetValue(x, y).ToString(CultureInfo.InvariantCulture);
        }

        private static bool IsZeroNonActiveCell(CostsTable costsTable, int x, int y)
        {
            return (!costsTable.IsCellActiveForLeastCost(x, y) && costsTable.GetValue(x, y) == 0);
        }


        public static bool IsCellActiveForLeastCost(this CostsTable costsTable, int indexX, int indexY)
        {
            return costsTable.GetHeaderXCurrentValue(indexX) > 0 
                   && costsTable.GetHeaderYCurrentValue(indexY) > 0;
        }
    }
}