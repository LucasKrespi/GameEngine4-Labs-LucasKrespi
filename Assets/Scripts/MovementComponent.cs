using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementComponent : MonoBehaviour
{
    //movement variables
    [SerializeField]
    float walkSpeed = 5;
    [SerializeField]
    float runSpeed = 10;
    [SerializeField]
    float jumpForce = 5;

    //components
    PlayerController playerController;
    Rigidbody playerRigdbody;
    Animator playerAnimator;

    //movement references
    Vector2 inputVector2 = Vector2.zero;
    Vector3 moveDirection = Vector3.zero;

    public readonly int movementXHash = Animator.StringToHash("MovementX");
    public readonly int movementYHash = Animator.StringToHash("MovementY");
    public readonly int isJumpingHash = Animator.StringToHash("isJumping");
    public readonly int isRunningHash = Animator.StringToHash("isRunning");


    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerRigdbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        //if (playerController.isJumping) return;
        if (!(inputVector2.magnitude > 0)) moveDirection = Vector3.zero;

        moveDirection = transform.forward * inputVector2.y + transform.right * inputVector2.x;
        float currentSpeed = playerController.isRunning ? runSpeed : walkSpeed;

        Vector3 movementDirection = moveDirection * (currentSpeed * Time.deltaTime);

        transform.position += movementDirection;
    }

    public void OnMovement(InputValue value)
    {
        inputVector2 = value.Get<Vector2>();

        playerAnimator.SetFloat(movementXHash, inputVector2.x);
        playerAnimator.SetFloat(movementYHash, inputVector2.y);
    }
    public void OnRun(InputValue value)
    {
        playerController.isRunning = value.isPressed;
        playerAnimator.SetBool(isRunningHash, playerController.isRunning);
    }
    public void OnJump(InputValue value)
    {
        playerController.isJumping = value.isPressed;

        playerRigdbody.AddForce((transform.up + moveDirection) * jumpForce, ForceMode.Impulse);

        playerAnimator.SetBool(isJumpingHash, playerController.isJumping);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground") && !playerController.isJumping) return;

        playerController.isJumping = false;

        playerAnimator.SetBool(isJumpingHash,false);
    }
}
