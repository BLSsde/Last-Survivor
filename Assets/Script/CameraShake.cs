using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]private Camera mainCamera;
    private float shakeAmount = 0;

    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            Shake(0.1f, 0.2f);
        }
    }
    public void Shake(float amnt, float length)
    {
        shakeAmount = amnt;
        InvokeRepeating("BeginShake", 0, 0.01f);
        Invoke("StopShake", length);
    }

    private void BeginShake()
    {
        if(shakeAmount > 0)
        {
            Vector3 camPos = mainCamera.transform.position;

            float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
            float offsetY = Random.value * shakeAmount * 2 - shakeAmount;
            camPos.x += offsetX;
            camPos.y += offsetY;

            mainCamera.transform.position = camPos;
        }
    }

    private void StopShake()
    {
        CancelInvoke("BeginShake");
        mainCamera.transform.localPosition = Vector3.zero;
    }
}
