using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 10;
    public float runSpeed = 15;
    public float jumpHeight = 0.5f;
    public float gravity = -9.81f;
    public float crouchSpeed = 5;
    public float crouchHeight = 0.5f;
    public float standingHeight = 2;
    public KeyCode interactKey = KeyCode.E;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Interaction Settings")]
    public float interactDistance = 3f;
  
    float hInput;
    float vInput;
    CharacterController controller;

    public bool active;
    public Transform playerCamera;
    public Transform body;
    public Transform icon;
    private Animator animator;

    Vector3 direction;
    Vector3 velocity;
    public bool isGrounded;

    private bool isCrouching;
    public bool IsCrouching => isCrouching;
    public bool hidden = false;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = body.GetComponent<Animator>();
    }

    void HandleInputs()
    {
        if (active == true)
        {
            // Ground check
            isGrounded = controller.isGrounded;
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            // Toggle crouch
            if (Input.GetKeyDown(crouchKey))
            {
                isCrouching = !isCrouching;
                controller.height = isCrouching ? crouchHeight : standingHeight;
            }

            // Movement input
            hInput = Input.GetAxis("Horizontal");
            vInput = Input.GetAxis("Vertical");
            direction = transform.right * hInput + transform.forward * vInput;

            // Running
            float currentSpeed = isCrouching ? crouchSpeed : (Input.GetKey(KeyCode.LeftShift) ? runSpeed : speed);
            controller.Move(direction * currentSpeed * Time.deltaTime);

            // Jumping
            if (isGrounded && Input.GetButtonDown("Jump") && !isCrouching)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            // Apply gravity
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }


    void Interact()
    {
        if (!active) return;

        if (Input.GetKeyDown(interactKey))
        {
            Ray ray = new Ray(playerCamera.position, playerCamera.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
            {
                if (hit.collider.TryGetComponent<Interactable>(out var interactable))
                {
                    interactable.Interact(this.transform);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleInputs();
        Interact();
    }

    public void Deactivate()
    {
        playerCamera.gameObject.SetActive(false);
        body.gameObject.SetActive(true);

        if(isCrouching)
        {
            animator.SetBool("crouched", true);
        }
        else
        {
            animator.SetBool("crouched", false);
        }

        active = false;
        icon.gameObject.SetActive(false);
    }

    public void Activate()
    {
        playerCamera.gameObject.SetActive(true);
        active = true;
        body.gameObject.SetActive(false);
        icon.gameObject.SetActive(true);
    }
}

