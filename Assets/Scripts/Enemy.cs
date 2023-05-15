using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float health = 50f;
    public GameObject objective;
    public NavMeshAgent nM;
    public Animator anim;
    Rigidbody rb;
    public GameObject raycastOrigin;
  
    public float range,saveSpeed,saveRotation;

    public float movementSpeed;
    private bool isAttacking = false;

    public CapsuleCollider capCol;

    public float attackRange = 10f;
    public float attackDamage = 10f;
    public LayerMask attackLayer;


    private void Start()
    {
        capCol = GetComponentInChildren<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        nM = GetComponent<NavMeshAgent>();
        saveRotation = nM.angularSpeed;
        saveSpeed = nM.speed;
    }

    private void Update()
    {
        RaycastHit hit;
        nM.destination = objective.transform.position;
        Debug.DrawRay(raycastOrigin.transform.position, raycastOrigin.transform.forward * range, Color.red);

        if (rb.velocity.magnitude > 0 )
        {
            anim.Play("Walk_Golem");
        }

        if (isAttacking && objective != null)
        {
            // Perform the attack
            Attack();
        }




        /*if (Physics.Raycast(raycastOrigin.transform.position, raycastOrigin.transform.forward, out hit,range,attackEnemyLayer))
        {
            nM.angularSpeed = 0f;
            movementSpeed = 0;
            nM.speed = 0;
            anim.SetTrigger("Attack");
            // Raycast hit an object
            Debug.Log("Raycast hit: " + hit.collider.gameObject.name);


            // Perform actions based on the hit object (e.g., apply damage, trigger events, etc.)

        }*/

    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if(health <= 0f)
        {
            Die();
        }
    }
    private void FixedUpdate()
    {
        // Calculate the desired velocity for climbing slopes
        Vector3 desiredVelocity = transform.forward * movementSpeed;

        // Adjust the Rigidbody's velocity to climb slopes
        rb.velocity = new Vector3(desiredVelocity.x, rb.velocity.y, desiredVelocity.z);
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public void Attack()
    {
        // Apply damage to the target
        objective.SendMessage("TakeDamage", attackDamage, SendMessageOptions.DontRequireReceiver);
    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entered collider is a valid target
        if (attackLayer == (attackLayer | (1 << other.gameObject.layer)))
        {
            objective = other.gameObject;
            isAttacking = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // Check if the exited collider is the current target
        if (other.gameObject == objective)
        {
            objective = null;
            isAttacking = false;
        }
    }
    
}
