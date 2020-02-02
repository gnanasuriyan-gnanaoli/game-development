using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlockController : MonoBehaviour
{
    float speed;
    public Transform playerTransform;
    public Vector2 speedMinMax;

    void Start()
    {
        speed = Mathf.Lerp(speedMinMax.x, speedMinMax.y, Difficulty.getDifficultyPercent());
    }
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < (-Camera.main.orthographicSize))
        {
            Destroy(gameObject);
        }
    }
}