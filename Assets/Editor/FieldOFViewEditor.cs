using UnityEditor;
using UnityEngine;
using static System.Net.WebRequestMethods;
using static Unity.Burst.Intrinsics.X86.Avx;
/*
*Script Reference
 * -----------------------------------------------------------------------------------------
 *Comp - 3 Interactive
 * "How to Add a Field of View for Your Enemies [Unity Tutorial]"
 * https://www.youtube.com/watch?v=j1-OyLo77ss
 *-----------------------------------------------------------------------------------------
 */

[CustomEditor(typeof(anamolySight_Sensor))]
public class FieldOFViewEditor : Editor
{
    private void OnSceneGUI()
    {
        anamolySight_Sensor fov = (anamolySight_Sensor)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radius);


        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angle /2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.radius);

        if (fov.canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.playerReference.transform.position);
        }

    }





    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }



}
