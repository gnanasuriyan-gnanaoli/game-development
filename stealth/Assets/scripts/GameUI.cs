using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public GameObject gameWinUI;
    public GameObject gameLoseUI;
    bool gameIsOver;
    void Start()
    {
        Guard.OnGuardHasSpottedPlayer += ShowGameLoseUI;
        FindObjectOfType<Player>().OnReachedEndOfLevel += ShowGameWinUI;

    }

    // Update is called once per frame
    void Update()
    {
        if (gameIsOver)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }
        }
        
    }
    void ShowGameLoseUI()
    {
        gameLoseUI.SetActive(true);
        ShowGameUI();
    }
    void ShowGameWinUI()
    {
        gameWinUI.SetActive(true);
        ShowGameUI();
    }

    void ShowGameUI()
    {
        gameIsOver = true;
        Guard.OnGuardHasSpottedPlayer -= ShowGameLoseUI;
        FindObjectOfType<Player>().OnReachedEndOfLevel -= ShowGameWinUI;
    }
}
