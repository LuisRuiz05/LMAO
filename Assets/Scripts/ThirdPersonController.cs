using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public CharacterController controller;
    public Transform mainCamera;
    Animator animator;
    
    public float speed = 6f;
    public float runSpeed = 9f;
    
    //
    public float gravity = 9.81f;
    public float jumpSpeed = 3.5f;
    //

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection;
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(hAxis, 0f, vAxis);

        //direction.y -= gravity * Time.deltaTime;

        if (direction.magnitude >= 0.1f)
        {
            
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Run", true);
                controller.Move(moveDirection * runSpeed * Time.deltaTime);
            }
            else
            {
                animator.SetBool("Walk", true);
                animator.SetBool("Run", false);
                controller.Move(moveDirection * speed * Time.deltaTime);
            }
        }
        else
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
        }

        //Animations
        if (Input.GetKeyDown(KeyCode.Q))
        {
            animator.Play("Punch");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            animator.Play("Dance");
        }
    }
}