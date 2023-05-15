using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimControllerGolem : MonoBehaviour
{

    Enemy en;
    // Start is called before the first frame update
    void Start()
    {
        en = GetComponentInParent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void animA()
    {
        en.Attack();
    }
}
