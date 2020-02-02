using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event System.Action OnReachedEndOfLevel;
    public float moveSpeed = 5f;
    public float turnSpeed = 5f;
    Vector3 velocity;
    float lerpAngle;
    bool disabled;

    Rigidbody rigidBody;

    void Start()
    {
        disabled = false;
        rigidBody = GetComponent<Rigidbody>();
        Guard.OnGuardHasSpottedPlayer += Disable;

    }
    void Update()
    {
        Vector3 inputDirection = Vector3.zero;
        if (!disabled)
        {
            inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        }

        float inputMagnitude = inputDirection.magnitude;
        float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
        lerpAngle = Mathf.LerpAngle(lerpAngle, targetAngle, Time.deltaTime * turnSpeed * inputMagnitude);
        //transform.eulerAngles = Vector3.up * lerpAngle;
        velocity = transform.forward * inputMagnitude * moveSpeed;
        //transform.Translate(transform.forward * inputMagnitude * moveSpeed * Time.deltaTime, Space.World);
    }
    void FixedUpdate()
    {
        rigidBody.MoveRotation(Quaternion.Euler(Vector3.up * lerpAngle));
        rigidBody.MovePosition(rigidBody.position + velocity * Time.deltaTime);
    }
    void Disable()
    {
        disabled = true;
    }
    void OnDestroy()
    {
        Guard.OnGuardHasSpottedPlayer -= Disable;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Finish")
        {
            if (OnReachedEndOfLevel != null)
                OnReachedEndOfLevel();
        }
    }
}
