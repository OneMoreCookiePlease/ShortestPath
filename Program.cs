using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortestPath
{


    class Program
    {

        class Node
        {

            private List<Node> neighbors;
            private int i, j;
            public int I
            {
                get { return i; }
            }
            public int J
            {
                get { return j; }
            }

            public void addNeighbor(Node g)
            {
                neighbors.Add(g);
            }

            public int getDegree()
            {
                return neighbors.Count();
            }

            public Node(int i, int j)
            {
                neighbors = new List<Node>();
                this.i = i;
                this.j = j;
            }

        }

        class Graph
        {

            private List<Node> nodes;
            private Node start;
            private Node finish;

            public Graph(List<List<char>> matrix)
            {
                nodes = new List<Node>();


                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {

                        if (matrix[i][j] != '#')
                        {
                            Node node = new Node(i, j);
                            if (i!= 0 && matrix[i - 1][j] != '#') node.addNeighbor(findByIndex(i - 1, j));
                            if (i!= 9 && matrix[i + 1][j] != '#') node.addNeighbor(findByIndex(i + 1, j));
                            if (j!= 0 && matrix[i][j - 1] != '#') node.addNeighbor(findByIndex(i, j - 1));
                            if (j!= 9 && matrix[i][j + 1] != '#') node.addNeighbor(findByIndex(i, j + 1));

                            if (matrix[i][j] == 'A') start = findByIndex(i,j);
                            if (matrix[i][j] == 'B') finish = findByIndex(i,j);
                        }

                    }
                }


            }

            private Node findByIndex(int i, int j)
            {
                return nodes.Find((n) => { return n.I == i && n.J == j; });
                //return new Node(i, j);
            }

        }

        static class Dijkstra
        {


            static void Apply(Matrix matrix)
            {
                Graph g = new Graph(matrix.GetMatrix);
                List <Node> _check = new List<Node>();
                



            }

        }

        class Matrix
        {

            private List<List<char>> matrix;

            public List<List<char>> GetMatrix {
                get { return matrix; }
            }

            public void print()
            {
                System.Console.Out.WriteLine(new String('-', 23));

                for (int i = 0; i < 10; i++)
                {
                    System.Console.Out.Write("| ");

                    for (int j = 0; j < 10; j++)
                    {
                        System.Console.Out.Write(matrix[i][j]);
                        System.Console.Out.Write(" ");

                    }

                    System.Console.Out.WriteLine("|");
                }
                System.Console.Out.WriteLine(new String('-', 23));


            }

            public Matrix()
            {
                matrix = new List<List<char>>();
                for (int i = 0; i < 10; i++)
                {
                    matrix.Add(new List<char>());
                    for (int j = 0; j < 10; j++)
                    {
                        matrix[i].Add(' ');
                    }
                }
            }

            public void setStart(int i, int j)
            {
                Console.Out.WriteLine("Setting start point at ({0},{1})", i, j);
                matrix[i][j] = 'A';
            }

            public void setObsticle(int i, int j)
            {
                matrix[i][j] = '#';
            }

            public void setPathTile(int i, int j)
            {
                matrix[i][j] = 'o';
            }

            public void setDestination(int i, int j)
            {
                Console.Out.WriteLine("Setting destionation point at ({0},{1})", i, j);
                matrix[i][j] = 'B';
            }


        }

        static void Main(string[] args)
        {
            Matrix matrix = new Matrix();

            matrix.setStart(3, 2);
            matrix.setDestination(4, 5);
            matrix.setObsticle(3, 3);
            matrix.setObsticle(4, 4);
            matrix.setObsticle(5, 5);
            matrix.setObsticle(2, 2);

            Graph g = new Graph(matrix.GetMatrix);

            matrix.print();
            Console.In.Read();
        }
    }
}
