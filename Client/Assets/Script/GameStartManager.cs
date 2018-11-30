using UnityEngine;
using System.Collections;
using NetWork;
using easy_moba;

public class GameStartManager : MonoBehaviour
{
    private bool is_web_connected = false;
    IEnumerator Start()
    {
        yield return StartCoroutine(NetworkManager.Instance.StartConnect());
        is_web_connected = true;
        SendTestMessage();
    }

    private void SendTestMessage()
    {
        down_msg msg = new down_msg()
        {
            svr_ts = 1543483207,
            seq = 0,
            err_code = new reply_err_code()
            {
                err_code = "Yingjiaqi is your daddy"
            }
        };
        up_msg msg2 = new up_msg()
        {
            data_op = new req_data_op()
            {
                op = t_op.add
            }
        };
        NetworkManager.Instance.SendMessage(msg2);
    }

    private void Update()
    {
        if (is_web_connected)
        {
            var data = NetworkManager.Instance.ReceiveMessage();
            if(data != null)
            {
                Debug.Log(data.err_code);
            }
        }
    }
}


