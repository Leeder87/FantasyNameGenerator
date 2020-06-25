using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptoRandomTests
{
    [TestClass]
    public class RandomTests
    {
        [TestMethod]
        public void Random_5_0to4expected()
        {
            int[] expected = (0, 1, 2, 3, 4);

            int actual = CryptoRandom.Random(5);

            CollectionAssert.Contains(expected, actual);
        }
    }
}
