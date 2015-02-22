namespace RP.Math.Tests
{
    using FluentAssertions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DoubleExtensionTests
    {
        #region AlmostEqualsWithAbsTolerance Tests

        [TestMethod, TestCategory("AlmostEqualsWithAbsTolerance")]
        public void AlmostEqualsWithAbsTolerance_WithZeroAndZeroAndAbsoluteToleranceZero_ShouldBeTrue_Test()
        {
            var result = DoubleExtension.AlmostEqualsWithAbsTolerance(0, 0, 0);
            result.Should().Be(true);
        }

        [TestMethod, TestCategory("AlmostEqualsWithAbsTolerance")]
        public void AlmostEqualsWithAbsTolerance_WithOneAndFloatingPointNumberNearOneAndValidAbsoluteTolerance_ShouldBeTrue_Test()
        {
            var result = DoubleExtension.AlmostEqualsWithAbsTolerance(1, 0.9999, 0.0002);
            result.Should().Be(true);
        }

        [TestMethod, TestCategory("AlmostEqualsWithAbsTolerance")]
        public void AlmostEqualsWithAbsTolerance_WithOneAndZeroAndAbsoluteToleranceZero_ShouldBeFalse_Test()
        {
            var result = DoubleExtension.AlmostEqualsWithAbsTolerance(1, 0, 0);
            result.Should().Be(false);
        }

        [TestMethod, TestCategory("AlmostEqualsWithAbsTolerance")]
        public void AlmostEqualsWithAbsTolerance_WithZeroAndNaNAndAbsoluteToleranceZero_ShouldBeFalse_Test()
        {
            var result = DoubleExtension.AlmostEqualsWithAbsTolerance(0, double.NaN, 0);
            result.Should().Be(false);
        }

        #endregion

        #region AlmostEqualsWithAbsOrUlpsTolerance Tests

        [TestMethod, TestCategory("AlmostEqualsWithAbsOrUlpsTolerance")]
        public void TwosCompliment_WithPoint0001_ShouldBeCorrect_Test()
        {
            var result = DoubleExtension.AlmostEqualsWithAbsOrUlpsTolerance(0, 0, 0, 0);
            result.Should().Be(true);
        }

        [TestMethod, TestCategory("AlmostEqualsWithAbsOrUlpsTolerance")]
        public void AlmostEqualsWithAbsOrUlpsTolerance_WithZeroAndZeroAndZeroUlpsAndZeroAbsoluteTolerance_ShouldBeTrue_Test()
        {
            var result = DoubleExtension.AlmostEqualsWithAbsOrUlpsTolerance(0, 0, 0, 0);
            result.Should().Be(true);
        }

        [TestMethod, TestCategory("AlmostEqualsWithAbsOrUlpsTolerance")]
        public void AlmostEqualsWithAbsOrUlpsTolerance_WithOneAndZeroAndZeroUlpsAndZeroAbsoluteTolerance_ShouldBeFalse_Test()
        {
            var result = DoubleExtension.AlmostEqualsWithAbsOrUlpsTolerance(1, 0, 0, 0);
            result.Should().Be(false);
        }

        [TestMethod, TestCategory("AlmostEqualsWithAbsOrUlpsTolerance")]
        public void AlmostEqualsWithAbsOrUlpsTolerances_WithZeroAndNaNAndZeroUlpsAndZeroAbsoluteTolerance_ShouldBeFalse_Test()
        {
            var result = DoubleExtension.AlmostEqualsWithAbsOrUlpsTolerance(0, double.NaN, 0, 0);
            result.Should().Be(false);
        }

        [TestMethod, TestCategory("AlmostEqualsWithAbsOrUlpsTolerance")]
        public void AlmostEqualsWithAbsOrUlpsTolerance_WithZeroAndInfinityAndZeroUlpsAndZeroAbsoluteTolerance_ShouldBeFalse_Test()
        {
            var result = DoubleExtension.AlmostEqualsWithAbsOrUlpsTolerance(0, double.PositiveInfinity, 0, 0);
            result.Should().Be(false);
        }

        [TestMethod, TestCategory("AlmostEqualsWithAbsOrUlpsTolerance")]
        public void AlmostEqualsWithAbsOrUlpsTolerance_WithZeroAndInfinityAndTenUlpsAndZeroAbsoluteTolerance_ShouldBeFalse_Test()
        {
            var result = DoubleExtension.AlmostEqualsWithAbsOrUlpsTolerance(0, double.PositiveInfinity, 0, 10);
            result.Should().Be(false);
        }

        [TestMethod, TestCategory("AlmostEqualsWithAbsOrUlpsTolerance")]
        public void AlmostEqualsWithAbsOrUlpsTolerance_WithFloatingPointNumbersAndValidUlpsAndZeroAbsoluteTolerance_ShouldBeTrue_Test()
        {
            var result = DoubleExtension.AlmostEqualsWithAbsOrUlpsTolerance(67329.234d, 67329.242d, 0, 1);

            result.Should().Be(true, "these number are exactly one ULP apart (the last digit)");
        }

        #endregion

        #region AlmostEqualsWithAbsOrRelativeTolerance Tests

        [TestMethod, TestCategory("AlmostEqualsWithAbsOrRelativeTolerance")]
        public void AlmostEqualsWithAbsOrRelativeTolerance_WithZeroAndZeroAndAbsoluteToleranceZeroAndRelativeToleranceZero_ShouldBeTrue_Test()
        {
            var result = DoubleExtension.AlmostEqualsWithAbsOrRelativeTolerance(0, 0, 0, 0);
            result.Should().Be(true);
        }

        [TestMethod, TestCategory("AlmostEqualsWithAbsOrRelativeTolerance")]
        public void AlmostEqualsWithAbsOrRelativeTolerance_WithOneAndZeroAndAbsoluteToleranceZeroAndRelativeToleranceZero_ShouldBeFalse_Test()
        {
            var result = DoubleExtension.AlmostEqualsWithAbsOrRelativeTolerance(1, 0, 0, 0);
            result.Should().Be(false);
        }

        [TestMethod, TestCategory("AlmostEqualsWithAbsOrRelativeTolerance")]
        public void AlmostEqualsWithAbsOrRelativeTolerance_WithZeroAndNaNAndAbsoluteToleranceZeroAndRelativeToleranceZero_ShouldBeFalse_Test()
        {
            var result = DoubleExtension.AlmostEqualsWithAbsOrRelativeTolerance(0, double.NaN, 0, 0);
            result.Should().Be(false);
        }

        [TestMethod, TestCategory("AlmostEqualsWithAbsOrRelativeTolerance")]
        public void AlmostEqualsWithAbsOrRelativeTolerance_WithZeroAndInfinityAndAbsoluteToleranceZeroAndRelativeToleranceZero_ShouldBeFalse_Test()
        {
            var result = DoubleExtension.AlmostEqualsWithAbsOrRelativeTolerance(0, double.PositiveInfinity, 0, 0);
            result.Should().Be(false);
        }

        #endregion
    }
}
