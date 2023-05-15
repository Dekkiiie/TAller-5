using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activar : MonoBehaviour
{
    public GameObject enemyGroup, bloqueo, triggerBloq;


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyGroup.SetActive(true);
            bloqueo.SetActive(true);
            triggerBloq.SetActive(false);
        }
    }
}
