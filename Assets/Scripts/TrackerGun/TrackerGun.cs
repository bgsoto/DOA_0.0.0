using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackerGun : MonoBehaviour
{
    public Camera fpsCamera;
    public float range = 100f;
    private Dictionary<GameObject, Coroutine> trackedMonsters = new Dictionary<GameObject, Coroutine>();

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            Debug.Log("Tracker Gun shot");
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        Vector3 rayOrigin = fpsCamera.transform.position;
        Vector3 rayDirection = fpsCamera.transform.forward;

        // Draw the raycast line in the Scene view for debugging (visible for 1 second)
        Debug.DrawLine(rayOrigin, rayOrigin + rayDirection * range, Color.green, 1f);

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, range))
        {
            if (hit.collider.tag == "Monster" && !trackedMonsters.ContainsKey(hit.collider.gameObject))
            {
                Coroutine trackingCoroutine = StartCoroutine(TrackMonsterPosition(hit.collider.gameObject, 60f)); // Track for 60 seconds
                trackedMonsters.Add(hit.collider.gameObject, trackingCoroutine);

                Debug.DrawLine(rayOrigin, hit.point, Color.red, 1f);
            }
        }
    }


    IEnumerator TrackMonsterPosition(GameObject monster, float duration)
    {
        float startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            // Update the position every frame 
            Debug.Log("Tracking monster at: " + monster.transform.position);
            yield return null; // Wait for the next frame
        }
        trackedMonsters.Remove(monster);
    }

    public List<Vector3> GetTrackedPositions()
    {
        // Returns the current positions of monster
        List<Vector3> positions = new List<Vector3>();
        foreach (var monster in trackedMonsters.Keys)
        {
            positions.Add(monster.transform.position);
        }
        return positions;
    }
}
