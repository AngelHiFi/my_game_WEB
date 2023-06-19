using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_2 : MonoBehaviour
{
    private KeyCode moveForward = KeyCode.W;
    private KeyCode moveBack = KeyCode.S;
    private KeyCode moveLeft = KeyCode.A;
    private KeyCode moveRight = KeyCode.D;

    [Space]
    private KeyCode acceleration = KeyCode.LeftShift;      // ���

    [Space]
    private KeyCode jump = KeyCode.Space;                  // ������

    private PlayerMove movementController;

    private void Start()
    {
        movementController = GetComponent<PlayerMove>();
    }

    private void Update()
    {
        Vector2 input = Vector2.zero;

        if (Input.GetKey(moveForward))
            input.y = 1;
        else if (Input.GetKey(moveBack))
            input.y = -1;

        if (Input.GetKey(moveLeft))
            input.x = -1;
        else if (Input.GetKey(moveRight))
            input.x = 1;

        if (Input.GetKeyDown(jump))
            movementController.Jump();

        // �������� ��������� ������ � ������ ��������. ��� ���������� �� ������ true ��� falce
        movementController.Move(input, Input.GetKey(acceleration));
    }

}
