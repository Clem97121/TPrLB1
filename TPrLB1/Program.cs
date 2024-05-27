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
            List<int> criterion_values = new List<int>() { 2,2,3,4,1 };

            int Number_of_possible_alternatives = 48;

            int[,] alternatives = new int[criterion_values.Count, Number_of_possible_alternatives];

            int[] prikol = new int[criterion_values.Count];

            PrikolTest(criterion_values, Number_of_possible_alternatives, prikol);

            int count;
            for (int i = 0; i < criterion_values.Count; i++)
            {
                count = 0;
                for (int j = 0; j < Number_of_possible_alternatives / prikol[i]; j++)
                {
                    for (int k = 0; k < prikol[i]; k++)
                    {
                        alternatives[i, count] = (j) % criterion_values[i] + 1;
                        count++;
                    }
                }
            }

            int[,] test = new int[2, 9];
            test[0, 0] = 1; test[1, 0] = 1;
            test[0, 1] = 1; test[1, 1] = 2;
            test[0, 2] = 1; test[1, 2] = 3;
            test[0, 3] = 2; test[1, 3] = 1;
            test[0, 4] = 2; test[1, 4] = 2;
            test[0, 5] = 2; test[1, 5] = 3;
            test[0, 6] = 3; test[1, 6] = 1;
            test[0, 7] = 3; test[1, 7] = 2;
            test[0, 8] = 3; test[1, 8] = 3;

            Print(ref alternatives);

            AlgoritmStart(ref alternatives);



            Console.ReadLine();
        }

        public static void PrikolTest(List<int> criterion_values, int Number_of_possible_alternatives, int[] prikol)
        {
            prikol[0] = Number_of_possible_alternatives / criterion_values[0];

            for (int i = 1; i < criterion_values.Count; i++)
            {
                prikol[i] = prikol[i - 1] / criterion_values[i];
            }
        }

        public static void Print(ref int[,] alter)
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
        public static void AlgoritmStart(ref int[,] alter)
        {
            int NumAl = alter.GetLength(1);
            int NumCrit = alter.GetLength(0);

            //int[] iterClass = new int[15] { 1, 2, 1, 1, 2, 1, 1, 2, 1, 2, 1, 1, 2, 1, 2 };
            int[] iterClass = new int[15] { 2, 2, 2, 1, 2, 1, 1, 2, 1, 2, 1, 1, 2, 1, 2 };

            int[] G = new int[NumAl]; // Инициализируем массив G
            float[] d1 = new float[NumAl];
            float[] d2 = new float[NumAl];
            float[] p1 = new float[NumAl];
            float[] p2 = new float[NumAl];
            int[] g1 = new int[NumAl];
            int[] g2 = new int[NumAl];
            float[] F1 = new float[NumAl];
            float[] F2 = new float[NumAl];
            float[] F = new float[NumAl];

            bool cont = false;

            int NumOfIter = 0;

            float[] centr1 = new float[NumCrit];

            for (int i = 0; i < NumCrit; i++)
            {
                centr1[i] = 1;
            }

            float[] centr2 = new float[NumCrit];

            for (int i = 0; i < NumCrit; i++)
            {
                centr2[i] = alter[i,NumAl-1];
            }

            float D = 0;

            for (int i = 0;i < NumAl; i++)
            {
                G[i] = 3;
            }
            G[0] = 1; G[NumAl - 1] = 2;

            do
            {
                NumOfIter++;
                cont = false;
                for (int i = 0; i < NumAl; i++)
                {
                    if (G[i] == 3)
                        cont = true;
                }

                for (int i = 0; i < NumAl; i++)
                {
                    float temp = 0;
                    for (int j = 0; j < NumCrit; j++)
                    {
                        temp += Math.Abs((float)alter[j, i] - centr1[j]);
                    }
                    d1[i] = temp;
                }
                for (int i = 0; i < NumAl; i++)
                {
                    float temp = 0;
                    for (int j = 0; j < NumCrit; j++)
                    {
                        temp += Math.Abs(alter[j, i] - centr2[j]);
                    }
                    d2[i] = temp;
                }
                D = Math.Max(d1.Max(), d2.Max());
                Console.WriteLine("D = " + D);
                for (int i = 0; i < NumAl; i++)
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
                for (int i = 0; i < NumAl; i++)
                {
                    p2[i] = 1 - p1[i];
                }
                int NumUnDec = 0;
                for (int i = 0; i < NumAl; i++)
                    if (G[i] == 3) NumUnDec++;
                int[,] CurentAlter = new int[NumCrit, NumUnDec];

                int[] tempg1 = new int[NumAl];
                int[] tempg2 = new int[NumAl];

                int tempForCur = 0;
                for (int i = 0; i < NumAl; i++)
                {
                    if (G[i] == 3)
                    {
                        for (int j = 0; j < NumCrit; j++)
                        {
                            CurentAlter[j, tempForCur] = alter[j, i];
                        }
                        tempForCur++;
                    }
                }
                for (int k = 0; k < NumUnDec; k++)
                {
                    for (int i = 0; i < NumUnDec; i++)
                    {
                        int temp = 0;
                        for (int j = 0; j < NumCrit; j++)
                        {
                            if (CurentAlter[j, k] >= CurentAlter[j, i])
                            {
                                temp++;
                            }
                        }
                        if (temp == NumCrit) tempg1[k]++;
                    }
                    tempg1[k] -= 1;
                }
                for (int k = 0; k < NumUnDec; k++)
                {
                    for (int i = 0; i < NumUnDec; i++)
                    {
                        int temp = 0;
                        for (int j = 0; j < NumCrit; j++)
                        {
                            if (CurentAlter[j, k] <= CurentAlter[j, i])
                            {
                                temp++;
                            }
                        }
                        if (temp == NumCrit) tempg2[k]++;
                    }
                    tempg2[k] -= 1;
                }
                int tempForG1 = 0;
                int tempForG2 = 0;
                for (int i = 0; i < NumAl; i++)
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
                for (int i = 0; i < NumAl; i++)
                {
                    F1[i] = p1[i] * g1[i];
                    F2[i] = p2[i] * g2[i];
                    F[i] = F1[i] + F2[i];
                }

                float MaxF = F.Max();
                int indMaxF = 0;

                for (int i = 0;i < NumAl; i++)
                {
                    if (F[i] == MaxF)
                    {
                        indMaxF = i;
                        break;
                    }
                }

                // Printing 

                string str = string.Empty;
                for (int i = 0; i < NumCrit; i++)
                {
                    str += alter[i, i];
                }

                string outFirstStr = "";
                for (int j = 0; j < NumCrit; j++)
                {
                    outFirstStr += "K" + (j + 1) + " ";
                }
                Console.WriteLine(outFirstStr + "G     d1     d2    p1    p2   g1    g2    F1    F2     F");
                for (int i = 0; i < NumAl; i++)
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
                    for (int j = 0; j < NumCrit; j++)
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
                for (int i = 0; i < NumCrit; i++)
                {
                    strCentr1 += centr1[i].ToString() + " ";
                    strCentr2 += centr2[i].ToString() + " ";
                }
                Console.WriteLine("Centr1 = " + strCentr1);
                Console.WriteLine("Centr2 = " + strCentr2);
                Console.WriteLine("Num of Iteration = " + NumOfIter);
                Console.WriteLine();

                G[indMaxF] = iterClass[NumOfIter - 1];

                for (int i = 0; i < NumAl; i++)
                {
                    int temp = 0;
                    for (int j = 0; j < NumCrit; j++)
                    {
                        if(iterClass[NumOfIter - 1] == 1)
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
                    if (temp == NumCrit) G[i] = iterClass[NumOfIter - 1];
                }


                int[] indexSum1 = new int[NumCrit];
                int numOfNewIndex1 = 0;
                int[] indexSum2 = new int[NumCrit];
                int numOfNewIndex2 = 0;
                for (int i = 0; i < NumAl; i++)
                {
                    for (int j = 0;j < NumCrit; j++)
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
                numOfNewIndex1 /= NumCrit;
                numOfNewIndex2 /= NumCrit;
                for (int i = 0; i < NumCrit; i++)
                {
                    centr1[i] = (float)indexSum1[i] / numOfNewIndex1;
                    centr2[i] = (float)indexSum2[i] / numOfNewIndex2;
                }

                } while (cont);
        }

    }
}
