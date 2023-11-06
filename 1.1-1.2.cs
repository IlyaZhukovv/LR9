using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите размер матрицы: ");
            int size = Convert.ToInt32(Console.ReadLine());

            int[,] adjacencyMatrix = GenerateAdjacencyMatrix(size);

            Console.WriteLine("Матрица смежности для графа G1:");
            PrintMatrix(adjacencyMatrix);

            Console.Write("Введите вершину, с которой хотите начать обход: ");
            int start = Convert.ToInt32(Console.ReadLine());

            BFS(adjacencyMatrix, size, start);
        }
        //Метод GenerateAdjacencyMatrix генерирует случайную матрицу смежности для графа.
        private static int[,] GenerateAdjacencyMatrix(int size)
        {
            Random r = new Random();

            int[,] matrix = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i != j)
                    {
                        //для каждой пары вершин (i, j) генерируется случайное число 0 или 1, которое указывает наличие или отсутствие ребра между вершинами.
                        matrix[i, j] = r.Next(2);
                        matrix[j, i] = matrix[i, j];
                    }
                }
            }
            return matrix;
        }
        //Метод PrintMatrix выводит матрицу смежности на экран.
        static void PrintMatrix(int[,] matrix)
        {
            int size = matrix.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        //Метод BFS осуществляет поиск в ширину. Он использует очередь для сохранения вершин, которые нужно посетить. 
        private static void BFS(int[,] adjacencyMatrix, int size, int source)
        {
            //Создается очередь queue
            Queue<int> queue = new Queue<int>();

            //массив visited для отслеживания посещенных вершин
            bool[] visited = new bool[size];

            //массив distance для хранения расстояния от исходной вершины до каждой вершины графа.
            int[] distance = new int[size];

            //Исходная вершина помечается как посещенная и добавляется в очередь.
            visited[source] = true;
            queue.Enqueue(source);

            //Пока очередь не пуста, извлекаем элемент из очереди и перебираем его соседей в матрице смежности.
            while (queue.Count > 0)
            {
                int currentVertex = queue.Dequeue();

                //Перебираем соседей текущей вершины
                for (int i = 0; i < size; i++)
                {
                    //Проверяем, явяется ли вершина с индексом i соседом текущей вершины
                    if (adjacencyMatrix[currentVertex, i] == 1 && !visited[i])
                    {
                        //Помечаем соседнюю вершину как посещенную
                        visited[i] = true;

                        //Устанавливаем расстояние до соседней вершины
                        distance[i] = distance[currentVertex] + 1;

                        //Добавляем соседнюю вершину в очередь
                        queue.Enqueue(i);
                    }
                }
            }

            //Выводим расстояния до всех вершин графа
            Console.WriteLine("Расстояния до вершин графа:");
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine("Расстояние до вершины {0}:{1}", i, distance[i]);
            }
        }
    }
}
