using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFollow : MonoBehaviour
{

    public Transform lightRoot;
    [SerializeField] private float speed = 2;

    private void OnEnable()
    {

        var rotation = lightRoot.rotation;
        transform.rotation = rotation;
        transform.position = lightRoot.position;

    }

    void Update()
    {
        var rotation = lightRoot.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed);
        transform.position = lightRoot.position;
    }
}
