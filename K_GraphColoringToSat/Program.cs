using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace K_GraphColoringToSat
{
    class GraphToSat {
        
        public string reduce_graphColorToSat(string lineInd,  int iNumbVertex ,  int iNumbColor, int iNumbEdge, int[,] lsMatrixGraph , int u, int v  )
        {

            string sRes = "";
            if (lineInd.Contains("p"))
            {
                int counter = 1;
                for (int i = 1; i <= iNumbVertex; i++)
                {
                    for (int j = 1; j <= iNumbColor; j++)
                    {
                        lsMatrixGraph[i, j] = counter;
                        counter++;

                    }
                    
                }


                sRes += "p cnf " + (iNumbVertex * iNumbColor).ToString() + " " + (iNumbVertex + iNumbVertex * iNumbColor * (iNumbColor - 1) / 2 + iNumbColor * iNumbEdge).ToString();

                //at least one color for vertex
                for (int i = 1; i <= iNumbVertex; i++)
                {
                    for (int j = 1; j <= iNumbColor; j++)
                    {


                        sRes += "" + lsMatrixGraph[i, j].ToString() + " ";


                    }

                    sRes += "0" + Environment.NewLine ;
                }
              

                //at most one color for vertex
                for (int i = 1; i <= iNumbVertex; i++)
                {
                    for (int j = 1; j <= iNumbColor - 1; j++)
                    {
                        for (int l = j + 1; l <= iNumbColor; l++)
                        {
                            sRes += "-" + lsMatrixGraph[i, j].ToString() + " -" + lsMatrixGraph[i, l].ToString() + " 0" + Environment.NewLine;


                        }

                    }
                }
            }
            //for (u,m) that are in E(G)
            if (lineInd.Contains("e"))
            {
              
                for (int l = 1; l <= 2; l++)
                    {
                            sRes += "-" + lsMatrixGraph[u, l] + " -" + lsMatrixGraph[v, l] + " 0" + Environment.NewLine;

                     }
               
            }
            return sRes;
        }
    
    }

    class Program
    {
      

        static void Main(string[] args)
        {

        
            int iNumbVertex =0;
             int iNumbColor=3;
            int iNumbEdge=0;
            int[,] lsMatrixGraph = null;
            string line;
            string res = "";
            GraphToSat qn = new GraphToSat();
            Console.WriteLine("Info: The number of coloring graphs has not been parameterized. ");
            Console.WriteLine("Enter graph:");

            do
            {
                line = Console.ReadLine();
                if (line.StartsWith("p"))
                {
                    string[] lsElementLine = line.Split(' ');
                    iNumbVertex = Int32.Parse(lsElementLine[2]);
                    iNumbEdge = Int32.Parse(lsElementLine[3]);
              
                    lsMatrixGraph = new int[iNumbVertex + 1, iNumbColor + 1];
                    res += qn.reduce_graphColorToSat("p", iNumbVertex, iNumbColor, iNumbEdge, lsMatrixGraph, 0, 0);

                }
                if (line.StartsWith("e"))
                {
                    string[] lsElementLine = line.Split(' ');
                    int iFirstEdge = Int32.Parse(lsElementLine[1]);
                    int iSecondEdge = Int32.Parse(lsElementLine[2]);
                    res += qn.reduce_graphColorToSat("e", iNumbVertex, iNumbColor, iNumbEdge, lsMatrixGraph, iFirstEdge, iSecondEdge);

                }

            } while (line  != null && line != "");

            System.IO.File.WriteAllText(@"C:\Users\Public\Documents\output.txt", res);


            Console.WriteLine("finish");
            Thread.Sleep(19000);

           
        }

        }
    }

