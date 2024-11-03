using UnityEngine;

public class FloorTransition : MonoBehaviour
{
    [Tooltip("The floor this trigger corresponds to. 0 = Ground Floor, 1 = First Floor, etc.")]
    public int targetFloor; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            UpdateCurrentFloor(targetFloor);
        }
    }

   private void UpdateCurrentFloor(int floor)
{
    Debug.Log("Transitioning to floor: " + floor);

    // Assuming you have a NavigationManager instance handling pathfinding
    NavigationController.Instance.UpdateFloor(floor);

    // Update the user interface to reflect the current floor if needed
    UIManager.Instance.ShowFloorIndicator(floor); // Optional for UI feedback
}

}
