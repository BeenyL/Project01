using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] Text MenuText;
    [SerializeField] GameObject Menu;
    bool esc;
    //restart / quit
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            esc = !esc;
            if(esc == true)
            {
                MenuText.text = "Menu";
                Menu.SetActive(true);
            }
            else
            {
                Menu.SetActive(false);
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void EndGame()
    {
        Application.Quit();
    }


}