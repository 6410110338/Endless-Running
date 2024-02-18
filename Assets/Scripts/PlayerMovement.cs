using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private bool isDead = false;
    private Vector3 moveVector;

    [SerializeField] public float moveSpeed = 4.2f;
    [SerializeField] private float jumpHeight = 3.0f;
    private float verticalVelocity;
    private float gravity = 9.81f;
    private float groundedTimer;

    private Animator anim;
    private float animationDuration = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
            return;

        bool groundedPlayer = characterController.isGrounded;

        if (Time.time < animationDuration)
        {
            characterController.Move(Vector3.forward * moveSpeed * Time.deltaTime);
            return;
        }

        //Debug.Log(groundedPlayer);

        if (groundedPlayer)
        {
            anim.SetBool("isJump", false);
            // cooldown interval to allow reliable jumping even whem coming down ramps
            groundedTimer = 0.2f;
        }

        if (groundedTimer > 0)
        {
            groundedTimer -= Time.deltaTime;
        }

        if (groundedPlayer && verticalVelocity < 0.0f)
        {
            verticalVelocity = -0.5f;
        }
        
        verticalVelocity -= gravity * Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
        {
            anim.SetBool("isJump", true);
            if (groundedTimer > 0)
            {
                groundedTimer = 0;
                verticalVelocity += Mathf.Sqrt(jumpHeight * 2 * gravity);
            }
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("isSlide",true);
        }
        else
        {
            anim.SetBool("isSlide", false);
        }

        //X - Left and Right
        moveVector.x = Input.GetAxisRaw("Horizontal") * moveSpeed;

        //Y - Up and Down
        moveVector.y = verticalVelocity;

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


    public void SetSpeed(int modifier)
    {
        moveSpeed = 5.0f + modifier;
    }

    //It is being called every time our capsule hits something
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Debug.Log("HIT");
        if (hit.point.z > transform.position.z + characterController.radius)
            Death();
    }

    private void Death()
    {
        isDead = true;
        GetComponent<UIScore>().OnDeath();
    }
}
