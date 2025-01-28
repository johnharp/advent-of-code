using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day14
{
    [TestClass]
    public class TestApplyRulesToElements
    {
        [TestMethod]
        public void TestFirstFourIterations()
        {
            PuzzleInput input = new PuzzleInput("input-example.txt");
            ElementTracker tracker = new ElementTracker();
            tracker.InitializeRules(input.InsertionRules);
            tracker.InitializeElements(input.InitialElements);

            Assert.AreEqual("NNCB", tracker.CurrentElementsStr());
            tracker.ApplyRulesToElements();
            Assert.AreEqual("NCNBCHB", tracker.CurrentElementsStr());
            tracker.ApplyRulesToElements();
            Assert.AreEqual("NBCCNBBBCBHCB", tracker.CurrentElementsStr());
            tracker.ApplyRulesToElements();
            Assert.AreEqual("NBBBCNCCNBBNBNBBCHBHHBCHB", tracker.CurrentElementsStr());
            tracker.ApplyRulesToElements();
            Assert.AreEqual("NBBNBNBBCCNBCNCCNBBNBBNBBBNBBNBBCBHCBHHNHCBBCBHCB", tracker.CurrentElementsStr());
        }
    }
}
