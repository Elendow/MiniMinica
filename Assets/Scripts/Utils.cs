using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static T Random<T>(this IList<T> list)
    {
        if (list.Count == 0)
        {
            Debug.Log("Cannot select a random item from an empty list");
            return default(T);
        }

        return list[UnityEngine.Random.Range(0, list.Count)];
    }
    public static void Shuffle<T>(this IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}