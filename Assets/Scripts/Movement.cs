using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private CharacterController character;
    public Transform cam;

    public float speed = 6f;
    public float run_speed = 7.5f;
    public float turn_smooth_time = 0.05f;
    
    float turn_smooth_velocity;

    Vector3 velocity;

    void Start()
    {
        character = GetComponent<CharacterController>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = 0f;
        float z = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(x, y, z).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turn_smooth_velocity, turn_smooth_time);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                character.Move(moveDir.normalized * speed * 1.5f * Time.deltaTime);
            }
            else
            {
                character.Move(moveDir.normalized * speed * Time.deltaTime);
            }
        }

    }
}
