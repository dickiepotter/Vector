namespace RP.Math.Vector3.Tests
{
    using System;

    using FluentAssertions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DoubleExtensionTests
    {
        [TestMethod, TestCategory("EqualWithinTolerance")]
        public void EqualWithinTolerance_WithZeroAndZeroAndAbsoluteToleranceZero_ShouldBeTrue_Test()
        {
            var result = DoubleExtension.EqualWithinTolerance(0, 0, 0);
            result.Should().Be(true);
        }

        [TestMethod, TestCategory("EqualWithinTolerance")]
        public void EqualWithinTolerance_WithZeroAndZeroAndAbsoluteToleranceZeroAndRelativeToleranceZero_ShouldBeTrue_Test()
        {
            var result = DoubleExtension.EqualWithinTolerance(0, 0, 0, 0);
            result.Should().Be(true);
        }

        [TestMethod, TestCategory("EqualToUlps")]
        public void EqualToUlps_WithZeroAndZeroAndZeroUlps_ShouldBeTrue_Test()
        {
            var result = DoubleExtension.EqualToUlps(0, 0, 0);
            result.Should().Be(true);
        }

        [TestMethod, TestCategory("EqualWithinTolerance")]
        public void EqualWithinTolerance_WithZeroAndZeroAndUlpsZero_ShouldBeTrue_Test()
        {
            var result = DoubleExtension.EqualWithinTolerance(0, 0, (int)0);
            result.Should().Be(true);
        }

        [TestMethod, TestCategory("EqualWithinTolerance")]
        public void EqualWithinTolerance_WithOneAndZeroAndAbsoluteToleranceZero_ShouldBeFalse_Test()
        {
            var result = DoubleExtension.EqualWithinTolerance(1, 0, 0);
            result.Should().Be(false);
        }

        [TestMethod, TestCategory("EqualToUlps")]
        public void EqualToUlps_WithOneAndZeroAndZeroUlps_ShouldBeFalse_Test()
        {
            var result = DoubleExtension.EqualToUlps(1, 0, 0);
            result.Should().Be(false);
        }

        [TestMethod, TestCategory("EqualWithinTolerance")]
        public void EqualWithinTolerance_WithOneAndZeroAndAbsoluteToleranceZeroAndRelativeToleranceZero_ShouldBeFalse_Test()
        {
            var result = DoubleExtension.EqualWithinTolerance(1, 0, 0, 0);
            result.Should().Be(false);
        }

        [TestMethod, TestCategory("EqualWithinTolerance")]
        public void EqualWithinTolerance_WithZeroAndNaNAndAbsoluteToleranceZero_ShouldBeFalse_Test()
        {
            var result = DoubleExtension.EqualWithinTolerance(0, double.NaN, 0);
            result.Should().Be(false);
        }

        [TestMethod, TestCategory("EqualWithinTolerance")]
        public void EqualWithinTolerance_WithZeroAndNaNAndAbsoluteToleranceZeroAndRelativeToleranceZero_ShouldBeFalse_Test()
        {
            var result = DoubleExtension.EqualWithinTolerance(0, double.NaN, 0, 0);
            result.Should().Be(false);
        }

        [TestMethod, TestCategory("EqualToUlps")]
        public void EqualToUlps_WithZeroAndNaNAndZeroUlps_ShouldBeFalse_Test()
        {
            var result = DoubleExtension.EqualToUlps(0, double.NaN, 0);
            result.Should().Be(false);
        }

        [TestMethod, TestCategory("EqualWithinTolerance")]
        public void EqualWithinTolerance_WithZeroAndInfinityAndAbsoluteToleranceZeroAndRelativeToleranceZero_ShouldBeFalse_Test()
        {
            var result = DoubleExtension.EqualWithinTolerance(0, double.PositiveInfinity, 0, 0);
            result.Should().Be(false);
        }

        [TestMethod, TestCategory("EqualToUlps")]
        public void EqualToUlps_WithZeroAndInfinityAndZeroUlps_ShouldBeFalse_Test()
        {
            var result = DoubleExtension.EqualToUlps(0, double.PositiveInfinity, 0);
            result.Should().Be(false);
        }

        [TestMethod, TestCategory("EqualToUlps")]
        public void EqualToUlps_WithZeroAndInfinityAndTenUlps_ShouldBeFalse_Test()
        {
            var result = DoubleExtension.EqualToUlps(0, double.PositiveInfinity, 10);
            result.Should().Be(false);
        }
    }
}
