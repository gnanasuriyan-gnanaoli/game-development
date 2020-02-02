using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    bool gameInProgress = true;
    float delay = 2f;
    public Text gameInfo;
    public void GameOver(){
        if (gameInProgress)
        {
            FindObjectOfType<Score>().GameOver();
            gameInProgress = false;
            gameInfo.text = "GAME OVER \nYour Score: "+ FindObjectOfType<Score>().CurrentScore();

            Invoke("RestartGame", delay);
        }
            
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void CompleteLevel()
    {
        gameInfo.text = "Level " + currentLevel() + " Complete";
        Invoke("clearInfoText", delay);
        
    }

    void clearInfoText()
    {
        if(currentLevel() != 4)
        {
            gameInfo.text = "";
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            gameInfo.text = "Congratulations! You have won!";
        }
    }

    int currentLevel()
    {
        return SceneManager.GetActiveScene().buildIndex + 1;
    }
}
