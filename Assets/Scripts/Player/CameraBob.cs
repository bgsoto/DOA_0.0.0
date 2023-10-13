using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBob : MonoBehaviour
{
    public CharacterController player;
    public CinemachineVirtualCamera vcam;

    private void Update()
    {
        //get player velocity and adjust amp/freq accordingly

        //vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.1f;
        //vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0.5f; will change amp and freq of head bob
       
    }
}