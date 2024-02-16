using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 moveVector;

    [SerializeField] public float moveSpeed = 4.2f;
    private float vertiicalVelocity = 0.0f;
    private float gravity = 10;

    private float animationDuration = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        moveVector = Vector3.zero;

        if (Time.time < animationDuration)
        {
            characterController.Move(Vector3.forward * moveSpeed * Time.deltaTime);
            return;
        }


        if (characterController.isGrounded)
        {
            vertiicalVelocity = -0.5f;
        }
        else
        {
            vertiicalVelocity -= gravity * Time.deltaTime;
        }

        //X - Left and Right
        moveVector.x = Input.GetAxisRaw("Horizontal") * moveSpeed;

        //Y - Up and Down
        moveVector.y = vertiicalVelocity;

        //Z - Forward and Backward
        moveVector.z = moveSpeed;

        characterController.Move(moveVector * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "item")
        {
            Debug.Log("Destroy");
            Destroy(other.gameObject);
        }
        else
        {
            Debug.Log("NoPlayer");
        }

    }
}
