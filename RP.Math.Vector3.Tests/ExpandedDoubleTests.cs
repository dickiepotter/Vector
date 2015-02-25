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
        #region IsNegative Tests

        [TestMethod, TestCategory("IsNegative")]
        public void Construction_WherePositiveWholeNumber_ShouldBePositive_Test()
        {
            double d = 100;
            var result = new ExpandedDouble(d);

            result.IsNegative.Should().Be(false, "100 is a positive number");
        }

        [TestMethod, TestCategory("IsNegative")]
        public void Construction_WhereNegativeWholeNumber_ShouldBeNagative_Test()
        {
            double d = -100;
            var result = new ExpandedDouble(d);

            result.IsNegative.Should().Be(true, "-100 is a negative number");
        }

        [TestMethod, TestCategory("IsNegative")]
        public void Construction_WherePositiveFloatingPointNumber_ShouldBePositive_Test()
        {
            double d = 100.005;
            var result = new ExpandedDouble(d);

            result.IsNegative.Should().Be(false, "100.005 is a positive number");
        }

        [TestMethod, TestCategory("IsNegative")]
        public void Construction_WhereNegativeFloatingPointNumber_ShouldBeNagative_Test()
        {
            double d = -100.005;
            var result = new ExpandedDouble(d);

            result.IsNegative.Should().Be(true, "-100.005 is a negative number");
        }

        [TestMethod, TestCategory("IsNegative")]
        public void Construction_WherePositiveInfinity_ShouldBePositive_Test()
        {
            double d = double.PositiveInfinity;
            var result = new ExpandedDouble(d);

            result.IsNegative.Should().Be(false, "+inf is a positive number");
        }

        [TestMethod, TestCategory("IsNegative")]
        public void Construction_WhereNegativeInfinity_ShouldBeNagative_Test()
        {
            double d = double.NegativeInfinity;
            var result = new ExpandedDouble(d);

            result.IsNegative.Should().Be(true, "-inf is a negative number");
        }

        [TestMethod, TestCategory("IsNegative")]
        public void Construction_WhereNaN_ShouldBePositive_Test()
        {
            // Todo: We should maybe throw an exception when testing the sign of NaN instead
            double d = double.NaN;
            var result = new ExpandedDouble(d);

            result.IsNegative.Should().Be(true, "-NaN is a negative number");
        }

        #endregion

        #region IsNaN Tests

        [TestMethod]
        public void IsNaN_WithNaN_ShouldBeTrue_Test()
        {
            double d = double.NaN;
            var result = new ExpandedDouble(d);

            result.IsNaN.Should().Be(true);
        }

        [TestMethod]
        public void IsNaN_WithOne_ShouldBeFalse_Test()
        {
            double d = 1d;
            var result = new ExpandedDouble(d);

            result.IsNaN.Should().Be(false);
        }

        [TestMethod]
        public void IsNaN_WithNegativeOne_ShouldBeFalse_Test()
        {
            double d = -1d;
            var result = new ExpandedDouble(d);

            result.IsNaN.Should().Be(false);
        }

        /// <summary>
        /// Check for Signalling NaN. Ignored: I haven't found a way of producing a SNaN.
        /// </summary>
        [TestMethod, Ignore]
        public void IsSignnallingNaN_WithSignallingNaN_ShouldBeTrue_Test()
        {
            double d = 0d / 0d; // Produces quite NaN
            var result = new ExpandedDouble(d);

            result.IsSignallingNaN.Should().Be(true);
        }

        [TestMethod]
        public void IsQuietNaN_WithSignallingNaN_ShouldBeTrue_Test()
        {
            double d = 0d / 0d;
            var result = new ExpandedDouble(d);

            result.IsQuietNaN.Should().Be(true);
        }

        #endregion

        #region IsInfinte

        [TestMethod]
        public void IsInfinite_WithPositiveInfinity_ShouldBeTrue_Test()
        {
            double d = double.PositiveInfinity;
            var result = new ExpandedDouble(d);

            result.IsInifinite.Should().Be(true);
        }

        [TestMethod]
        public void IsInfinite_WithNegativeInfinity_ShouldBeTrue_Test()
        {
            double d = double.NegativeInfinity;
            var result = new ExpandedDouble(d);

            result.IsInifinite.Should().Be(true);
        }

        [TestMethod]
        public void IsInfinite_WithOne_ShouldBeFalse_Test()
        {
            double d = 1d;
            var result = new ExpandedDouble(d);

            result.IsInifinite.Should().Be(false);
        }

        [TestMethod]
        public void IsPositiveInfinity_WithPositiveInfinity_ShouldBeTrue_Test()
        {
            double d = double.PositiveInfinity;
            var result = new ExpandedDouble(d);

            result.IsPositiveInfinity.Should().Be(true);
        }

        [TestMethod]
        public void IsPositiveInfinity_WithNegativeInfinity_ShouldBeFalse_Test()
        {
            double d = double.NegativeInfinity;
            var result = new ExpandedDouble(d);

            result.IsPositiveInfinity.Should().Be(false);
        }

        [TestMethod]
        public void IsPositiveInfinitye_WithOne_ShouldBeFalse_Test()
        {
            double d = 1d;
            var result = new ExpandedDouble(d);

            result.IsPositiveInfinity.Should().Be(false);
        }

        [TestMethod]
        public void IsNegativeInfinity_WithPositiveInfinity_ShouldBeFalse_Test()
        {
            double d = double.PositiveInfinity;
            var result = new ExpandedDouble(d);

            result.IsNegativeInfinity.Should().Be(false);
        }

        [TestMethod]
        public void IsNegativeInfinity_WithNegativeInfinity_ShouldBeTrue_Test()
        {
            double d = double.NegativeInfinity;
            var result = new ExpandedDouble(d);

            result.IsNegativeInfinity.Should().Be(true);
        }

        [TestMethod]
        public void IsNegativeInfinity_WithOne_ShouldBeFalse_Test()
        {
            double d = 1d;
            var result = new ExpandedDouble(d);

            result.IsNegativeInfinity.Should().Be(false);
        }

        #endregion

        #region Exponent

        [TestMethod]
        public void ExponentAsStored_WhereExponentIsNotZero_ShouldBeCorrect_Test()
        {
            double d = 75.5;
            var result = new ExpandedDouble(d);

            result.ExponentAsStored.Should().Be(1029, "The exponent of 75.5 should be 1029 before removing the bias");
        }

        [TestMethod]
        public void Exponent_WhereExponentIsNotZero_ShouldBeCorrect_Test()
        {
            double d = 75.5;
            var result = new ExpandedDouble(d);

            result.Exponent.Should().Be(3, "The exponent of 75.5 should be 3 after removing the bias");
        }

        [TestMethod]
        public void ExponentBitsAsStored_WhereExponentIsNotZero_ShouldBeCorrect_Test()
        {
            double d = 75.5;
            var result = new ExpandedDouble(d);

            result.ExponentBitsAsStored.Should().BeEquivalentTo(1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1);
        }

        #endregion

        #region Mantissa

        [TestMethod]
        public void MantissaAsStored_WhereExponentIsNotZero_ShouldBeCorrect_Test()
        {
            double d = 75.5;
            var result = new ExpandedDouble(d);

            result.MantissaAsStored.Should().Be(809240558043136, "The mantissa of 75.5 should be 809240558043136 as it is stored in binary");
        }

        [TestMethod]
        public void Mantissa_WhereExponentIsNotZero_ShouldBeCorrectWithImpliedLeadingOne_Test()
        {
            double d = 75.5;
            var result = new ExpandedDouble(d);

            result.Mantissa.Should().Be(809240558043137, "The mantissa of 75.5 should be 809240558043137 with the leading one added");
        }

        [TestMethod]
        public void MantissaBitsAsStored_WhereExponentIsNotZero_ShouldBeCorrect_Test()
        {
            double d = 75.5;
            var result = new ExpandedDouble(d);

            result.MantissaBitsAsStored.Should()
                .BeEquivalentTo(0, 0, 1, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        }

        #endregion
    }
}
