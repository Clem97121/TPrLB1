using System;
using System.Collections.Generic;
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
            List<int> criterionValues = new List<int>() { 2, 2 };

            int numOfAlternatives = Program.CalculateNumAlter(criterionValues);

            int numOfCrit = criterionValues.Count;

            int[] stepForAlters = new int[numOfCrit];

            Program.CalculateStepForAlters(criterionValues, numOfAlternatives, stepForAlters);

            int[] expected = new int[2] { 2, 1 };

            Assert.That(stepForAlters, Is.EqualTo(expected));
        }
        [Test]
        public void Test_CalculateNumAlter() 
        {
            List<int> criterionValues = new List<int>() { 2, 5, 2};

            int result = Program.CalculateNumAlter(criterionValues);

            Assert.That(result, Is.EqualTo(20));
        }

    }
}
