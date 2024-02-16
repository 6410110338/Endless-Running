using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Debug.Log("Start");
    }

    // Update is called once per frame
    void Update()
    {
        if (characterController.isGrounded)
            Debug.Log("HI");
    }
}
