using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport2 : MonoBehaviour
{
   
    public Camera playerCamera;
    public float blinkRange = 10f;
    public float blinkCooldown = 3f;
    public GameObject markerPrefab; // Prefab for the marker object

    private bool canBlink = true;
    private GameObject markerInstance; // Instance of the marker object

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && canBlink)
        {
            Blink();
        }
    }

    private void Blink()
    {
        // Cast a ray from the camera's position in the direction of the player's aim
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, blinkRange))
        {
            // Check if the raycast hit an object that blocks Blink
            if (hit.collider.CompareTag("Obstacle"))
            {
                // Handle obstacle hit - you can implement custom logic here
                Debug.Log("Cannot Blink. Obstacle in the way!");
                return;
            }

            // Instantiate the marker at the raycast hit point
            markerInstance = Instantiate(markerPrefab, hit.point, Quaternion.identity);
        }
        else
        {
            // Instantiate the marker at the maximum Blink range if no obstacles were hit
            markerInstance = Instantiate(markerPrefab, ray.GetPoint(blinkRange), Quaternion.identity);
        }

        // Teleport the player to the marker's position
        transform.position = markerInstance.transform.position;

        // Trigger any visual effects or animations here

        // Start the cooldown timer
        canBlink = false;
        Invoke(nameof(ResetBlinkCooldown), blinkCooldown);
    }

    private void ResetBlinkCooldown()
    {
        canBlink = true;
        if (markerInstance != null)
        {
            Destroy(markerInstance); // Destroy the marker after Blink cooldown ends
        }
    }
}

