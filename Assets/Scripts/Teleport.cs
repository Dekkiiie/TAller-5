using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    
    
    public LayerMask teleportLayerMask;
    public GameObject player;
    public Camera fpsCam;
    public float range;
    RaycastHit hit;
    public float cooldown;
    public bool canTp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.T) && canTp == true)
        {
            
            StartCoroutine(Tepe());
            
        }
    }

    IEnumerator Tepe() 
    {
        
        
            canTp = false;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                
                player.transform.position = hit.point;

            }
            yield return new WaitForSeconds(cooldown);
        canTp = true;
        
        

    }
}
