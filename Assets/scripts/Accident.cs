// HTET PAING OO

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Accident : MonoBehaviour
{
    public GameObject crashUI; // Assign in Inspector
    public Button hitAndRunButton;
    public Button restartButton;
    public GameObject[] policeCars; // array of police cars to activate
    public float policeSpawnDelay = 1f; // delay between activating each police car

    void Start()
    {
        // Set up button listeners
        if (hitAndRunButton != null)
        {
            hitAndRunButton.onClick.AddListener(HitAndRun); // handle hit and run action 
        }

        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame); // handle restart action 
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Vehicle")) // check if the collided object is a vehicle
        {
            crashUI.SetActive(true);
            Time.timeScale = 0f; // Pause game

            // Enable the buttons when crashed
            if (hitAndRunButton != null)
                hitAndRunButton.interactable = true;
            if (restartButton != null)
                restartButton.interactable = true;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // reload the current scene
    }

    public void HitAndRun()
    {
        crashUI.SetActive(false);
        Time.timeScale = 1f;

        // Disable buttons after use
        if (hitAndRunButton != null)
            hitAndRunButton.interactable = false;
        if (restartButton != null)
            restartButton.interactable = false;

        StartCoroutine(ActivatePoliceCars()); // activate and start multiple police cars chase

        Debug.Log("police cars are coming!");
    }

    System.Collections.IEnumerator ActivatePoliceCars()
    {
        for (int i = 0; i < policeCars.Length; i++)
        {
            if (policeCars[i] != null)
            {
                policeCars[i].SetActive(true); // activate police car at its current position

                Police policeScript = policeCars[i].GetComponent<Police>(); // start chase if Police component exists

                if (policeScript != null)
                {
                    policeScript.TriggerChase(); // trigger chase in police script
                }

                Debug.Log($"Police car {i + 1} activated and chasing!");

                yield return new WaitForSeconds(policeSpawnDelay); // wait before activating the next police car
            }
        }
    }
}
