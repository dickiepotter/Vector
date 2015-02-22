// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExpandedDoubleTests.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the ExpandedDoubleTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RP.Math.Tests
{
    using FluentAssertions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ExpandedDoubleTests
    {
        #region Construction Tests

        // TODO Need a better understanding of the underlying structure of doubles, this test is plain wrong
        [TestMethod, TestCategory("Construction"), Ignore]
        public void Construction_WhereWholeNumberDouble_ShouldSplitCorrectly_Test()
        {
            double d = 100;
            var result = new ExpandedDouble(d);

            result.IsNegative.Should().Be(false, "100.5 is a positive number");
            result.Exponent.Should().Be(1, "the exponent of 100.5 is 1");
            result.Mantissa.Should().Be(1005, "the mantissa of 100.5 is 1005");
        }

        #endregion
    }
}
