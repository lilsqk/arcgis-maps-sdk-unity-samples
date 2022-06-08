using Esri.HPFramework;
using Unity.Mathematics;
using UnityEngine;

public class MapManMovement : MonoBehaviour
{
    [SerializeField] private Animator Animator;
    [SerializeField] private float Speed = 5f;
    [SerializeField] private float RotationSpeed = 1f;
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

        // Rotate the player character based on horizontal input.
        CharacterController.transform.Rotate(0, Input.GetAxisRaw("Horizontal") * RotationSpeed * 360f * Time.deltaTime, 0);

        movement = CharacterController.transform.TransformDirection(Vector3.forward);

        // Forward movement based on the up and down input.
        movement *= Input.GetAxisRaw("Vertical");

        // This is the shift key by default.
        bool running = Input.GetButton("Fire3");

        movement *= running ? Speed * 2 : Speed;

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

        // Bring character above the map if it falls below.
        if (characterHP.UniversePosition.y < -50)
        {
            characterHP.UniversePosition = new double3(characterHP.UniversePosition.x, 50, characterHP.UniversePosition.z);
            currentY = 0;
        }
    }

    private bool TerrainLoaded()
    {
        // Raycast downwards to check if mesh colliders have loaded for the terrain.
        return Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo);
    }
}