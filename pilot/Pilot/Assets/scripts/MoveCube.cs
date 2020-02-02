using UnityEngine;

public class MoveCube : MonoBehaviour
{
    public Rigidbody rb;
    private float forwardForce = 1200f;
    private float sidewaysForce = 1500f;
    // Start is called before the first frame update

    
    // Update is called once per frame
    void FixedUpdate(){
        rb.AddForce(0, 0, forwardForce * Time.deltaTime);
        //if (Input.GetKey("w") || Input.GetKey("up"))
        //{
        //    rb.AddForce(0, sidewaysForce * Time.deltaTime, 0);
        //}
        if (Input.GetKey("s"))
        {
            rb.AddForce(0, -sidewaysForce * Time.deltaTime, 0);
        }
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0);
        }
        if(Input.GetKey("a") || Input.GetKey("left"))
        {
            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0);
        }
        if(rb.position.y < 0)
        {
            CallGameOver();
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Obstacle")
        {
            this.enabled = false;
            CallGameOver();
        }
    }
    void CallGameOver()
    {

        FindObjectOfType<GameManager>().GameOver();
    }
}
