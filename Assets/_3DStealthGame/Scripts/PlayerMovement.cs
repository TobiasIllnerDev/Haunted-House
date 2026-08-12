using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputAction MoveAction;

    public float walkSpeed = 1.0f;
    public float turnSpeed = 20f;

    Rigidbody m_Rigibody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    private void Start()
    {
        m_Rigibody = GetComponent<Rigidbody>();
        MoveAction.Enable();
    }

    void FixedUpdate()
    {
        var pos = MoveAction.ReadValue<Vector2>();

        float horizontal = pos.x;
        float vertical = pos.y;

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);

        m_Rigibody.MoveRotation(m_Rotation);
        m_Rigibody.MovePosition(m_Rigibody.position + m_Movement * walkSpeed * Time.deltaTime);
    }
}
