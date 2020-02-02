using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Transform cubeTransform;
    public Text score;
    bool gameInProgress = true;
    void Update()
    {
        if(gameInProgress)
            score.text =  cubeTransform.position.z.ToString("0");
    }
    public void GameOver()
    {
        gameInProgress = false;
    }
    public string CurrentScore()
    {
       return score.text;
    }

}
