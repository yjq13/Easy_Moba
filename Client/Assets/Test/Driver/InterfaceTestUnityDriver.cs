using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

namespace Test
{

    public interface Interface_TestDriver_Unity
    {
        IEnumerator Test();
        void ClearContext();
        void InitContext();
    }

    public abstract class UnityDriverBase : Interface_TestDriver_Unity
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

        public abstract IEnumerator Test();

        [UnityTest]
        public IEnumerator StartTest()
        {
            yield return Test();
        }
    }
}
