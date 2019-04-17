using UnityEngine;
using System.Collections;
using NetWork;
using easy_moba;
using Common;

public class GameStartManager : MonoBehaviour
{
    private GameEngine m_GameEngine;
    private bool is_web_connected = false;
    IEnumerator Start()
    {
        m_GameEngine = GameEngine.instance;
        yield return StartCoroutine(NetworkManager.Instance.StartConnect());
        is_web_connected = true;
    }

    public void CurrentGameStartCoroutine(IEnumerator erator)
    {
        StartCoroutine(erator);
    }

    private void Update()
    {
        UpdateGetWebInfomation();
        m_GameEngine.RunOneFrame();
    }

    private void UpdateGetWebInfomation()
    {
        if (is_web_connected)
        {
            var data = NetworkManager.Instance.ReceiveMessage();
            if (data != null)
            {
                Debug.Log(data);
            }
        }
    }
}


