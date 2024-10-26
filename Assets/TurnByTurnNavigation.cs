using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnByTurnNavigation : MonoBehaviour
{
    public GameObject arrowPrefab;
    private GameObject arrowInstance;
    public Transform userPosition;
    public float arrivalThreshold = 1.0f;

    private void Start()
    {
        arrowInstance = Instantiate(arrowPrefab);
        arrowInstance.SetActive(false);
    }

    public IEnumerator NavigatePath(List<KeyPoint> path)
    {
        foreach (var waypoint in path)
        {
            arrowInstance.SetActive(true);
            UpdateArrowPosition(waypoint.transform.position);

            while (Vector3.Distance(userPosition.position, waypoint.transform.position) > arrivalThreshold)
            {
                UpdateArrowPosition(waypoint.transform.position);
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
        }

        arrowInstance.SetActive(false);
        Debug.Log("Destination reached!");
    }

    private void UpdateArrowPosition(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - userPosition.position).normalized;
        arrowInstance.transform.position = userPosition.position + direction * 0.5f; // Set position slightly ahead
        arrowInstance.transform.rotation = Quaternion.LookRotation(direction); // Orient toward target
    }
}
