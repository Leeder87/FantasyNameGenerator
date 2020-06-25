using System;
using System.Security.Cryptography;

namespace FantasyNameGen
{
    public static class CryptoRandom
    {
        // от 0 до num-1, то есть Random(5) даст 0..4
        // если нужно от 1 до num, то сделать val+1
        public static int Random(int num)
        {
            RNGCryptoServiceProvider random = new RNGCryptoServiceProvider("testo");
            byte[] randomBytes = new byte[1024 * sizeof(int)];
            random.GetBytes(randomBytes);
            int val = BitConverter.ToInt32(randomBytes, 4);
            val &= 0x7fffffff;
            val = val % num;
            return val;
        }
    }
}
