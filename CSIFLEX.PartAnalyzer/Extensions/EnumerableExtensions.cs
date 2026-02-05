using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<IList<T>> ChunkOn<T>(this IEnumerable<T> source, Func<T, bool> startChunk)
        {
            List<T> list = new List<T>();

            foreach (var item in source)
            {
                if (startChunk(item) && list.Count > 0)
                {
                    yield return list;
                    list = new List<T>();
                }

                list.Add(item);
            }

            if (list.Count > 0)
            {
                yield return list;
            }
        }

        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
    }
}
