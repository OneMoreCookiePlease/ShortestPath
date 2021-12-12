using System;
using System.Collections.Generic;
using System.Linq;

namespace ShortestPath
{


    class Program
    {

        class Node
        {

            private List<Node> neighbors;
            private int i, j;


            public List<Node> Neighbors
            {
                get { return neighbors; }
            }
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

            public override string ToString()
            {
                return string.Format("({0}, {1})", i, j);
            }

        }

        class Graph
        {

            private List<Node> nodes;
            private Tuple<int, int> start;
            private Tuple<int, int> finish;

            public Node Start
            {
                get { return findByIndex(start.Item1, start.Item2); }
            }

            public Node Finish
            {
                get { return findByIndex(finish.Item1, finish.Item2); }
            }

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
                            if (matrix[i][j] == 'A') start = new Tuple<int, int>(i, j);
                            if (matrix[i][j] == 'B') finish = new Tuple<int, int>(i, j);
                            nodes.Add(node);
                        }

                    }
                }

                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (matrix[i][j] != '#')
                        {
                            Node node = findByIndex(i, j);
                            if (i != 0 && matrix[i - 1][j] != '#') node.addNeighbor(findByIndex(i - 1, j));
                            if (i != 9 && matrix[i + 1][j] != '#') node.addNeighbor(findByIndex(i + 1, j));
                            if (j != 0 && matrix[i][j - 1] != '#') node.addNeighbor(findByIndex(i, j - 1));
                            if (j != 9 && matrix[i][j + 1] != '#') node.addNeighbor(findByIndex(i, j + 1));
                        }
                    }
                }


            }

            private Node findByIndex(int i, int j)
            {
                Node node = nodes.Find((n) => { return n.I == i && n.J == j; });
                if (node == null) return new Node(i, j);
                else return node;
            }

        }

        static class Dijkstra
        {
            static public void Apply(Matrix matrix)
            {
                Graph g = new Graph(matrix.GetMatrix);
                Dictionary<Node, Tuple<Node, int>> distance = new Dictionary<Node, Tuple<Node, int>>(); //(actual node, (node added by, distance))
                List<Node> check = new List<Node>();

                distance.Add(g.Start, new Tuple<Node, int>(null, 0));

                while (!distance.ContainsKey(g.Finish))
                {

                    Node node = distance.OrderBy(n => n.Value.Item2).Where((n) => { return !check.Contains(n.Key); }).FirstOrDefault().Key;
                    foreach (Node item in node.Neighbors.SkipWhile((n) => { return check.Contains(n); }))
                    {
                        if (distance.ContainsKey(item))
                        {
                            if (distance[item].Item2 > distance[node].Item2 + 1)
                            {
                                distance[item] = new Tuple<Node, int>(node, distance[node].Item2 + 1);
                            }

                        }
                        else
                        {
                            distance[item] = new Tuple<Node, int>(node, distance[node].Item2 + 1);
                        }

                    }
                    check.Add(node);

                }

                Node curr = distance.Last().Key;
                curr = distance[curr].Item1;
                while (curr != g.Start)
                {
                    matrix.GetMatrix[curr.I][curr.J] = '~';
                    curr = distance[curr].Item1;
                }

                Console.Out.WriteLine("Shortest path from A to B takes {0} steps", distance[g.Finish].Item2);
            }

        }

        class Matrix
        {

            private List<List<char>> matrix;

            public List<List<char>> GetMatrix
            {
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

            public void setObstacle(int i, int j)
            {
                matrix[i][j] = '#';
            }

            public void setPathTile(int i, int j)
            {
                matrix[i][j] = 'o';
            }

            public void setDestination(int i, int j)
            {
                Console.Out.WriteLine("Setting destination point at ({0},{1})", i, j);
                matrix[i][j] = 'B';
            }


        }

        static void Main(string[] args)
        {
            Matrix matrix = new Matrix();

            matrix.setStart(4, 0);
            matrix.setDestination(5, 9);
            matrix.setObstacle(3, 3);
            matrix.setObstacle(4, 4);
            matrix.setObstacle(5, 5);
            matrix.setObstacle(2, 2);
            matrix.setObstacle(9, 5);
            matrix.setObstacle(5, 0);
            matrix.setObstacle(4, 1);

            Dijkstra.Apply(matrix);

            matrix.print();
            Console.In.Read();
        }
    }
}
