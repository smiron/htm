using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Misc
{
    public static class Extensions
    {
        public static void ForEach<T>(this T[] elements, Action<T> action)
        {
            foreach (var element in elements)
            {
                action(element);
            }
        }

        public static void ForEach<T>(this T[,] elementsMatrix, Action<T> action)
        {
            foreach (var elementsRow in elementsMatrix)
            {
                action(elementsRow);
            }
        }
    }
}
