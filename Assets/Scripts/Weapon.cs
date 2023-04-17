using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
   public float damage;
   public float range;

    public Camera fpsCam;


    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * range, Color.green);
    }

    //Disparar
    void Shoot()
    {

        RaycastHit hit;

        if(Physics.Raycast(fpsCam.transform.position,fpsCam.transform.forward,out hit, range))
        {
            Debug.Log(hit.transform.name);
            
            Enemy target = hit.transform.GetComponent<Enemy>();

            if(target != null)
            {
                target.TakeDamage(damage);
            }
            
        }



    }
}
