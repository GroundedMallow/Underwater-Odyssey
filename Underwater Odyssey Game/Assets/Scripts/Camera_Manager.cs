using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Manager : MonoBehaviour
{
    public static Camera_Manager instance;
    [SerializeField] private CinemachineVirtualCamera[] _virtualCams;

    [SerializeField] private float fallPan = .25f;
    [SerializeField] private float fallPanTime = .35f;
    public float fallSpeedThreshold = -15f;

    public bool isLerpYDamping { get; private set; }
    public bool LerpedFromPlayerFalling { get; set; }
    
    private Coroutine lerpYCoroutine;

    private CinemachineVirtualCamera currentCam;
    private CinemachineFramingTransposer framingTransposer;

    private float normalizedPan;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        for (int i = 0; i < _virtualCams.Length; i++)
        {
            if(_virtualCams[i].enabled)
            {
                //set current cam active
                currentCam = _virtualCams[i];
                framingTransposer = currentCam.GetCinemachineComponent<CinemachineFramingTransposer>();
            }
        }

        normalizedPan = framingTransposer.m_YDamping;
    }

    public void LerpYDamping(bool isPlayerFalling)
    {
        lerpYCoroutine = StartCoroutine(LerpYAction(isPlayerFalling));
    }

    private IEnumerator LerpYAction(bool isPlayerFalling)
    {
        isLerpYDamping = true;

        //starting damping amount
        float startDamp = framingTransposer.m_YDamping;
        float endDamp = 0f;

        if (isPlayerFalling) //end damping amount
        {
            endDamp = fallPan;
            LerpedFromPlayerFalling = true;
        }
        else
        {
            endDamp = normalizedPan;
        }

        float time = 0f;
        while(time > fallPanTime)
        {
            time += Time.deltaTime;
            float lerpedPan = Mathf.Lerp(startDamp, endDamp, (time / fallPanTime));
            framingTransposer.m_YDamping = lerpedPan;

            yield return null;
        }

        isLerpYDamping = false;
    }
}
