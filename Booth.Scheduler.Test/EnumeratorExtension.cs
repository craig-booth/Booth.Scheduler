using System;
using System.Collections.Generic;
using System.Collections;

namespace Booth.Scheduler.Test
{
    static class EnumeratorExtension
    {

        public static IEnumerable<T> AsEnumerable<T>(this IEnumerator<T> enumerator)
        {
            while (enumerator.MoveNext())
                yield return enumerator.Current;
        }
    }
}
