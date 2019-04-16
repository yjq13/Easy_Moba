using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

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
        public abstract void ClearContext();

        public abstract void InitContext();

        public abstract void Test();

        [Test]
        public void StartTest()
        {
            InitContext();
            Test();
            ClearContext();
        }


    }
}
