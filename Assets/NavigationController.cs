using UnityEngine;

public class NavigationController : MonoBehaviour
{
    public static NavigationController Instance { get; private set; }

    public TargetDestinationUI targetDestinationUI; 
    public Transform targetPosition;  
    public float destinationThreshold = 1.0f; 
    
    private bool destinationReached = false;
    private int currentFloor = 0;  
    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (destinationReached) return;

        if (IsAtDestination())
        {
            destinationReached = true;
            targetDestinationUI.ShowDestinationReachedPanel();
        }
    }

    private bool IsAtDestination()
    {
        
        return Vector3.Distance(transform.position, targetPosition.position) < destinationThreshold;
    }

    public void UpdateFloor(int newFloor)
    {
        if (currentFloor != newFloor)  
        {
            currentFloor = newFloor;
            Debug.Log("Transitioning to floor: " + newFloor);

           
        }
    }
}
