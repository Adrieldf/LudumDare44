using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void OpenGameScene()
    {
        StartCoroutine(Play());
    }
    public void ExitGame()
    {
       StartCoroutine(Exit());
    }
    public void OpenCredits()
    {
        StartCoroutine(Credits());
    }
    public void OpenMainMenuScene()
    {
        StartCoroutine(MainMenu());
    }
    IEnumerator Play()
    {
        AudioMenuController.Instance.PlayClickSound();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Game");
    }
    IEnumerator MainMenu()
    {
        AudioMenuController.Instance.PlayClickSound();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator Exit()
    {
        AudioMenuController.Instance.PlayClickSound();
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }

    IEnumerator Credits()
    {
        AudioMenuController.Instance.PlayClickSound();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Credits");
    }

}
