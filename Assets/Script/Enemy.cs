using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int health = 50;
    public int expReward = 10;

    private GameObject player;
    PlayerController playerController;
    private NavMeshAgent agent;
    private Vector3 lastTargetPosition;
    public Animate AttackStick;
    WaveManager waveManager;
    void Start()
    {
        player = GameObject.Find("Player(Clone)");
        waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        playerController = player.GetComponent<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 1.5f;
    }

    void Update()
    {
        if (player != null && agent.enabled && agent.isOnNavMesh)
        {
            float dist = Vector3.Distance(player.transform.position, transform.position);

            if (dist > agent.stoppingDistance)
            {
                agent.SetDestination(player.transform.position);
                AttackStick.Ideal();
            }
            else
            {
                agent.ResetPath(); // stop moving when in range
                AttackStick.Attack();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            PlayerStats.Instance.GainExp(expReward);
            playerController.controller.SetScore(1);
            waveManager.EnemyDied();
            Destroy(gameObject);
        }
    }

}
