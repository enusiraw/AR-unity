using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public GameObject arrow;  
    public Transform arCamera;  

    private Vector3 targetPosition;
    private bool isNavigationActive = false;

    private float initialDistanceFromUser = 2.0f; 

    void Start()
    {
        arrow.SetActive(false); 
    }

    void Update()
    {
        if (isNavigationActive)
        {
            UpdateArrowPosition();
        }
    }

    public void StartNavigation(Vector3 destination)
    {
        targetPosition = destination;
        isNavigationActive = true;
        arrow.SetActive(true);
        UpdateArrowPosition();
    }

    public void StopNavigation()
    {
        isNavigationActive = false;
        arrow.SetActive(false);  
    }

    private void UpdateArrowPosition()
    {
        Vector3 direction = (targetPosition - arCamera.position).normalized;
        Vector3 arrowPosition = arCamera.position + direction * initialDistanceFromUser;

        arrow.transform.position = arrowPosition;
        arrow.transform.LookAt(targetPosition);
    }
}
