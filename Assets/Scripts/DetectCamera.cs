using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class DetectCamera : MonoBehaviour
{
    public TMPro.TMP_Text text;

    void Update()
    {
        var camera = FindObjectOfType<Camera>();
        if (camera == null)
        {
            text.text = "NO CAMERA";
        }
        else
        {
            text.text = "has camera";

            if (camera.TryGetComponent(out NetworkObject camNetObj))
            {
                text.text += " (networked)";
            }
            else
            {
                text.text += " (MISSING NETWORK OBJECT)";
            }
        }

    }
}
