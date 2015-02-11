﻿namespace RP.Math.Tests
{
    using System;

    using FluentAssertions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Vector3Tests
    {
        private static readonly double Deg90AsRad = System.Math.PI / 2;

        #region Constructor tests

        [TestMethod]
        public void ConstructionWithXYZParametersTest()
        {
            var vector = new Vector3(100, -100, 0.01);
            vector.X.Should().Be(100d);
            vector.Y.Should().Be(-100d);
            vector.Z.Should().Be(0.01d);
        }

        [TestMethod]
        public void ConstructionWithAVector3ParameterTest()
        {
            var vector = new Vector3(new Vector3(100, -100, 0));

            vector.X.Should().Be(100);
            vector.Y.Should().Be(-100);
            vector.Z.Should().Be(0);
        }

        [TestMethod]
        public void ConstructorWithAnArrayOfXYZParameterTest()
        {
            var vector = new Vector3(new[] { 100, -100, 0.01 });

            vector.X.Should().Be(100);
            vector.Y.Should().Be(-100);
            vector.Z.Should().Be(0.01);
        }

        [TestMethod]
        public void ConstructorWithNoParametersTest()
        {
            var vector = new Vector3();

            vector.X.Should().Be(0);
            vector.Y.Should().Be(0);
            vector.Z.Should().Be(0);
        }

        #endregion

        #region Abs Tests

        [TestMethod]
        public void AbsOfVector000Is0Test()
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

        [TestMethod]
        public void DotProductUsingWholeNumbersTest()
        {
            var a = new Vector3(12, 20, 0);
            var b = new Vector3(16, -5, 0);

            a.DotProduct(b).Should().Be(92);
        }

        [TestMethod]
        public void CrossProductUsingWholeNumbersTest()
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
        public void Magnitude_UsingPositiveAndNegativeWholeNumberParameters_Test()
        {
            var vector = new Vector3(3, 1, -1);
            var magnitude = vector.Magnitude;

            magnitude.Should().Be(System.Math.Sqrt(11));
        }

        [TestMethod, TestCategory("Magnitude")]
        public void Magnitude_UsingPositiveWholeNumberParameters_Test()
        {
            var vector = new Vector3(2, 3, 4);
            var magnitude = vector.Magnitude;

            magnitude.Should().Be(System.Math.Sqrt(29));
        }

        /// <summary>
        /// Test scaling a vector with X component only
        /// </summary>
        [TestMethod, TestCategory("Magnitude")]
        public void Scale_X_Test()
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
        public void Scale_Y_Test()
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
        public void Scale_XZ_Test()
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

        [TestMethod]
        public void AngelOfTwoVectorsUsingWholeNumbersResultingIn90DegTest()
        {
            var a = new Vector3(1, 0, 0);
            var b = new Vector3(0, 1, 0);
            var angle = a.Angle(b);

            angle.Should().Be(Deg90AsRad);
        }

        [TestMethod]
        public void AngelOfTwoPerpendicularVectorsUsingWholeNumbers()
        {
            var a = new Vector3(1, 0, 0);
            var b = new Vector3(-1, 0, 0);
            var angle = a.Angle(b);

            angle.Should().Be(System.Math.PI);
        }

        [TestMethod]
        public void AngelOfTwoVectorsUsingWholeNumbersResultingIn180DegTest()
        {
            var a = new Vector3(1, 0, 0);
            var b = new Vector3(-1, 0, 0);
            var angle = a.Angle(b);

            angle.Should().Be(System.Math.PI);
        }

        [TestMethod]
        public void AngleOfIdenticalVectorsUsingWholeNumbersTest()
        {
            var a = new Vector3(1, 0, 0);
            var b = new Vector3(1, 0, 0);
            var angle = a.Angle(b);

            angle.Should().Be(0);
        }

        [TestMethod]
        public void AngleOfIdenticalVectorsUsingLongFloatingPointNumbersTest()
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
        [TestMethod]
        public void AngleOfVectorsUsingLongFloatingPointNumbersThatShouldNotResultInNaNTest()
        {
            var a = new Vector3(0.795271508195995f, -0.0612034045226753f, -0.603156175071185f);
            var b = new Vector3(0.795271508449802f, -0.0612033993276936f, -0.60315617526368f);
            var angle = a.Angle(b);

            angle.Should().NotBe(float.NaN);
            angle.Should().BeLessThan(1);
            angle.Should().BeGreaterOrEqualTo(0);
        }

        [TestMethod]
        public void AngleOfIdenticalVectorsUsingDoubleFloatingPointNumbersTest()
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
        [TestMethod]
        public void AngleOfVectorsUsingDoubleFloatingPointNumbersThatShouldNotResultInNaNTest()
        {
            var a = new Vector3(0.795271508195995d, -0.0612034045226753d, -0.603156175071185d);
            var b = new Vector3(0.795271508449802d, -0.0612033993276936d, -0.60315617526368d);
            var angle = a.Angle(b);

            angle.Should().NotBe(float.NaN);
            angle.Should().BeLessThan(1);
            angle.Should().BeGreaterOrEqualTo(0);
        }

        [TestMethod]
        public void AngleOfIdenticalVectorsUsingMaxAndMinFloatingPointNumbersTest()
        {
            var a = new Vector3(float.MaxValue, float.MinValue, 0);
            var b = new Vector3(float.MaxValue, float.MinValue, 0);
            var angle = a.Angle(b);

            angle.Should().Be(0);
        }

        [TestMethod]
        public void AngleUsingDifferentWholeNumbersResultingInAFractionalNumberTest()
        {
            Vector3 s0 = new Vector3(7719, 0, 38);
            Vector3 s1 = new Vector3(38, 0, 7719);
            var result = s0.Angle(s1);

            result.Should().Be(1.560950571379345, "this was the result worked out by Pex");
            Math.Round(result, 3, MidpointRounding.AwayFromZero).Should().Be(1.561d, @"this is what http://calculator.tutorvista.com/angle-between-two-vectors-calculator.html worked out the result to be");
        }

        #endregion

        #region Normalize

        /// <summary>
        /// Test the normalization of a vector
        /// </summary>
        /// <acknowlagement>Example from http://www.fundza.com/vectors/normalize/ </acknowlagement>
        [TestMethod]
        public void NormalizeTest()
        {
            var vector = new Vector3(3, 1, 2);
            var result = vector.Normalize();

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
        public void ReflectionTest()
        {
            var a = new Vector3(1, 0, 0);
            var b = new Vector3(0, 1, 0);
            var result = a.Reflection(b);

            result.X.Should().Be(-1, "X should be -1");
            result.Y.Should().Be(0, "Y should be 0");
            result.Z.Should().Be(0, "Z should be 0");
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
            Vector3 s1 = new Vector3(449, 2282, 609);

            var result = s0.CompareTo(s1);
            result.Should().Be(1, "magnitude of s0 is bigger than magnitude of s1");

            result = s1.CompareTo(s0);
            result.Should().Be(-1, "magnitude of s1 is less than magnitude of s0");

            result = s1.CompareTo(box);
            result.Should().Be(-1, "magnitude of s1 is less than magnitude of s0 even when s0 has been cast to an object");
        }

        [TestMethod, TestCategory("CompareTo")]
        public void CompareTo_WhereZComponentsArePositiveInfinity_ShouldBe0_Test()
        {
            Vector3 s1 = new Vector3(0, 0, double.PositiveInfinity);
            Vector3 s2 = new Vector3(0, 0, double.PositiveInfinity);

            var zComponentResult = s1.Z.CompareTo(s2.Z);
            var result = s1.CompareTo(s2);

            // Test our assumption about the .Net framework expectations
            double.PositiveInfinity.CompareTo(double.PositiveInfinity).Should().Be(0, "the .Net framework should find double positive infinty equal to positive infinity (if this assumption is wront then the logic of this test is also wrong)");

            // Test that the component operation matches our assumption about the .Net framework
            zComponentResult.Should().Be(0, "z components of positive infinty and positive infinty should be equal");

            // Test that our result matches the assumption
            result.Should().Be(0, "positive infinty and positive infinty should be equal, as should the other components");
        }

        [TestMethod, TestCategory("CompareTo")]
        public void CompareTo_WhereZComponentsAreNegativeInfinity_ShouldBe0_Test()
        {
            Vector3 s1 = new Vector3(0, 0, double.NegativeInfinity);
            Vector3 s2 = new Vector3(0, 0, double.NegativeInfinity);

            var zComponentResult = s1.Z.CompareTo(s2.Z);
            var result = s1.CompareTo(s2);

            // Test our assumption about the .Net framework expectations
            double.NegativeInfinity.CompareTo(double.NegativeInfinity).Should().Be(0, "the .Net framework should find double negative infinty equal to negative infinity (if this assumption is wront then the logic of this test is also wrong)");

            // Test that the component operation matches our assumption about the.Net framework
            zComponentResult.Should().Be(0, "z components of negative infinty and negative infinty should be equal");

            // Test that our result matches the assumption
            result.Should().Be(0, "negative infinty and negative infinty should be equal, as should the other components");
        }

        [TestMethod, TestCategory("CompareTo")]
        public void CompareTo_WhereZComponentsArePositiveInfinityAndNegativeInfinity_ShouldNotBe0_Test()
        {
            Vector3 s1 = new Vector3(0, 0, double.PositiveInfinity);
            Vector3 s2 = new Vector3(0, 0, double.NegativeInfinity);

            var s1s2Result = s1.CompareTo(s2);
            var s2s1Result = s2.CompareTo(s1);

            s1s2Result.Should().Be(0, "when comparing magnitude sign should not be important so positive infinty should be equal to negative infinty");
            s2s1Result.Should().Be(0, "when comparing magnitude sign should not be important so negative infinty should be equal to positive infinty");
        }

        [TestMethod, TestCategory("CompareTo")]
        public void CompareToWithTolerance_WhereTheZComponentsAreDifferentByLessThanTheTolerance_ShouldBe0_Test()
        {
            Vector3 s1 = new Vector3(0, 0, 3);
            Vector3 s2 = new Vector3(0, 0, 2.9999);

            var result = s1.CompareTo(s2, 0.0002);

            // Quick test to ensure that the double minus operation does not introduce an unexpected margin of error that is higher than our margin of error
            (3d - (2.9999d)).Should().BeLessThan(0.0002d, "the double calculations of 3 minus 2.9999 should be less than 0.0002 or our test is incorrect");

            // Assert our acual test
            result.Should().Be(0, "the tolerance should have been greter than the difference between the z components for s1 and s2");
        }

        [TestMethod, TestCategory("CompareTo")]
        public void CompareToWithTolerance_WhereZComponentDifferenceIsGreaterThanTheTolerance_ShouldBe1_Test()
        {
            Vector3 s1 = new Vector3(0, 0, 3);
            Vector3 s2 = new Vector3(0, 0, 2.9999);

            var result = s1.CompareTo(s2, 0.00009);

            // Quick test to ensure that the double minus operation does not introduce an unexpected margin of error
            (3d - 2.9999d).Should().BeGreaterThan(0.00009d, "the double calculations of 3 minus 2.9999 should be greater than 0.00009 or our test is incorrect");

            result.Should().Be(1, "the tolerance should be less than the difference between the z components for s1 and s2, so 3 should be greater than 2.9999");
        }

        [TestMethod, TestCategory("CompareTo")]
        public void CompareToWithTolerance_WhereZComponentDifferenceIsGreaterThanTheTolerance_ShouldBeNegative1_Test()
        {
            Vector3 s1 = new Vector3(0, 0, 2.9999);
            Vector3 s2 = new Vector3(0, 0, 3);
            
            var result = s1.CompareTo(s2, 0.00009);

            // Quick test to ensure that the double minus operation does not introduce an unexpected margin of error
            (3d - 2.9999d).Should().BeGreaterThan(0.00009d, "the double calculations of 3 minus 2.9999 should be greater than 0.00009 or our test is incorrect");

            result.Should().Be(-1, "the tolerance should be less than the difference between the z components for s1 and s2, so 2.9999 should be less than 3");
        }

        [TestMethod, TestCategory("CompareTo")]
        public void CompareToWithTolerance_WhereZComponentsArePositiveInfinity_ShouldBe0_Test()
        {
            Vector3 s1 = new Vector3(0, 0, double.PositiveInfinity);
            Vector3 s2 = new Vector3(0, 0, double.PositiveInfinity);

            var result = s1.CompareTo(s2, 0.00001);

            result.Should().Be(0, "positive infinty and positive infinty should be equal regardless of tolerance, as should the other components");
        }

        [TestMethod, TestCategory("CompareTo")]
        public void CompareToWithTolerance_WhereZComponentsAreNegativeInfinity_ShouldBe0_Test()
        {
            Vector3 s1 = new Vector3(0, 0, double.NegativeInfinity);
            Vector3 s2 = new Vector3(0, 0, double.NegativeInfinity);

            var result = s1.CompareTo(s2, 0.00001);

            result.Should().Be(0, "negative infinty and negative infinty should be equal regardless of tolerance, as should the other components");
        }

        [TestMethod, TestCategory("CompareTo")]
        public void CompareToWithTolerance_WhereZComponentsArePositiveInfinityAndNegativeInfinity_ShouldNotBe0_Test()
        {
            Vector3 s1 = new Vector3(0, 0, double.PositiveInfinity);
            Vector3 s2 = new Vector3(0, 0, double.NegativeInfinity);

            var s1s2Result = s1.CompareTo(s2, 0.00001);
            var s2s1Result = s2.CompareTo(s1, 0.00001);

            s1s2Result.Should().Be(0, "when comparing magnitude sign should not be important so positive infinty should be equal to negative infinty");
            s2s1Result.Should().Be(0, "when comparing magnitude sign should not be important so negative infinty should be equal to positive infinty");
        }

        #endregion

        #region Equality

        [TestMethod, TestCategory("Equals")]
        public void Equality_WhereTheComponentsAreEqual_ShouldBeTrue_Test()
        {
            Vector3 s1 = new Vector3(1, 2, 3);
            Vector3 s2 = new Vector3(1, 2, 3);

            var result = s1.Equals(s2);

            result.Should().Be(true);
        }

        [TestMethod, TestCategory("Equals")]
        public void Equality_WhereXYZOrderIsImportantAndTheComponentAreNotInTheCorrectOrder_ShouldBeFalse_Test()
        {
            Vector3 s1 = new Vector3(1, 2, 3);
            Vector3 s2 = new Vector3(3, 2, 1);

            var result = s1.Equals(s2);

            result.Should().Be(false);
        }

        [TestMethod, TestCategory("Equals")]
        public void Equality_WhereZComponentsArePositiveInfinity_ShouldBeTrue_Test()
        {
            Vector3 s1 = new Vector3(1, 2, double.PositiveInfinity);
            Vector3 s2 = new Vector3(1, 2, double.PositiveInfinity);

            var zComponentResult = s1.Z.Equals(s2.Z);
            var result = s1.Equals(s2);

            // Test our assumption about the .Net framework expectations
            double.PositiveInfinity.Equals(double.PositiveInfinity).Should().Be(true, "the .Net framework should find double positive infinty equal to positive infinity (if this assumption is wront then the logic of this test is also wrong)");

            // Test that the component operation matches our assumption about the .Net framework
            zComponentResult.Should().Be(true, "z components of positive infinty and positive infinty should be equal");

            // Test that our result matches the assumption
            result.Should().Be(true, "positive infinty and positive infinty should be equal");
        }

        [TestMethod, TestCategory("Equals")]
        public void Equality_WhereZComponentsAreNegativeInfinity_ShouldBeTrue_Test()
        {
            Vector3 s1 = new Vector3(1, 2, double.NegativeInfinity);
            Vector3 s2 = new Vector3(1, 2, double.NegativeInfinity);

            var zComponentResult = s1.Z.Equals(s2.Z);
            var result = s1.Equals(s2);

            // Test our assumption about the .Net framework expectations
            double.NegativeInfinity.Equals(double.NegativeInfinity).Should().Be(true, "the .Net framework should find double negative infinty equal to negative infinity (if this assumption is wront then the logic of this test is also wrong)");

            // Test that the component operation matches our assumption about the.Net framework
            zComponentResult.Should().Be(true, "z components of negative infinty and negative infinty should be equal");

            // Test that our result matches the assumption
            result.Should().Be(true, "negative infinty and negative infinty should be equal");
        }

        [TestMethod, TestCategory("Equals")]
        public void EqualityWithTolerance_WhereTheZComponentsAreDifferentByLessThanTheTolerance_ShouldBeTrue_Test()
        {
            Vector3 s1 = new Vector3(1, 2, 3);
            Vector3 s2 = new Vector3(1, 2, 2.9999);

            var result = s1.Equals(s2, 0.0002);

            // Quick test to ensure that the double minus operation does not introduce an unexpected margin of error that is higher than our margin of error
            (3d - (2.9999d)).Should().BeLessThan(0.0002d, "the double calculations of 3 minus 2.9999 should be less than 0.0002 or our test is incorrect");
            
            // Assert our acual test
            result.Should().Be(true, "the tolerance should have been greter than the difference between the z components for s1 and s2");
        }

        [TestMethod, TestCategory("Equals")]
        public void EqualityWithTolerance_WhereZComponentDifferenceIsGreaterThanTheTolerance_ShouldBeFalse_Test()
        {
            Vector3 s1 = new Vector3(1, 2, 3);
            Vector3 s2 = new Vector3(1, 2, 2.9999);

            var result = s1.Equals(s2, 0.00009);

            // Quick test to ensure that the double minus operation does not introduce an unexpected margin of error
            (3d - 2.9999d).Should().BeGreaterThan(0.00009d, "the double calculations of 3 minus 2.9999 should be greater than 0.00009 or our test is incorrect");

            result.Should().Be(false, "the tolerance should be less than the difference between the z components for s1 and s2");
        }

        [TestMethod, TestCategory("Equals")]
        public void EqualityWithTolerance_WhereZComponentsArePositiveInfinity_ShouldBeTrue_Test()
        {
            Vector3 s1 = new Vector3(1, 2, double.PositiveInfinity);
            Vector3 s2 = new Vector3(1, 2, double.PositiveInfinity);

            var zComponentResult = s1.Z.Equals(s2.Z);
            var result = s1.Equals(s2, 0.00001);

            // Test our assumption about the .Net framework expectations
            double.PositiveInfinity.Equals(double.PositiveInfinity).Should().Be(true, "the .Net framework should find double positive infinty equal to positive infinity (if this assumption is wront then the logic of this test is also wrong)");

            // Test that the component operation matches our assumption about the .Net framework
            zComponentResult.Should().Be(true, "positive infinty and positive infinty should be equal regardless of tolerance");

            // Test that our result matches the assumption
            result.Should().Be(true, "positive infinty and positive infinty should be equal regardless of tolerance");
        }

        [TestMethod, TestCategory("Equals")]
        public void EqualityWithTolerance_WhereZComponentsAreNegativeInfinity_ShouldBeTrue_Test()
        {
            Vector3 s1 = new Vector3(1, 2, double.NegativeInfinity);
            Vector3 s2 = new Vector3(1, 2, double.NegativeInfinity);

            var zComponentResult = s1.Z.Equals(s2.Z);
            var result = s1.Equals(s2, 0.00001);

            // Test our assumption about the .Net framework expectations
            double.NegativeInfinity.Equals(double.NegativeInfinity).Should().Be(true, "the .Net framework should find double negative infinty equal to negative infinity (if this assumption is wront then the logic of this test is also wrong)");

            // Test that the component operation matches our assumption about the .Net framework
            zComponentResult.Should().Be(true, "negative infinty and negative infinty should be equal regardless of tolerance");

            // Test that our result matches the assumption
            result.Should().Be(true, "the two vectors should be equal given negative infinty and negative infinty should be equal regardless of tolerance");
        }

        [TestMethod, TestCategory("Equals")]
        public void EqualityWithTolerance_WhereZComponentsArePositiveInfinityAndNegativeInfinity_ShouldBeFalse_Test()
        {
            Vector3 s1 = new Vector3(1, 2, double.PositiveInfinity);
            Vector3 s2 = new Vector3(1, 2, double.NegativeInfinity);

            var zComponentResult = s1.Z.Equals(s2.Z);
            var result = s1.Equals(s2, 0.00001);

            // Test our assumption about the .Net framework expectations
            double.PositiveInfinity.Equals(double.NegativeInfinity).Should().Be(false, "the .Net framework should find double positive infinty not equal to negative infinity (if this assumption is wront then the logic of this test is also wrong)");

            // Test that the component operation matches our assumption about the .Net framework
            zComponentResult.Should().Be(false, "positive infinty and negative infinty should not be equal");

            // Test that our result matches the assumption
            result.Should().Be(false, "the two vectors should not be equal given positive infinty and negative infinty should not be equal");
        }

        #endregion

        #region Is Unit Vector Tests

        [TestMethod, TestCategory("IsUnitVector")]
        public void IsUnitVector_x1y0z0_ShouldBeTrue_Test()
        {
            Vector3 s1 = new Vector3(1, 0, 0);
            var result = s1.IsUnitVector();

            result.Should().Be(true, "vector (1,0,0) is a unit vector");
        }

        [TestMethod, TestCategory("IsUnitVector")]
        public void IsUnitVector_WhereXYZAreOne_ShouldBeFalse_Test()
        {
            Vector3 s1 = new Vector3(1, 1, 1);
            var result = s1.IsUnitVector();

            result.Should().Be(false, "vector (1,1,1) is not a unit vector");
        }

        [TestMethod, TestCategory("IsUnitVector")]
        public void IsUnitVector_WhereZComponentIsPositiveInfinity_ShouldBeFalse_Test()
        {
            Vector3 s1 = new Vector3(1, 2, double.PositiveInfinity);
            var result = s1.IsUnitVector();

            result.Should().Be(false, "vector (0,0, +Inf) is not a unit vector");
        }

        [TestMethod, TestCategory("IsUnitVector")]
        public void IsUnitVector_WhereZComponentIsNegativeInfinity_ShouldBeFalse_Test()
        {
            Vector3 s1 = new Vector3(1, 2, double.NegativeInfinity);
            var result = s1.IsUnitVector();

            result.Should().Be(false, "vector (0,0, -Inf) is not a unit vector");
        }

        [TestMethod, TestCategory("IsUnitVector")]
        public void IsUnitVectorWithTolerance_x1y0z0_ShouldBeTrue_Test()
        {
            Vector3 s1 = new Vector3(1, 0, 0);
            var result = s1.IsUnitVector(0.0002);

            result.Should().Be(true, "vector (1,0,0) is a unit vector");
        }

        [TestMethod, TestCategory("IsUnitVector")]
        public void IsUnitVectorWithTolerance_WhereTheZComponentIsNearlyOneByLessThanTheTolerance_ShouldBeTrue_Test()
        {
            Vector3 s1 = new Vector3(0, 0, 0.9999);
            var result = s1.IsUnitVector(0.0002);

            // Quick test to ensure that the double minus operation does not introduce an unexpected margin of error that is higher than our margin of error
            (1d - 0.9999d).Should().BeLessThan(0.0002d, "the double calculations of 1 minus 0.9999 should be less than 0.0002 or our test is incorrect");

            // Assert our acual test
            result.Should().Be(true, "the tolerance should have been greater than the difference between the z components and 1");
        }

        [TestMethod, TestCategory("IsUnitVector")]
        public void IsUnitVectorWithTolerance_WhereTheZComponentIsNearlyOneByMoreThanTheTolerance_ShouldBeFalse_Test()
        {
            Vector3 s1 = new Vector3(0, 0, 0.9999);

            var result = s1.IsUnitVector(0.00009);

            // Quick test to ensure that the double minus operation does not introduce an unexpected margin of error
            (1d - 0.9999d).Should().BeGreaterThan(0.00009d, "the double calculations of 3 minus 2.9999 should be greater than 0.00009 or our test is incorrect");

            result.Should().Be(false, "the tolerance should be less than the difference between the z component and 1");
        }

        #endregion

        #region Is Perpendicular Tests

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicular_x1y0z0_x0y1z0_ShouldBeTrue_Test()
        {
            Vector3 s1 = new Vector3(1, 0, 0);
            Vector3 s2 = new Vector3(0, 1, 0);
            var result = s1.IsPerpendicular(s2);

            result.Should().Be(true, "vector (1,0,0) is perpendicular to vector (0,1,0)");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicular_x1y0z0_x1y1z1_ShouldBeFalse_Test()
        {
            Vector3 s1 = new Vector3(1, 0, 0);
            Vector3 s2 = new Vector3(1, 1, 1);
            var result = s1.IsPerpendicular(s2);

            result.Should().Be(false, "vector (1,0,0) is not perpendicular to vector (1,1,1)");
        }

        [TestMethod, TestCategory("IsPerpendicular")]
        public void IsPerpendicular_WhereFirstVectorXIsNegative1AndSecondVectorYIsOne_ShouldBeTrue_Test()
        {
            Vector3 s1 = new Vector3(-1, 0, 0);
            Vector3 s2 = new Vector3(0, 1, 0);
            var result = s1.IsPerpendicular(s2);

            result.Should().Be(true, "vector (-1,0,0) is perpendicular to vector (0,1,0)");
        }

        #endregion
    }
}
