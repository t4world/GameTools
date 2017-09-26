using UnityEngine;
using System.Collections;
using System;

namespace zyz
{
    public class GccMath
    {

        
    }

    public class GccRandom
    {
        /* Period parameters */  
        private const uint CMATH_N = 624;
        private const uint CMATH_M = 397;
        private const uint CMATH_MATRIX_A   = 0x9908b0df;  /* constant vector a */
        private const uint CMATH_UPPER_MASK = 0x80000000;/* most significant w-r bits */
        private const uint CMATH_LOWER_MASK= 0x7fffffff;/* least significant r bits */

        /* Tempering parameters */   
        private const uint CMATH_TEMPERING_MASK_B = 0x9d2c5680;
        private const uint CMATH_TEMPERING_MASK_C = 0xefc60000;
        private ulong CMATH_TEMPERING_SHIFT_U(ulong y)
        {
            return (y >> 11);
        }

        private ulong CMATH_TEMPERING_SHIFT_S(ulong y)
        {
            return (y << 7);
        }

        private ulong CMATH_TEMPERING_SHIFT_T(ulong y)
        {
            return (y << 15);
        }

        private ulong CMATH_TEMPERING_SHIFT_L(ulong y)
        {
            return (y >> 18);
        }

        private uint rseed = 1;
	    private uint rseed_sp = 0;
        private ulong[] mt = new ulong[CMATH_N]; /* the array for the state vector  */
	    private int mti = (int)CMATH_N + 1; /* mti==N+1 means mt[N] is not initialized */
        private ulong[] mag01 = { 0x0, CMATH_MATRIX_A };
        // Returns a number from 0 to n (excluding n)
        public uint GetRandom( uint n)
        {
            ulong y;
            if(n==0)
	            return(0);
            /* mag01[x] = x * MATRIX_A  for x=0,1 */

            if (mti >= CMATH_N) { /* generate N words at one time */
                int kk;

                if (mti == CMATH_N+1)   /* if sgenrand() has not been called, */
                    SetRandomSeed(4357); /* a default initial seed is used   */

                for (kk = 0;kk < CMATH_N-CMATH_M;kk++) {
                    y = (mt[kk] & CMATH_UPPER_MASK) | (mt[kk+1] & CMATH_LOWER_MASK);
                    mt[kk] = mt[kk+CMATH_M] ^ (y >> 1) ^ mag01[y & 0x1];
                }
                for (;kk < CMATH_N-1;kk++) {
                    y = (mt[kk] & CMATH_UPPER_MASK)|(mt[kk+1] & CMATH_LOWER_MASK);
                    mt[kk] = mt[kk + CMATH_M - CMATH_N] ^ (y >> 1) ^ mag01[y & 0x1];
                }
                y = (mt[CMATH_N-1] & CMATH_UPPER_MASK) | (mt[0] & CMATH_LOWER_MASK);
                mt[CMATH_N-1] = mt[CMATH_M-1] ^ (y >> 1) ^ mag01[y & 0x1];

                mti = 0;
            }
  
            y = mt[mti++];
            y ^= CMATH_TEMPERING_SHIFT_U(y);
            y ^= CMATH_TEMPERING_SHIFT_S(y) & CMATH_TEMPERING_MASK_B;
            y ^= CMATH_TEMPERING_SHIFT_T(y) & CMATH_TEMPERING_MASK_C;
            y ^= CMATH_TEMPERING_SHIFT_L(y);

            // ET - old engine added one to the result.
            // We almost NEVER wanted to use this function
            // like this.  So, removed the +1 to return a 
            // range from 0 to n (not including n).
            return (uint)(y % n );
        }


        public float GetRandom( )
        {
            float r = (float)GetRandom(Int32.MaxValue);
            float divisor = (float)Int32.MinValue;
            return (r / divisor);
        }



        public void SetRandomSeed(uint n)
        {
            /* setting initial seeds to mt[N] using         */
            /* the generator Line 25 of Table 1 in          */
            /* [KNUTH 1981, The Art of Computer Programming */
            /*    Vol. 2 (2nd Ed.), pp102]                  */
            mt[0]= n & 0xffffffff;
            for (mti=1; mti<CMATH_N; mti++)
	            mt[mti] = (69069 * mt[mti-1]) & 0xffffffff;

            rseed = n;
        }

        public  uint GetRandomSeed()
        {
            return(rseed);
        }

        public void Randomize()
        {
            uint time = (uint)(DateTime.Now - (new  DateTime(1970,1,1))).Seconds;
            SetRandomSeed(time);
        }

    }
    //质数搜索
    /******************************************************************
    This class enables you to visit each and every member of an array
    exactly once in an apparently random order.

    An application of this algorithim would be a good pixel fade in/fade out
    where each pixel of the frame buffer was set to black one at a time.

    Here's how you would implement a pixel fade using this class:

    void FadeToBlack(Screen *screen)
    {
	    int w = screen.GetWidth();
	    int h = screen.GetHeight();

	    int pixels = w * h;

	    PrimeSearch search(pixels);

	    int p;

	    while((p=search.GetNext())!=-1)
	    {
		    int x = p % w;
		    int y = h / p;

		    screen.SetPixel(x, y, BLACK);

		    // of course, you wouldn't blit every pixel change.
		    screen.Blit();
	    }
    }


    NOTE: If you want the search to start over at the beginning again - 
    you must call the Restart() method, OR call GetNext(true).

    ********************************************************************/
    public class PrimeSearch
    {
        
