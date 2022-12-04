using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class HostConnect : MonoBehaviour
{
    NetworkManager manager;
    public InputField ip_field;
    public GameObject HostConnect_go;

    private void Awake()
    {
        manager = GetComponent<NetworkManager>();
    }

    public void HostFunction()
    {
        manager.StartHost();

        HostConnect_go.SetActive(false);
    }

    public void ConnectionFunction()
    {
        manager.networkAddress = ip_field.text;
        manager.StartClient();

        HostConnect_go.SetActive(false);
    }
}
