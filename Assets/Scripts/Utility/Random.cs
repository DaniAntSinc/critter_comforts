using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

public class Random
{
    private uint seed;
    // Used in Box-Muller transform for Gaussian RNG
    private ulong? last;
    private MersenneTwister rng;

    public Random(string s = null)
    {
        // TODO Resolve namespace issues
        seed = 1;
        if (s != null)
        {
            // seed = hexdec(hash("crc32", s)) % (2 << 31 - 1);
        }

        last = null;
        rng = new MersenneTwister(seed);
    }

    public float Float(float min = 0f, float max = 1f)
    {
        return 0.0f;
    }

    public float Normal(float mean, float sd, float? clampMin = null, float? clampMax = null)
    {
        // stuff
        return 0.0f;
    }

    public float Normal(int mean, int sd, int? clampMin = null, int? clampMax = null)
    {
        return 0.0f;
    }

    public int Int(int min, int max)
    {
        return 0;
    }

    /*
     * Functions for: Array/Dict/etc key
     * Choice from array, shorthand for values[Key(values)]
     * Shuffle (using fisher-yates)
     */
}
