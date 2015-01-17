namespace RP.Math.Tests
{
    using FluentAssertions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Vector3Tests
    {
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

        #region Square and Square-Root Tests

        [TestMethod]
        public void SquareComponentsTest()
        {
            var vector = new Vector3(2, 3, 4);
            vector.SqrComponents();

            vector.X.Should().Be(4);
            vector.Y.Should().Be(9);
            vector.Z.Should().Be(16);
        }

        [TestMethod]
        public void SquareRootComponentsTest()
        {
            var vector = new Vector3(4, 9, 16);
            vector.SqrtComponents();

            vector.X.Should().Be(2);
            vector.Y.Should().Be(3);
            vector.Z.Should().Be(4);
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

            angle.Should().Be(System.Math.PI /2);
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

        #endregion
    }
}
