using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WinCondition : NetworkBehaviour
{
    [SerializeField] GameObject VictoryScreen1;
    [SerializeField] GameObject VictoryScreen2;

    private void OnCollisionEnter(Collision collision)
    {
        if (!isServer)
        {
            VictoryScreen1.SetActive(true);
        }
        else
        {
            VictoryScreen2.SetActive(true);
        }
    }

    private void Start()
    {
        VictoryScreen1.SetActive(false);
        VictoryScreen2.SetActive(false);
    }
}
