namespace RP.Math.Tests
{
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
            angle.Should().BeGreaterThan(0);
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
            angle.Should().BeGreaterThan(0);
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
    }
}