	    int skip;
	    int currentPosition;
	    int maxElements;
	    int currentPrime;
	    int searches;
        int[] prime_array = 
        {
            2, 3, 5, 7, 
	        11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 
	        53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 
	        101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 
	        151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199, 
	        211, 223, 227, 229, 233, 239, 241,
	        251, 257, 263, 269, 271, 277, 281, 283, 293, 
	        307, 311, 313, 317, 331, 337, 347, 349, 
	        353, 359, 367, 373, 379, 383, 389, 397, 
	        401, 409, 419, 421, 431, 433, 439, 443, 449, 
	        457, 461, 463, 467, 479, 487, 491, 499, 
	        503, 509, 521, 523, 541, 547, 
	        557, 563, 569, 571, 577, 587, 593, 599, 
	        601, 607, 613, 617, 619, 631, 641, 643, 647, 
	        653, 659, 661, 673, 677, 683, 691, 
	        701, 709, 719, 727, 733, 739, 743, 
	        751, 757, 761, 769, 773, 787, 797, 
	        809, 811, 821, 823, 827, 829, 839, 
	        853, 857, 859, 863, 877, 881, 883, 887, 
	        907, 911, 919, 929, 937, 941, 947, 
	        953, 967, 971, 977, 983, 991, 997, 

	        // after 1000, for space efficiency reasons, 
	        // we choose to include fewer prime numbers the bigger the number get.

	        1009, 1051, 1103, 1151, 1201, 1259, 1301, 1361, 1409, 1451, 
	        1511, 1553, 1601, 1657, 1709, 1753, 1801, 1861, 1901, 1951, 
	        2003, 2053, 2111, 2113, 2153, 2203, 2251, 
	        2309, 2351, 2411, 2459, 2503, 2551, 2557, 
	        2609, 2657, 2707, 2753, 2767, 2801, 2851, 2903, 2953,
	        3001, 3061, 3109, 3163, 3203, 3251, 3301, 3359, 3407, 3457, 
	        3511, 3557, 3607, 3659, 3701, 3761, 3803, 3851, 3907, 3967, 
	        4001, 4051, 4111, 4153, 4201, 4253, 4327, 4357, 4409, 4451, 
	        4507, 4561, 4603, 4651, 4703, 4751, 4801, 4861, 4903, 4951, 

	        // begin to skip even more primes

	        5003, 5101, 5209, 5303, 5407, 5501, 5623, 5701, 5801, 5903, 
	        6007, 6101, 6211, 6301, 6421, 6521, 6607, 6701, 6803, 6907, 
	        7001, 7103, 7207, 7307, 7411, 7507, 7603, 7703, 7817, 7901, 
	        8009, 8101, 8209, 8311, 8419, 8501, 8609, 8707, 8803, 8923, 
	        9001, 9103, 9203, 9311, 9403, 9511, 9601, 9719, 9803, 9901, 

	        // and even more
	        10007, 10501, 11003, 11503, 12007, 12503, 13001, 13513, 14009, 14503, 
	        15013, 15511, 16033, 16519, 17011, 17509, 18013, 18503, 19001, 19501, 
	        20011, 20507, 21001, 21503, 22003, 22501, 23003, 23509, 24001, 24509

	        // if you need more - go get them yourself!!!!

	        // Create a bigger array of prime numbers by using this web site: 
	        // http://www.rsok.com/~jrm/printprimes.html
        };

        public PrimeSearch(int elements)
        {
            //GCC_ASSERT(elements>0 && "Can't do a PrimeSearch if you have 0 elements to search through, buddy-boy");
	        maxElements = elements;
            System.Random rnd = new System.Random();
	        int a = (rnd.Next() % 13) + 1;
	        int b = (rnd.Next() % 7) + 1;
	        int c = (rnd.Next() % 5) + 1;

	        skip = (a * maxElements * maxElements) + (b * maxElements) + c;
	        skip &= (int)(~0xc0000000);		// this keeps skip from becoming too large....

	        Restart();

	        currentPrime = 0;
            int s = prime_array.Length;

	        // if this GCC_ASSERT gets hit you didn't have enough prime numbers to deal with this number of 
	        // elements. Go back to the web site.
	        //GCC_ASSERT(prime_array[s-1]>maxElements);

            while (prime_array[currentPrime] < maxElements)
	        {
		        currentPrime++;
	        }

            int test = skip % prime_array[currentPrime];
	        if (test == 0)
		        skip++;
        }

	    private void Restart() { currentPosition=0; searches=0; }

        public int GetNext(bool restart = false)
        {
            if (restart)
                Restart();

            if (Done())
                return -1;

            bool done = false;

            int nextMember = currentPosition;

            while (!done)
            {
                nextMember = nextMember + skip;
                nextMember %= prime_array[currentPrime];
                searches++;

                if (nextMember < maxElements)
                {
                    currentPosition = nextMember;
                    done = true;
                }
            }
            return currentPosition;
        }

        private bool Done() { return (searches == prime_array[currentPrime]); }
    }
}


