using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CombatAI))]

public class AngleViewer : Editor
{
    private void OnSceneGUI()
    {
        CombatAI fov = (CombatAI)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.detectionradius);

        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.minimumdetectionangle);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.maximumdetectionangle);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.detectionradius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.detectionradius);
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
