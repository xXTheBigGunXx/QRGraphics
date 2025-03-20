using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace QRGraphics
{
    public static class PolyDivition
    {
        private static readonly int[] GF256_EXP = new int[512];
        private static readonly int[] GF256_LOG = new int[256];

        static PolyDivition()
        {
            // Initialize GF(2^8) lookup tables using a primitive polynomial
            int x = 1;
            for (int i = 0; i < 255; i++)
            {
                GF256_EXP[i] = x;
                GF256_LOG[x] = i;
                x <<= 1;
                if (x >= 256)
                    x ^= 0x11D; // Irreducible polynomial for GF(2^8)
            }
            for (int i = 255; i < 512; i++)
                GF256_EXP[i] = GF256_EXP[i - 255];
        }

        public static List<int> DividePolynomials(List<int> dividend, List<int> divisor, int err)
        {
            // Step 1: Multiply the message polynomial by x^n (pad with zeros)
            List<int> remainder = new List<int>(dividend);
            for (int i = 0; i < err; i++)
                remainder.Add(0);

            // Perform polynomial division in GF(2^8)
            for (int i = 0; i <= remainder.Count - divisor.Count; i++)
            {
                int leadTerm = remainder[i];
                if (leadTerm != 0)
                {
                    for (int j = 0; j < divisor.Count; j++)
                    {
                        remainder[i + j] ^= GFMul(divisor[j], leadTerm);
                    }
                }
            }

            // Extract the remainder (last 'err' elements)
            return remainder.GetRange(remainder.Count - err, err);
        }

        // GF(2^8) Multiplication
        private static int GFMul(int a, int b)
        {
            if (a == 0 || b == 0)
                return 0;
            return GF256_EXP[GF256_LOG[a] + GF256_LOG[b]];
        }
    }
}
