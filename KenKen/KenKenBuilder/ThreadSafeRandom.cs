using System;

namespace KenKenBuilder
{
    public class ThreadSafeRandom
    {
        private static readonly Random Global = new Random();
        [ThreadStatic] private static Random _local;

        public ThreadSafeRandom()
        {
            if (_local == null)
            {
                int seed;
                lock (Global)
                {
                    seed = Global.Next();
                }
                _local = new Random(seed);
            }
        }

        public int Next()
        {
            return _local.Next();
        }

        public double NextDouble()
        {
            return _local.NextDouble();
        }
    }
}
