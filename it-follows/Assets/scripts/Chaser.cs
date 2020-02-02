using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{

    public float speed;
    public Transform playerTransform;
    public GameManager gameManager;

    void Update()
    {
        Vector3 displacement = playerTransform.position - transform.position;
        Vector3 direction = displacement.normalized;
        Vector3 velocity = speed * direction;
        Vector3 moveAmount = velocity * Time.deltaTime;
        if(displacement.magnitude < 20f)
        {
            if (displacement.magnitude > 1.5f)
            {
                transform.Translate(moveAmount);
            }
            else
            {
                gameManager.GameOver();
            }
        }
        
    }
}
