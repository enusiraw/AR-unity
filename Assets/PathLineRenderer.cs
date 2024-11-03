using UnityEngine;

public class PathLineRenderer : MonoBehaviour
{
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer component is missing.");
            return;
        }

        lineRenderer.positionCount = 2; 
    }
    public void UpdateLine(Vector3 startPosition, Vector3 endPosition)
    {
        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, startPosition);
            lineRenderer.SetPosition(1, endPosition);
        }
    }
}
