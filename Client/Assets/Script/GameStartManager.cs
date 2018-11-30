using UnityEngine;
using System.Collections;
using NetWork;

public class GameStartManager : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return StartCoroutine(NetworkManager.Instance.GetWebSocket().Connect());
        NetworkManager.Instance.Connect();

    }
}


