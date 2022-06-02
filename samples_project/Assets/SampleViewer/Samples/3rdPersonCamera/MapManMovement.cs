using Esri.HPFramework;
using System;
using UnityEditor.Animations;
using UnityEngine;

public class MapManMovement : MonoBehaviour
{
    [SerializeField] private Animator Animator;

    [SerializeField] private float Speed = 10f;
    [SerializeField] private float RotationSpeed = 10f;
    [SerializeField] private float jumpSpeed = 10f;
    [SerializeField] private float gravityScalar = 1f;

    [SerializeField] private HPTransform CameraTransform;
    [SerializeField] private Vector3 offset;

    private float currentY = 0f;

    private bool started = false;

    private CharacterController CharacterController;

    private HPTransform characterHP;

#if ENABLE_INPUT_SYSTEM
    InputAction movement;
    InputAction jump;

    void Start()
    {
        movement = new InputAction("PlayerMovement", binding: "<Gamepad>/leftStick");
        movement.AddCompositeBinding("Dpad")
            .With("Up", "<Keyboard>/w")
            .With("Up", "<Keyboard>/upArrow")
            .With("Down", "<Keyboard>/s")
            .With("Down", "<Keyboard>/downArrow")
            .With("Left", "<Keyboard>/a")
            .With("Left", "<Keyboard>/leftArrow")
            .With("Right", "<Keyboard>/d")
            .With("Right", "<Keyboard>/rightArrow");

        jump = new InputAction("PlayerJump", binding: "<Gamepad>/a");
        jump.AddBinding("<Keyboard>/space");

        movement.Enable();
        jump.Enable();
    }
#endif

    // Start is called before the first frame update
    private void Start()
    {
        CharacterController = GetComponent<CharacterController>();
        characterHP = GetComponent<HPTransform>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Create movement vector.
        Vector3 movement = new Vector3(0f, 0f, 0f);

        // Forward movement.
        movement.z = Input.GetAxisRaw("Vertical");
        movement.x = Input.GetAxisRaw("Horizontal");
        movement *= Speed;

        // rotation
        var newQuat = Quaternion.LookRotation(movement, Vector3.up).normalized;
        CharacterController.transform.localRotation = Quaternion.Slerp(CharacterController.transform.localRotation, newQuat, RotationSpeed * Time.deltaTime).normalized;
        

        // Handle jump.
        if (CharacterController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                currentY = jumpSpeed;
            }
        }

        // Handle falling.
        if (!CharacterController.isGrounded)
        {
            currentY += Physics.gravity.y * gravityScalar * Time.deltaTime;
        }
        movement.y = currentY;

        CharacterController.Move(movement * Time.deltaTime);

        //Change animation
        var horizontalMagnitude = new Vector3(CharacterController.velocity.x, 0, CharacterController.velocity.z).magnitude;
        Debug.Log(horizontalMagnitude);
        Animator.SetBool("IsWalking", horizontalMagnitude > 0f);

        //if (horizontalMagnitude == 0f)
        //{
        //    Animator.SetBool("IsWalking", false);
        //    Animator.SetBool("IsRunning", false);
        //}
        //else if (5f > horizontalMagnitude && horizontalMagnitude > 0f )
        //{
        //    Animator.SetBool("IsWalking", true);
        //    Animator.SetBool("IsRunning", false);
        //}
        //else
        //{
        //    Animator.SetBool("IsWalking", false);
        //    Animator.SetBool("IsRunning", true);
        //}
        // Bring map man above the map if he falls below.
        if (transform.localPosition.y < -50)
        {
            //characterHP.UniversePosition = new Unity.Mathematics.double3(characterHP.UniversePosition.x, 50, characterHP.UniversePosition.z);
            CharacterController.transform.localPosition = new Vector3(transform.localPosition.x, 50, transform.localPosition.z); 
            currentY = 0;
        }
    }
}