using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Test
{
    interface Interface_TestDriver
    {
        void Test();
        void ClearContext();
        void InitContext();
    }

    public abstract class DriverBase : Interface_TestDriver
    {

        [TearDown]
        public void TearDown()
        {
            ClearContext();
        }

        [SetUp]
        public void SetUp()
        {
            InitContext();
        }

        public abstract void ClearContext();

        public abstract void InitContext();

        public abstract void Test();

        [Test]
        public void StartTest()
        {
            Test();
        }
    }
}
