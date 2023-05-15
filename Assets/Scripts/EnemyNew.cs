using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNew : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsplayer;

    public Animator anim;

    public int health;

    public float force, upForce;

    public GameObject proyectil,spawn;

    Rigidbody rb;

    public Bloqueo b; 

    public string nombreAnimacionAtaque, nombreAnimacionMuerte;

    //Patrol
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attack
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public float sightRange, attackRange;
    public bool playerInSightRange,playerInAttackRange,isDead,look;
    private void Start()
    {
       
    }
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        spawn.SetActive(true);
        Destroy(spawn, 1f);
    }
    private void Update()
    {

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsplayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsplayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
        
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);

            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            //Llego

            if(distanceToWalkPoint.magnitude < 1f)
            {
                walkPointSet = false;
            }
        }
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }
    private void ChasePlayer()
    {
        if (isDead == true) return;
        agent.SetDestination(player.position);
        
    }
    private void AttackPlayer()
    {
        if (isDead == true) return;

        agent.SetDestination(transform.position);
        if(look == true)
        {
            transform.LookAt(player);
        }
       // 

        if (!alreadyAttacked)
        {

            anim.Play(nombreAnimacionAtaque);
            Rigidbody rb = Instantiate(proyectil, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * force, ForceMode.Impulse);
            rb.AddForce(transform.up * upForce, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack),timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Hit Enemy");
        if(health <= 0)
        {
            look = false;
            isDead = true;
            agent.SetDestination(transform.position);
            alreadyAttacked = true;
            anim.Play(nombreAnimacionMuerte);
            if(b != null)
            {
                b.EnemyDefeated(this);
            }
            
            Invoke(nameof(DestroyEnemy), 5f);
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

    }

    
}
