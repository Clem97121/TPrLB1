using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using TPrLB1;

namespace TPrLB1.Tests
{
    public class Tests
    {
        [Test]
        public void Test_CalculateStepForAlters()
        {
            // Arrange
            List<int> criterionValues = new List<int>() { 2, 2 };

            int numOfAlternatives = Program.CalculateNumAlter(criterionValues);

            int numOfCrit = criterionValues.Count;

            int[] stepForAlters = new int[numOfCrit];

            // Act
            Program.CalculateStepForAlters(criterionValues, numOfAlternatives, stepForAlters);

            // Assert
            int[] expected = new int[2] { 2, 1 };

            Assert.That(stepForAlters, Is.EqualTo(expected));
        }
        [Test]
        public void Test_CalculateNumAlter() 
        {
            // Arrange
            List<int> criterionValues = new List<int>() { 2, 5, 2};

            // Act
            int result = Program.CalculateNumAlter(criterionValues);

            // Assert
            Assert.That(result, Is.EqualTo(20));
        }
        [Test]
        public void Test_CalculateAlternatives_1()
        {
            // Arrange
            List<int> criterionValues = new List<int>() { 2, 2};

            int numOfAlternatives = Program.CalculateNumAlter(criterionValues);

            int[,] alternatives = new int[criterionValues.Count, numOfAlternatives];

            // Act
            Program.CalculateAlternatives(criterionValues, numOfAlternatives, alternatives);

            // Assert
            int[,] expected = new int[2, 4] { { 1, 1, 2, 2 }, { 1, 2, 1, 2 }};

            Assert.That(alternatives, Is.EqualTo(expected));
        }
        [Test]
        public void Test_CalculateAlternatives_2()
        {
            // Arrange
            List<int> criterionValues = new List<int>() { 3, 3 };

            int numOfAlternatives = Program.CalculateNumAlter(criterionValues);

            int[,] alternatives = new int[criterionValues.Count, numOfAlternatives];

            // Act
            Program.CalculateAlternatives(criterionValues, numOfAlternatives, alternatives);

            // Assert
            int[,] expected = new int[2, 9] { { 1, 1, 1, 2, 2, 2, 3, 3, 3 }, { 1, 2, 3, 1, 2, 3, 1, 2, 3 } };

            Assert.That(alternatives, Is.EqualTo(expected));
        }
        [Test]
        public void Test_Print_Alters()
        {
            // Arrange
            List<int> criterionValues = new List<int>() { 3, 3 };

            int numOfAlternatives = Program.CalculateNumAlter(criterionValues);

            int[,] alternatives = new int[criterionValues.Count, numOfAlternatives];

            Program.CalculateAlternatives(criterionValues, numOfAlternatives, alternatives);
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            Program.Print(alternatives);

            // Assert
            string expectedOutput = "1 1 \r\n1 2 \r\n1 3 \r\n2 1 \r\n2 2 \r\n2 3 \r\n3 1 \r\n3 2 \r\n3 3 \r\n\r\n"; 

            Assert.That(expectedOutput, Is.EqualTo(sw.ToString()));


        }
    }
}
