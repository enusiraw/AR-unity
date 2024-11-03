using UnityEngine;
using UnityEngine.UI; // Include this for using UI elements
using System.Collections.Generic;


public class POIManager : MonoBehaviour
{
    public List<PointOfInterest> poiList;
    public Text poiNameText; 
    private void Start()
    {
        LoadPOIs();
        DisplayPOINames();
    }

    private void LoadPOIs()
    {
        poiList = new List<PointOfInterest>();

        GameObject poiParent = GameObject.Find("POI");
        if (poiParent != null)
        {
            foreach (Transform child in poiParent.transform)
            {
                PointOfInterest poi = new PointOfInterest
                {
                    name = child.name,
                    position = child.position,
                    description = "Description for " + child.name,
                  
                };

                poiList.Add(poi);
            }
        }
        else
        {
            Debug.LogWarning("POI GameObject not found!");
        }
    }

    private void DisplayPOINames()
    {
       
        string poiNames = string.Join(", ", poiList.ConvertAll(poi => poi.name));
        poiNameText.text = "Points of Interest: " + poiNames; 
    }

    public PointOfInterest GetNearestPOI(Vector3 userPosition)
    {
        PointOfInterest nearestPOI = null;
        float nearestDistance = float.MaxValue;

        foreach (PointOfInterest poi in poiList)
        {
            float distance = Vector3.Distance(userPosition, poi.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestPOI = poi;
            }
        }

        return nearestPOI;
    }
}
