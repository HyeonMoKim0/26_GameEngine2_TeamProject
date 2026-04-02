using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    CharacterController controller;

    float horizontalInput;
    float verticalInput;
    Vector3 move;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        move = transform.right * horizontalInput + transform.forward * verticalInput;
        controller.Move(move * moveSpeed * Time.deltaTime);
    }
}
