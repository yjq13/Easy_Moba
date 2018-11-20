using UnityEngine;
using System.Collections;
using NetWork;

public class GameStartManager : MonoBehaviour
{
    void Start()
    {
        NetworkManager.Instance.InitComponent(gameObject);
        NetworkManager.Instance.Connect();

    }
}


