using UnityEngine;

public class ARCameramov : MonoBehaviour
{
    public Transform arrow;
    public Transform destination;
    public float moveSpeed = 1.0f;

    private bool isNavigating = false;

    void Update()
    {
        if (isNavigating)
        {
            Vector3 targetPosition = arrow.position;
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            transform.LookAt(arrow);

            if (Vector3.Distance(transform.position, destination.position) < 0.5f)
            {
                isNavigating = false;
                Debug.Log("Arrived at the destination!");
            }
        }
    }

    public void StartNavigation()
    {
        isNavigating = true;
    }
}
