using UnityEngine;

public class NavigationSystem : MonoBehaviour
{
    public Transform userPosition;
    public GameObject arrowPrefab;
    private GameObject arrowInstance;
    private KeyPoint targetKeyPoint;
    public float arrivalThreshold = 1.0f;
    public float proximityThreshold = 12.0f; 
    private KeyPoint[] keyPoints;

    void Start()
    {
        if (userPosition == null)
    {
        
        userPosition = GameObject.FindWithTag("MainCamera")?.transform;

        if (userPosition == null)
        {
            Debug.LogError("User position is not assigned and couldn't be found automatically.");
        }
        else
        {
            Debug.Log("User position assigned automatically to AR Camera.");
        }
    }

        keyPoints = FindObjectsOfType<KeyPoint>();
        if (keyPoints.Length == 0)
        {
            Debug.LogError("No keypoints found in the scene. Ensure KeyPoint objects are added.");
        }
        
        
        arrowInstance = Instantiate(arrowPrefab);
        arrowInstance.SetActive(false);
    }

    public void StartNavigation(KeyPoint destination)
    {
        KeyPoint startingPoint = GetCurrentKeyPoint();

        if (startingPoint == null)
        {
            Debug.LogError("Failed to find the starting point.");
            return;
        }

        targetKeyPoint = destination;
        arrowInstance.SetActive(true);
        Debug.Log("Navigation started towards " + destination.name);
    }

    KeyPoint GetCurrentKeyPoint()
    {
        if (userPosition == null)
        {
            Debug.LogError("User position is not assigned.");
            return null;
        }

        KeyPoint nearestKeyPoint = null;
        float closestDistance = Mathf.Infinity;

        foreach (KeyPoint keyPoint in keyPoints)
        {
            if (keyPoint == null)
            {
                Debug.LogError("A keypoint is null in the list. Please check your scene setup.");
                continue;
            }

            float distanceToUser = Vector3.Distance(userPosition.position, keyPoint.transform.position);
            Debug.Log($"Checking keypoint: {keyPoint.name}, Distance to user: {distanceToUser}");

            if (distanceToUser < closestDistance && distanceToUser <= proximityThreshold)
            {
                closestDistance = distanceToUser;
                nearestKeyPoint = keyPoint;
            }
        }

        if (nearestKeyPoint == null)
        {
            Debug.LogError("No valid keypoint found near the user. Consider increasing the proximity threshold.");
        }
        else
        {
            Debug.Log($"Nearest keypoint found: {nearestKeyPoint.name}, Distance: {closestDistance}");
        }

        return nearestKeyPoint;
    }

    void Update()
    {
        if (targetKeyPoint != null)
        {
            PointArrowAt(targetKeyPoint.transform.position);
            CheckArrival();
        }
    }

    void PointArrowAt(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - userPosition.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        arrowInstance.transform.position = userPosition.position + direction.normalized * 0.5f;
        arrowInstance.transform.rotation = rotation;
    }

    void CheckArrival()
    {
        float distance = Vector3.Distance(userPosition.position, targetKeyPoint.transform.position);
        if (distance <= arrivalThreshold)
        {
            arrowInstance.SetActive(false);
            Debug.Log("You have arrived at the destination!");
            targetKeyPoint = null;
        }
    }
}
