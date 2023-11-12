using FMODUnity;
using UnityEngine;

public class AnomalyAudioUpdater : MonoBehaviour
{
    StudioEventEmitter emitter;
    private void OnEnable()
    {
        MonsterStateMachine.StateChanged += UpdateFmodParameter;
        var target = GameObject.Find("MonsterAudioSource");
        emitter = target.GetComponent<StudioEventEmitter>();
    }
    private void OnDisable()
    {
        MonsterStateMachine.StateChanged -= UpdateFmodParameter;
    }

    void UpdateFmodParameter(int monsterState)
    {
        if (monsterState < 3)
        {
            emitter.SetParameter("Anomaly State", 0);
            Debug.Log("Anomaly Fmod Updated to Seek!");
        }
        if (monsterState == 3)
        {
            emitter.SetParameter("Anomaly State", 1);
            Debug.Log("Anomaly Fmod Updated to Chase!");
        }
    }
}
