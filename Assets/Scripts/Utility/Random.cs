using System;
using System.Collections;
using System.Collections.Generic;

public class Random
{
    private ulong seed;
    // Used in Box-Muller transform for Gaussian RNG
    private double last;
    private MersenneTwister rng;

    public Random()
    {
        System.Random seeder = new System.Random();
        seed = (ulong)seeder.Next();

        rng = new MersenneTwister(seed);
    }

    public Random(ulong s)
    {
        seed = s;
        rng = new MersenneTwister(seed);
    }

    // The three constructor types are mostly to plan for saving/loading systems.
    // However, a game run will have one seed, which will be stored alongside the component's name.
    // The string-based and one of (ulong, System.Random) should be needed.
    public Random(string s)
    {
        seed = ConvertStringToSeed(s);

        rng = new MersenneTwister(seed);
    }

    public uint GetConsumedRolls()
    {
        return rng.ConsumedRolls;
    }

    private ulong ConvertStringToSeed(string s)
    {
        // The Songbird implementation used CRC32, but this should work equally well for our purposes
        ulong hash = (ulong)seed.GetHashCode();
        // The modulus shouldn't be needed.
        // If something else down the line is breaking, then it should probably be dropped from ulong.MaxValue to something like (2 << 31 - 1) just for familiarity.
        return hash % ulong.MaxValue;
    }

    public double Double(double min = 0f, double max = 1f)
    {
        return min + rng.Real() * (max - min);
    }

    public double Normal(double mean, double sd)
    {
        if (last == default(double))
        {
            last = Double();
        }
        double current = Double();
        double root = sd * Math.Sqrt(-2 * Math.Log(last));
        double zNought = root * Math.Cos(2 * Math.PI * current) + mean;
        double zOne = root * Math.Sin(2 * Math.PI * current) + mean;
        last = current;

        return zOne;
    }

    public double Normal(double mean, double sd, double clampMin)
    {
        return Math.Max(clampMin, Normal(mean, sd));
    }

    public double Normal(double mean, double sd, double clampMin, double clampMax)
    {
        return Math.Min(clampMax, Normal(mean, sd, clampMin));
    }

    public int Normal(int mean, int sd, int? clampMin = null, int? clampMax = null)
    {
        return (int)Normal((double)mean, (double)sd, (double)clampMin, (double)clampMax);
    }

    public int Int(int min = 0, int max = 100)
    {
        // There might be an issue with this value.
        return (int)Double((double)min, (double)max);
    }

    // Returns a random element from list
    // If the list is empty, that would be a problem.
    public T Element<T>(T[] list)
    {
        int key = Int(0, list.Length);
        return list[key];
    }

    /*
     * Functions for: Array/Dict/etc key
     * Choice from array, shorthand for values[Key(values)]
     * Shuffle (using fisher-yates)
     */
}
