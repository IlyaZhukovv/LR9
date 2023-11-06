using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._2
{
    class Graph
    {
        //поле adjacencyList типа List<List<int>>, которое представляет список списков для представления смежности графа. 
        List<List<int>> adjacencyList;

        //Конструктор класса Graph принимает размер графа и инициализирует adjacencyList пустыми списками для каждой вершины графа.
        public Graph(int size)
        {
            adjacencyList = new List<List<int>>();
            for (int i = 0; i < size; i++)
            {
                adjacencyList.Add(new List<int>());
            }
        }
        //Метод AddEdge служит для добавления ребра между двумя вершинами графа. Он принимает два параметра: вершину from и вершину to,
        //и добавляет to в список смежности from, а также добавляет from в список смежности to. 
        public void AddEdge(int from, int to)
        {
            adjacencyList[from].Add(to);
            adjacencyList[to].Add(from);
        }

        //Метод GetNeighbors принимает вершину графа и возвращает список смежных с ней вершин.
        public List<int> GetNeighbors(int vertex)
        {
            return adjacencyList[vertex];
        }
        //Метод PrintGraph выводит на консоль информацию о графе. Он перебирает все вершины графа и для каждой вершины выводит ее номер и список смежных вершин.
        public void PrintGraph()
        {
            for (int i = 0; i < adjacencyList.Count; i++)
            {
                Console.Write($"Вершина {i + 1}: ");
                foreach (var vertex in adjacencyList[i])
                {
                    Console.Write($"{vertex + 1} ");
                }
                Console.WriteLine();
            }
        }

        //Метод FindDistance используется для нахождения минимального расстояния между двумя вершинами графа from и to. 
        public int FindDistance(int from, int to)
        {
            //Сначала метод проверяет, являются ли вершины from и to одинаковыми. Если да, то возвращается 0, так как расстояние от вершины до самой себя равно 0.
            if (from == to)
            {
                return 0;
            }

            //создается стек stack
            Stack<int> stack = new Stack<int>();

            //массив visited для отслеживания посещенных вершин 
            bool[] visited = new bool[adjacencyList.Count];

            //массив distance для хранения расстояния от вершины from до каждой вершины в графе.
            int[] distance = new int[adjacencyList.Count];

            //Исходная вершина from помечается как посещенная и добавляется в стек.
            stack.Push(from);
            visited[from] = true;

            //В цикле, пока стек не пуст, извлекается вершина current из стека.
            while (stack.Count > 0)
            {
                int current = stack.Pop();

                //Для каждого соседа neighbor данной вершины current, проверяется, является ли он непосещенным.
                foreach (int neighbor in adjacencyList[current])
                {
                    //Если соседняя вершина не посещена, то она добавляется в стек, помечается как посещенная, вычисляется расстояние от
                    //исходной вершины from до данной соседней вершины neighbor как расстояние от текущей вершины current + 1.
                    if (!visited[neighbor])
                    {
                        stack.Push(neighbor);
                        visited[neighbor] = true;
                        distance[neighbor] = distance[current] + 1;

                        //Если соседняя вершина равна вершине to, то найдено минимальное расстояние от вершины from до вершины to и это расстояние возвращается.
                        if (neighbor == to)
                        {
                            return distance[to];
                        }
                    }
                }
            }
            //Если после прохода по всем вершинам в графе не удалось достичь вершины to, то возвращается -1, что означает, что пути между этими вершинами не существует.
            return -1;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите размер графа: ");
            int size = Convert.ToInt32(Console.ReadLine());

            Graph graph = GenerateAdjacencyList(size);

            Console.WriteLine("Список смежности для графа G1:");
            graph.PrintGraph();

            Console.Write("Введите вершину от: ");
            int from = Convert.ToInt32(Console.ReadLine()) - 1;

            Console.Write("Введите вершину до: ");
            int to = Convert.ToInt32(Console.ReadLine()) - 1;

            int distance = graph.FindDistance(from, to);

            if (distance == -1)
            {
                Console.WriteLine("Нет пути между указанными вершинами.");
            }
            else
            {
                Console.WriteLine($"Расстояние между вершинами {from + 1} и {to + 1}: {distance}");
            }
        }
        //Функция GenerateAdjacencyList генерирует случайный граф заданного размера size в виде списка смежности.
        private static Graph GenerateAdjacencyList(int size)
        {
            Random r = new Random();
            Graph graph = new Graph(size);

            //двойной цикл, который перебирает все возможные комбинации вершин графа.
            for (int i = 0; i < size; i++)
            {
                for (int j = i + 1; j < size; j++)
                {
                    //Если сгенерированное число равно 1, то вызывается метод AddEdge объекта graph для добавления ребра между вершинами i и j.
                    if (r.Next(2) == 1)
                    {
                        graph.AddEdge(i, j);
                    }
                }
            }
            //После завершения циклов возвращается объект graph, содержащий случайно сгенерированный граф в виде списка смежности.
            return graph;
        }
    }
}
