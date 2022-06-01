using Esri.HPFramework;
using UnityEditor.Animations;
using UnityEngine;

public class MapManMovement : MonoBehaviour
{
    [SerializeField] private AnimatorController AnimationController;

    [SerializeField] private float Speed = 2f;
    [SerializeField] private float jumpSpeed = 8.0f;

    [SerializeField] private float GravityValue = 9.8f;
    [SerializeField] private HPTransform CameraTransform;
    [SerializeField] private Vector3 offset;

    private float CurrentVerticalSpeed = 0f;

    private bool started = false;

    private CharacterController CharacterController;

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
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 movement = new Vector3(0f, 0f, 0f);

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");
        movement *= Speed;

        
        if(started)
        {


            if (!CharacterController.isGrounded)
            {
                movement.y += Physics.gravity.y;
            }

            if (CharacterController.isGrounded)
            {
                movement.y = 0;
            }
        }
        if (Input.GetButtonDown("Jump"))
        {
            started = true;
            movement.y = jumpSpeed;
        }


        movement *= Time.deltaTime;

        CharacterController.Move(movement);

        //CameraTransform.LocalPosition = new Unity.Mathematics.double3(CharacterController.transform.localPosition + offset);

        // Bring map man above the map if he falls below.
        if (CharacterController.transform.position.y < 0)
        {
            CharacterController.transform.position.Set(CharacterController.transform.position.x, 100, CharacterController.transform.position.z);
        }
    }
}