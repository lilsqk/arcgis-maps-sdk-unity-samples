using Esri.HPFramework;
using UnityEngine;

public class MapManMovement : MonoBehaviour
{
    [SerializeField] private Animator Animator;
    [SerializeField] private float Speed = 5f;
    [SerializeField] private float RotationSpeed = 10f;
    [SerializeField] private float jumpSpeed = 10f;
    [SerializeField] private float gravityScalar = 1f;

    [SerializeField] private Vector3 offset;

    private float currentY = 0f;

    private bool terrainLoaded = false;

    private CharacterController CharacterController;

    private HPTransform characterHP;

    // Start is called before the first frame update
    private void Start()
    {
        CharacterController = GetComponent<CharacterController>();
        characterHP = GetComponent<HPTransform>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!terrainLoaded)
        {
            terrainLoaded = TerrainLoaded();
            return;
        }

        // Create movement vector.
        Vector3 movement = new Vector3(0f, 0f, 0f);

        // Forward movement.
        movement.z = Input.GetAxisRaw("Vertical");
        movement.x = Input.GetAxisRaw("Horizontal");

        // This is the shift key by default.
        bool running = Input.GetButton("Fire3");

        movement *= running ? Speed * 2 : Speed;

        // Rotate hero to match current movement direction.
        if (movement != Vector3.zero)
        {
            var newQuat = Quaternion.LookRotation(movement, Vector3.up).normalized;
            CharacterController.transform.localRotation = Quaternion.Slerp(CharacterController.transform.localRotation, newQuat, RotationSpeed * Time.deltaTime).normalized;
        }

        // Handle jump.
        if (CharacterController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                currentY = jumpSpeed;
                Animator.SetBool("Jumping", true);
            }
        }

        // Handle gravity.
        if (!CharacterController.isGrounded)
        {
            currentY += Physics.gravity.y * gravityScalar * Time.deltaTime;
            Animator.SetBool("Jumping", false);
        }
        movement.y = currentY;

        CharacterController.Move(movement * Time.deltaTime);

        // Change animation when moving.
        var horizontalMagnitude = new Vector3(CharacterController.velocity.x, 0, CharacterController.velocity.z).magnitude;
        
        Animator.SetBool("IsRunning", horizontalMagnitude > 0f && running);
        Animator.SetBool("IsWalking", horizontalMagnitude > 0f && !running);

        // Bring map man above the map if he falls below.
        //if (transform.localPosition.y < -50)
        //{
        //    CharacterController.transform.localPosition = new Vector3(transform.localPosition.x, 50, transform.localPosition.z);
        //    currentY = 0;
        //}
    }

    private bool TerrainLoaded()
    {
        // Raycast downwards to check if mesh colliders have loaded for the terrain.
        return Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo);
    }
}