using System;
using System.Collections.Generic;

class Graph
{
    private int V; // Количество вершин
    private List<int>[] adj; // Список рёбер

    // Конструктор
    public Graph(int v)
    {
        V = v;
        adj = new List<int>[V];
        for (int i = 0; i < V; ++i)
            adj[i] = new List<int>();
    }

    // Добавление ребра в граф
    public void AddEdge(int v, int w)
    {
        adj[v].Add(w);
        adj[w].Add(v);
    }

    // Проверка наличия эйлерова цикла в графе
    private bool HasEulerianCycle()
    {
        // Проверка степеней вершин
        for (int i = 0; i < V; ++i)
            if (adj[i].Count % 2 != 0)
                return false;

        return true;
    }

    // Вывод пути обхода
    public void PrintEulerTour()
    {
        if (!HasEulerianCycle())
        {
            Console.WriteLine("Путь обхода не существует.");
            return;
        }

        // Используем стек для хранения пути
        Stack<int> stack = new Stack<int>();
        List<int> tour = new List<int>();

        int v = 0; // Начинаем с первой вершины

        while (stack.Count > 0 || adj[v].Count > 0)
        {
            // Если вершишна имеет смежные вершины
            if (adj[v].Count > 0)
            {
                stack.Push(v);
                int next = adj[v][0]; // Берем первую смежную вершину
                adj[v].Remove(next); // Удаляем ребро
                adj[next].Remove(v);
                v = next; // Переходим к следующей вершине
            }
            else
            {
                // Если вершина не имеет смежных вершин
                tour.Add(v);
                v = stack.Pop(); // Возвращаемся к предыдущей вершине
            }
        }

        // Выводим путь обхода
        Console.WriteLine("Путь обхода:");
        foreach (var vertex in tour)
        {
            Console.Write(vertex + " ");
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // список значений вершин
        Graph graph = new Graph(4);
        graph.AddEdge(0, 1);
        graph.AddEdge(0, 2);
        graph.AddEdge(1, 3);
        graph.AddEdge(2, 3);

        // путь обхода
        graph.PrintEulerTour();
    }
}