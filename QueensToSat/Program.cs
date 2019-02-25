using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace QueensToSat
{
    class Program
    {

            static void Main(string[] args)
        {
            Console.WriteLine("Enter n:");
            int n =Int32.Parse( Console.ReadLine());
            Qeens qn = new Qeens(n);
            Console.WriteLine(qn.reduce_nq_sat());

            Thread.Sleep(190000);

            // for rows 


        }
    }

    class Qeens {

        public Qeens(int n) {
            dimMatrix = n;
        }

        int dimMatrix = 0;

        public List<Int32> lsQueensPosition = new List<int>();

        public Dictionary<string, string> mapStrToStr(int n) {

            string s = "";
            int counter = 0;
            Dictionary<string, string> dictMapMatrix = new Dictionary<string, string>();
            for (int i = 1; i <= n; i++) {
                for (int j = 1; j <= n; j++)
                {
                    counter += 1;
                    s = "X" + i.ToString() + j.ToString();
                    dictMapMatrix.Add(s, counter.ToString());


                }
            }
            return dictMapMatrix;
        }

        public  int CountStringOccurrences(string text, string pattern)
        {
            // Loop through all instances of the string 'text'.
            int count = 0;
            int i = 0;
            while ((i = text.IndexOf(pattern, i)) != -1)
            {
                i += pattern.Length;
                count++;
            }
            return count;
        }

        public string convertToDIMACS(string sSatformat, int n) {

            Dictionary<string, string> dictMap = mapStrToStr(n);

            int closures = CountStringOccurrences(sSatformat, "(");
            string res = "p cnf " + n.ToString() + " " + closures.ToString() + Environment.NewLine;
            StringBuilder b = new StringBuilder(sSatformat);
            foreach (KeyValuePair<string, string> pair in dictMap)
            {
                b.Replace(pair.Key, pair.Value);

            }

            b.Replace("!", "-");
            b.Replace(",", Environment.NewLine);
            b.Replace("V", " ");
            b.Replace(")", " 0 ");
            b.Replace("( ", string.Empty);


            return res + b.ToString();
            


        }

        public string FindPairDiagonal(int i, int k , int n , string sfinal )
        {
            string s = " ";
            for (int j = 1; j <= n ; j++)
            {
                for (int l = 1; l <= n; l++)
                {
                    bool a = (j + l == i + k) && (i != j || k != l) ;
                    while (a)
                    {
                        if (sfinal.Contains("X" + j.ToString() + l.ToString() + "V" + "!X" + i.ToString() + k.ToString()) == false)
                        { s += "( !X" + i.ToString() + k.ToString() + "V" + "!X" + j.ToString() + l.ToString() + ") ,"; }

                        a = false;

                    }

                }
            }
            return s;

        }


        
        public string reduce_nq_sat()
        {
            int n = dimMatrix;
            string closures = "";
            //rows initial 
            for (int i = 1; i <= n; i++)
            {
                closures += "( ";
                for (int j = 1; j <= n; j++)
                {
                    closures += "X" + i.ToString() + j.ToString() + " ";
                }
                closures += ") ,";
            }
          

            //rows at least one 
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    for (int k = 1; k <= n && k != j; k++)
                    {
                        closures += "( !X" + i.ToString() + j.ToString() + "V" + "!X" + i.ToString() + k.ToString() + ") ,";
                    }
                }
            }

            //columns initial 
            for (int i = 1; i <= n; i++) {
                closures += "( ";
                for (int j =1; j<= n; j ++)
                {
                
                    closures += "X" + j.ToString() + i.ToString() + " ";
                    
                }
                closures += ") , ";
            }
          


            //columns at least one 
            for (int i = 1; i <= n; i++)
            {
               
                for (int j = 1; j <= n; j++)
                {
                    for (int k = 1; k <= n && k != j; k++)
                    {
                        closures += "( !X" + j.ToString() + i.ToString() + "V" + "!X" + k.ToString() + i.ToString() + ") ,";
                    }
                }
            }
 
            //main diagonal 
            for (int i = 1; i < n; i++)
            {
                for (int j = i + 1; j <= n; j++)
                {
                    closures += "( !X" + i.ToString() + i.ToString() + "V" + "!X" + j.ToString() + j.ToString() + ") ,";
                }
            }

           

            //diagonal left  and right triangle
            for (int i = 1; i < n - 1; i++)
            {
                for (int j = 1; (j < n + 1 - i); j++)
                {

                    for (int k = i + 1;( k < n + 1 - i); k++)
                    {
                        bool a = (j != k);
                       while (a){
                            closures += "( !X" + (j + i).ToString() + (j).ToString() + "V" + "!X" + (k + i).ToString() + k.ToString() + "),";
                            closures += "( !X" + (j).ToString() + (j + i).ToString() + "V" + "!X" + k.ToString() + (k + i).ToString() + "),";
                            a = false;
                        }
                    }
                }
            }
           
            //diagonal up trangle
            for (int i = 1; i < n; i++)
            {
                for (int k = n; k > 1; k--)
                {
                    string s = FindPairDiagonal(i, k, n, closures);
                    closures += s;

                }
            }

            return convertToDIMACS(closures  , n);



        }

        

    }
}
