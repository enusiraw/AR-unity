using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TMP_Dropdown destinationDropdown;
    public Button startButton;
    public TMP_Text feedbackText;
    public GameObject feedbackPanel;
    public NavigationSystem navigationSystem;
    private KeyPoint[] keyPoints;
    public ArrowController arrowController;  // Reference to the ArrowController

    void Start()
    {
        if (feedbackPanel != null)
        {
            feedbackPanel.SetActive(false);
        }

        keyPoints = FindObjectsOfType<KeyPoint>();
        PopulateDropdown();

        startButton.onClick.AddListener(OnStartNavigation);
        destinationDropdown.onValueChanged.AddListener(OnDestinationSelected);

        HideFeedbackAlert();
    }

    void PopulateDropdown()
    {
        destinationDropdown.ClearOptions();
        foreach (KeyPoint kp in keyPoints)
        {
            destinationDropdown.options.Add(new TMP_Dropdown.OptionData(kp.gameObject.name));
        }
        destinationDropdown.RefreshShownValue();
    }

    public void OnDestinationSelected(int index)
    {
        if (index >= 0 && index < keyPoints.Length)
        {
            KeyPoint selectedKeyPoint = keyPoints[index];
            ShowFeedbackAlert("Navigate to: " + selectedKeyPoint.gameObject.name + "?");
        }
    }

    void OnStartNavigation()
    {
        int selectedIndex = destinationDropdown.value;

        if (selectedIndex < 0 || selectedIndex >= keyPoints.Length)
        {
            Debug.LogError("Invalid destination selected.");
            return;
        }

        KeyPoint selectedKeyPoint = keyPoints[selectedIndex];
        navigationSystem.StartNavigation(selectedKeyPoint);

        if (feedbackText != null)
        {
            feedbackText.text = "Navigating to: " + selectedKeyPoint.gameObject.name;
        }

        arrowController.StartNavigation(selectedKeyPoint.transform.position);  // Activate arrow for navigation
        startButton.gameObject.SetActive(false);
        HideFeedbackAlert();
    }

    public void ShowFeedbackAlert(string message)
    {
        if (feedbackPanel != null)
        {
            feedbackPanel.SetActive(true);
        }
        feedbackText.text = message;
        feedbackText.gameObject.SetActive(true);
        startButton.gameObject.SetActive(true);
    }

    public void HideFeedbackAlert()
    {
        if (feedbackPanel != null)
        {
            feedbackPanel.SetActive(false);
        }
        feedbackText.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
    }
}
