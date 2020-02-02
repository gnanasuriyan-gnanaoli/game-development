using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 7f;
    float halfWidthOfTheScreen = 8f;
    public event System.Action OnPlayerDeath;

    void Start(){
        float halfPlayerWidth = transform.localScale.x / 2;
        halfWidthOfTheScreen = Camera.main.aspect * Camera.main.orthographicSize + halfPlayerWidth;
    }
    void Update(){
        float xDirection = Input.GetAxisRaw("Horizontal");
        float velocity = speed * xDirection;
        transform.Translate(Vector2.right * velocity * Time.deltaTime);
        if (transform.position.x > halfWidthOfTheScreen)
            transform.position = new Vector2(-halfWidthOfTheScreen, transform.position.y);
        if (transform.position.x < -halfWidthOfTheScreen)
            transform.position = new Vector2(halfWidthOfTheScreen, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "Falling Block"){
            if(OnPlayerDeath != null)
            {
                OnPlayerDeath();
            }
            Destroy(gameObject);
            
        }
    }
}
