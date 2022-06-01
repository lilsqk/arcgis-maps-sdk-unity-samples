using Esri.HPFramework;
using System;
using UnityEditor.Animations;
using UnityEngine;

public class MapManMovement : MonoBehaviour
{
    [SerializeField] private Animator Animator;

    [SerializeField] private float Speed = 10f;
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
        Vector3 movement = new Vector3(0f, 0f, 0f);
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");
        movement *= Speed;

        if (CharacterController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                currentY = jumpSpeed;
            }
        }

        if (!CharacterController.isGrounded)
        {
            currentY += Physics.gravity.y * gravityScalar * Time.deltaTime;
        }
        movement.y = currentY;

        CharacterController.Move(movement * Time.deltaTime);

        //Change animation
        Debug.Log(CharacterController.velocity.magnitude);

        Animator.SetBool("IsWalking", CharacterController.velocity.magnitude > 0f);
        //if (CharacterController.velocity.magnitude == 0f)
        //{
        //    Animator.SetBool("IsWalking", false); 
        //    Animator.SetBool("IsRunning", false);
        //}
        //else if(CharacterController.velocity.magnitude > 0f && CharacterController.velocity.magnitude < 5f)
        //{
        //    Animator.SetBool("IsWalking", true);
        //}
        //else
        //{
        //    Animator.SetBool("IsRunning", true);
        //}
        // Bring map man above the map if he falls below.
        if (characterHP.UniversePosition.y < 0)
        {
            characterHP.UniversePosition = new Unity.Mathematics.double3(characterHP.UniversePosition.x, 50, characterHP.UniversePosition.z);
            currentY = 0;
        }
    }
}