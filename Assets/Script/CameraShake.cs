
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }
    private CinemachineVirtualCamera VCam;
    private float shakeTimer;

    private void Awake()
    {
        Instance = this;
        VCam = GetComponent<CinemachineVirtualCamera>();

    }

    
    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cmMultiCP = VCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cmMultiCP.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }
    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if(shakeTimer <=0f)
            {
                // Time Over
                CinemachineBasicMultiChannelPerlin cmMultiCP = VCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cmMultiCP.m_AmplitudeGain = 0f;
            }
        }
    }

}
