using UnityEngine;
using UnityEngine.AI;

public class Police : MonoBehaviour
{
    public NavMeshAgent agent;
    private Transform player;
    private GameOver gameOverScript;

    void Start()
    {
        // Find the GameOver script in the scene
        gameOverScript = FindFirstObjectByType<GameOver>();
        if (gameOverScript == null)
        {
            Debug.LogError("GameOver script not found in scene! Make sure there's a GameObject with GameOver script.");
        }
    }

    public void StartChase(Transform playerTarget)
    {
        player = playerTarget;
        Debug.Log($"Police car {gameObject.name} started chasing the player!");
    }

    void Update()
    {
        if (player != null && agent != null)
        {
            agent.SetDestination(player.position);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the police car collided with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Police caught the player!");
            
            // Stop the game and show game over
            if (gameOverScript != null)
            {
                gameOverScript.ShowGameOver();
            }
            else
            {
                Debug.LogError("GameOver script reference is null!");
            }
            
            // Stop this police car from moving
            if (agent != null)
            {
                agent.isStopped = true;
                agent.velocity = Vector3.zero;
            }
        }
    }

    
}
