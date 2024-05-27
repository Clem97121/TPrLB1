using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPrLB1
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<int> criterionValues = new List<int>() { 2, 2, 3, 4, 1 };

            int[] iterClass = new int[15] { 2, 2, 2, 1, 2, 1, 1, 2, 1, 2, 1, 1, 2, 1, 2 };

            int numOfAlternatives = CalculateNumAlter(criterionValues);

            int[,] alternatives = new int[criterionValues.Count, numOfAlternatives];

            CalculateAlternatives(criterionValues, numOfAlternatives, alternatives);

            Print(alternatives);

            AlgoritmStart(alternatives, iterClass);

            Console.ReadLine();
        }

        public static void CalculateAlternatives(List<int> criterionValues, int numOfAlternatives, int[,] alternatives)
        {
            int[] stepForAlters = new int[criterionValues.Count];

            CalculateStepForAlters(criterionValues, numOfAlternatives, stepForAlters);

            int temp;
            for (int i = 0; i < criterionValues.Count; i++)
            {
                temp = 0;
                for (int j = 0; j < numOfAlternatives / stepForAlters[i]; j++)
                {
                    for (int k = 0; k < stepForAlters[i]; k++)
                    {
                        alternatives[i, temp] = (j) % criterionValues[i] + 1;
                        temp++;
                    }
                }
            }
        }

        public static int CalculateNumAlter(List<int> criterionValues)
        {
            int mul = 1;
            foreach(int i in criterionValues)
            {
                mul *= i;
            }
            return mul;
        }

        public static void CalculateStepForAlters(List<int> criterion_values, int Number_of_possible_alternatives, int[] prikol)
        {
            prikol[0] = Number_of_possible_alternatives / criterion_values[0];

            for (int i = 1; i < criterion_values.Count; i++)
            {
                prikol[i] = prikol[i - 1] / criterion_values[i];
            }
        }

        public static void Print(int[,] alter)
        {
            for (int i = 0; i < alter.GetLength(1); i++)
            {
                for (int j = 0; j < alter.GetLength(0); j++)
                {
                    Console.Write(alter[j, i].ToString() + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public static void AlgoritmStart(int[,] alter, int[] iterClass)
        {
            int numOfCrit = alter.GetLength(0);
            int numOfAlt = alter.GetLength(1);

            int[] G = new int[numOfAlt];
            float[] d1 = new float[numOfAlt];
            float[] d2 = new float[numOfAlt];
            float[] p1 = new float[numOfAlt];
            float[] p2 = new float[numOfAlt];
            int[] g1 = new int[numOfAlt];
            int[] g2 = new int[numOfAlt];
            float[] F1 = new float[numOfAlt];
            float[] F2 = new float[numOfAlt];
            float[] F = new float[numOfAlt];

            bool restart;

            int numOfIter = 0;

            float[] centr1 = new float[numOfCrit];
            FirtstlyCalculateCentr1(numOfCrit, centr1);

            float[] centr2 = new float[numOfCrit];
            FirstlyCalculateCentr2(alter, numOfCrit, numOfAlt, centr2);

            float D;
            FirstlyCalculateG(numOfAlt, G);

            do
            {
                numOfIter++;
                restart = CheckingRestart(numOfAlt, G);

                Calculate_d1(alter, numOfCrit, numOfAlt, d1, centr1);
                Calculate_d2(alter, numOfCrit, numOfAlt, d2, centr2);

                D = Math.Max(d1.Max(), d2.Max());

                Console.WriteLine("D = " + D);  //Винести в вивід!!!

                Calculate_p1(numOfAlt, G, d1, d2, p1, D);
                Calculate_p2(numOfAlt, p1, p2);
                int numUnDec = CalculateNumUnDec(numOfAlt, G);
                int[,] curentAlter = CalculateCurentAlter(alter, numOfCrit, numOfAlt, G, numUnDec);

                int[] tempg1 = CalculateTemp_g1(numOfCrit, numOfAlt, numUnDec, curentAlter);
                int[] tempg2 = CalculateTemp_g2(numOfCrit, numOfAlt, numUnDec, curentAlter);

                Calculate_g1_g2(numOfAlt, G, g1, g2, tempg1, tempg2);
                Calculate_F1_F2_F(numOfAlt, p1, p2, g1, g2, F1, F2, F);
                float MaxF;
                int indMaxF;
                Calculate_MaxF_IndMaxF(numOfAlt, F, out MaxF, out indMaxF);

                // Printing 

                string str = string.Empty;
                for (int i = 0; i < numOfCrit; i++)
                {
                    str += alter[i, i];
                }

                string outFirstStr = "";
                for (int j = 0; j < numOfCrit; j++)
                {
                    outFirstStr += "K" + (j + 1) + " ";
                }
                Console.WriteLine(outFirstStr + "G     d1     d2    p1    p2   g1    g2    F1    F2     F");
                for (int i = 0; i < numOfAlt; i++)
                {
                    string d1StrTemp = d1[i].ToString();
                    string d1Str = d1[i].ToString().Substring(0, Math.Min(d1StrTemp.ToString().Length, 4));

                    string d2StrTemp = d2[i].ToString();
                    string d2Str = d2[i].ToString().Substring(0, Math.Min(d2StrTemp.ToString().Length, 4));

                    string p1StrTemp = p1[i].ToString();
                    string p1Str = p1[i].ToString().Substring(0, Math.Min(p1StrTemp.ToString().Length, 4));

                    string p2StrTemp = p2[i].ToString();
                    string p2Str = p2[i].ToString().Substring(0, Math.Min(p2StrTemp.ToString().Length, 4));

                    string F1StrTemp = F1[i].ToString();
                    string F1Str = F1[i].ToString().Substring(0, Math.Min(F1StrTemp.ToString().Length, 4));

                    string F2StrTemp = F2[i].ToString();
                    string F2Str = F2[i].ToString().Substring(0, Math.Min(F2StrTemp.ToString().Length, 4));

                    string FStrTemp = F[i].ToString();
                    string FStr = F[i].ToString().Substring(0, Math.Min(FStrTemp.ToString().Length, 4));


                    string output = "";
                    for (int j = 0; j < numOfCrit; j++)
                    {
                        output += alter[j, i] + "  ";
                    }
                    output += G[i] + "  " + d1Str.PadLeft(5) + "  " + d2Str.PadLeft(5) + " " +
                        p1Str.PadLeft(5) + " " + p2Str.PadLeft(5) + " " +
                        g1[i].ToString().PadLeft(4) + " " + g2[i].ToString().PadLeft(5)
                        + " " + F1Str.PadLeft(5) + " " + F2Str.PadLeft(5) + " " + FStr.PadLeft(5);
                    Console.WriteLine(output);
                }
                Console.WriteLine("Max F = " + MaxF);
                Console.WriteLine("Index of Max F = " + indMaxF);
                string strCentr1 = "";
                string strCentr2 = "";
                for (int i = 0; i < numOfCrit; i++)
                {
                    strCentr1 += centr1[i].ToString() + " ";
                    strCentr2 += centr2[i].ToString() + " ";
                }
                Console.WriteLine("Centr1 = " + strCentr1);
                Console.WriteLine("Centr2 = " + strCentr2);
                Console.WriteLine("Num of Iteration = " + numOfIter);
                Console.WriteLine();

                G[indMaxF] = iterClass[numOfIter - 1];

                for (int i = 0; i < numOfAlt; i++)
                {
                    int temp = 0;
                    for (int j = 0; j < numOfCrit; j++)
                    {
                        if (iterClass[numOfIter - 1] == 1)
                        {
                            if (alter[j, indMaxF] >= alter[j, i])
                            {
                                temp++;
                            }
                        }
                        else
                        {
                            if (alter[j, indMaxF] <= alter[j, i])
                            {
                                temp++;
                            }
                        }
                    }
                    if (temp == numOfCrit) G[i] = iterClass[numOfIter - 1];
                }


                int[] indexSum1 = new int[numOfCrit];
                int numOfNewIndex1 = 0;
                int[] indexSum2 = new int[numOfCrit];
                int numOfNewIndex2 = 0;
                for (int i = 0; i < numOfAlt; i++)
                {
                    for (int j = 0; j < numOfCrit; j++)
                    {
                        if (G[i] == 1)
                        {
                            numOfNewIndex1++;
                            indexSum1[j] += alter[j, i];
                        }
                        if (G[i] == 2)
                        {
                            numOfNewIndex2++;
                            indexSum2[j] += alter[j, i];
                        }
                    }
                }
                numOfNewIndex1 /= numOfCrit;
                numOfNewIndex2 /= numOfCrit;
                for (int i = 0; i < numOfCrit; i++)
                {
                    centr1[i] = (float)indexSum1[i] / numOfNewIndex1;
                    centr2[i] = (float)indexSum2[i] / numOfNewIndex2;
                }

            } while (restart);
        }

        private static void Calculate_MaxF_IndMaxF(int numOfAlt, float[] F, out float MaxF, out int indMaxF)
        {
            MaxF = F.Max();
            indMaxF = 0;
            for (int i = 0; i < numOfAlt; i++)
            {
                if (F[i] == MaxF)
                {
                    indMaxF = i;
                    break;
                }
            }
        }

        private static void Calculate_F1_F2_F(int numOfAlt, float[] p1, float[] p2, int[] g1, int[] g2, float[] F1, float[] F2, float[] F)
        {
            for (int i = 0; i < numOfAlt; i++)
            {
                F1[i] = p1[i] * g1[i];
                F2[i] = p2[i] * g2[i];
                F[i] = F1[i] + F2[i];
            }
        }

        private static void Calculate_g1_g2(int numOfAlt, int[] G, int[] g1, int[] g2, int[] tempg1, int[] tempg2)
        {
            int tempForG1 = 0;
            int tempForG2 = 0;
            for (int i = 0; i < numOfAlt; i++)
            {
                if (G[i] == 3)
                {
                    g1[i] = tempg1[i - tempForG1];
                    g2[i] = tempg2[i - tempForG2];
                }
                else
                {
                    g1[i] = 0;
                    tempForG1++;
                    g2[i] = 0;
                    tempForG2++;
                }
            }
        }

        private static int[] CalculateTemp_g2(int numOfCrit, int numOfAlt, int numUnDec, int[,] curentAlter)
        {
            int[] tempg2 = new int[numOfAlt];
            for (int k = 0; k < numUnDec; k++)
            {
                for (int i = 0; i < numUnDec; i++)
                {
                    int temp = 0;
                    for (int j = 0; j < numOfCrit; j++)
                    {
                        if (curentAlter[j, k] <= curentAlter[j, i])
                        {
                            temp++;
                        }
                    }
                    if (temp == numOfCrit) tempg2[k]++;
                }
                tempg2[k] -= 1;
            }

            return tempg2;
        }

        private static int[] CalculateTemp_g1(int numOfCrit, int numOfAlt, int numUnDec, int[,] curentAlter)
        {
            int[] tempg1 = new int[numOfAlt];

            for (int k = 0; k < numUnDec; k++)
            {
                for (int i = 0; i < numUnDec; i++)
                {
                    int temp = 0;
                    for (int j = 0; j < numOfCrit; j++)
                    {
                        if (curentAlter[j, k] >= curentAlter[j, i])
                        {
                            temp++;
                        }
                    }
                    if (temp == numOfCrit) tempg1[k]++;
                }
                tempg1[k] -= 1;
            }

            return tempg1;
        }

        private static int[,] CalculateCurentAlter(int[,] alter, int numOfCrit, int numOfAlt, int[] G, int numUnDec)
        {
            int[,] curentAlter = new int[numOfCrit, numUnDec];

            int tempForCur = 0;
            for (int i = 0; i < numOfAlt; i++)
            {
                if (G[i] == 3)
                {
                    for (int j = 0; j < numOfCrit; j++)
                    {
                        curentAlter[j, tempForCur] = alter[j, i];
                    }
                    tempForCur++;
                }
            }

            return curentAlter;
        }

        private static int CalculateNumUnDec(int numOfAlt, int[] G)
        {
            int numUnDec = 0;
            for (int i = 0; i < numOfAlt; i++)
                if (G[i] == 3) numUnDec++;
            return numUnDec;
        }

        private static void Calculate_p2(int numOfAlt, float[] p1, float[] p2)
        {
            for (int i = 0; i < numOfAlt; i++)
            {
                p2[i] = 1 - p1[i];
            }
        }

        private static void Calculate_p1(int numOfAlt, int[] G, float[] d1, float[] d2, float[] p1, float D)
        {
            for (int i = 0; i < numOfAlt; i++)
            {
                if (G[i] == 3)
                    p1[i] = (float)(D - d1[i]) / (D - d1[i] + D - d2[i]);
                else if (G[i] == 1)
                {
                    p1[i] = 1;
                }
                else
                {
                    p1[i] = 0;
                }
            }
        }

        private static void Calculate_d2(int[,] alter, int numOfCrit, int numOfAlt, float[] d2, float[] centr2)
        {
            for (int i = 0; i < numOfAlt; i++)
            {
                float temp = 0;
                for (int j = 0; j < numOfCrit; j++)
                {
                    temp += Math.Abs(alter[j, i] - centr2[j]);
                }
                d2[i] = temp;
            }
        }

        private static void Calculate_d1(int[,] alter, int numOfCrit, int numOfAlt, float[] d1, float[] centr1)
        {
            for (int i = 0; i < numOfAlt; i++)
            {
                float temp = 0;
                for (int j = 0; j < numOfCrit; j++)
                {
                    temp += Math.Abs((float)alter[j, i] - centr1[j]);
                }
                d1[i] = temp;
            }
        }

        private static bool CheckingRestart(int numOfAlt, int[] G)
        {
            bool restart = false;
            for (int i = 0; i < numOfAlt; i++)
            {
                if (G[i] == 3)
                    restart = true;
            }

            return restart;
        }

        private static void FirstlyCalculateG(int numOfAlt, int[] G)
        {
            for (int i = 0; i < numOfAlt; i++)
            {
                G[i] = 3;
            }
            G[0] = 1; G[numOfAlt - 1] = 2;
        }

        private static void FirstlyCalculateCentr2(int[,] alter, int numOfCrit, int numOfAlt, float[] centr2)
        {
            for (int i = 0; i < numOfCrit; i++)
            {
                centr2[i] = alter[i, numOfAlt - 1];
            }
        }

        private static void FirtstlyCalculateCentr1(int numOfCrit, float[] centr1)
        {
            for (int i = 0; i < numOfCrit; i++)
            {
                centr1[i] = 1;
            }
        }
    }
}
