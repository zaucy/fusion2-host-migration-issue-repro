using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.UI;

public class Disconnect : MonoBehaviour
{
    public Button disconnectButton;
    void Start()
    {
        disconnectButton = disconnectButton ?? GetComponent<Button>();
        disconnectButton.onClick.AddListener(OnDisconnectButtonPressed);
    }

    private void OnDisconnectButtonPressed()
    {
        var runner = FindObjectOfType<NetworkRunner>();
        if (runner != null)
        {
            if (runner.IsServer)
            {
                runner.Shutdown();
            }
            else
            {
                runner.Disconnect(runner.LocalPlayer);
            }
        }
    }

    void Update()
    {
        var runner = FindObjectOfType<NetworkRunner>();
        if (runner != null)
        {
            disconnectButton.interactable = true;
        }
        else
        {
            disconnectButton.interactable = false;
        }
    }
}
