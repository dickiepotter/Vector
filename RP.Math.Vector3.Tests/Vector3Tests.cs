namespace RP.Math.Tests
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
            var vector = new Vector3( new Vector3(100, -100, 0));

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

        [TestMethod]
        public void MagnitudeUsingPositiveAndNegativeWholeNumberParametersTest()
        {
            var vector = new Vector3(3, 1, -1);
            var magnitude = vector.Magnitude;

            magnitude.Should().Be(System.Math.Sqrt(11));
        }

        [TestMethod]
        public void MagnitudeUsingPositiveWholeNumberParametersTest()
        {
            var vector = new Vector3(2, 3, 4);
            var magnitude = vector.Magnitude;

            magnitude.Should().Be(System.Math.Sqrt(29));
        }

        /// <summary>
        /// Test scaling a vector with X component only
        /// </summary>
        [TestMethod]
        public void ScaleXTest()
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
        [TestMethod]
        public void ScaleYTest()
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
        [TestMethod]
        public void ScaleXZTest()
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

            result.X.Should().Be(6d/25d, "X should be 6/25");
            result.Y.Should().Be(-(8d/25d), "Y should be -(8/25)");
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

            result.X.Should().Be(2d -(6d / 25d), "X should be 2-(6/25)");
            result.Y.Should().Be(1d +(8d / 25d), "Y should be 1+(8/25)");
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
    }
}
