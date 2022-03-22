using System.Collections;

public class MersenneTwister
{
    private const int N = 624;
    private const int M = 397;
    private const ulong MATRIX_A = 0x9908_B0DFUL;
    private const ulong UPPER_MASK = 0x8000_0000UL;
    private const ulong LOWER_MASK = 0x7FFF_FFFFUL;

    private ulong[] mt;
    private ulong mti;

    // Track the number of uses for consistency after storage
    public uint ConsumedRolls { get; private set; }

    public MersenneTwister(ulong seed)
    {
        ConsumedRolls = 0;

        mt = new ulong[N];
        mt[0] = seed & 0xFFFF_FFFFUL;
        for (mti = 1; mti < N; mti++)
        {
            mt[mti] = (1812433253UL * (mt[mti-1] ^ (mt[mti-1] >> 30)) + mti);
            mt[mti] &= 0xFFFF_FFFFUL;
        }
    }

    public ulong Int32()
    {
        ulong y;
        ulong[] mag = new ulong[] { 0x0UL, MATRIX_A };
        /* mag01[x] = x * MATRIX_A  for x=0,1 */

        if (mti >= N)
        { /* generate N words at one time */
            int kk;

            for (kk = 0; kk < N - M; kk++)
            {
                y = (mt[kk] & UPPER_MASK) | (mt[kk + 1] & LOWER_MASK);
                mt[kk] = mt[kk + M] ^ (y >> 1) ^ mag[y & 0x1UL];
            }
            for (; kk < N - 1; kk++)
            {
                y = (mt[kk] & UPPER_MASK) | (mt[kk + 1] & LOWER_MASK);
                mt[kk] = mt[kk + (M - N)] ^ (y >> 1) ^ mag[y & 0x1UL];
            }
            y = (mt[N - 1] & UPPER_MASK) | (mt[0] & LOWER_MASK);
            mt[N - 1] = mt[M - 1] ^ (y >> 1) ^ mag[y & 0x1UL];

            mti = 0;
        }

        y = mt[mti++];

        /* Tempering */
        y ^= (y >> 11);
        y ^= (y << 7) & 0x9d2c5680UL;
        y ^= (y << 15) & 0xefc60000UL;
        y ^= (y >> 18);

        ConsumedRolls++;
        return y;
    }

    /* generates a random number on [0,1)-real-interval */
    public double Real()
    {
        return Int32() * (1.0 / 4294967296.0);
        /* divided by 2^32 */
    }
}