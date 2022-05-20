using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class MapManMovement : MonoBehaviour
{
    [SerializeField] private AnimatorController AnimationController;

    [SerializeField] private float Speed = 15f;


    private CharacterController CharacterController;


    // Start is called before the first frame update
    void Start()
    {
        CharacterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3();
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            movement.z = Speed;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            movement.x = -Speed;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            movement.z = -Speed;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            movement.x = Speed;
        }

        CharacterController.Move(movement * Time.deltaTime);

    }
}
