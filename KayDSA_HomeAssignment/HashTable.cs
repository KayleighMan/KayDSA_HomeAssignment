using System;
using System.Collections.Generic;

namespace KayDSA_HomeAssignment
{
    public class HashTable<K, V>
    {
        private class HashNode
        {
            public K Key { get; set; }
            public V Value { get; set; }
            public HashNode Next { get; set; }

            public HashNode(K key, V value)
            {
                Key = key;
                Value = value;
                Next = null;
            }
        }

        private List<HashNode>[] buckets;
        private int size;
        private const double MaxLoadFactor = 0.8;
        private const int MaxBucketLength = 3;

        public HashTable(int capacity = 10)
        {
            buckets = new List<HashNode>[capacity];
            size = 0;
        }

        private int GetBucketIndex(K key)
        {
            return Math.Abs(key.GetHashCode()) % buckets.Length;
        }

        public void AddOrUpdate(K key, V value)
        {
            int index = GetBucketIndex(key);
            if (buckets[index] == null)
                buckets[index] = new List<HashNode>();

            foreach (var node in buckets[index])
            {
                if (EqualityComparer<K>.Default.Equals(node.Key, key))
                {
                    node.Value = value;
                    return;
                }
            }

            buckets[index].Add(new HashNode(key, value));
            size++;

            if (buckets[index].Count > MaxBucketLength || (double)size / buckets.Length > MaxLoadFactor)
            {
                Rehash();
            }
        }

        private void Rehash()
        {
            var oldBuckets = buckets;
            buckets = new List<HashNode>[oldBuckets.Length * 2];
            size = 0;

            foreach (var bucket in oldBuckets)
            {
                if (bucket != null)
                {
                    foreach (var node in bucket)
                    {
                        AddOrUpdate(node.Key, node.Value);
                    }
                }
            }
        }

        public V Get(K key)
        {
            int index = GetBucketIndex(key);
            if (buckets[index] != null)
            {
                foreach (var node in buckets[index])
                {
                    if (EqualityComparer<K>.Default.Equals(node.Key, key))
                    {
                        return node.Value;
                    }
                }
            }
            throw new KeyNotFoundException("Key not found.");
        }

        public List<K> GetAll()
        {
            List<K> keys = new List<K>();
            foreach (var bucket in buckets)
            {
                if (bucket != null)
                {
                    foreach (var node in bucket)
                    {
                        keys.Add(node.Key);
                    }
                }
            }
            return keys;
        }

        public V Remove(K key)
        {
            int index = GetBucketIndex(key);
            if (buckets[index] != null)
            {
                for (int i = 0; i < buckets[index].Count; i++)
                {
                    if (EqualityComparer<K>.Default.Equals(buckets[index][i].Key, key))
                    {
                        V value = buckets[index][i].Value;
                        buckets[index].RemoveAt(i);
                        size--;
                        return value;
                    }
                }
            }
            throw new KeyNotFoundException("Key not found.");
        }
    }
}

