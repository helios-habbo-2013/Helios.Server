using System.Linq;

namespace System.Collections.Generic
{
    public static class List
    {
        public static List<T> Create<T>(params T[] values)
        {
            return new List<T>(values);
        }

        public static List<List<T>> Batch<T>(this List<T> originalList, int chunkSize)
        {
            if (chunkSize <= 0)
                throw new ArgumentException("Chunk size must be greater than zero.", nameof(chunkSize));

            var listOfChunks = new List<List<T>>();

            for (int i = 0; i < originalList.Count / chunkSize; i++)
            {
                listOfChunks.Add(originalList.GetRange(i * chunkSize, chunkSize));
            }

            // Handle the remaining items
            int remainder = originalList.Count % chunkSize;
            if (remainder != 0)
            {
                listOfChunks.Add(originalList.GetRange(originalList.Count - remainder, remainder));
            }

            return listOfChunks;
        }
    }
}
