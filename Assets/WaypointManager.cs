using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>(); 

    void Start()
    {
        
        FindWaypoints();
        DrawPathBetweenWaypoints();
    }

    void FindWaypoints()
    {
       
        GameObject[] waypointObjects = GameObject.FindGameObjectsWithTag("WayPoint");
        foreach (GameObject obj in waypointObjects)
        {
            waypoints.Add(obj.transform);
        }
    }

   
    void DrawPathBetweenWaypoints()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = waypoints.Count;
        Vector3[] positions = new Vector3[waypoints.Count];
        for (int i = 0; i < waypoints.Count; i++)
        {
            positions[i] = waypoints[i].position;
        }
        lineRenderer.SetPositions(positions);
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Unlit/Color")) { color = Color.green };
    }
}
