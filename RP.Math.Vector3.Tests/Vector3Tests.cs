namespace RP.Math.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using FluentAssertions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RP.Math;
    using RP.Math.Exceptions;

    /// <summary>
    /// Unit tests for the <see cref="Vector3"/> class
    /// </summary>
    /// TODO Test What happens when we try to get the angle of vectors with (inf, n, n) and NaN components. We may need to add AnngleOrDefault but that seems wrong when getting scalar results. Should be NaN.
    /// TODO Rethink fix IsPerpendicular special case when near zero or infinty components.
    /// TODO Test the logic of Abs
    [TestClass]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Vector3Tests
    {
        private const double ArbitaryTestDouble = 109.005;

        private static readonly double Deg90AsRad = System.Math.PI / 2;

        #region Constructor tests

        [TestMethod, TestCategory("Construction")]
        public void Construction_WithXYZParameters_ShouldConstructCorrectly_Test()
        {
            var vector = new Vector3(100, -100, 0.01);
            vector.X.Should().Be(100d);
            vector.Y.Should().Be(-100d);
            vector.Z.Should().Be(0.01d);
        }

        [TestMethod, TestCategory("Construction")]
        public void Construction_WithAVector3Parameter_ShouldConstructCorrectly_Test()
        {
            var vector = new Vector3(new Vector3(100, -100, 0));

            vector.X.Should().Be(100);
            vector.Y.Should().Be(-100);
            vector.Z.Should().Be(0);
        }

        [TestMethod, TestCategory("Construction")]
        public void Constructor_WithAnArrayOfXYZParameter_ShouldConstructCorrectly_Test()
        {
            var vector = new Vector3(new[] { 100, -100, 0.01 });

            vector.X.Should().Be(100);
            vector.Y.Should().Be(-100);
            vector.Z.Should().Be(0.01);
        }

        [TestMethod, TestCategory("Construction")]
        public void Constructor_WithNoParameters_ShouldConstructCorrectly_Test()
        {
            var vector = new Vector3();

            vector.X.Should().Be(0);
            vector.Y.Should().Be(0);
            vector.Z.Should().Be(0);
        }

        #endregion

        #region Abs Tests

        [TestMethod, TestCategory("Abs")]
        public void Abs_WhereVectorIsZero_Test()
        {
            var vector = new Vector3(0, 0, 0);
            var result = vector.Abs();
            result.Should().Be(0);
        }

        #endregion

        #region Square, Square-Root and Pow Tests

        [TestMethod]
        public void SquareComponentsTest()
        {
            var vector = new Vector3(2, 3, 4);
            var result = vector.SqrComponents();

            result.X.Should().Be(4);
            result.Y.Should().Be(9);
            result.Z.Should().Be(16);
        }

        [TestMethod]
        public void SquareRootComponentsTest()
        {
            var vector = new Vector3(4, 9, 16);
            var result = vector.SqrtComponents();

            result.X.Should().Be(2);
            result.Y.Should().Be(3);
            result.Z.Should().Be(4);
        }

        [TestMethod]
        public void PowComponentsTest()
        {
            var vector = new Vector3(2, 1, 3);
            var result = vector.PowComponents(2);

            result.X.Should().Be(4);
            result.Y.Should().Be(1);
            result.Z.Should().Be(9);
        }

        #endregion

        #region Add and Subtract tests

        [TestMethod]
        public void AddUsingWholeNumbersTest()
        {
            var a = new Vector3(3, 7, 4);
            var b = new Vector3(2, 9, 11);
            var result = a + b;

            result.X.Should().Be(5);
            result.Y.Should().Be(16);
            result.Z.Should().Be(15);
        }

        [TestMethod]
        public void SubtractUingWholeNumbersTest()
        {
            var a = new Vector3(1, 2, 3);
            var b = new Vector3(3, 3, 3);
            var result = a - b;

            result.X.Should().Be(-2);
            result.Y.Should().Be(-1);
            result.Z.Should().Be(-0);
        }

        #endregion

        #region Cross and dot product tests

        [TestMethod, TestCategory("Product")]
        public void DotProduct_WhereWholeNumbers_ShouldResultInTheCorrectDotProduct_Test()
        {
            var a = new Vector3(12, 20, 0);
            var b = new Vector3(16, -5, 0);

            a.DotProduct(b).Should().Be(92);
        }

        [TestMethod, TestCategory("Product")]
        public void DotProduct_WhereV1XIsPositiveInfinity_ShouldResultInPositiveInfinity_Test()
        {
            var v1 = new Vector3(double.PositiveInfinity, 0, 0);
            var v2 = new Vector3(1, 0, 0);

            var result = v1.DotProduct(v2);

            result.Should().Be(double.PositiveInfinity);
        }

        [TestMethod, TestCategory("Product")]
        public void DotProduct_WhereV2XIsPositiveInfinity_ShouldResultInPositiveInfinity_Test()
        {
            var v1 = new Vector3(1, 0, 0);
            var v2 = new Vector3(double.PositiveInfinity, 0, 0);

            var result = v1.DotProduct(v2);

            result.Should().Be(double.PositiveInfinity);
        }

        [TestMethod, TestCategory("Product")]
        public void DotProduct_WhereXIsPositiveInfinity_ShouldResultInPositiveInfinity_Test()
        {
            var v1 = new Vector3(double.PositiveInfinity, 0, 0);
            var v2 = new Vector3(double.PositiveInfinity, 0, 0);

            var result = v1.DotProduct(v2);

            result.Should().Be(double.PositiveInfinity);
        }

        [TestMethod, TestCategory("Product")]
        public void DotProduct_WhereV1XIsNegativeInfinity_ShouldResultInNegativeInfinity_Test()
        {
            var v1 = new Vector3(double.NegativeInfinity, 0, 0);
            var v2 = new Vector3(1, 0, 0);

            var result = v1.DotProduct(v2);

            result.Should().Be(double.NegativeInfinity);
        }

        [TestMethod, TestCategory("Product")]
        public void DotProduct_WhereV2XIsNegativeInfinity_ShouldResultInNegativeInfinity_Test()
        {
            var v1 = new Vector3(1, 0, 0);
            var v2 = new Vector3(double.NegativeInfinity, 0, 0);

            var result = v1.DotProduct(v2);

            result.Should().Be(double.NegativeInfinity);
        }

        [TestMethod, TestCategory("Product")]
        public void DotProduct_WhereXIsNegativeInfinity_ShouldResultInPositiveInfinity_Test()
        {
            var v1 = new Vector3(double.NegativeInfinity, 0, 0);
            var v2 = new Vector3(double.NegativeInfinity, 0, 0);

            var result = v1.DotProduct(v2);

            result.Should().Be(double.PositiveInfinity);
        }

        [TestMethod, TestCategory("Product")]
        public void DotProduct_WhereV1XIsNaN_ShouldResultInNaN_Test()
        {
            var v1 = new Vector3(double.NaN, 0, 0);
            var v2 = new Vector3(1, 0, 0);

            var result = v1.DotProduct(v2);

            result.Should().Be(double.NaN);
        }

        [TestMethod, TestCategory("Product")]
        public void DotProduct_WhereV1XIs10AndV2IsUnitVectorXAxis_ShouldResultIn10_Test()
        {
            var v1 = new Vector3(10, 0, 0);
            var v2 = new Vector3(1, 0, 0);

            var result = v1.DotProduct(v2);

            result.Should().Be(10);
        }

        [TestMethod, TestCategory("Product")]
        public void DotProduct_WhereV1XIsNegative10AndV2IsNegativeUnitVectorXAxis_ShouldResultInPositive10_Test()
        {
            var v1 = new Vector3(-10, 0, 0);
            var v2 = new Vector3(-1, 0, 0);

            var result = v1.DotProduct(v2);

            result.Should().Be(10);
        }

        [TestMethod, TestCategory("Product")]
        public void DotProduct_WhereV1XIsNegative10AndV2IsPositiveUnitVectorXAxis_ShouldResultInNegative10_Test()
        {
            var v1 = new Vector3(-10, 0, 0);
            var v2 = new Vector3(1, 0, 0);

            var result = v1.DotProduct(v2);

            result.Should().Be(-10);
        }

        [TestMethod, TestCategory("Product")]
        public void CrossProduct_WhereWholeNumbers_ShouldResultInTheCorrectCrossProduct_Test()
        {
            var a = new Vector3(4, 1, 0);
            var b = new Vector3(-5, 6, 0);
            var result = a.CrossProduct(b);

            result.X.Should().Be(0);
            result.Y.Should().Be(0);
            result.Z.Should().Be(29);
        }

        #endregion

        #region Magnitude tests

        [TestMethod, TestCategory("Magnitude")]
        public void Magnitude_WithPositiveZeroVector_ShouldBe0_Test()
        {
            var vector = new Vector3(0, 0, 0);
            var magnitude = vector.Magnitude;

            magnitude.Should().Be(0);
        }

        [TestMethod, TestCategory("Magnitude")]
        public void Magnitude_WithNegativeZeroVector_ShouldBe0_Test()
        {
            var vector = new Vector3(-0, -0, -0);
            var magnitude = vector.Magnitude;

            magnitude.Should().Be(0);
        }

        [TestMethod, TestCategory("Magnitude")]
        public void Magnitude_WithPositievAndNegativeZeroVector_ShouldBe0_Test()
        {
            var vector = new Vector3(-0, +0, -0);
            var magnitude = vector.Magnitude;

            magnitude.Should().Be(0);
        }

        [TestMethod, TestCategory("Magnitude")]
        public void Magnitude_WithXNaN_ShouldBeNaN_Test()
        {
            var vector = new Vector3(double.NaN, 0, 0);
            var magnitude = vector.Magnitude;

            magnitude.Should().Be(double.NaN);
        }

        [TestMethod, TestCategory("Magnitude")]
        public void Magnitude_WithZNaNAndXYOne_ShouldBeNaN_Test()
        {
            var vector = new Vector3(1, 1, double.NaN);
            var magnitude = vector.Magnitude;

            magnitude.Should().Be(double.NaN);
        }

        [TestMethod, TestCategory("Magnitude")]
        public void Magnitude_WithXPositiveNumber_ShouldBeX_Test()
        {
            var vector = new Vector3(10, 0, 0);
            var magnitude = vector.Magnitude;

            magnitude.Should().Be(10);
        }

        [TestMethod, TestCategory("Magnitude")]
        public void Magnitude_WithPositiveInfinityX_ShouldBePositiveInfinity_Test()
        {
            var vector = new Vector3(double.PositiveInfinity, 0, 0);
            var magnitude = vector.Magnitude;

            magnitude.Should().Be(double.PositiveInfinity);
        }

        [TestMethod, TestCategory("Magnitude")]
        public void Magnitude_WithPositiveInfinityXAndOtherComponensAreWholeNumbers_ShouldBePositiveInfinity_Test()
        {
            var vector = new Vector3(double.PositiveInfinity, 3, 6);
            var magnitude = vector.Magnitude;

            magnitude.Should().Be(double.PositiveInfinity);
        }

        [TestMethod, TestCategory("Magnitude")]
        public void Magnitude_WithNegativeInfinityX_ShouldBePositiveInfinity_Test()
        {
            var vector = new Vector3(double.NegativeInfinity, 0, 0);
            var magnitude = vector.Magnitude;

            // Check .Net framework logic for ABS Infinity
            (Math.Abs(double.NegativeInfinity)).Should().Be(double.PositiveInfinity, "the.Net framework should find ABS Negative Infinity equal to Positive Infinity(if this assumption is wrong then the logic of this test is also wrong");

            magnitude.Should().Be(double.PositiveInfinity);
        }

        [TestMethod, TestCategory("Magnitude")]
        public void Magnitude_WithNegativeInfinityXAndOtherComponensAreWholeNumbers_ShouldBePositiveInfinity_Test()
        {
            var vector = new Vector3(double.NegativeInfinity, 3, 6);
            var magnitude = vector.Magnitude;

            magnitude.Should().Be(double.PositiveInfinity);
        }

        [TestMethod, TestCategory("Magnitude")]
        public void Magnitude_WithPositiveAndNegativeWholeNumberParameters_ShouldResultInTheCorrectMagnitude_Test()
        {
            var vector = new Vector3(3, 1, -1);
            var magnitude = vector.Magnitude;

            magnitude.Should().Be(System.Math.Sqrt(11));
        }

        [TestMethod, TestCategory("Magnitude")]
        public void Magnitude_WithPositiveWholeNumberParameters_ShouldResultInTheCorrectMagnitude_Test()
        {
            var vector = new Vector3(2, 3, 4);
            var magnitude = vector.Magnitude;

            magnitude.Should().Be(System.Math.Sqrt(29));
        }

        /// <summary>
        /// Test scaling a vector with X component only
        /// </summary>
        [TestMethod, TestCategory("Magnitude")]
        public void Scale_WithXAsOne_ShouldScaleCorrectly_Test()
        {
            var vector = new Vector3(1, 0, 0);
            var result = vector.Scale(10);

            result.X.Should().Be(10);
            result.Y.Should().Be(0);
            result.Z.Should().Be(0);
        }

        /// <summary>
        /// Test scaling a vector with Y component only
        /// </summary>
        [TestMethod, TestCategory("Magnitude")]
        public void Scale_WithYAsOne_ShouldScaleCorrectly_Test()
        {
            var vector = new Vector3(0, 1, 0);
            var result = vector.Scale(10);

            result.X.Should().Be(0);
            result.Y.Should().Be(10);
            result.Z.Should().Be(0);
        }

        /// <summary>
        /// Test scaling a vector with X and Z components (checking the result to six decimal places)
        /// </summary>
        [TestMethod, TestCategory("Magnitude")]
        public void Scale_WithXZAsOne_ShouldScaleCorrectly_Test()
        {
            var vector = new Vector3(1, 0, 1);
            var result = vector.Scale(10);

            var expected = System.Math.Round(5 * System.Math.Sqrt(2), 6);

            System.Math.Round(result.X, 6).Should().Be(expected);
            result.Y.Should().Be(0);
            System.Math.Round(result.Z, 6).Should().Be(expected);
        }

        #endregion

        #region Angle Tests

        [TestMethod, TestCategory("Angle")]
        public void Angel_WithTwoVectorsUsingWholeNumbers_ShouldResultIn90Deg_Test()
        {
            var a = new Vector3(1, 0, 0);
            var b = new Vector3(0, 1, 0);
            var angle = a.Angle(b);

            angle.Should().Be(Deg90AsRad, "vector (1,0,0) is perpendicular to vector (0,1,0)");
        }

        [TestMethod, TestCategory("Angle")]
        public void Angle_WhereVectorsInfinitePositiveXY_ShouldResultIn90Deg_Test()
        {
            Vector3 v1 = new Vector3(double.PositiveInfinity, 0, 0);
            Vector3 v2 = new Vector3(0, double.PositiveInfinity, 0);
            var result = v1.Angle(v2);

            result.Should().Be(Deg90AsRad, "vector (+inf,0,0) is perpendicular to vector (0,+inf,0)");
        }

        [TestMethod, TestCategory("Angle")]
        public void Angle_WhereVectorsAreEqualAndXIsNaN_ShouldResultInNaN_Test()
        {
            Vector3 v1 = new Vector3(double.NaN, 3, 6);
            Vector3 v2 = new Vector3(double.NaN, 3, 6);
            var result = v1.Angle(v2);

            result.Should().Be(double.NaN, "the angle between vector (+NaN,3,6) and (+NaN,3,6) should be NaN");
        }

        [TestMethod, TestCategory("Angle")]
        public void Angle_WhereVectorsAreNotEqualAndXIsNaN_ShouldResultInNaN_Test()
        {
            Vector3 v1 = new Vector3(double.NaN, 0, 0);
            Vector3 v2 = new Vector3(double.NaN, 1, 2);
            var result = v1.Angle(v2);

            result.Should().Be(double.NaN, "the angle between vector (+NaN,0,0) and (+NaN,1,2) should be NaN");
        }

        [TestMethod, TestCategory("Angle")]
        public void Angle_WhereVectorsAreEqualAndXIsInfinite_ShouldResultInZero_Test()
        {
            Vector3 v1 = new Vector3(double.PositiveInfinity, 3, 6);
            Vector3 v2 = new Vector3(double.PositiveInfinity, 3, 6);
            var result = v1.Angle(v2);

            result.Should().Be(0, "vector (+inf,3,6) equal to (+inf,3,6)");
        }

        [TestMethod, TestCategory("Angle")]
        public void Angle_WhereUnitVectorsPositiveYX_ShouldResultIn90Deg_Test()
        {
            Vector3 v1 = new Vector3(0, 1, 0);
            Vector3 v2 = new Vector3(1, 0, 0);
            var result = v1.Angle(v2);

            result.Should().Be(Deg90AsRad, "vector (0,1,0) is perpendicular to vector (1,0,0)");
        }

        [TestMethod, TestCategory("Angle")]
        public void Angle_WhereVectorsInfinitePositiveYX_ShouldResultIn90Deg_Test()
        {
            Vector3 v1 = new Vector3(0, double.PositiveInfinity, 0);
            Vector3 v2 = new Vector3(double.PositiveInfinity, 0, 0);
            var result = v1.Angle(v2);

            result.Should().Be(Deg90AsRad, "vector (0,+inf,0) is perpendicular to vector (+inf,0,0)");
        }

        [TestMethod, TestCategory("Angle")]
        public void Angle_WhereUnitVectorsPositiveXNegativeY_ShouldResultIn90Deg_Test()
        {
            Vector3 v1 = new Vector3(1, 0, 0);
            Vector3 v2 = new Vector3(0, -1, 0);
            var result = v1.Angle(v2);

            result.Should().Be(Deg90AsRad, "vector (1,0,0) is perpendicular to vector (0,-1,0)");
        }

        [TestMethod, TestCategory("Angle")]
        public void Angle_WhereVectorsInfinitePositiveXNegativeY_ShouldResultIn90Deg_Test()
        {
            Vector3 v1 = new Vector3(double.PositiveInfinity, 0, 0);
            Vector3 v2 = new Vector3(0, double.NegativeInfinity, 0);
            var result = v1.Angle(v2);

            result.Should().Be(Deg90AsRad, "vector (+inf,0,0) is perpendicular to vector (0,-inf,0)");
        }

        [TestMethod, TestCategory("Angle")]
        public void Angle_WhereUnitVectorsNegativeYPositiveX_SShouldResultIn90Deg_Test()
        {
            Vector3 v1 = new Vector3(0, -1, 0);
            Vector3 v2 = new Vector3(1, 0, 0);
            var result = v1.Angle(v2);

            result.Should().Be(Deg90AsRad, "vector (0,-1,0) is perpendicular to vector (1,0,0)");
        }

        [TestMethod, TestCategory("Angle")]
        public void Angle_WhereVectorsInfiniteNegativeYPositiveX_ShouldResultIn90Deg_Test()
        {
            Vector3 v1 = new Vector3(0, double.NegativeInfinity, 0);
            Vector3 v2 = new Vector3(double.PositiveInfinity, 0, 0);
            var result = v1.Angle(v2);

            result.Should().Be(Deg90AsRad, "vector (0,-inf,0) is perpendicular to vector (+inf,0,0)");
        }

        [TestMethod, TestCategory("Angle")]
        public void Angle_WhereUnitVectorsNegativeXY_ShouldResultIn90Deg_Test()
        {
            Vector3 v1 = new Vector3(-1, 0, 0);
            Vector3 v2 = new Vector3(0, -1, 0);
            var result = v1.Angle(v2);

            result.Should().Be(Deg90AsRad, "vector (-1,0,0) is perpendicular to vector (0,-1,0)");
        }

        [TestMethod, TestCategory("Angle")]
        public void Angle_WhereVectorsInfiniteNegativeXY_ShouldResultIn90Deg_Test()
        {
            Vector3 v1 = new Vector3(double.NegativeInfinity, 0, 0);
            Vector3 v2 = new Vector3(0, double.NegativeInfinity, 0);
            var result = v1.Angle(v2);

            result.Should().Be(Deg90AsRad, "vector (-inf,0,0) is perpendicular to vector (0,-inf,0)");
        }

        [TestMethod, TestCategory("Angle")]
        public void Angle_WhereUnitVectorsNegativeYX_ShouldResultIn90Deg_Test()
        {
            Vector3 v1 = new Vector3(0, -1, 0);
            Vector3 v2 = new Vector3(-1, 0, 0);
            var result = v1.Angle(v2);

            result.Should().Be(Deg90AsRad, "vector (0,-1,0) is perpendicular to vector (-1,0,0)");
        }

        [TestMethod, TestCategory("Angle")]
        public void Angle_WhereVectorsInfiniteNegativeYX_ShouldResultIn90Deg_Test()
        {
            Vector3 v1 = new Vector3(0, double.NegativeInfinity, 0);
            Vector3 v2 = new Vector3(double.NegativeInfinity, 0, 0);
            var result = v1.Angle(v2);

            result.Should().Be(Deg90AsRad, "vector (0,-inf,0) is perpendicular to vector (-inf,0,0)");
        }

        [TestMethod, TestCategory("Angle")]
        public void Angle_WhereUnitVectorsNegativeXPositiveY_ShouldResultIn90Deg_Test()
        {
            Vector3 v1 = new Vector3(-1, 0, 0);
            Vector3 v2 = new Vector3(0, 1, 0);
            var result = v1.Angle(v2);

            result.Should().Be(Deg90AsRad, "vector (-1,0,0) is perpendicular to vector (0,1,0)");
        }

        [TestMethod, TestCategory("Angle")]
        public void Angle_WhereVectorsInfiniteNegativeXPositiveY_ShouldResultIn90Deg_Test()
        {
            Vector3 v1 = new Vector3(double.NegativeInfinity, 0, 0);
            Vector3 v2 = new Vector3(0, double.PositiveInfinity, 0);
            var result = v1.Angle(v2);

            result.Should().Be(Deg90AsRad, "vector (-inf,0,0) is perpendicular to vector (0,+inf,0)");
        }

        [TestMethod, TestCategory("Angle")]
        public void Angle_WhereUnitVectorsPositiveYNegativeX_ShouldResultIn90Deg_Test()
        {
            Vector3 v1 = new Vector3(0, 1, 0);
            Vector3 v2 = new Vector3(-1, 0, 0);
            var result = v1.Angle(v2);

            result.Should().Be(Deg90AsRad, "vector (0,1,0) is perpendicular to vector (-1,0,0)");
        }

        [TestMethod, TestCategory("Angle")]
        public void Angle_WhereVectorsInfinitePositiveYNegativeX_ShouldResultIn90Deg_Test()
        {
            Vector3 v1 = new Vector3(0, double.PositiveInfinity, 0);
            Vector3 v2 = new Vector3(double.NegativeInfinity, 0, 0);
            var result = v1.Angle(v2);

            result.Should().Be(Deg90AsRad, "vector (0,+inf,0) is perpendicular to vector (-inf,0,0)");
        }

        [TestMethod, TestCategory("Angle")]
        public void Angle_WherePositiveXY_ShouldResultIn90Deg_Test()
        {
            Vector3 v1 = new Vector3(ArbitaryTestDouble, 0, 0);
            Vector3 v2 = new Vector3(0, ArbitaryTestDouble, 0);
            var result = v1.Angle(v2);

            result.Should().Be(Deg90AsRad, "vector (n,0,0) is perpendicular to vector (0,n,0)");
        }

        [TestMethod, TestCategory("Angle")]
        public void Angle_WherePositiveYX_ShouldResultIn90Deg_Test()
        {
            Vector3 v1 = new Vector3(0, ArbitaryTestDouble, 0);
            Vector3 v2 = new Vector3(ArbitaryTestDouble, 0, 0);
            var result = v1.Angle(v2);

            result.Should().Be(Deg90AsRad, "vector (0,n,0) is perpendicular to vector (n,0,0)");
        }

        [TestMethod, TestCategory("Angle")]
        public void Angle_WherePositiveXNegativeY_ShouldResultIn90Deg_Test()
        {
            Vector3 v1 = new Vector3(ArbitaryTestDouble, 0, 0);
            Vector3 v2 = new Vector3(0, -ArbitaryTestDouble, 0);
            var result = v1.Angle(v2);

            result.Should().Be(Deg90AsRad, "vector (n,0,0) is perpendicular to vector (0,-n,0)");
        }

        [TestMethod, TestCategory("Angle")]
        public void Angle_WhereNegativeYPositiveX_ShouldResultIn90Deg_Test()
        {
            Vector3 v1 = new Vector3(0, -ArbitaryTestDouble, 0);
            Vector3 v2 = new Vector3(ArbitaryTestDouble, 0, 0);
            var result = v1.Angle(v2);

            result.Should().Be(Deg90AsRad, "vector (0,-n,0) is perpendicular to vector (n,0,0)");
        }

        [TestMethod, TestCategory("Angle")]
        public void Angle_WhereNegativeXY_ShouldResultIn90Deg_Test()
        {
            Vector3 v1 = new Vector3(-ArbitaryTestDouble, 0, 0);
            Vector3 v2 = new Vector3(0, -ArbitaryTestDouble, 0);
            var result = v1.Angle(v2);

            result.Should().Be(Deg90AsRad, "vector (-n,0,0) is perpendicular to vector (0,-n,0)");
        }

        [TestMethod, TestCategory("Angle")]
        public void Angle_WhereNegativeYX_ShouldResultIn90Deg_Test()
        {
            Vector3 v1 = new Vector3(0, -ArbitaryTestDouble, 0);
            Vector3 v2 = new Vector3(-ArbitaryTestDouble, 0, 0);
            var result = v1.Angle(v2);

            result.Should().Be(Deg90AsRad, "vector (0,-n,0) is perpendicular to vector (-n,0,0)");
        }

        [TestMethod, TestCategory("Angle")]
        public void Angle_WhereNegativeXPositiveY_ShouldResultIn90Deg_Test()
        {
            Vector3 v1 = new Vector3(-ArbitaryTestDouble, 0, 0);
            Vector3 v2 = new Vector3(0, ArbitaryTestDouble, 0);
            var result = v1.Angle(v2);

            result.Should().Be(Deg90AsRad, "vector (-n,0,0) is perpendicular to vector (0,n,0)");
        }

        [TestMethod, TestCategory("Angle")]
        public void Angle_WherePositiveYNegativeX_ShouldResultIn90Deg_Test()
        {
            Vector3 v1 = new Vector3(0, ArbitaryTestDouble, 0);
            Vector3 v2 = new Vector3(-ArbitaryTestDouble, 0, 0);
            var result = v1.Angle(v2);

            result.Should().Be(Deg90AsRad, "vector (0,n,0) is perpendicular to vector (-n,0,0)");
        }

        [TestMethod, TestCategory("Angle")]
        public void Angel_WithTwoPerpendicularVectorsUsingWholeNumbers_ShouldBePIRad_Test()
        {
            var a = new Vector3(1, 0, 0);
            var b = new Vector3(-1, 0, 0);
            var angle = a.Angle(b);

            angle.Should().Be(System.Math.PI);
        }

        [TestMethod, TestCategory("Angle")]
        public void Angel_WithTwoVectorsUsingWholeNumbers_ShouldResultin180Deg_Test()
        {
            var a = new Vector3(1, 0, 0);
            var b = new Vector3(-1, 0, 0);
            var angle = a.Angle(b);

            angle.Should().Be(System.Math.PI);
        }

        [TestMethod, TestCategory("Angle")]
        public void Angle_WithIdenticalVectorsUsingWholeNumbers_ShouldBe0_Test()
        {
            var a = new Vector3(1, 0, 0);
            var b = new Vector3(1, 0, 0);
            var angle = a.Angle(b);

            angle.Should().Be(0);
        }

        [TestMethod, TestCategory("Angle")]
        public void Angle_WithIdenticalVectorsUsingLongFloatingPointNumbers_ShouldBe0_Test()
        {
            var a = new Vector3(0.795271508195995f, -0.0612034045226753f, -0.603156175071185f);
            var b = new Vector3(0.795271508195995f, -0.0612034045226753f, -0.603156175071185f);
            var angle = a.Angle(b);

            angle.Should().Be(0);
        }

        /// <summary>
        /// Check that the angle between two vectors does not result in NaN
        /// When using a floating point number with positive and negative values as data type float
        /// </summary>
        /// <acknowlagement>Based on an example issue and solution from Dennis E. Cox (In comments on CodeProject, http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C )</acknowlagement>
        [TestMethod, TestCategory("Angle")]
        public void Angle_WithVectorsUsingLongFloatingPointNumbers_ShouldNotResultInNaN_Test()
        {
            var a = new Vector3(0.795271508195995f, -0.0612034045226753f, -0.603156175071185f);
            var b = new Vector3(0.795271508449802f, -0.0612033993276936f, -0.60315617526368f);
            var angle = a.Angle(b);

            angle.Should().NotBe(float.NaN);
            angle.Should().BeLessThan(1);
            angle.Should().BeGreaterOrEqualTo(0);
        }

        [TestMethod, TestCategory("Angle")]
        public void Angle_WithIdenticalVectorsUsingDoubleFloatingPointNumbers_ShouldBe0_Test()
        {
            var a = new Vector3(0.795271508195995d, -0.0612034045226753d, -0.603156175071185d);
            var b = new Vector3(0.795271508195995d, -0.0612034045226753d, -0.603156175071185d);
            var angle = a.Angle(b);

            angle.Should().Be(0);
        }

        /// <summary>
        /// Check that the angle between two vectors does not result in NaN
        /// When using a floating point number with positive and negative values as data type double
        /// </summary>
        /// <acknowlagement>Based on an example issue and solution from Dennis E. Cox (In comments on CodeProject, http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C )</acknowlagement>
        [TestMethod, TestCategory("Angle")]
        public void Angle_WithVectorsUsingDoubleFloatingPointNumbers_ShouldNotResultInNaN_Test()
        {
            var a = new Vector3(0.795271508195995d, -0.0612034045226753d, -0.603156175071185d);
            var b = new Vector3(0.795271508449802d, -0.0612033993276936d, -0.60315617526368d);
            var angle = a.Angle(b);

            angle.Should().NotBe(float.NaN);
            angle.Should().BeLessThan(1);
            angle.Should().BeGreaterOrEqualTo(0);
        }

        [TestMethod, TestCategory("Angle")]
        public void Angle_WithOfIdenticalVectorsUsingMaxAndMinFloatingPointNumbers_ShouldBe0_Test()
        {
            var a = new Vector3(float.MaxValue, float.MinValue, 0);
            var b = new Vector3(float.MaxValue, float.MinValue, 0);
            var angle = a.Angle(b);

            angle.Should().Be(0);
        }

        [TestMethod, TestCategory("Angle")]
        public void Angle_WithDifferentWholeNumbers_ShouldResultInAFractionalNumber_Test()
        {
            Vector3 s0 = new Vector3(7719, 0, 38);
            Vector3 v1 = new Vector3(38, 0, 7719);
            var result = s0.Angle(v1);

            result.Should().Be(1.560950571379345, "this was the result worked out by Pex");
            Math.Round(result, 3, MidpointRounding.AwayFromZero).Should().Be(1.561d, @"this is what http://calculator.tutorvista.com/angle-between-two-vectors-calculator.html worked out the result to be");
        }

        #endregion

        #region Normalize

        [TestMethod, TestCategory("Normalize")]
        public void NormalizeOrDefault_WithPositiveZeroVector_ShouldBe0_Test()
        {
            var vector = new Vector3(0, 0, 0);
            var result = Vector3.NormalizeOrDefault(vector);

            result.Should().Be(new Vector3(0, 0, 0));
        }

        [TestMethod, TestCategory("Normalize")]
        public void Normalize_WithPositiveZeroVector_ShouldThrowException_Test()
        {
            var vector = new Vector3(0, 0, 0);
            Action action = () => vector.Normalize();

            action.ShouldThrow<NormalizeVectorException>();
        }

        [TestMethod, TestCategory("Normalize")]
        public void NormalizeOrDefault_WithNegativeZeroVector_ShouldBe0_Test()
        {
            var vector = new Vector3(-0, -0, -0);
            var result = Vector3.NormalizeOrDefault(vector);

            result.Should().Be(new Vector3(0, 0, 0));
        }

        [TestMethod, TestCategory("Normalize")]
        public void Normalize_WithNegativeZeroVector_ShouldThrowException_Test()
        {
            var vector = new Vector3(-0, -0, -0);
            Action action = () => vector.Normalize();

            action.ShouldThrow<NormalizeVectorException>();
        }

        [TestMethod, TestCategory("Normalize")]
        public void NormalizeOrDefault_WithPositievAndNegativeZeroVector_ShouldBe0_Test()
        {
            var vector = new Vector3(-0, +0, -0);
            var result = Vector3.NormalizeOrDefault(vector);

            result.Should().Be(new Vector3(0, 0, 0));
        }

        [TestMethod, TestCategory("Normalize")]
        public void Normalize_WithPositievAndNegativeZeroVector_ShouldThrowException_Test()
        {
            var vector = new Vector3(-0, +0, -0);
            Action action = () => vector.Normalize();

            action.ShouldThrow<NormalizeVectorException>();
        }

        [TestMethod, TestCategory("Normalize")]
        public void NormalizeOrDefault_WithPositievAndNegativeZeroVector_ShouldThrowException_Test()
        {
            var vector = new Vector3(-0, +0, -0);
            var result = Vector3.NormalizeOrDefault(vector);

            result.Should().Be(new Vector3(-0, +0, -0));
        }

        [TestMethod, TestCategory("Normalize")]
        public void Normalize_WithUnitVectorXIsOne_ShouldNotChangeDuringNormalization_Test()
        {
            var vector = new Vector3(1, 0, 0);
            var result = vector.Normalize();

            result.X.Should().Be(1);
            result.Y.Should().Be(0);
            result.Z.Should().Be(0);
        }

        [TestMethod, TestCategory("Normalize")]
        public void NormalizeOrDefault_WithUnitVectorXIsOne_ShouldNotChangeDuringNormalization_Test()
        {
            var vector = new Vector3(1, 0, 0);
            var result = Vector3.NormalizeOrDefault(vector);

            result.X.Should().Be(1);
            result.Y.Should().Be(0);
            result.Z.Should().Be(0);
        }

        [TestMethod, TestCategory("Normalize")]
        public void Normalize_WithUnitVectorXIsNegativeOne_ShouldNotChangeDuringNormalization_Test()
        {
            var vector = new Vector3(-1, 0, 0);
            var result = vector.Normalize();

            result.X.Should().Be(-1);
            result.Y.Should().Be(0);
            result.Z.Should().Be(0);
        }

        [TestMethod, TestCategory("Normalize")]
        public void NormalizeOrDefault_WithUnitVectorXIsNegativeOne_ShouldNotChangeDuringNormalization_Test()
        {
            var vector = new Vector3(-1, 0, 0);
            var result = Vector3.NormalizeOrDefault(vector);

            result.X.Should().Be(-1);
            result.Y.Should().Be(0);
            result.Z.Should().Be(0);
        }

        [TestMethod, TestCategory("Normalize")]
        public void Normalize_WithUnitVectorXIsPositiveInfinity_ShouldResultInXBeingOne_Test()
        {
            var vector = new Vector3(double.PositiveInfinity, 0, 0);
            var result = vector.Normalize();

            result.X.Should().Be(1);
            result.Y.Should().Be(0);
            result.Z.Should().Be(0);
        }

        [TestMethod, TestCategory("Normalize")]
        public void NormalizeOrDefault_WithUnitVectorXIsPositiveInfinity_ShouldResultInXBeingOne_Test()
        {
            var vector = new Vector3(double.PositiveInfinity, 0, 0);
            var result = Vector3.NormalizeOrDefault(vector);

            result.X.Should().Be(1);
            result.Y.Should().Be(0);
            result.Z.Should().Be(0);
        }

        [TestMethod, TestCategory("Normalize")]
        public void Normalize_WithVectorXIsPositiveInfinityAndOtherComponentsAreNotNull_ShouldThrowException_Test()
        {
            var vector = new Vector3(double.PositiveInfinity, 3, 6);
            Action action = () => vector.Normalize();

            action.ShouldThrow<NormalizeVectorException>();
        }

        [TestMethod, TestCategory("Normalize")]
        public void NormalizeOrDefault_WithVectorXIsPositiveInfinityAndOtherComponentsAreNotNull_ShouldResultInNaN_Test()
        {
            var vector = new Vector3(double.PositiveInfinity, 3, 6);
            var result = Vector3.NormalizeOrDefault(vector);

            result.X.Should().Be(double.NaN);
            result.Y.Should().Be(double.NaN);
            result.Z.Should().Be(double.NaN);
        }

        [TestMethod, TestCategory("Normalize")]
        public void Normalize_WithUnitVectorXIsNegativeInfinity_ShouldResultInXBeingOne_Test()
        {
            var vector = new Vector3(double.NegativeInfinity, 0, 0);
            var result = vector.Normalize();

            result.X.Should().Be(-1);
            result.Y.Should().Be(0);
            result.Z.Should().Be(0);
        }

        [TestMethod, TestCategory("Normalize")]
        public void Normalize_WithVectorXIsNegativeInfinityAndOtherComponentsAreNotNull_ShouldThrowException_Test()
        {
            var vector = new Vector3(double.NegativeInfinity, 3, 6);
            Action action = () => vector.Normalize();

            action.ShouldThrow<NormalizeVectorException>();
        }

        [TestMethod, TestCategory("Normalize")]
        public void NormalizeOrDefault_WithVectorXIsNegativeInfinityAndOtherComponentsAreNotNull_ShouldResultInNaN_Test()
        {
            var vector = new Vector3(double.NegativeInfinity, 3, 6);
            var result = Vector3.NormalizeOrDefault(vector);

            result.X.Should().Be(double.NaN);
            result.Y.Should().Be(double.NaN);
            result.Z.Should().Be(double.NaN);
        }

        [TestMethod, TestCategory("Normalize")]
        public void NormalizeOrDefault_WithUnitVectorXIsNegativeInfinity_ShouldResultInXBeingOne_Test()
        {
            var vector = new Vector3(double.NegativeInfinity, 0, 0);
            var result = Vector3.NormalizeOrDefault(vector);

            result.X.Should().Be(-1);
            result.Y.Should().Be(0);
            result.Z.Should().Be(0);
        }

        [TestMethod, TestCategory("Normalize")]
        public void Normalize_WithUnitVectorXIsPositiveNumber_ShouldResultInXBeingOne_Test()
        {
            var vector = new Vector3(10, 0, 0);
            var result = vector.Normalize();

            result.X.Should().Be(1);
            result.Y.Should().Be(0);
            result.Z.Should().Be(0);
        }

        [TestMethod, TestCategory("Normalize")]
        public void NormalizeOrDefault_WithUnitVectorXIsPositiveNumber_ShouldResultInXBeingOne_Test()
        {
            var vector = new Vector3(10, 0, 0);
            var result = Vector3.NormalizeOrDefault(vector);

            result.X.Should().Be(1);
            result.Y.Should().Be(0);
            result.Z.Should().Be(0);
        }

        /// <summary>
        /// Test that vectors containing a NaN component will throw an exception when being Normalized
        /// </summary>
        [TestMethod, TestCategory("Normalize")]
        public void Normalize_WithUnitVectorXIsNaN_ShouldThrowException_Test()
        {
            var vector = new Vector3(double.NaN, 0, 0);

            Action act = () => vector.Normalize();

            act.ShouldThrow<NormalizeVectorException>("you should not be able to normalize NaN values");
        }

        [TestMethod, TestCategory("Normalize")]
        public void NormalizeOrDefault_WithUnitVectorXIsNaN_ShouldReturnXYZOfNaN_Test()
        {
            var vector = new Vector3(double.NaN, 0, 0);
            var result = Vector3.NormalizeOrDefault(vector);

            result.X.Should().Be(double.NaN);
            result.Y.Should().Be(double.NaN);
            result.Z.Should().Be(double.NaN);
        }

        /// <summary>
        /// Test the normalization of a vector
        /// </summary>
        /// <acknowlagement>Example from http://www.fundza.com/vectors/normalize/ </acknowlagement>
        [TestMethod, TestCategory("Normalize")]
        public void Normalize_WithPositiveWholeNumbers_ShouldBeCorrect_Test()
        {
            var vector = new Vector3(3, 1, 2);
            var result = vector.Normalize();

            result.X.Should().BeInRange(0.8014, 0.8026);
            result.Y.Should().BeInRange(0.2664, 0.2676);
            result.Z.Should().BeInRange(0.5334, 0.5346);
        }

        /// <summary>
        /// Test the normalization of a vector
        /// </summary>
        [TestMethod, TestCategory("Normalize")]
        public void NormalizeOrDefault_WithPositiveWholeNumbers_ShouldBeCorrect_Test()
        {
            var vector = new Vector3(3, 1, 2);
            var result = Vector3.NormalizeOrDefault(vector);

            result.X.Should().BeInRange(0.8014, 0.8026);
            result.Y.Should().BeInRange(0.2664, 0.2676);
            result.Z.Should().BeInRange(0.5334, 0.5346);
        }

        #endregion

        #region Yaw, Pitch, Roll Tests

        /// <summary>
        /// Yaw 90 degrees (checking the result to six decimal places)
        /// </summary>
        [TestMethod]
        public void YawTest()
        {
            var vector = new Vector3(1, 0, 0);
            var result = vector.Yaw(Deg90AsRad);

            System.Math.Round(result.X, 6).Should().Be(0);
            System.Math.Round(result.Y, 6).Should().Be(0);
            System.Math.Round(result.Z, 6).Should().Be(-1);
        }

        /// <summary>
        /// Pitch 90 degrees (checking the result to six decimal places)
        /// </summary>
        [TestMethod]
        public void PitchTest()
        {
            var vector = new Vector3(0, 1, 0);
            var result = vector.Pitch(Deg90AsRad);

            System.Math.Round(result.X, 6).Should().Be(0);
            System.Math.Round(result.Y, 6).Should().Be(0);
            System.Math.Round(result.Z, 6).Should().Be(1);
        }

        /// <summary>
        /// Roll 90 degrees (checking the result to six decimal places)
        /// </summary>
        [TestMethod]
        public void RollTest()
        {
            var vector = new Vector3(1, 0, 0);
            var result = vector.Roll(Deg90AsRad);

            System.Math.Round(result.X, 6).Should().Be(0);
            System.Math.Round(result.Y, 6).Should().Be(1);
            System.Math.Round(result.Z, 6).Should().Be(0);
        }

        #endregion

        #region Project, Rejection and Reflection Tests

        /// <summary>
        /// Test the projection of one vector onto another
        /// </summary>
        /// <acknowlagement>Example from http://www.vitutor.com/geometry/vec/vector_projection.html </acknowlagement>
        [TestMethod]
        public void ProjectionTest()
        {
            var a = new Vector3(2, 1, 0);
            var b = new Vector3(-3, 4, 0);
            var result = a.Projection(b);

            result.X.Should().Be(6d / 25d, "X should be 6/25");
            result.Y.Should().Be(-(8d / 25d), "Y should be -(8/25)");
            result.Z.Should().Be(0, "Z should be 0");
        }

        /// <summary>
        /// Test the projection of one vector onto another at 90 deg is 0
        /// </summary>
        /// <acknowlagement>Example from http://en.wikipedia.org/wiki/Vector_projection </acknowlagement>
        [TestMethod]
        public void ProjectionWhereVectorsAreAt90DegTest()
        {
            var a = new Vector3(1, 0, 0);
            var b = new Vector3(0, 1, 0);
            var result = a.Projection(b);

            result.X.Should().Be(0, "X should be 0");
            result.Y.Should().Be(0, "Y should be 0");
            result.Z.Should().Be(0, "Z should be 0");
        }

        /// <summary>
        /// Test the rejection of one vector onto another
        /// </summary>
        [TestMethod]
        public void RejectionTest()
        {
            var a = new Vector3(2, 1, 0);
            var b = new Vector3(-3, 4, 0);
            var result = a.Rejection(b);

            result.X.Should().Be(2d - (6d / 25d), "X should be 2-(6/25)");
            result.Y.Should().Be(1d + (8d / 25d), "Y should be 1+(8/25)");
            result.Z.Should().Be(0, "Z should be 0");
        }

        [TestMethod]
        public void Reflection_WhereXis1AboutVectorWhereYisOne_ShouldBeVectorWhereXisNegativeOne_Test()
        {
            var a = new Vector3(1, 0, 0);
            var b = new Vector3(0, 1, 0);
            var result = a.Reflection(b);

            result.X.Should().Be(-1, "X should be -1");
            result.Y.Should().Be(0, "Y should be 0");
            result.Z.Should().Be(0, "Z should be 0");
        }

        [TestMethod]
        public void Reflection_WhereV1IsXNegative10Yis10AboutYAxisUnitVector_ShouldBeX10Y10_Test()
        {
            var a = new Vector3(-10, 10, 0);
            var b = new Vector3(0, 1, 0); // about y unit vector
            var result = a.Reflection(b);

            result.X.Should().Be(10, "X should be 10");
            result.Y.Should().Be(10, "Y should be 10");
            result.Z.Should().Be(0, "Z should be 0");
        }

        [TestMethod]
        public void Reflection_WhereV1IsY10AboutX5Y5_ShouldBeX10_Test()
        {
            var a = new Vector3(0, 10, 0);
            var b = new Vector3(5, 5, 0);
            var result = a.Reflection(b);

            result.X.Should().Be(10, "X should be 10");

            // Epsilon is not tolerant enough
            //result.Y.Should().BeInRange(-double.Epsilon, double.Epsilon, "Y should be 0 with a tolerance (double.epsilon)");
            //result.Z.Should().BeInRange(-double.Epsilon, double.Epsilon, "Z should be 0 with a tolerance (double.epsilon)");

            result.Y.Should().BeInRange(-1E-10, 1E-10, "Y should be 0 with a tolerance");
            result.Z.Should().BeInRange(-1E-10, 1E-10, "Z should be 0 with a tolerance");
        }

        #endregion

        #region Rounding Tests

        [TestMethod]
        public void RoundingTest()
        {
            var vector = new Vector3(0.1, 0.5, 0.9);
            var result = vector.Round(MidpointRounding.AwayFromZero);

            result.X.Should().Be(0, "X should be rounded down");
            result.Y.Should().Be(1, "Y should be rounded up");
            result.Y.Should().Be(1, "Z should be rounded up");
        }

        #endregion

        #region Rotation Tests

        [TestMethod]
        public void RotateArroundYAxisShouldBeTheSameAsYawTest()
        {
            var vector = new Vector3(1, 0, 0);
            var yaw = vector.Yaw(Deg90AsRad);
            var rotate = vector.RotateY(Deg90AsRad);

            rotate.Should().Be(yaw);
        }

        /// <summary>
        /// Pitch 90 degrees (checking the result to six decimal places)
        /// </summary>
        [TestMethod]
        public void RotateArroundXAxisShouldBeTheSameAsPitchTest()
        {
            var vector = new Vector3(0, 1, 0);
            var pitch = vector.Pitch(Deg90AsRad);
            var rotate = vector.RotateX(Deg90AsRad);

            System.Math.Round(pitch.X, 6).Should().Be(0);
            System.Math.Round(pitch.Y, 6).Should().Be(0);
            System.Math.Round(pitch.Z, 6).Should().Be(1);

            rotate.Should().Be(pitch);
        }

        /// <summary>
        /// Roll 90 degrees (checking the result to six decimal places)
        /// </summary>
        [TestMethod]
        public void RotateArroundZShouldBeTheSameAsRollTest()
        {
            var vector = new Vector3(1, 0, 0);
            var roll = vector.Roll(Deg90AsRad);
            var rotate = vector.RotateZ(Deg90AsRad);

            rotate.Should().Be(roll);
        }

        [TestMethod]
        public void RotateArroundYWith0OffsetParametersAxisShouldBeTheSameAsYawTest()
        {
            var vector = new Vector3(1, 0, 0);
            var yaw = vector.Yaw(Deg90AsRad);
            var rotate = vector.RotateY(0, 0, Deg90AsRad);

            rotate.Should().Be(yaw);
        }

        /// <summary>
        /// Pitch 90 degrees (checking the result to six decimal places)
        /// </summary>
        [TestMethod]
        public void RotateArroundXAxis0OffsetParametersShouldBeTheSameAsPitchTest()
        {
            var vector = new Vector3(0, 1, 0);
            var pitch = vector.Pitch(Deg90AsRad);
            var rotate = vector.RotateX(0, 0, Deg90AsRad);

            System.Math.Round(pitch.X, 6).Should().Be(0);
            System.Math.Round(pitch.Y, 6).Should().Be(0);
            System.Math.Round(pitch.Z, 6).Should().Be(1);

            rotate.Should().Be(pitch);
        }

        /// <summary>
        /// Roll 90 degrees (checking the result to six decimal places)
        /// </summary>
        [TestMethod]
        public void RotateArroundZ0OffsetParametersShouldBeTheSameAsRollTest()
        {
            var vector = new Vector3(1, 0, 0);
            var roll = vector.Roll(Deg90AsRad);
            var rotate = vector.RotateZ(0, 0, Deg90AsRad);

            rotate.Should().Be(roll);
        }

        #endregion

        #region Comparison Tests

        [TestMethod, TestCategory("CompareTo")]
        public void CompareTo_WhereWeAreTestingGreaterObjectEquivenenceAndLess_Test()
        {
            // Magnitude 8555.6321215910166084530447384188
            Vector3 s0 = new Vector3(1796, 0, 8365);
            object box = (object)(s0);

            // Magnitude 2404.1643038694339619441734523204
            Vector3 v1 = new Vector3(449, 2282, 609);

            var result = s0.CompareTo(v1);
            result.Should().Be(1, "magnitude of s0 is bigger than magnitude of s1");

            result = v1.CompareTo(s0);
            result.Should().Be(-1, "magnitude of s1 is less than magnitude of s0");

            result = v1.CompareTo(box);
            result.Should().Be(-1, "magnitude of s1 is less than magnitude of s0 even when s0 has been cast to an object");
        }

        [TestMethod, TestCategory("CompareTo")]
        public void CompareTo_WhereZComponentsArePositiveInfinity_ShouldBe0_Test()
        {
            Vector3 v1 = new Vector3(0, 0, double.PositiveInfinity);
            Vector3 v2 = new Vector3(0, 0, double.PositiveInfinity);

            var zComponentResult = v1.Z.CompareTo(v2.Z);
            var result = v1.CompareTo(v2);

            // Test our assumption about the .Net framework expectations
            double.PositiveInfinity.CompareTo(double.PositiveInfinity).Should().Be(0, "the .Net framework should find double positive infinty equal to positive infinity (if this assumption is wrong then the logic of this test is also wrong)");

            // Test that the component operation matches our assumption about the .Net framework
            zComponentResult.Should().Be(0, "z components of positive infinty and positive infinty should be equal");

            // Test that our result matches the assumption
            result.Should().Be(0, "positive infinty and positive infinty should be equal, as should the other components");
        }

        [TestMethod, TestCategory("CompareTo")]
        public void CompareTo_WhereZComponentsAreNaN_ShouldBe0_Test()
        {
            Vector3 v1 = new Vector3(0, 0, double.NaN);
            Vector3 v2 = new Vector3(0, 0, double.NaN);

            var zComponentResult = v1.Z.CompareTo(v2.Z);
            var result = v1.CompareTo(v2);

            // Test our assumption about the .Net framework expectations
            double.NaN.CompareTo(double.NaN).Should().Be(0, "the .Net framework should find double NaN equal to NaN (if this assumption is wrong then the logic of this test is also wrong)");

            // Test that the component operation matches our assumption about the .Net framework
            zComponentResult.Should().Be(0, "z components of NaN and NaN should be equal");

            // Test that our result matches the assumption
            result.Should().Be(0, "NaN and NaN should be equal, as should the other components");
        }

        [TestMethod, TestCategory("CompareTo")]
        public void CompareTo_WhereOneZIsNaNAndTheOtherXIsNaN_ShouldBe0_Test()
        {
            Vector3 v1 = new Vector3(0, 0, double.NaN);
            Vector3 v2 = new Vector3(double.NaN, 0, 0);

            var result = v1.CompareTo(v2);

            result.Should().Be(0, "both magnitudes should be NaN so comparison should find the vectors equivelent");
        }

        [TestMethod, TestCategory("CompareTo")]
        public void CompareTo_WhereOneZIsNaNAndTheOtherXIsNaNAndTheYComponentsDoNotMatch_ShouldBe0_Test()
        {
            Vector3 v1 = new Vector3(0, 10, double.NaN);
            Vector3 v2 = new Vector3(double.NaN, -3, 0);

            var result = v1.CompareTo(v2);

            result.Should().Be(0, "both magnitudes should be NaN when any component is NaN so comparison should find the vectors equivelent");
        }

        [TestMethod, TestCategory("CompareTo")]
        public void CompareTo_WhereOneXYIsNaNAndTheOtherXIsNaN_ShouldBe0_Test()
        {
            Vector3 v1 = new Vector3(double.NaN, double.NaN, 0);
            Vector3 v2 = new Vector3(double.NaN, 0, 0);

            var result = v1.CompareTo(v2);

            result.Should().Be(0, "both magnitudes should be NaN so comparison should find the vectors equivelent");
        }

        [TestMethod, TestCategory("CompareTo")]
        public void CompareTo_WhereZComponentsAreNegativeInfinity_ShouldBe0_Test()
        {
            Vector3 v1 = new Vector3(0, 0, double.NegativeInfinity);
            Vector3 v2 = new Vector3(0, 0, double.NegativeInfinity);

            var zComponentResult = v1.Z.CompareTo(v2.Z);
            var result = v1.CompareTo(v2);

            // Test our assumption about the .Net framework expectations
            double.NegativeInfinity.CompareTo(double.NegativeInfinity).Should().Be(0, "the .Net framework should find double negative infinty equal to negative infinity (if this assumption is wrong then the logic of this test is also wrong)");

            // Test that the component operation matches our assumption about the.Net framework
            zComponentResult.Should().Be(0, "z components of negative infinty and negative infinty should be equal");

            // Test that our result matches the assumption
            result.Should().Be(0, "negative infinty and negative infinty should be equal, as should the other components");
        }

        [TestMethod, TestCategory("CompareTo")]
        public void CompareTo_WhereZComponentsArePositiveInfinityAndNegativeInfinity_ShouldNotBe0_Test()
        {
            Vector3 v1 = new Vector3(0, 0, double.PositiveInfinity);
            Vector3 v2 = new Vector3(0, 0, double.NegativeInfinity);

            var s1s2Result = v1.CompareTo(v2);
            var s2s1Result = v2.CompareTo(v1);

            s1s2Result.Should().Be(0, "when comparing magnitude sign should not be important so positive infinty should be equal to negative infinty");
            s2s1Result.Should().Be(0, "when comparing magnitude sign should not be important so negative infinty should be equal to positive infinty");
        }

        [TestMethod, TestCategory("CompareTo")]
        public void CompareToWithTolerance_WhereTheZComponentsAreDifferentByLessThanTheTolerance_ShouldBe0_Test()
        {
            Vector3 v1 = new Vector3(0, 0, 3);
            Vector3 v2 = new Vector3(0, 0, 2.9999);

            var result = v1.CompareTo(v2, 0.0002);

            // Quick test to ensure that the double minus operation does not introduce an unexpected margin of error that is higher than our margin of error
            (3d - (2.9999d)).Should().BeLessThan(0.0002d, "the double calculations of 3 minus 2.9999 should be less than 0.0002 or our test is incorrect");

            // Assert our acual test
            result.Should().Be(0, "the tolerance should have been greter than the difference between the z components for s1 and s2");
        }

        [TestMethod, TestCategory("CompareTo")]
        public void CompareToWithTolerance_WhereZComponentDifferenceIsGreaterThanTheTolerance_ShouldBe1_Test()
        {
            Vector3 v1 = new Vector3(0, 0, 3);
            Vector3 v2 = new Vector3(0, 0, 2.9999);

            var result = v1.CompareTo(v2, 0.00009);

            // Quick test to ensure that the double minus operation does not introduce an unexpected margin of error
            (3d - 2.9999d).Should().BeGreaterThan(0.00009d, "the double calculations of 3 minus 2.9999 should be greater than 0.00009 or our test is incorrect");

            result.Should().Be(1, "the tolerance should be less than the difference between the z components for s1 and s2, so 3 should be greater than 2.9999");
        }

        [TestMethod, TestCategory("CompareTo")]
        public void CompareToWithTolerance_WhereZComponentDifferenceIsGreaterThanTheTolerance_ShouldBeNegative1_Test()
        {
            Vector3 v1 = new Vector3(0, 0, 2.9999);
            Vector3 v2 = new Vector3(0, 0, 3);
            
            var result = v1.CompareTo(v2, 0.00009);

            // Quick test to ensure that the double minus operation does not introduce an unexpected margin of error
            (3d - 2.9999d).Should().BeGreaterThan(0.00009d, "the double calculations of 3 minus 2.9999 should be greater than 0.00009 or our test is incorrect");

            result.Should().Be(-1, "the tolerance should be less than the difference between the z components for s1 and s2, so 2.9999 should be less than 3");
        }

        [TestMethod, TestCategory("CompareTo")]
        public void CompareToWithTolerance_WhereZComponentsArePositiveInfinity_ShouldBe0_Test()
        {
            Vector3 v1 = new Vector3(0, 0, double.PositiveInfinity);
            Vector3 v2 = new Vector3(0, 0, double.PositiveInfinity);

            var result = v1.CompareTo(v2, 0.00001);

            result.Should().Be(0, "positive infinty and positive infinty should be equal regardless of tolerance, as should the other components");
        }

        [TestMethod, TestCategory("CompareTo")]
        public void CompareToWithTolerance_WhereZComponentsAreNegativeInfinity_ShouldBe0_Test()
        {
            Vector3 v1 = new Vector3(0, 0, double.NegativeInfinity);
            Vector3 v2 = new Vector3(0, 0, double.NegativeInfinity);

            var result = v1.CompareTo(v2, 0.00001);

            result.Should().Be(0, "negative infinty and negative infinty should be equal regardless of tolerance, as should the other components");
        }

        [TestMethod, TestCategory("CompareTo")]
        public void CompareToWithTolerance_WhereZComponentsArePositiveInfinityAndNegativeInfinity_ShouldNotBe0_Test()
        {
            Vector3 v1 = new Vector3(0, 0, double.PositiveInfinity);
            Vector3 v2 = new Vector3(0, 0, double.NegativeInfinity);

            var s1s2Result = v1.CompareTo(v2, 0.00001);
            var s2s1Result = v2.CompareTo(v1, 0.00001);

            s1s2Result.Should().Be(0, "when comparing magnitude sign should not be important so positive infinty should be equal to negative infinty");
            s2s1Result.Should().Be(0, "when comparing magnitude sign should not be important so negative infinty should be equal to positive infinty");
        }

        #endregion

        #region Equality

        [TestMethod, TestCategory("Equals")]
        public void EqualityOpererator_WhereTheComponentsAreEqual_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(1, 2, 3);
            Vector3 v2 = new Vector3(1, 2, 3);

            var result = v1 == v2;

            result.Should().Be(true);
        }

        [TestMethod, TestCategory("Equals")]
        public void EqualityOpererator_WhereXYZOrderIsImportantAndTheComponentAreNotInTheCorrectOrder_ShouldBeFalse_Test()
        {
            Vector3 v1 = new Vector3(1, 2, 3);
            Vector3 v2 = new Vector3(3, 2, 1);

            var result = v1 == v2;

            result.Should().Be(false);
        }

        [TestMethod, TestCategory("Equals")]
        public void EqualityOpererator_WhereZComponentsArePositiveInfinity_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(1, 2, double.PositiveInfinity);
            Vector3 v2 = new Vector3(1, 2, double.PositiveInfinity);

            var zComponentResult = v1.Z == v2.Z;
            var result = v1 == v2;

            // Test our assumption about the .Net framework expectations
            (double.PositiveInfinity == double.PositiveInfinity).Should().Be(true, "the .Net framework should find double positive infinty equal to positive infinity (if this assumption is wrong then the logic of this test is also wrong)");

            // Test that the component operation matches our assumption about the .Net framework
            zComponentResult.Should().Be(true, "z components of positive infinty and positive infinty should be equal");

            // Test that our result matches the assumption
            result.Should().Be(true, "positive infinty and positive infinty should be equal");
        }

        [TestMethod, TestCategory("Equals")]
        public void EqualityOpererator_WhereZComponentsAreNegativeInfinity_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(1, 2, double.NegativeInfinity);
            Vector3 v2 = new Vector3(1, 2, double.NegativeInfinity);

            var zComponentResult = v1.Z == v2.Z;
            var result = v1 == v2;

            // Test our assumption about the .Net framework expectations
            (double.NegativeInfinity == double.NegativeInfinity).Should().Be(true, "the .Net framework should find double negative infinty equal to negative infinity (if this assumption is wrong then the logic of this test is also wrong)");

            // Test that the component operation matches our assumption about the.Net framework
            zComponentResult.Should().Be(true, "z components of negative infinty and negative infinty should be equal");

            // Test that our result matches the assumption
            result.Should().Be(true, "negative infinty and negative infinty should be equal");
        }

        [TestMethod, TestCategory("Equals")]
        public void EqualityOpererator_WhereOneZComponentIsNaN_ShouldBeFalse_Test()
        {
            Vector3 v1 = new Vector3(1, 2, 3);
            Vector3 v2 = new Vector3(1, 2, double.NaN);

            var zComponentResult = v1.Z == v2.Z;
            var result = v1 == v2;

            // Test our assumption about the .Net framework expectations
            (3 == double.NaN).Should().Be(false, "the .Net framework should find double 3 not equal to double NaN (if this assumption is wrong then the logic of this test is also wrong)");

            // Test that the component operation matches our assumption about the .Net framework
            zComponentResult.Should().Be(false, "3 and NaN should not be equal");

            // Test that our result matches the assumption
            result.Should().Be(false, "the two vectors should not be equal given one of the Z components is NaN");
        }

        [TestMethod, TestCategory("Equals")]
        public void EqualityOpererator_WhereZComponentsAreNaN_ShouldBeFalse_Test()
        {
            // Interestingly double.NaN == double.NaN is false while double.NaN.Equals(double.NaN) is true.
            // Lets be consistent with the .Net framework even if it isn't consistent with itself

            Vector3 v1 = new Vector3(1, 2, double.NaN);
            Vector3 v2 = new Vector3(1, 2, double.NaN);

            var zComponentResult = v1.Z == v2.Z;
            var result = v1 == v2;

            // Test our assumption about the .Net framework expectations
            (double.NaN == double.NaN).Should().Be(false, "the .Net framework should find double NaN not equal to double NaN using the == operator (if this assumption is wrong then the logic of this test is also wrong)");

            // Test that the component operation matches our assumption about the .Net framework
            zComponentResult.Should().Be(false, "NaN and NaN should not be equal when using the == operator");

            // Test that our result matches the assumption
            result.Should().Be(false, "the two vectors should be not equal given the z components are NaN");
        }

        [TestMethod, TestCategory("Equals")]
        public void Equality_WhereTheComponentsAreEqual_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(1, 2, 3);
            Vector3 v2 = new Vector3(1, 2, 3);

            var result = v1.Equals(v2);

            result.Should().Be(true);
        }

        [TestMethod, TestCategory("Equals")]
        public void Equality_WhereXYZOrderIsImportantAndTheComponentAreNotInTheCorrectOrder_ShouldBeFalse_Test()
        {
            Vector3 v1 = new Vector3(1, 2, 3);
            Vector3 v2 = new Vector3(3, 2, 1);

            var result = v1.Equals(v2);

            result.Should().Be(false);
        }

        [TestMethod, TestCategory("Equals")]
        public void Equality_WhereZComponentsArePositiveInfinity_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(1, 2, double.PositiveInfinity);
            Vector3 v2 = new Vector3(1, 2, double.PositiveInfinity);

            var zComponentResult = v1.Z.Equals(v2.Z);
            var result = v1.Equals(v2);

            // Test our assumption about the .Net framework expectations
            double.PositiveInfinity.Equals(double.PositiveInfinity).Should().Be(true, "the .Net framework should find double positive infinty equal to positive infinity (if this assumption is wrong then the logic of this test is also wrong)");

            // Test that the component operation matches our assumption about the .Net framework
            zComponentResult.Should().Be(true, "z components of positive infinty and positive infinty should be equal");

            // Test that our result matches the assumption
            result.Should().Be(true, "positive infinty and positive infinty should be equal");
        }

        [TestMethod, TestCategory("Equals")]
        public void Equality_WhereZComponentsAreNegativeInfinity_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(1, 2, double.NegativeInfinity);
            Vector3 v2 = new Vector3(1, 2, double.NegativeInfinity);

            var zComponentResult = v1.Z.Equals(v2.Z);
            var result = v1.Equals(v2);

            // Test our assumption about the .Net framework expectations
            double.NegativeInfinity.Equals(double.NegativeInfinity).Should().Be(true, "the .Net framework should find double negative infinty equal to negative infinity (if this assumption is wrong then the logic of this test is also wrong)");

            // Test that the component operation matches our assumption about the.Net framework
            zComponentResult.Should().Be(true, "z components of negative infinty and negative infinty should be equal");

            // Test that our result matches the assumption
            result.Should().Be(true, "negative infinty and negative infinty should be equal");
        }

        [TestMethod, TestCategory("Equals")]
        public void Equality_WhereOneZComponentIsNaN_ShouldBeFalse_Test()
        {
            Vector3 v1 = new Vector3(1, 2, 3);
            Vector3 v2 = new Vector3(1, 2, double.NaN);

            var zComponentResult = v1.Z.Equals(v2.Z);
            var result = v1.Equals(v2);

            // Test our assumption about the .Net framework expectations
            3.Equals(double.NaN).Should().Be(false, "the .Net framework should find double 3 not equal to double NaN (if this assumption is wrong then the logic of this test is also wrong)");

            // Test that the component operation matches our assumption about the .Net framework
            zComponentResult.Should().Be(false, "3 and NaN should not be equal");

            // Test that our result matches the assumption
            result.Should().Be(false, "the two vectors should not be equal given one of the Z components is NaN");
        }

        [TestMethod, TestCategory("Equals")]
        public void Equality_WhereZComponentsAreNaN_ShouldBeTrue_Test()
        {
            // Interestingly double.NaN == double.NaN is false while double.NaN.Equals(double.NaN) is true.
            // Lets be consistent with the .Net framework even if it isn't consistent with itself

            Vector3 v1 = new Vector3(1, 2, double.NaN);
            Vector3 v2 = new Vector3(1, 2, double.NaN);

            var zComponentResult = v1.Z.Equals(v2.Z);
            var result = v1.Equals(v2);

            // Test our assumption about the .Net framework expectations
            double.NaN.Equals(double.NaN).Should().Be(true, "the .Net framework should find double NaN equal to double NaN using the Equals method (if this assumption is wrong then the logic of this test is also wrong)");

            // Test that the component operation matches our assumption about the .Net framework
            zComponentResult.Should().Be(true, "NaN and NaN should be equal");

            // Test that our result matches the assumption
            result.Should().Be(true, "the two vectors should be equal");
        }

        [TestMethod, TestCategory("Equals")]
        public void EqualityWithTolerance_WhereTheZComponentsAreDifferentByLessThanTheTolerance_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(1, 2, 3);
            Vector3 v2 = new Vector3(1, 2, 2.9999);

            var result = v1.Equals(v2, 0.0002);

            // Quick test to ensure that the double minus operation does not introduce an unexpected margin of error that is higher than our margin of error
            (3d - (2.9999d)).Should().BeLessThan(0.0002d, "the double calculations of 3 minus 2.9999 should be less than 0.0002 or our test is incorrect");
            
            // Assert our acual test
            result.Should().Be(true, "the tolerance should have been greter than the difference between the z components for s1 and s2");
        }

        [TestMethod, TestCategory("Equals")]
        public void EqualityWithTolerance_WhereZComponentDifferenceIsGreaterThanTheTolerance_ShouldBeFalse_Test()
        {
            Vector3 v1 = new Vector3(1, 2, 3);
            Vector3 v2 = new Vector3(1, 2, 2.9999);

            var result = v1.Equals(v2, 0.00009);

            // Quick test to ensure that the double minus operation does not introduce an unexpected margin of error
            (3d - 2.9999d).Should().BeGreaterThan(0.00009d, "the double calculations of 3 minus 2.9999 should be greater than 0.00009 or our test is incorrect");

            result.Should().Be(false, "the tolerance should be less than the difference between the z components for s1 and s2");
        }

        [TestMethod, TestCategory("Equals")]
        public void EqualityWithTolerance_WhereZComponentsArePositiveInfinity_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(1, 2, double.PositiveInfinity);
            Vector3 v2 = new Vector3(1, 2, double.PositiveInfinity);

            var zComponentResult = v1.Z.Equals(v2.Z);
            var result = v1.Equals(v2, 0.00001);

            // Test our assumption about the .Net framework expectations
            double.PositiveInfinity.Equals(double.PositiveInfinity).Should().Be(true, "the .Net framework should find double positive infinty equal to positive infinity (if this assumption is wrong then the logic of this test is also wrong)");

            // Test that the component operation matches our assumption about the .Net framework
            zComponentResult.Should().Be(true, "positive infinty and positive infinty should be equal regardless of tolerance");

            // Test that our result matches the assumption
            result.Should().Be(true, "positive infinty and positive infinty should be equal regardless of tolerance");
        }

        [TestMethod, TestCategory("Equals")]
        public void EqualityWithTolerance_WhereZComponentsAreNegativeInfinity_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(1, 2, double.NegativeInfinity);
            Vector3 v2 = new Vector3(1, 2, double.NegativeInfinity);

            var zComponentResult = v1.Z.Equals(v2.Z);
            var result = v1.Equals(v2, 0.00001);

            // Test our assumption about the .Net framework expectations
            double.NegativeInfinity.Equals(double.NegativeInfinity).Should().Be(true, "the .Net framework should find double negative infinty equal to negative infinity (if this assumption is wrong then the logic of this test is also wrong)");

            // Test that the component operation matches our assumption about the .Net framework
            zComponentResult.Should().Be(true, "negative infinty and negative infinty should be equal regardless of tolerance");

            // Test that our result matches the assumption
            result.Should().Be(true, "the two vectors should be equal given negative infinty and negative infinty should be equal regardless of tolerance");
        }

        [TestMethod, TestCategory("Equals")]
        public void EqualityWithTolerance_WhereZComponentsArePositiveInfinityAndNegativeInfinity_ShouldBeFalse_Test()
        {
            Vector3 v1 = new Vector3(1, 2, double.PositiveInfinity);
            Vector3 v2 = new Vector3(1, 2, double.NegativeInfinity);

            var zComponentResult = v1.Z.Equals(v2.Z);
            var result = v1.Equals(v2, 0.00001);

            // Test our assumption about the .Net framework expectations
            double.PositiveInfinity.Equals(double.NegativeInfinity).Should().Be(false, "the .Net framework should find double positive infinty not equal to negative infinity (if this assumption is wrong then the logic of this test is also wrong)");

            // Test that the component operation matches our assumption about the .Net framework
            zComponentResult.Should().Be(false, "positive infinty and negative infinty should not be equal");

            // Test that our result matches the assumption
            result.Should().Be(false, "the two vectors should not be equal given positive infinty and negative infinty should not be equal");
        }

        [TestMethod, TestCategory("Equals")]
        public void EqualityWithTolerance_WhereOneZComponentIsNaN_ShouldBeFalse_Test()
        {
            Vector3 v1 = new Vector3(1, 2, 3);
            Vector3 v2 = new Vector3(1, 2, double.NaN);

            var zComponentResult = v1.Z.Equals(v2.Z);
            var result = v1.Equals(v2, 0.00001);

            // Test our assumption about the .Net framework expectations
            3.Equals(double.NaN).Should().Be(false, "the .Net framework should find double 3 not equal to double NaN (if this assumption is wrong then the logic of this test is also wrong)");

            // Test that the component operation matches our assumption about the .Net framework
            zComponentResult.Should().Be(false, "3 and NaN should not be equal");

            // Test that our result matches the assumption
            result.Should().Be(false, "the two vectors should not be equal given one of the Z components is NaN");
        }

        [TestMethod, TestCategory("Equals")]
        public void EqualityWithTolerance_WhereZComponentsAreNaN_ShouldBeTrue_Test()
        {
            /* Interestingly double.NaN == double.NaN is false while double.NaN.Equals(double.NaN) is true.
               Lets be consistent with the .Net framework even if it isn't consistent with itself */

            Vector3 v1 = new Vector3(1, 2, double.NaN);
            Vector3 v2 = new Vector3(1, 2, double.NaN);

            var zComponentResult = v1.Z.Equals(v2.Z);
            var result = v1.Equals(v2, 0.00001);

            // Test our assumption about the .Net framework expectations
            double.NaN.Equals(double.NaN).Should().Be(true, "the .Net framework should find double NaN equal to double NaN using the Equals method (if this assumption is wrong then the logic of this test is also wrong)");

            // Test that the component operation matches our assumption about the .Net framework
            zComponentResult.Should().Be(true, "NaN and NaN should be equal");

            // Test that our result matches the assumption
            result.Should().Be(true, "the two vectors should be equal");
        }

        [TestMethod, TestCategory("Equals")]
        public void GetHashCode_WhereTheComponentsAreEqual_ShouldResultInEquivelentHashCodes_Test()
        {
            Vector3 v1 = new Vector3(1, 2, 3);
            Vector3 v2 = new Vector3(1, 2, 3);

            var result = v1.GetHashCode() == v2.GetHashCode();

            result.Should().Be(true);
        }

        [TestMethod, TestCategory("Equals")]
        public void GetHashCode_WhereXYZOrderIsImportantAndTheComponentAreNotInTheCorrectOrder_ShouldNotResultInEquivelentHashCodes_Test()
        {
            Vector3 v1 = new Vector3(1, 2, 3);
            Vector3 v2 = new Vector3(3, 2, 1);

            var result = v1.GetHashCode() == v2.GetHashCode();

            result.Should().Be(false);
        }

        [TestMethod, TestCategory("Equals")]
        public void GetHashCode_WhereZComponentsArePositiveInfinity_ShouldResultInEquivelentHashCodes_Test()
        {
            Vector3 v1 = new Vector3(1, 2, double.PositiveInfinity);
            Vector3 v2 = new Vector3(1, 2, double.PositiveInfinity);

            var zComponentResult = v1.Z.GetHashCode() == v2.Z.GetHashCode();
            var result = v1.GetHashCode() == v2.GetHashCode();

            // Test our assumption about the .Net framework expectations
            (double.PositiveInfinity.GetHashCode() == double.PositiveInfinity.GetHashCode()).Should().Be(true, "the .Net framework should find double positive infinty's hash code equal to positive infinity's hash code (if this assumption is wrong then the logic of this test is also wrong)");

            // Test that the component operation matches our assumption about the .Net framework
            zComponentResult.Should().Be(true, "z component hash codes of positive infinty and positive infinty should be equal");

            // Test that our result matches the assumption
            result.Should().Be(true, "positive infinty and positive infinty hash codes should be equal");
        }

        [TestMethod, TestCategory("Equals")]
        public void GetHashCode_WhereZComponentsAreNegativeInfinity_ShouldResultInEquivelentHashCodes_Test()
        {
            Vector3 v1 = new Vector3(1, 2, double.NegativeInfinity);
            Vector3 v2 = new Vector3(1, 2, double.NegativeInfinity);

            var zComponentResult = v1.Z.GetHashCode() == v2.Z.GetHashCode();
            var result = v1.GetHashCode() == v2.GetHashCode();

            // Test our assumption about the .Net framework expectations
            (double.NegativeInfinity.GetHashCode() == double.NegativeInfinity.GetHashCode()).Should().Be(true, "the .Net framework should find double negative infinty's has code equal to negative infinity's hash code (if this assumption is wrong then the logic of this test is also wrong)");

            // Test that the component operation matches our assumption about the.Net framework
            zComponentResult.Should().Be(true, "z component hash codes of negative infinty and negative infinty should be equal");

            // Test that our result matches the assumption
            result.Should().Be(true, "negative infinty and negative infinty hash codes should be equal");
        }

        [TestMethod, TestCategory("Equals")]
        public void GetHashCode_WhereOneZComponentIsNaN_ShouldNotResultInEquivelentHashCodes_Test()
        {
            Vector3 v1 = new Vector3(1, 2, 3);
            Vector3 v2 = new Vector3(1, 2, double.NaN);

            var result = v1.GetHashCode() == v2.GetHashCode();

            result.Should().Be(false, "the two vectors' hash codes should not be equal given one of the Z components is NaN");
        }

        [TestMethod, TestCategory("Equals")]
        public void GetHashCode_WhereZComponentsAreNaN_ShouldResultInEquivelentHashCodes_Test()
        {
            // Interestingly double.NaN == double.NaN is false while double.NaN.Equals(double.NaN) is true.
            // I am testing the assumption that GetHashCode follows the logic of .Equals()

            Vector3 v1 = new Vector3(1, 2, double.NaN);
            Vector3 v2 = new Vector3(1, 2, double.NaN);

            var zComponentResult = v1.Z.GetHashCode() == v2.Z.GetHashCode();
            var result = v1.GetHashCode() == v2.GetHashCode();

            // Test our assumption about the .Net framework expectations
            (double.NaN.GetHashCode() == double.NaN.GetHashCode()).Should().Be(true, "the .Net framework should find the hash codes of double NaN and double NaN equal (if this assumption is wrong then the logic of this test is also wrong)");

            // Test that the component operation matches our assumption about the .Net framework
            zComponentResult.Should().Be(true, "the hash codes of NaN and NaN should be equal");

            // Test that our result matches the assumption
            result.Should().Be(true, "the two vectors' hashcodes should be equal given the z components are NaN");
        }

        #endregion

        #region Is Unit Vector Tests

        [TestMethod, TestCategory("IsUnitVector")]
        public void IsUnitVector_WhereXIsNaN_ShouldBeFalse_Test()
        {
            Vector3 v1 = new Vector3(double.NaN, 0, 0);
            var result = v1.IsUnitVector();

            result.Should().Be(false, "vector (NaN,0,0) is not a unit vector");
        }

        [TestMethod, TestCategory("IsUnitVector")]
        public void IsUnitVector_x1y0z0_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(1, 0, 0);
            var result = v1.IsUnitVector();

            result.Should().Be(true, "vector (1,0,0) is a unit vector");
        }

        [TestMethod, TestCategory("IsUnitVector")]
        public void IsUnitVector_WhereXYZAreOne_ShouldBeFalse_Test()
        {
            Vector3 v1 = new Vector3(1, 1, 1);
            var result = v1.IsUnitVector();

            result.Should().Be(false, "vector (1,1,1) is not a unit vector");
        }

        [TestMethod, TestCategory("IsUnitVector")]
        public void IsUnitVector_WhereZComponentIsPositiveInfinity_ShouldBeFalse_Test()
        {
            Vector3 v1 = new Vector3(1, 2, double.PositiveInfinity);
            var result = v1.IsUnitVector();

            result.Should().Be(false, "vector (0,0, +Inf) is not a unit vector");
        }

        [TestMethod, TestCategory("IsUnitVector")]
        public void IsUnitVector_WhereZComponentIsNegativeInfinity_ShouldBeFalse_Test()
        {
            Vector3 v1 = new Vector3(1, 2, double.NegativeInfinity);
            var result = v1.IsUnitVector();

            result.Should().Be(false, "vector (0,0, -Inf) is not a unit vector");
        }

        [TestMethod, TestCategory("IsUnitVector")]
        public void IsUnitVectorWithTolerance_x1y0z0_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(1, 0, 0);
            var result = v1.IsUnitVector(0.0002);

            result.Should().Be(true, "vector (1,0,0) is a unit vector");
        }

        [TestMethod, TestCategory("IsUnitVector")]
        public void IsUnitVectorWithTolerance_WhereTheZComponentIsNearlyOneByLessThanTheTolerance_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(0, 0, 0.9999);
            var result = v1.IsUnitVector(0.0002);

            // Quick test to ensure that the double minus operation does not introduce an unexpected margin of error that is higher than our margin of error
            (1d - 0.9999d).Should().BeLessThan(0.0002d, "the double calculations of 1 minus 0.9999 should be less than 0.0002 or our test is incorrect");

            // Assert our acual test
            result.Should().Be(true, "the tolerance should have been greater than the difference between the z components and 1");
        }

        [TestMethod, TestCategory("IsUnitVector")]
        public void IsUnitVectorWithTolerance_WhereTheZComponentIsNearlyOneByMoreThanTheTolerance_ShouldBeFalse_Test()
        {
            Vector3 v1 = new Vector3(0, 0, 0.9999);

            var result = v1.IsUnitVector(0.00009);

            // Quick test to ensure that the double minus operation does not introduce an unexpected margin of error
            (1d - 0.9999d).Should().BeGreaterThan(0.00009d, "the double calculations of 3 minus 2.9999 should be greater than 0.00009 or our test is incorrect");

            result.Should().Be(false, "the tolerance should be less than the difference between the z component and 1");
        }

        #endregion

        #region Is Perpendicular Tests

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicular_WhereUnitVectorsPositiveXY_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(1, 0, 0);
            Vector3 v2 = new Vector3(0, 1, 0);
            var result = v1.IsPerpendicular(v2);

            result.Should().Be(true, "vector (1,0,0) is perpendicular to vector (0,1,0)");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicular_WhereUnitVectorsInfinitePositiveXY_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(double.PositiveInfinity, 0, 0);
            Vector3 v2 = new Vector3(0, double.PositiveInfinity, 0);
            var result = v1.IsPerpendicular(v2);

            result.Should().Be(true, "vector (+inf,0,0) is perpendicular to vector (0,+inf,0)");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicular_WhereUnitVectorsPositiveYX_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(0, 1, 0);
            Vector3 v2 = new Vector3(1, 0, 0);
            var result = v1.IsPerpendicular(v2);

            result.Should().Be(true, "vector (0,1,0) is perpendicular to vector (1,0,0)");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicular_WhereUnitVectorsInfinitePositiveYX_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(0, double.PositiveInfinity, 0);
            Vector3 v2 = new Vector3(double.PositiveInfinity, 0, 0);
            var result = v1.IsPerpendicular(v2);

            result.Should().Be(true, "vector (0,+inf,0) is perpendicular to vector (+inf,0,0)");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicular_WhereUnitVectorsPositiveXNegativeY_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(1, 0, 0);
            Vector3 v2 = new Vector3(0, -1, 0);
            var result = v1.IsPerpendicular(v2);

            result.Should().Be(true, "vector (1,0,0) is perpendicular to vector (0,-1,0)");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicular_WhereUnitVectorsInfinitePositiveXNegativeY_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(double.PositiveInfinity, 0, 0);
            Vector3 v2 = new Vector3(0, double.NegativeInfinity, 0);
            var result = v1.IsPerpendicular(v2);

            result.Should().Be(true, "vector (+inf,0,0) is perpendicular to vector (0,-inf,0)");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicular_WhereUnitVectorsNegativeYPositiveX_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(0, -1, 0);
            Vector3 v2 = new Vector3(1, 0, 0);
            var result = v1.IsPerpendicular(v2);

            result.Should().Be(true, "vector (0,-1,0) is perpendicular to vector (1,0,0)");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicular_WhereUnitVectorsInfiniteNegativeYPositiveX_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(0, double.NegativeInfinity, 0);
            Vector3 v2 = new Vector3(double.PositiveInfinity, 0, 0);
            var result = v1.IsPerpendicular(v2);

            result.Should().Be(true, "vector (0,-inf,0) is perpendicular to vector (+inf,0,0)");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicular_WhereUnitVectorsNegativeXY_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(-1, 0, 0);
            Vector3 v2 = new Vector3(0, -1, 0);
            var result = v1.IsPerpendicular(v2);

            result.Should().Be(true, "vector (-1,0,0) is perpendicular to vector (0,-1,0)");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicular_WhereUnitVectorsInfiniteNegativeXY_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(double.NegativeInfinity, 0, 0);
            Vector3 v2 = new Vector3(0, double.NegativeInfinity, 0);
            var result = v1.IsPerpendicular(v2);

            result.Should().Be(true, "vector (-inf,0,0) is perpendicular to vector (0,-inf,0)");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicular_WhereUnitVectorsNegativeYX_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(0, -1, 0);
            Vector3 v2 = new Vector3(-1, 0, 0);
            var result = v1.IsPerpendicular(v2);

            result.Should().Be(true, "vector (0,-1,0) is perpendicular to vector (-1,0,0)");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicular_WhereUnitVectorsInfiniteNegativeYX_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(0, double.NegativeInfinity, 0);
            Vector3 v2 = new Vector3(double.NegativeInfinity, 0, 0);
            var result = v1.IsPerpendicular(v2);

            result.Should().Be(true, "vector (0,-inf,0) is perpendicular to vector (-inf,0,0)");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicular_WhereUnitVectorsNegativeXPositiveY_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(-1, 0, 0);
            Vector3 v2 = new Vector3(0, 1, 0);
            var result = v1.IsPerpendicular(v2);

            result.Should().Be(true, "vector (-1,0,0) is perpendicular to vector (0,1,0)");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicular_WhereUnitVectorsInfiniteNegativeXPositiveY_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(double.NegativeInfinity, 0, 0);
            Vector3 v2 = new Vector3(0, double.PositiveInfinity, 0);
            var result = v1.IsPerpendicular(v2);

            result.Should().Be(true, "vector (-inf,0,0) is perpendicular to vector (0,+inf,0) but ");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicular_WhereUnitVectorsPositiveYNegativeX_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(0, 1, 0);
            Vector3 v2 = new Vector3(-1, 0, 0);
            var result = v1.IsPerpendicular(v2);

            result.Should().Be(true, "vector (0,1,0) is perpendicular to vector (-1,0,0)");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicular_WhereUnitVectorsInfinitePositiveYNegativeX_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(0, double.PositiveInfinity, 0);
            Vector3 v2 = new Vector3(double.NegativeInfinity, 0, 0);
            var result = v1.IsPerpendicular(v2);

            result.Should().Be(true, "vector (0,+inf,0) is perpendicular to vector (-inf,0,0)");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicular_WherePositiveXY_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(ArbitaryTestDouble, 0, 0);
            Vector3 v2 = new Vector3(0, ArbitaryTestDouble, 0);
            var result = v1.IsPerpendicular(v2);

            result.Should().Be(true, "vector (n,0,0) is perpendicular to vector (0,n,0)");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicular_WherePositiveYX_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(0, ArbitaryTestDouble, 0);
            Vector3 v2 = new Vector3(ArbitaryTestDouble, 0, 0);
            var result = v1.IsPerpendicular(v2);

            result.Should().Be(true, "vector (0,n,0) is perpendicular to vector (n,0,0)");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicular_WherePositiveXNegativeY_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(ArbitaryTestDouble, 0, 0);
            Vector3 v2 = new Vector3(0, -ArbitaryTestDouble, 0);
            var result = v1.IsPerpendicular(v2);

            result.Should().Be(true, "vector (n,0,0) is perpendicular to vector (0,-n,0)");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicular_WhereUNegativeYPositiveX_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(0, -ArbitaryTestDouble, 0);
            Vector3 v2 = new Vector3(ArbitaryTestDouble, 0, 0);
            var result = v1.IsPerpendicular(v2);

            result.Should().Be(true, "vector (0,-n,0) is perpendicular to vector (n,0,0)");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicular_WhereNegativeXY_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(-ArbitaryTestDouble, 0, 0);
            Vector3 v2 = new Vector3(0, -ArbitaryTestDouble, 0);
            var result = v1.IsPerpendicular(v2);

            result.Should().Be(true, "vector (-n,0,0) is perpendicular to vector (0,-n,0)");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicular_WhereNegativeYX_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(0, -ArbitaryTestDouble, 0);
            Vector3 v2 = new Vector3(-ArbitaryTestDouble, 0, 0);
            var result = v1.IsPerpendicular(v2);

            result.Should().Be(true, "vector (0,-n,0) is perpendicular to vector (-n,0,0)");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicular_WhereNegativeXPositiveY_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(-ArbitaryTestDouble, 0, 0);
            Vector3 v2 = new Vector3(0, ArbitaryTestDouble, 0);
            var result = v1.IsPerpendicular(v2);

            result.Should().Be(true, "vector (-n,0,0) is perpendicular to vector (0,n,0)");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicular_WherePositiveYNegativeX_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(0, ArbitaryTestDouble, 0);
            Vector3 v2 = new Vector3(-ArbitaryTestDouble, 0, 0);
            var result = v1.IsPerpendicular(v2);

            result.Should().Be(true, "vector (0,n,0) is perpendicular to vector (-n,0,0)");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicular_WherePositiveXYAnd45deg_ShouldBeFalse_Test()
        {
            Vector3 v1 = new Vector3(1, 0, 0);
            Vector3 v2 = new Vector3(1, 1, 0);
            var result = v1.IsPerpendicular(v2);

            result.Should().Be(false, "vector (1,0,0) is not perpendicular to vector (1,1,0)");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicular_WhereInfinitePositiveXYAnd45deg_ShouldBeFalse_Test()
        {
            Vector3 v1 = new Vector3(double.PositiveInfinity, 0, 0);
            Vector3 v2 = new Vector3(double.PositiveInfinity, double.PositiveInfinity, 0);
            var result = v1.IsPerpendicular(v2);

            result.Should().Be(false, "vector (+inf,0,0) is not perpendicular to vector (+inf,+inf,0)");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicular_WherePositiveXAndNegativeXYAnd45deg_ShouldBeFalse_Test()
        {
            Vector3 v1 = new Vector3(0, 1, 0);
            Vector3 v2 = new Vector3(-1, -1, 0);
            var result = v1.IsPerpendicular(v2);

            result.Should().Be(false, "vector (1,0,0) is not perpendicular to vector (-1,-1,0)");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicular_WhereInfinitePositiveXAndNegativeXYAnd45deg_ShouldBeFalse_Test()
        {
            Vector3 v1 = new Vector3(0, double.PositiveInfinity, 0);
            Vector3 v2 = new Vector3(double.NegativeInfinity, double.NegativeInfinity, 0);
            var result = v1.IsPerpendicular(v2);

            result.Should().Be(false, "vector (+inf,0,0) is not perpendicular to vector (-inf,-inf,0)");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicular_WhereZeroAndZero_ShouldBeFalse_Test()
        {
            Vector3 v1 = new Vector3(0, 0, 0);
            Vector3 v2 = new Vector3(0, 0, 0);
            var result = v1.IsPerpendicular(v2);

            result.Should().Be(false, "vector (0,0,0) is not perpendicular to vector (0,0,0)");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicularWithTolerance_WhereUnitVectorsPositiveXYAndThereIsNoError_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(0, 1, 0);
            Vector3 v2 = new Vector3(1, 0, 0); // with an error in Y that should produce a slightly off 90deg angle (as radians)

            var result = v1.IsPerpendicular(v2, 0.0002); // remember the tolerance should be for the angle in Rad compared to 90deg (as a rad)

            result.Should().Be(true, "vector (0,1,0) is perpendicular to vector (1,0,0)");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicularWithTolerance_WhereUnitVectorsInfinitePositiveXYAndThereIsNoError_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(0, double.PositiveInfinity, 0);
            Vector3 v2 = new Vector3(double.PositiveInfinity, 0, 0); // with an error in Y that should produce a slightly off 90deg angle (as radians)

            var result = v1.IsPerpendicular(v2, 0.0002); // remember the tolerance should be for the angle in Rad compared to 90deg (as a rad)

            result.Should().Be(true, "vector (0,+inf,0) is perpendicular to vector (+inf,0,0)");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicularWithTolerance_WhereUnitVectorsPositiveXYAndThereIsAnError_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(0, 1, 0);
            Vector3 v2 = new Vector3(1, 0.001, 0); // with an error in Y that should produce a slightly off 90deg angle (as radians)

            var result = v1.IsPerpendicular(v2, 0.001); // remember the tolerance should be for the angle in Rad compared to 90deg (as a rad)

            // Check the tolerance on the angle
            var angle = v1.Angle(v2);
            angle.AlmostEqualsWithAbsTolerance(Deg90AsRad, 0.001).Should().Be(true, string.Format("the angle between v1 and v2 should be 90 deg within 0.001 radians (found {0} rad)", angle));

            // Check the actual result
            result.Should().Be(true, "vector (0,1,0) is perpendicular to vector (1,0.001,0)");
        }

        /// <summary>
        /// Check that the special case vectors use the tolerance when determinitng if it is a apecial case.
        /// Ignored: This should not work! The tolerance is for the dot product equality to 0 NOT for special case snap to zero.
        /// We could add another parameter, i.e. SpecialCaseSnapToZeroTolerance but we are making special cases more complicated.
        /// </summary>
        [TestMethod, TestCategory("IsPerpendicular"), Ignore]
        public void IsPerpendicularWithTolerance_WhereUnitVectorsInfinitePositiveXYAndThereIsAnError_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(0, double.PositiveInfinity, 0);
            Vector3 v2 = new Vector3(double.PositiveInfinity, 0.001, 0); // with an error in Y that should produce a slightly off 90deg angle (as radians)

            var result = v1.IsPerpendicular(v2, 0.001); // remember the tolerance should be for the angle in Rad compared to 90deg (as a rad)

            // Check the tolerance on the angle after normalization
            var angle = v1.NormalizeOrDefault().Angle(v2.NormalizeOrDefault());
            angle.AlmostEqualsWithAbsTolerance(Deg90AsRad, 0.001).Should().Be(true, string.Format("the angle between v1 and v2 should be 90 deg within 0.001 radians (found {0} rad)", angle));

            // Check the actual result
            result.Should().Be(true, "vector (0,+inf,0) is perpendicular to vector (+inf,0.001,0)");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicular_WhereOneVectorsXIsNaNAndTheOtherVectorsYIs1_ShouldBeFalse_Test()
        {
            Vector3 v1 = new Vector3(double.NaN, 0, 0);
            Vector3 v2 = new Vector3(0, 1, 0);
            var result = v1.IsPerpendicular(v2);

            result.Should().Be(false, "vector (NaN,0,0) is not perpendicular to vector (0,1,0)");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicular_WhereOneVectorsXIsNaNAndTheOtherVectorsYIsNaN_ShouldBeFalse_Test()
        {
            Vector3 v1 = new Vector3(double.NaN, 0, 0);
            Vector3 v2 = new Vector3(0, double.NaN, 0);
            var result = v1.IsPerpendicular(v2);

            result.Should().Be(false, "vector (NaN,0,0) is not perpendicular to vector (0,NaN,0)");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicularWithTolerance_WhereOneVectorsXIsNaNAndTheOtherVectorsYIs1_ShouldBeFalse_Test()
        {
            Vector3 v1 = new Vector3(double.NaN, 0, 0);
            Vector3 v2 = new Vector3(0, 1, 0);
            var result = v1.IsPerpendicular(v2, 1);

            result.Should().Be(false, "vector (NaN,0,0) is not perpendicular to vector (0,1,0) regardless of tolerance");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicularWithTolerance_WhereOneVectorsXIsNaNAndTheOtherVectorsYIsNaN_ShouldBeFalse_Test()
        {
            Vector3 v1 = new Vector3(double.NaN, 0, 0);
            Vector3 v2 = new Vector3(0, double.NaN, 0);
            var result = v1.IsPerpendicular(v2, 1);

            result.Should().Be(false, "vector (NaN,0,0) is not perpendicular to vector (0,NaN,0) regardless of tolerance");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicularWithTolerance_WhereZeroAndZero_ShouldBeFalse_Test()
        {
            Vector3 v1 = new Vector3(0, 0, 0);
            Vector3 v2 = new Vector3(0, 0, 0);
            var result = v1.IsPerpendicular(v2, 1);

            result.Should().Be(false, "vector (0,0,0) is not perpendicular to vector (0,0,0) regardless of tolerance");
        }

        #endregion

        #region IsNaN

        [TestMethod, TestCategory("IsNaN")]
        public void IsNaN_WithXIsNaN_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(double.NaN, 0, 0);
            var result = v1.IsNaN();

            result.Should().Be(true);
        }

        [TestMethod, TestCategory("IsNaN")]
        public void IsNaN_WithYIsNaN_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(0, double.NaN, 0);
            var result = v1.IsNaN();

            result.Should().Be(true);
        }

        [TestMethod, TestCategory("IsNaN")]
        public void IsNaN_WithZIsNaN_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(0, 0, double.NaN);
            var result = v1.IsNaN();

            result.Should().Be(true);
        }

        [TestMethod, TestCategory("IsNaN")]
        public void IsNaN_WithYZIsNaN_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(0, double.NaN, double.NaN);
            var result = v1.IsNaN();

            result.Should().Be(true);
        }

        [TestMethod, TestCategory("IsNaN")]
        public void IsNaN_WithXYZIsNaN_ShouldBeTrue_Test()
        {
            Vector3 v1 = new Vector3(double.NaN, double.NaN, double.NaN);
            var result = v1.IsNaN();

            result.Should().Be(true);
        }

        [TestMethod, TestCategory("IsNaN")]
        public void IsNaN_WithXYZWholeNumbers_ShouldBeFalse_Test()
        {
            Vector3 v1 = new Vector3(1, 2, 3);
            var result = v1.IsNaN();

            result.Should().Be(false);
        }

        #endregion
    }
}
