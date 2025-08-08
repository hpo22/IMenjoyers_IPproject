using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Accident : MonoBehaviour
{
    public GameObject crashUI; // Assign in Inspector
    public Button hitAndRunButton; // Assign the Hit & Run button in Inspector
    public Button restartButton; // Assign the Restart button in Inspector
    public GameObject[] policeCars; // Array of police cars to activate
    public float policeSpawnDelay = 1f; // Delay between activating each police car
    private bool isCrashed = false;

    void Start()
    {
        // Set up button listeners
        if (hitAndRunButton != null)
        {
            hitAndRunButton.onClick.AddListener(HitAndRun);
        }
        
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Vehicle"))
        {
            crashUI.SetActive(true);
            Time.timeScale = 0f; // Pause game
            isCrashed = true;
            
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void HitAndRun()
    {
        crashUI.SetActive(false);
        Time.timeScale = 1f;
        isCrashed = false;

        // Disable buttons after use
        if (hitAndRunButton != null)
            hitAndRunButton.interactable = false;
        if (restartButton != null)
            restartButton.interactable = false;

        // Activate and start multiple police cars chase
        StartCoroutine(ActivatePoliceCars());
        
        Debug.Log("police cars are coming!");
    }

    System.Collections.IEnumerator ActivatePoliceCars()
    {
        for (int i = 0; i < policeCars.Length; i++)
        {
            if (policeCars[i] != null)
            {
                // Activate the police car at its current position
                policeCars[i].SetActive(true);

                // Start chase if Police component exists
                Police policeScript = policeCars[i].GetComponent<Police>();
                if (policeScript != null)
                {
                    policeScript.TriggerChase();
                }

                Debug.Log($"Police car {i + 1} activated and chasing!");

                // Wait before activating the next police car
                yield return new WaitForSeconds(policeSpawnDelay);
            }
        }
    }
}
