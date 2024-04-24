using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extentions
{
    public static void Shuffle<T>(this List<T> _input)
    {
        for (int i = 0; i < _input.Count; i++)
        {
            T temp = _input[i];

            int random_index = Random.Range(0, _input.Count);
            _input[i] = _input[random_index];
            _input[random_index] = temp;
        }
    }
}
