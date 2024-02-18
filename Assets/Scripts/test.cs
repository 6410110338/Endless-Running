using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private CharacterController characterController;
    private Animator anim;
    private float speed = 3.0f;

    private Vector3 moveVector;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("isSlide", true);

            moveVector.z = 15;
        }
        else
        {
            anim.SetBool("isSlide", false);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("isJump", true);
        }
        else
        {
            anim.SetBool("isJump", false);
        }
    }
}
