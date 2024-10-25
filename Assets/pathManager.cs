using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public KeyPoint[] allKeyPoints;  // All key points in your scene

    // This method generates a path from the start KeyPoint to the destination KeyPoint
    public List<KeyPoint> GeneratePath(KeyPoint start, KeyPoint destination)
    {
        List<KeyPoint> path = new List<KeyPoint>();

        
        path.Add(start);

        
        foreach (KeyPoint keyPoint in allKeyPoints)
        {
            if (keyPoint != start && keyPoint != destination)
            {
                
                path.Add(keyPoint);
            }
        }

        
        path.Add(destination);

        if (path.Count == 0)
        {
            Debug.LogError("PathManager: Failed to generate a valid path.");
        }

        return path;
    }
}
