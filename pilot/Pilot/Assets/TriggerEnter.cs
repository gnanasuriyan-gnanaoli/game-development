
using UnityEngine;

public class TriggerEnter : MonoBehaviour
{
    public GameManager gameManager;
    void OnTriggerEnter(Collider coll)
    {
        Debug.Log("Triggered " + coll.tag);
        if(coll.tag == "Player")
            gameManager.CompleteLevel();
    }

}
