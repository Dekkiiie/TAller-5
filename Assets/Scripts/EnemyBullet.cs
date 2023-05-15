using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovementTutorial p = other.gameObject.GetComponent<PlayerMovementTutorial>();

            p.TakeDamage(damage);
        }
    }
}
