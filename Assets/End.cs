using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    public GameObject winPanel;
    bool gano = false;
    public PlayerMovementTutorial p;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)&&gano == true)
        {
            Application.Quit();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            winPanel.SetActive(true);
            p.isDead = true;
        }
    }

}
