using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

namespace Test
{

    public interface Interface_TestDriver_Unity
    {
        IEnumerator Test();
        IEnumerator ClearContext();
        IEnumerator InitContext();
    }

    public abstract class UnityDriverBase : Interface_TestDriver_Unity
    {
        public abstract IEnumerator ClearContext();

        public abstract IEnumerator InitContext();

        public abstract IEnumerator Test();

        [UnityTest]
        public IEnumerator StartTest()
        {
            yield return InitContext();
            yield return Test();
            yield return ClearContext();
        }
    }
}
