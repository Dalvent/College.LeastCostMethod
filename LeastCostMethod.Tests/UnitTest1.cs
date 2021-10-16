using NUnit.Framework;

namespace LeastCostMethod.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void IsTestMatrixReturn230()
        {
            CostsTable costsTable = new(CreateTestHeadersX(), CreateTestHeadersY(), CreateTestCosts());
            LeastCostMoveCommand leastCostMoveCommand = new();
            
            while (leastCostMoveCommand.Move(costsTable))
            {
            }
            
            ;
            Assert.AreEqual(costsTable.ResultSum, 230);
        }
        
        
        private static decimal[] CreateTestHeadersY()
        {
            return new decimal[] {10, 30, 30, 30, 40};
        }

        private static decimal[] CreateTestHeadersX()
        {
            return new decimal[] {10, 30, 60, 10, 60};
        }

        private static decimal[,] CreateTestCosts()
        {
            return new decimal[,]{
                {3, 1, 3, 4, 3},
                {5, 1, 2, 2, 6},
                {2, 3, 4, 1, 1},
                {6, 2, 5, 3, 2},
                {3, 7, 4, 4, 1},
            };
        }
    }
}