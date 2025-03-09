using System;
using System.Collections.Generic;
using System.Linq;

namespace KayDSA_HomeAssignment
{
    // Task 1: Implementing a Weighted Undirected Graph using an Adjacency List
    public class WeightedGraph
    {
        // Adjacency List stored in a HashTable
        private HashTable<string, List<(string, int)>> adjacencyList;

        public WeightedGraph()
        {
            adjacencyList = new HashTable<string, List<(string, int)>>();
        }

        public void AddVertex(string vertex)
        {
            try
            {
                adjacencyList.Get(vertex);
            }
            catch (KeyNotFoundException)
            {
                adjacencyList.AddOrUpdate(vertex, new List<(string, int)>());
            }
        }

        public void AddEdge(string vertex1, string vertex2, int weight)
        {
            AddVertex(vertex1);
            AddVertex(vertex2);

            adjacencyList.Get(vertex1).Add((vertex2, weight));
            adjacencyList.Get(vertex2).Add((vertex1, weight)); // Undirected graph
        }

        // Task 2: Dijkstra’s Algorithm using List as a Priority Queue
        public Dictionary<string, int> DijkstraUsingList(string start)
        {
            var distances = new Dictionary<string, int>();
            var priorityQueue = new List<(string, int)>();
            var visited = new HashSet<string>();

            foreach (var vertex in adjacencyList.GetAll())
            {
                distances[vertex] = int.MaxValue;
            }
            distances[start] = 0;
            priorityQueue.Add((start, 0));

            while (priorityQueue.Count > 0)
            {
                priorityQueue.Sort((a, b) => a.Item2.CompareTo(b.Item2)); // Sort by distance
                var (currentVertex, currentDist) = priorityQueue[0];
                priorityQueue.RemoveAt(0);

                if (visited.Contains(currentVertex)) continue;
                visited.Add(currentVertex);

                foreach (var (neighbor, weight) in adjacencyList.Get(currentVertex))
                {
                    int newDistance = currentDist + weight;
                    if (newDistance < distances[neighbor])
                    {
                        distances[neighbor] = newDistance;
                        priorityQueue.Add((neighbor, newDistance));
                    }
                }
            }
            return distances;
        }

        // Task 2: Dijkstra’s Algorithm using Chaining Hash Table as a Priority Queue
        public Dictionary<string, int> DijkstraUsingHashTable(string start)
        {
            var distances = new Dictionary<string, int>();
            var priorityQueue = new HashTable<string, int>();
            var visited = new HashSet<string>();

            foreach (var vertex in adjacencyList.GetAll())
            {
                distances[vertex] = int.MaxValue;
            }
            distances[start] = 0;
            priorityQueue.AddOrUpdate(start, 0);

            while (priorityQueue.GetAll().Count > 0)
            {
                string currentVertex = null;
                int minDistance = int.MaxValue;
                foreach (var key in priorityQueue.GetAll())
                {
                    int distance = priorityQueue.Get(key);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        currentVertex = key;
                    }
                }
                if (currentVertex == null) break;
                priorityQueue.Remove(currentVertex);

                if (visited.Contains(currentVertex)) continue;
                visited.Add(currentVertex);

                foreach (var (neighbor, weight) in adjacencyList.Get(currentVertex))
                {
                    int newDistance = minDistance + weight;
                    if (newDistance < distances[neighbor])
                    {
                        distances[neighbor] = newDistance;
                        priorityQueue.AddOrUpdate(neighbor, newDistance);
                    }
                }
            }
            return distances;
        }
    }

} 