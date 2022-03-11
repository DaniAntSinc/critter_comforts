using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMill : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MersenneTwister rng = new MersenneTwister(0UL);
        Dictionary<ulong, int> distribution = new Dictionary<ulong, int>();
        ulong max = ulong.MinValue;
        ulong min = ulong.MaxValue;
        ulong modulo;
        for (int i = 0; i < 10_000_000; i++)
        {
            ulong value = rng.Int32();
            if (value > max)
            {
                max = value;
            }
            if (value < min)
            {
                min = value;
            }
            modulo = value % 10;
            if (!distribution.ContainsKey(modulo))
            {
                distribution.Add(modulo, 0);
            }
            distribution[modulo]++;
        }
        Debug.Log("Max Value: " + max);
        Debug.Log("Min Value: " + min);
        Debug.Log("Distribution [0, 10): " + String.Join("\n", distribution));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
