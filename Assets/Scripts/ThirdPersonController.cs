using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public PlayerHandler player;
    public Transform modelTransform;
    public CharacterController controller;
    public Transform mainCamera;
    Animator animator;
    public Vector3 falling = new Vector3(0f,0f,0f);
    bool isAlive = true;

    bool isInCorrectPosition = true;
    bool canKart = true;
    public float speed = 6f;
    public float intoxicatedSpeed = 3.5f;
    public float runSpeed = 9f;
    public float intoxicatedRunSpeed = 6.5f;
    SoundFXManager soundFX;

    public Transform throwPosition;
    public GameObject kartXD;
    
    public float gravity = 9.81f;
    public float jumpHeight = 3.5f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controller = GetComponent<CharacterController>();
        soundFX = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundFXManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            Vector3 moveDirection;
            float hAxis = Input.GetAxisRaw("Horizontal");
            float vAxis = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(hAxis, 0f, vAxis);

            CheckModelsPosition();

            if (Input.GetKey(KeyCode.Space) && controller.isGrounded)
            {
                falling.y = jumpHeight;
            }

            falling.y -= (gravity * Time.deltaTime);

            controller.Move(falling * Time.deltaTime);

            if (direction.magnitude >= 0.1f)
            {

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                moveDirection = moveDirection + (falling * Time.deltaTime);


                if (Input.GetKey(KeyCode.LeftShift))
                {
                    animator.SetBool("Walk", false);
                    animator.SetBool("Run", true);
                    if (isDrunk())
                    {
                        controller.Move(moveDirection * intoxicatedRunSpeed * Time.deltaTime);
                    }
                    else
                    {
                        controller.Move(moveDirection * runSpeed * Time.deltaTime);
                    }
                }
                else
                {
                    animator.SetBool("Walk", true);
                    animator.SetBool("Run", false);
                    if (isDrunk())
                    {
                        controller.Move(moveDirection * intoxicatedSpeed * Time.deltaTime);
                    }
                    else
                    {
                        controller.Move(moveDirection * speed * Time.deltaTime);
                    }
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
                soundFX.source.PlayOneShot(soundFX.punch);
            }
            if (Input.GetMouseButtonDown(0) && hasEnoughLevels())
            {
                if (canKart)
                {
                    canKart = false;
                    GameObject kartClone = Instantiate(kartXD, throwPosition.position, throwPosition.rotation);
                    Rigidbody rkartClone = kartClone.AddComponent<Rigidbody>();

                    Vector3 angle = kartClone.transform.forward + kartClone.transform.up;
                    rkartClone.AddForce(angle * 15, ForceMode.Impulse);
                    StartCoroutine(WaitForNextKart());
                }
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                animator.Play("Dance");
            }
        }
    }

    public void Die()
    {
        animator.Play("Die");
        isAlive = false;
    }

    void CheckModelsPosition()
    {
        modelTransform.localRotation = new Quaternion(0, 0, 0, 1);
        if(modelTransform.localPosition != Vector3.zero)
        {
            isInCorrectPosition = false;
            StartCoroutine(FixModelsPostion());
        }
    }

    bool isDrunk()
    {
        if (player.intoxication > 0)
        {
            return true;
        }
        return false;
    }

    bool hasEnoughLevels()
    {
        if(player.level >= 16)
        {
            return true;
        }
        return false;
    }

    IEnumerator FixModelsPostion()
    {
        if (!isInCorrectPosition) {
            yield return new WaitForSecondsRealtime(12);
            modelTransform.localPosition = Vector3.zero;
            isInCorrectPosition = true;
        }
    }

    IEnumerator WaitForNextKart()
    {
        if (!canKart)
        {
            yield return new WaitForSecondsRealtime((float)1.5);
            canKart = true;
        }
    }
}