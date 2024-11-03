using UnityEngine;
using System.Collections.Generic;

public class NavigationSystem : MonoBehaviour
{
    public Transform userPosition;
    public KeyPoint targetKeyPoint;
    private TurnByTurnNavigation turnByTurnNavigation;
    private KeyPoint[] keyPoints;

    void Start()
    {
        userPosition = userPosition ?? GameObject.FindWithTag("MainCamera")?.transform;
        keyPoints = FindObjectsOfType<KeyPoint>();

        if (keyPoints.Length == 0) Debug.LogError("No keypoints found in the scene.");
        
        turnByTurnNavigation = GetComponent<TurnByTurnNavigation>();
        if (turnByTurnNavigation == null)
        {
            Debug.LogError("TurnByTurnNavigation component is missing.");
        }
    }

    public void StartNavigation(KeyPoint destination)
    {
        targetKeyPoint = destination;
        List<KeyPoint> path = FindPath(GetCurrentKeyPoint(), destination);
        
        if (path == null || path.Count == 0)
        {
            Debug.LogError("Failed to find a valid path.");
            return;
        }

        StartCoroutine(turnByTurnNavigation.NavigatePath(path));
    }

   List<KeyPoint> FindPath(KeyPoint start, KeyPoint goal)
{
    List<KeyPoint> path = new List<KeyPoint> { start, goal };
    return path;
}

    KeyPoint GetLowestFScoreNode(List<KeyPoint> openSet, Dictionary<KeyPoint, float> fScore)
    {
        KeyPoint lowest = openSet[0];
        foreach (var node in openSet)
        {
            if (fScore.ContainsKey(node) && fScore[node] < fScore[lowest])
            {
                lowest = node;
            }
        }
        return lowest;
    }

    // Heuristic and ReconstructPath functions remain the same
    float Heuristic(KeyPoint a, KeyPoint b)
    {
        return Vector3.Distance(a.transform.position, b.transform.position);
    }

    List<KeyPoint> ReconstructPath(Dictionary<KeyPoint, KeyPoint> cameFrom, KeyPoint current)
    {
        var path = new List<KeyPoint> { current };
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Insert(0, current);
        }
        return path;
    }

   KeyPoint GetCurrentKeyPoint()
{
    KeyPoint nearestKeyPoint = null;
    float closestDistance = Mathf.Infinity;

    foreach (KeyPoint keyPoint in keyPoints)
    {
        float distanceToUser = Vector3.Distance(userPosition.position, keyPoint.transform.position);
        if (distanceToUser < closestDistance)
        {
            closestDistance = distanceToUser;
            nearestKeyPoint = keyPoint;
        }
    }

    if (nearestKeyPoint == null)
    {
        Debug.LogError("Failed to find the nearest key point to the user's position.");
    }
    else
    {
       // Debug.Log($"Current Key Point: {nearestKeyPoint.pointID} on floor {nearestKeyPoint.floor}");
    }
    return nearestKeyPoint;
}

}
