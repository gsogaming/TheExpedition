using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicCameraController : MonoBehaviour
{
    [Range(0, 5)]
    [SerializeField] float camPanSpeed;

    //Spaceship
    public Transform spaceShip;

    // Position at which the camera should stop
    [SerializeField] float stopPositionZ;

    [SerializeField] float smoothTime = 2f; // Adjust this value for smoother or slower transition
    private Vector3 velocity = Vector3.zero;

   

    private bool shouldPan = true;

    private Camera mainCamera;
    void Start()
    {
        mainCamera = GetComponent<Camera>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (shouldPan)
        {
            // Calculate the target position for the camera to smoothly move towards
            Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, stopPositionZ);

            // Use SmoothDamp to smoothly move the camera
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

            // Check if the camera's z position is close to the stopPositionZ
            if (Mathf.Abs(spaceShip.transform.position.z - stopPositionZ) <0.1f)
            {
                shouldPan = false;
                
            }
        }

        if (spaceShip.position.z <= 24)
        {
            //mainCamera.transform.LookAt(spaceShip);
        }

    }

    
}
