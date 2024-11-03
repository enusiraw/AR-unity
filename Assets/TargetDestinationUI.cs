using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TargetDestinationUI : MonoBehaviour
{
    public TMP_Text destinationDistanceText;   
    public TMP_Text destinationNameText;
    public TMP_Text lastMarkerText;
    public GameObject destinationReachedPanel; 
    public TMP_Text destinationReachedMessage; 
    public Button doneButton;  

    private string lastDestinationName;

    void Start()
    {
        destinationReachedPanel.SetActive(false);
        doneButton.onClick.AddListener(HideDestinationReachedPanel);
    }

    public void DisplayTargetInformation(string destinationName, float targetDistance)
    {
        lastDestinationName = destinationName;
        destinationNameText.text = destinationName;
        destinationDistanceText.text = "(" + targetDistance.ToString("0.00") + " m)";
    }

    public void UpdateDistance(float targetDistance)
    {
        destinationDistanceText.text = "(" + targetDistance.ToString("0.00") + " m)";
    }

   
    public void UpdateLastMarker(string markerName)
    {
        lastMarkerText.text = "Last Marker: " + markerName;
    }


    public void ShowDestinationReachedPanel()
    {
        destinationReachedMessage.text = "You reached your destination: " + lastDestinationName;
        destinationReachedPanel.SetActive(true);
    }

    private void HideDestinationReachedPanel()
    {
        destinationReachedPanel.SetActive(false);
    }

    public void ResetTargetInformation()
    {
        destinationDistanceText.text = "(0.00 m)";
        destinationNameText.text = "Last destination: " + lastDestinationName;
        lastMarkerText.text = "";
    }
}
