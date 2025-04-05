using System;
using System.Collections.Generic;

class Grafo
{
    private int[,] matrizAdyacencia;
    private int numNodos;
    private const int INF = int.MaxValue;

    public Grafo(int n)
    {
        numNodos = n;
        matrizAdyacencia = new int[n, n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                matrizAdyacencia[i, j] = (i == j) ? 0 : INF;
    }

    public void AgregarArista(int origen, int destino, int peso)
    {
        if (origen >= 0 && origen < numNodos && destino >= 0 && destino < numNodos)
            matrizAdyacencia[origen, destino] = peso;
    }

    public void MostrarMatriz()
    {
        Console.WriteLine("Matriz de Adyacencia:");
        for (int i = 0; i < numNodos; i++)
        {
            for (int j = 0; j < numNodos; j++)
            {
                if (matrizAdyacencia[i, j] == INF)
                    Console.Write("INF".PadLeft(5));
                else
                    Console.Write(matrizAdyacencia[i, j].ToString().PadLeft(5));
            }
            Console.WriteLine();
        }
    }

    public void Dijkstra(int origen)
    {
        int[] distancias = new int[numNodos];
        bool[] visitado = new bool[numNodos];

        for (int i = 0; i < numNodos; i++)
        {
            distancias[i] = INF;
            visitado[i] = false;
        }

        distancias[origen] = 0;

        for (int contador = 0; contador < numNodos - 1; contador++)
        {
            int u = MinDistancia(distancias, visitado);
            if (u == -1) break;
            visitado[u] = true;

            for (int v = 0; v < numNodos; v++)
            {
                if (!visitado[v] && matrizAdyacencia[u, v] != INF && distancias[u] != INF &&
                    distancias[u] + matrizAdyacencia[u, v] < distancias[v])
                {
                    distancias[v] = distancias[u] + matrizAdyacencia[u, v];
                }
            }
        }

        Console.WriteLine("\nDistancias mínimas desde el nodo " + origen + ":");
        for (int i = 0; i < numNodos; i++)
        {
            if (distancias[i] == INF)
                Console.WriteLine($"Nodo {i}: INF");
            else
                Console.WriteLine($"Nodo {i}: {distancias[i]}");
        }
    }

    private int MinDistancia(int[] distancias, bool[] visitado)
    {
        int min = INF, minIndex = -1;
        for (int v = 0; v < numNodos; v++)
        {
            if (!visitado[v] && distancias[v] <= min)
            {
                min = distancias[v];
                minIndex = v;
            }
        }
        return minIndex;
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Ingrese el número de nodos: ");
        int n = int.Parse(Console.ReadLine());
        Grafo grafo = new Grafo(n);

        while (true)
        {
            Console.WriteLine("\n1. Agregar arista");
            Console.WriteLine("2. Mostrar matriz de adyacencia");
            Console.WriteLine("3. Ejecutar Dijkstra");
            Console.WriteLine("4. Salir");
            Console.Write("Seleccione una opción: ");
            int opcion = int.Parse(Console.ReadLine());

            if (opcion == 1)
            {
                Console.Write("Ingrese nodo origen: ");
                int origen = int.Parse(Console.ReadLine());
                Console.Write("Ingrese nodo destino: ");
                int destino = int.Parse(Console.ReadLine());
                Console.Write("Ingrese peso: ");
                int peso = int.Parse(Console.ReadLine());
                grafo.AgregarArista(origen, destino, peso);
            }
            else if (opcion == 2)
            {
                grafo.MostrarMatriz();
            }
            else if (opcion == 3)
            {
                Console.Write("Ingrese nodo origen para Dijkstra: ");
                int origen = int.Parse(Console.ReadLine());
                grafo.Dijkstra(origen);
            }
            else if (opcion == 4)
            {
                break;
            }
            else
            {
                Console.WriteLine("Opción inválida.");
            }
        }
    }
}
