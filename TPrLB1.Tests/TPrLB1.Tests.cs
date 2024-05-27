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
        public void Test_Prikola() 
        {
            List<int> criterion_values = new List<int>() { 2, 2};

            int Number_of_possible_alternatives = 4;

            int[] prikol = new int[criterion_values.Count];

            Program.PrikolTest(criterion_values, Number_of_possible_alternatives, prikol);

            int[] expected = new int[2] {2, 1};

            Assert.That(prikol, Is.EqualTo(expected));
        }

    }
}
