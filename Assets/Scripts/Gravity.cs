using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Gravity : MonoBehaviour
{
    public Vector3 gravity = new Vector3(0f, -9.81f, 0f);

    void Start()
    {
        Physics.gravity = gravity;
    }
}

