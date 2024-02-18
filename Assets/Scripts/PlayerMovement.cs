using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private bool isDead = false;
    private Vector3 moveVector;

    [SerializeField] private float moveSpeed = 4.2f;
    [SerializeField] private float jumpHeight = 3.0f;
    private float verticalVelocity;
    private float gravity = 9.81f;
    private float groundedTimer;

    private Animator anim;
    private float animationDuration = 3.0f;
    private bool AnimationRUN = true;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        StartCoroutine(WaitToMove());
    }

    // Update is called once per frame
    void Update()
    {
        if (AnimationRUN)
        {
            characterController.Move(Vector3.forward * moveSpeed * Time.deltaTime);
            return;
        }

        if (isDead)
            return;

        bool groundedPlayer = characterController.isGrounded;
        if (groundedPlayer)
        {
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

        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            if (groundedTimer > 0)
            {
                anim.SetBool("isJump", true);
                groundedTimer = 0;
                verticalVelocity += Mathf.Sqrt(jumpHeight * 2 * gravity);
            }
        }
        else
        {
            anim.SetBool("isJump", false);
        }
        

        if (Input.GetKeyDown(KeyCode.LeftShift) && groundedPlayer)
        {
            characterController.center = new Vector3(0.0f, 0.48f,0.0f);
            characterController.height = 0.64f;
            moveVector.z = moveSpeed + 0.8f;
            Invoke("ResetColliderForSlide",1.0f);
            anim.SetBool("isSlide",true);
        }

        
        //X - Left and Right
        moveVector.x = Input.GetAxisRaw("Horizontal") * (moveSpeed > 10? 7:4);

        //Y - Up and Down
        moveVector.y = verticalVelocity;

        //Z - Forward and Backward
        moveVector.z = moveSpeed;

        characterController.Move(moveVector * Time.deltaTime);
    }

    IEnumerator WaitToMove()
    {
        yield return new WaitForSeconds(animationDuration);
        AnimationRUN = false;
    }

    public void SetSpeed(int modifier)
    {
        moveSpeed += modifier;
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

    private void ResetColliderForSlide()
    {
        characterController.height = 1.6f;
        characterController.center = new Vector3(0f, 0.8f, 0f);
        anim.SetBool("isSlide", false);
    }
}
