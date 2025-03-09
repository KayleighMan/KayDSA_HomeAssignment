using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KayDSA_HomeAssignment
{
    public class Program
    {
        public static void Main()
        {
            HashTable<int, string> hashTable = new HashTable<int, string>();
            Random random = new Random();
            Dictionary<int, string> referenceData = new Dictionary<int, string>();

            // Generate and store 100 random key-value pairs
            while (referenceData.Count < 100)
            {
                int key = random.Next(1000, 9999);
                if (!referenceData.ContainsKey(key))
                {
                    referenceData[key] = "Value" + key;
                }
            }

                // Add all key-value pairs to the hash table
                foreach (var kvp in referenceData)
                {
                    hashTable.AddOrUpdate(kvp.Key, kvp.Value);
                }
                Console.WriteLine("Added 100 key-value pairs to the hash table.");

            // Get all keys from the hash table
            List<int> retrievedKeys = hashTable.GetAll();
            if (retrievedKeys.Count == 100 && new HashSet<int>(retrievedKeys).SetEquals(referenceData.Keys))
            {
                Console.WriteLine("Successfully retrieved all 100 unique keys from the hash table.");
            }
            else
            {
                Console.WriteLine("Error: Retrieved keys do not match the original 100 keys.");
            }

            // Verify values
            bool allValuesMatch = true;
                foreach (var key in retrievedKeys)
                {
                    if (!referenceData[key].Equals(hashTable.Get(key)))
                    {
                        allValuesMatch = false;
                        break;
                    }
                }
            if (allValuesMatch)
            {
                Console.WriteLine("Successfully verified all values. All key-value pairs match.");
            }
            else
            {
                Console.WriteLine("Error: Some values do not match the original.");
            }


            //section 2
            Console.WriteLine("");
            Console.WriteLine("Section 2");

            WeightedGraph graph = new WeightedGraph();
            graph.AddEdge("A", "B", 8);
            graph.AddEdge("A", "C", 12);
            graph.AddEdge("B", "C", 13);
            graph.AddEdge("B", "D", 25);
            graph.AddEdge("C", "D", 14);
            graph.AddEdge("B", "E", 9);
            graph.AddEdge("D", "E", 20);
            graph.AddEdge("D", "F", 8);
            graph.AddEdge("E", "F", 19);
            graph.AddEdge("C", "G", 21);
            graph.AddEdge("D", "G", 12);
            graph.AddEdge("D", "H", 12);
            graph.AddEdge("F", "H", 11);
            graph.AddEdge("D", "I", 16);
            graph.AddEdge("G", "I", 11);
            graph.AddEdge("H", "I", 9);

            Console.WriteLine("Finding shortest path from A to H using List-based priority queue:");
            var resultList = graph.DijkstraUsingList("A");
            Console.WriteLine("Shortest path distance to H: " + resultList["H"]);

            Console.WriteLine("\nFinding shortest path from A to H using Hash Table-based priority queue:");
            var resultHashTable = graph.DijkstraUsingHashTable("A");
            Console.WriteLine("Shortest path distance to H: " + resultHashTable["H"]);

            //section 4
            Console.WriteLine("");
            Console.WriteLine("Section 4");

            Random random4 = new Random();
            List<int> randomNumbers = Enumerable.Range(0, 1000).Select(_ => random4.Next(0, 10000)).ToList();

            List<int> sortedByMergeSort = SortingAlgorithm.MergeSort(randomNumbers);
            List<int> sortedByBuiltin = new List<int>(randomNumbers);
            sortedByBuiltin.Sort();

            if (sortedByMergeSort.SequenceEqual(sortedByBuiltin))
            {
                Console.WriteLine("Both sorting methods produced identical results.");
            }
            else
            {
                Console.WriteLine("The sorting results do not match!");
            }

            if (!randomNumbers.SequenceEqual(sortedByMergeSort) && !randomNumbers.SequenceEqual(sortedByBuiltin))
            {
                Console.WriteLine("Original list integrity maintained.");
            }
            else
            {
                Console.WriteLine("Error: The original list was modified.");
            }

        }
    }
}
