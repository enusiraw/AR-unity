using UnityEngine;

public class KeyPoint : MonoBehaviour
{
    public KeyPoint[] connectedPoints;

    void Start()
    {
        FindAndConnectAllKeyPoints();
    }

    private void FindAndConnectAllKeyPoints()
    {
        KeyPoint[] allKeyPoints = FindObjectsOfType<KeyPoint>();
        connectedPoints = new KeyPoint[allKeyPoints.Length - 1];

        int index = 0;
        foreach (var keyPoint in allKeyPoints)
        {
            if (keyPoint != this)
            {
                connectedPoints[index] = keyPoint;
                index++;
            }
        }
    }
}
