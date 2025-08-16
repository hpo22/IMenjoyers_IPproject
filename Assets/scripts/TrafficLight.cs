//NAN KHIN BHONE THANT

using TMPro;
using UnityEngine;
using System.Collections;

public class TrafficLight : MonoBehaviour
{
    enum State { Idle, Warning, Close }
    private State currentState = State.Idle;

    public GameObject warningUI;      
    public TMP_Text distanceText;     
    public Transform trafficLight;    
    public float deactivateDistance = 5f; 

    private Transform player;

    void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                // Do nothing unless player enters trigger
                break;

            case State.Warning:
                if (player != null && trafficLight != null)
                {
                    float distance = Vector3.Distance(player.position, trafficLight.position);

                    
                    if (distanceText != null)
                        distanceText.text = $"Traffic Light Ahead – {Mathf.Round(distance)}m";

                    // if player is close enough → Close state
                    if (distance <= deactivateDistance)
                    {
                        currentState = State.Close;
                        Debug.Log("Player reached traffic light");
                        SetUI(false);
                    }
                }
                break;

            case State.Close:
                
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            currentState = State.Warning;
            Debug.Log("Player entered zone");
            SetUI(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
            currentState = State.Idle;
            Debug.Log("Player exited zone");
            SetUI(false);
        }
    }

    void SetUI(bool active)
    {
        if (warningUI != null)
            warningUI.SetActive(active);
    }
}
