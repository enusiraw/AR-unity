using UnityEngine;
using System.Collections.Generic;

public class KeyPoint : MonoBehaviour
{
    public string pointID;
    public string pointDescription;
    public List<KeyPoint> connectedPoints = new List<KeyPoint>();

    void Start()
    {
        Debug.Log("Key Point: " + pointID + " is at position: " + transform.position);

        foreach (var connectedPoint in connectedPoints)
        {
            Debug.Log("Connected to: " + connectedPoint.pointID);
        }
    }
    public void AddConnection(KeyPoint keyPoint)
    {
        if (!connectedPoints.Contains(keyPoint))
        {
            connectedPoints.Add(keyPoint);
            Debug.Log($"{pointID} now connected to {keyPoint.pointID}");
        }
    }
}
