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
    [SerializeField]
    float aimSensitivity;

    
    //components
    PlayerController playerController;
    Rigidbody playerRigdbody;
    Animator playerAnimator;

    //movement references
    Vector2 inputVector2 = Vector2.zero;
    Vector3 moveDirection = Vector3.zero;
    Vector2 lookVector = Vector2.zero;

    [SerializeField]
    GameObject followTransform;

    public readonly int movementXHash = Animator.StringToHash("MovementX");
    public readonly int movementYHash = Animator.StringToHash("MovementY");
    public readonly int isJumpingHash = Animator.StringToHash("isJumping");
    public readonly int isRunningHash = Animator.StringToHash("isRunning");
    public readonly int isFiringHash = Animator.StringToHash("isFiring");
    public readonly int isReloadingHash = Animator.StringToHash("isReloading");
    public readonly int verticalAinHash = Animator.StringToHash("VerticalAim");


    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerRigdbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }
    void Start()
    {
        if (!GameManager.instance.cursorActive)
        {
            AppEvents.InvokOnMouseCursorEnableEvent(false);
        }
    }

    void Update()
    {

        followTransform.transform.rotation *= Quaternion.AngleAxis(lookVector.x * aimSensitivity, Vector3.up);
        followTransform.transform.rotation *= Quaternion.AngleAxis(lookVector.y * aimSensitivity, Vector3.left);

        var angles = followTransform.transform.localEulerAngles;
        angles.z = 0;

        var angle = followTransform.transform.localEulerAngles.x;

        float min = -60;

        float max = 70.0f;

        float range = max - min;

        float offsetToZero = 0 - min;

        float aimAngle = followTransform.transform.localEulerAngles.x;

        aimAngle = (aimAngle > 180) ? aimAngle - 360 : aimAngle;

        float val = (aimAngle + offsetToZero) / (range);


        playerAnimator.SetFloat(verticalAinHash, val);



        if(angle > 180 && angle < 300)
        {
            angles.x = 300;
        }
        else if (angle < 180 && angle > 70)
        {
            angles.x = 70;
        }

        followTransform.transform.localEulerAngles = angles;

        transform.rotation = Quaternion.Euler(0.0f, followTransform.transform.rotation.eulerAngles.y, 0.0f);


        followTransform.transform.localEulerAngles = new Vector3(angles.x, 0.0f, 0.0f);

        if (playerController.isJumping) return;
        if (!(inputVector2.magnitude > 0)) moveDirection = Vector3.zero;

        moveDirection = transform.forward * inputVector2.y + transform.right * inputVector2.x;
        float currentSpeed = playerController.isRunning ? runSpeed : walkSpeed;

        Vector3 movementDirection = moveDirection * (currentSpeed * Time.deltaTime);

        transform.position += movementDirection;
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        LayerMask layer = LayerMask.GetMask("Ground");
       
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 0.1f, layer))
        {
            if (gameObject.GetComponent<Rigidbody>().velocity.y > 0) return;

            playerController.isJumping = false;

            playerAnimator.SetBool(isJumpingHash, false);
        }
    }

    public void OnQuit(InputValue valeu)
    {
        Application.Quit();
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
        if (playerController.isJumping) return;

        playerController.isJumping = value.isPressed;

        playerRigdbody.AddForce((transform.up + moveDirection) * jumpForce, ForceMode.Impulse);

        playerAnimator.SetBool(isJumpingHash, playerController.isJumping);
    }

    public void OnAinm(InputValue value)
    {
        playerController.isAiming = value.isPressed;
    } 
    public void OnLook(InputValue value)
    {
        lookVector = value.Get<Vector2>();
    } 
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (!collision.gameObject.CompareTag("Ground") && !playerController.isJumping) return;

    //    playerController.isJumping = false;

    //    playerAnimator.SetBool(isJumpingHash,false);
    //}
}
