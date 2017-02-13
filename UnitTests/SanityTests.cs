using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace UnitTests
{
    public class SanityTests
    {
        [Test]
        public void TestEasyDCs()
        {
            Assert.AreEqual(8, Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 1));
            Assert.AreEqual(9, Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 2));
            Assert.AreEqual(9, Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 3));
            Assert.AreEqual(10, Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 4));
            Assert.AreEqual(10, Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 5));
            Assert.AreEqual(11, Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 6));
            Assert.AreEqual(11, Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 7));
            Assert.AreEqual(12, Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 8));
            Assert.AreEqual(12, Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 9));
            Assert.AreEqual(13, Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 10));
            Assert.AreEqual(13, Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 11));
            Assert.AreEqual(14, Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 12));
            Assert.AreEqual(14, Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 13));
            Assert.AreEqual(15, Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 14));
            Assert.AreEqual(15, Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 15));
            Assert.AreEqual(16, Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 16));
            Assert.AreEqual(16, Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 17));
            Assert.AreEqual(17, Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 18));
            Assert.AreEqual(17, Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 19));
            Assert.AreEqual(18, Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 20));
            Assert.AreEqual(19, Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 21));
            Assert.AreEqual(20, Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 22));
            Assert.AreEqual(20, Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 23));
            Assert.AreEqual(21, Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 24));
            Assert.AreEqual(21, Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 25));
            Assert.AreEqual(22, Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 26));
            Assert.AreEqual(22, Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 27));
            Assert.AreEqual(23, Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 28));
            Assert.AreEqual(23, Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 29));
            Assert.AreEqual(24, Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 30));
        }

        [Test]
        public void TestModerateDCs()
        {
            Assert.AreEqual(12, Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 1));
            Assert.AreEqual(13, Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 2));
            Assert.AreEqual(13, Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 3));
            Assert.AreEqual(14, Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 4));
            Assert.AreEqual(15, Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 5));
            Assert.AreEqual(15, Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 6));
            Assert.AreEqual(16, Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 7));
            Assert.AreEqual(16, Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 8));
            Assert.AreEqual(17, Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 9));
            Assert.AreEqual(18, Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 10));
            Assert.AreEqual(19, Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 11));
            Assert.AreEqual(20, Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 12));
            Assert.AreEqual(20, Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 13));
            Assert.AreEqual(21, Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 14));
            Assert.AreEqual(22, Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 15));
            Assert.AreEqual(22, Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 16));
            Assert.AreEqual(23, Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 17));
            Assert.AreEqual(23, Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 18));
            Assert.AreEqual(24, Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 19));
            Assert.AreEqual(25, Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 20));
            Assert.AreEqual(26, Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 21));
            Assert.AreEqual(27, Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 22));
            Assert.AreEqual(27, Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 23));
            Assert.AreEqual(28, Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 24));
            Assert.AreEqual(29, Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 25));
            Assert.AreEqual(29, Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 26));
            Assert.AreEqual(30, Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 27));
            Assert.AreEqual(30, Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 28));
            Assert.AreEqual(31, Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 29));
            Assert.AreEqual(32, Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 30));
        }

        [Test]
        public void TestHardDCs()
        {
            Assert.AreEqual(19, Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 1));
            Assert.AreEqual(20, Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 2));
            Assert.AreEqual(21, Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 3));
            Assert.AreEqual(21, Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 4));
            Assert.AreEqual(22, Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 5));
            Assert.AreEqual(23, Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 6));
            Assert.AreEqual(23, Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 7));
            Assert.AreEqual(24, Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 8));
            Assert.AreEqual(25, Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 9));
            Assert.AreEqual(26, Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 10));
            Assert.AreEqual(27, Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 11));
            Assert.AreEqual(28, Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 12));
            Assert.AreEqual(29, Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 13));
            Assert.AreEqual(29, Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 14));
            Assert.AreEqual(30, Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 15));
            Assert.AreEqual(31, Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 16));
            Assert.AreEqual(31, Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 17));
            Assert.AreEqual(32, Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 18));
            Assert.AreEqual(33, Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 19));
            Assert.AreEqual(34, Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 20));
            Assert.AreEqual(35, Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 21));
            Assert.AreEqual(36, Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 22));
            Assert.AreEqual(37, Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 23));
            Assert.AreEqual(37, Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 24));
            Assert.AreEqual(38, Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 25));
            Assert.AreEqual(39, Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 26));
            Assert.AreEqual(39, Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 27));
            Assert.AreEqual(40, Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 28));
            Assert.AreEqual(41, Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 29));
            Assert.AreEqual(42, Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 30));

            // Out of Bounds
            Assert.AreEqual(0, Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 0));
            Assert.AreEqual(0, Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 31));
            Assert.AreEqual(0, Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 0));
            Assert.AreEqual(0, Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 31));
            Assert.AreEqual(0, Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 0));
            Assert.AreEqual(0, Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 31));

        }
    }
}
