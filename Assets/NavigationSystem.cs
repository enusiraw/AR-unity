using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class NavigationSystem : MonoBehaviour
{
    public Transform userPosition;
    public GameObject arrowPrefab;
    private GameObject arrowInstance;
    private KeyPoint targetKeyPoint;
    public float arrivalThreshold = 1.0f;
    private KeyPoint[] keyPoints;

    void Start()
    {
        userPosition = userPosition ?? GameObject.FindWithTag("MainCamera")?.transform;

        keyPoints = FindObjectsOfType<KeyPoint>();
        if (keyPoints.Length == 0) Debug.LogError("No keypoints found in the scene.");

        arrowInstance = Instantiate(arrowPrefab);
        arrowInstance.SetActive(false);
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

        StartCoroutine(DisplayTurnByTurnNavigation(path));
    }

    List<KeyPoint> FindPath(KeyPoint start, KeyPoint goal)
    {
        var openSet = new SortedList<float, KeyPoint>();
        var cameFrom = new Dictionary<KeyPoint, KeyPoint>();
        var gScore = new Dictionary<KeyPoint, float>();
        var fScore = new Dictionary<KeyPoint, float>();

        gScore[start] = 0;
        fScore[start] = Heuristic(start, goal);
        openSet.Add(fScore[start], start);

        while (openSet.Count > 0)
        {
            KeyPoint current = openSet.Values[0];
            if (current == goal) return ReconstructPath(cameFrom, current);

            openSet.RemoveAt(0);
            foreach (KeyPoint neighbor in current.connectedPoints)
            {
                float tentativeGScore = gScore[current] + Vector3.Distance(current.transform.position, neighbor.transform.position);
                if (!gScore.ContainsKey(neighbor) || tentativeGScore < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, goal);
                    if (!openSet.ContainsValue(neighbor)) openSet.Add(fScore[neighbor], neighbor);
                }
            }
        }
        return null;
    }

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

    IEnumerator DisplayTurnByTurnNavigation(List<KeyPoint> path)
    {
        foreach (var waypoint in path)
        {
            arrowInstance.SetActive(true);
            PointArrowAt(waypoint.transform.position);
            while (Vector3.Distance(userPosition.position, waypoint.transform.position) > arrivalThreshold)
            {
                yield return null;
            }
            yield return new WaitForSeconds(0.5f);
        }
        arrowInstance.SetActive(false);
        Debug.Log("Destination reached!");
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

        return nearestKeyPoint;
    }

    void PointArrowAt(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - userPosition.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        arrowInstance.transform.position = userPosition.position + direction.normalized * 0.5f;
        arrowInstance.transform.rotation = rotation;
    }
}
