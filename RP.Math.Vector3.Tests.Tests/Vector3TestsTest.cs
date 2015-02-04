// <copyright file="Vector3TestsTest.cs">Copyright ©  2015</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RP.Math.Tests;

namespace RP.Math.Tests
{
    [TestClass]
    [PexClass(typeof(Vector3Tests))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class Vector3TestsTest
    {
        [PexMethod]
        public void AbsOfVector000Is0Test([PexAssumeUnderTest]Vector3Tests target)
        {
            target.AbsOfVector000Is0Test();
            // TODO: add assertions to method Vector3TestsTest.AbsOfVector000Is0Test(Vector3Tests)
        }
    }
}
