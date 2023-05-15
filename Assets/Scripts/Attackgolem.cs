using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackgolem : MonoBehaviour
{
    public int damage;
    public float lifeTime;
    PlayerMovementTutorial p;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            p.GetComponent<PlayerMovementTutorial>().TakeDamage(damage);
        }
    }
}
