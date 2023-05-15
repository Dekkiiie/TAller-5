using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public bool isWaving;
    float rad;

    public float intensity, speed;



    private void Update()
    {
        if (isWaving)
        {
            rad += speed * Time.deltaTime;
            
        }
        else
        {
            rad = rad % (Mathf.PI * 2);
            rad = Mathf.Lerp(rad, 0, 0.5f * Time.deltaTime);

        }
        transform.localPosition = new Vector3(0, Mathf.Cos(rad) * intensity, 0);
    }
}
