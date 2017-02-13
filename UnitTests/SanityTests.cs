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
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 1), 8);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 2), 9);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 3), 9);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 4), 10);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 5), 10);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 6), 11);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 7), 11);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 8), 12);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 9), 12);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 10), 13);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 11), 13);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 12), 14);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 13), 14);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 14), 15);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 15), 15);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 16), 16);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 17), 16);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 18), 17);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 19), 17);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 20), 18);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 21), 19);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 22), 20);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 23), 20);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 24), 21);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 25), 21);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 26), 22);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 27), 22);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 28), 23);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 29), 23);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 30), 24);
        }

        [Test]
        public void TestModerateDCs()
        {
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 1), 12);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 2), 13);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 3), 13);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 4), 14);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 5), 15);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 6), 15);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 7), 16);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 8), 16);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 9), 17);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 10), 18);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 11), 19);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 12), 20);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 13), 20);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 14), 21);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 15), 22);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 16), 22);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 17), 23);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 18), 23);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 19), 24);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 20), 25);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 21), 26);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 22), 27);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 23), 27);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 24), 28);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 25), 29);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 26), 29);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 27), 30);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 28), 30);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 29), 31);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 30), 32);
        }

        [Test]
        public void TestHardDCs()
        {
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 1), 19);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 2), 20);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 3), 21);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 4), 21);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 5), 22);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 6), 23);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 7), 23);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 8), 24);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 9), 25);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 10), 26);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 11), 27);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 12), 28);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 13), 29);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 14), 29);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 15), 30);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 16), 31);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 17), 31);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 18), 32);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 19), 33);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 20), 34);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 21), 35);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 22), 36);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 23), 37);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 24), 37);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 25), 38);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 26), 39);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 27), 39);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 28), 40);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 29), 41);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 30), 42);

            // Out of Bounds
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 0), 0);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Easy, 31), 0);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 0), 0);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Moderate, 31), 0);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 0), 0);
            Assert.AreEqual(Masterplan.Tools.AI.GetSkillDC(Difficulty.Hard, 31), 0);

        }
    }
}
