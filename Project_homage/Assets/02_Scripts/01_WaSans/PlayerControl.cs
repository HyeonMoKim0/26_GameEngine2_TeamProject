using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerControl : MonoBehaviour
{
    public GameObject player;
    public GameObject model;
    CharacterController controller;

    float horizontalInput;
    float verticalInput;
    Vector3 move;
    public float moveSpeed;

    public float rotationSpeed = 10f;

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
        if (SansManager.Instance.isGame)
        {
            move = transform.right * horizontalInput + transform.forward * verticalInput;
            controller.Move(move * moveSpeed * Time.deltaTime);

            if (move.sqrMagnitude > 0.001f)
            {
                // 이동하려는 방향(move)을 바라보는 회전값(Quaternion)을 계산합니다.
                Quaternion targetRotation = Quaternion.LookRotation(move);

                // 자식 모델링 오브젝트(player)만 현재 회전에서 목표 회전까지 부드럽게(Slerp) 돌려줍니다.
                model.transform.rotation = Quaternion.Slerp(model.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
