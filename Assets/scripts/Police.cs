//HTET PAING OO

using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Police : MonoBehaviour
{
    public enum State { Patrol, Idle, Chase }
    private State currentState;

    public Transform[] patrolPoints; // array of points for the police to patrol betwen
    public Transform player;
    public float patrolSpeed = 5f;
    public float chaseSpeed = 10f;
    public float idleTime = 2f;
    public float catchDistance = 3f; // distance that police can catch the player
    public GameObject gameOverUI; // Assign in Inspector

    private NavMeshAgent agent;
    private int patrolIndex = 0;
    private bool chaseTriggered = false; // not to chase the player first

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = State.Patrol;
        if (gameOverUI != null) gameOverUI.SetActive(false);
        StartCoroutine(FSM());
    }

    private IEnumerator FSM()
    {
        while (true)
        {
            if (currentState == State.Patrol)
            {
                Patrol();
            }
            else if (currentState == State.Idle)
            {
                yield return StartCoroutine(Idle());
            }
            else if (currentState == State.Chase)
            {
                Chase();
            }
            yield return null;
        }
    }

    private void Patrol()
    {
        if (!chaseTriggered) // Prevent accidental chase start
        {
            agent.speed = patrolSpeed;
            if (patrolPoints.Length == 0) return;

            agent.SetDestination(patrolPoints[patrolIndex].position); // police position is patrol point index position

            if (!agent.pathPending && agent.remainingDistance < 0.5f) // when police reach patrol point
            {
                patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
                currentState = State.Idle;
            }
        }
    }

    private IEnumerator Idle()
    {
        agent.ResetPath(); // stop police 
        yield return new WaitForSeconds(idleTime); // wait for idle time 
        currentState = State.Patrol;
    }

    public void Chase()
    {
        if (player != null)
        {
            agent.speed = chaseSpeed;
            agent.SetDestination(player.position); // police position is player position

            if (Vector3.Distance(transform.position, player.position) <= catchDistance) // if police position to player position is less than catchDistance show gameover
            {
                ShowGameOver();
            }
        }
    }

    public void TriggerChase()
    {
        chaseTriggered = true;
        currentState = State.Chase;
    }

    public void StopChase()
    {
        chaseTriggered = false;
        currentState = State.Patrol;
    }

    private void ShowGameOver()
    {
        Debug.Log("GAME OVER - Police caught the player!");
        Time.timeScale = 0; // stop the game 
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

    }

}
