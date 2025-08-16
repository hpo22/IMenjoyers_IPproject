//Htet Paing Oo

using UnityEngine;
using UnityEngine.UI;

public class FallOff : MonoBehaviour
{
    public GameObject gameOverUI; // assign game over UI 
    public float fallThreshold = -10f; // y position to detect fall

    void Start()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false); // hide game over UI when start
        }
    }
    void Update()
    {
        if (transform.position.y < fallThreshold)
        {
            Debug.Log("Player fell! Game Over.");
            
            GameOver();
        }
    }

    void GameOver()
    {
        Time.timeScale = 0f;

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true); 
        }
    }
}
