using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCameraBig : MonoBehaviour
{
    [SerializeField] GameObject player;
    public Camera camera ;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(player.transform.position.x, 125f, player.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, 125f, player.transform.position.z);
        
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput < 0f)
        {
            camera.orthographicSize++;
        }
        else if (scrollInput > 0f)
        {
            camera.orthographicSize--;
        }

        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, 50f, 230f);
    }
}
