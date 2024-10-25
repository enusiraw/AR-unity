using UnityEngine;

public class KeyPoint : MonoBehaviour
{
    public string pointID;
    public string pointDescription;

    void Start()
    {

        Debug.Log("Key Point: " + pointID + " is at position: " + transform.position);
    }

   
}
