using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;
    [SerializeField]
    private bool _isMainMenuChosen;
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(1); //Curent Game Scene
        }

        if(Input.GetKeyDown(KeyCode.M) && _isMainMenuChosen == true)
        {
            SceneManager.LoadScene(0); //Main Menu Scene
        }

        //if the escape is pressed
        //quit application
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }

    public void MainMenu()
    {
        _isMainMenuChosen = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
