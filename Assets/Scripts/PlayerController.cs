using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioSource footstep;
    public Manager manager;
    [Header("Animator")]
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        // manager = FindObjectOfType<Manager>();
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Manager.instance.GetGameState() == GameStates.countDown)
        {
            return;
        }

        float speed = 3f;
        float x = Input.GetAxis("Horizontal");
        float y = 0;
        float z = Input.GetAxis("Vertical");
        gameObject.transform.Translate(x * speed * Time.deltaTime, y, z * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)){
            footstep.enabled = true;
        } else{
            footstep.enabled = false;
        }

        if (speed >= 5) {
            anim.SetBool("isWalking", true);
        } else {
            anim.SetBool("isWalking", false);
        }


    }
}
