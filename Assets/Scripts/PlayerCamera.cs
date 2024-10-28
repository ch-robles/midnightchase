using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // Variables
    public Transform player;
    float mouseSensitivity = 100f;
    
    float rotationX;
    float rotationY;

    bool lockedCursor = true;


    void Start()
    {
        
    }


    void Update()
    {
        if (Manager.GameIsPaused == true || Manager.instance.GetGameState() == GameStates.raceOver)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        else
        {
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }

            else
            {
                // Lock and Hide the Cursor
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;

                if (Manager.instance.GetGameState() == GameStates.running)
                {
                    // Collect Mouse Input
                    float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
                    float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

                    rotationY += mouseX;
                    rotationX -= mouseY;

                    rotationX = Mathf.Clamp(rotationX, -25f, 25f);

                    // Rotate cam and orientation
                    transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
                    player.rotation = Quaternion.Euler(0, rotationY, 0);
                }
            }
        }
        
    }

}
