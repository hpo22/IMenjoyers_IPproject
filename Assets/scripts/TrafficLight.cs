//NAN KHIN BHONE THANT

using TMPro;
using UnityEngine;
using System.Collections;

public class TrafficLight : MonoBehaviour
{
    public GameObject warningUI; // Assign your UI sign here
    public TMP_Text distanceText; // The text that shows the distance
    public Transform trafficLight; // Reference to traffic light position
    public float deactivateDistance = 5f; // Hide UI when this close to traffic light

    private Transform player;
    private bool isPlayerInZone = false;

    

    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player")) 
        {
            player = other.transform;
            isPlayerInZone = true;
            
            if (warningUI != null)
            {
                warningUI.SetActive(true);
                Debug.Log("Warning UI activated - Player entered zone");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = false;
            if (warningUI != null)
            {
                warningUI.SetActive(false);
                Debug.Log("Warning UI deactivated - Player exited zone");
            }
            player = null;
        }
    }

    void Update()
    {
        if (isPlayerInZone && player != null && trafficLight != null)
        {
            float distance = Vector3.Distance(player.position, trafficLight.position);

            
            // Update distance text 
            if (distanceText != null)
            {
                distanceText.text = $" Traffic Light Ahead – {Mathf.Round(distance)}m";
            }

            // Hide UI when close to traffic light
            if (distance <= deactivateDistance)
            {
                if (warningUI != null)
                {
                    warningUI.SetActive(false);
                    Debug.Log("Warning UI hidden - Player reached traffic light");
                }
                isPlayerInZone = false;
                player = null;
            }
        }
    }
}
