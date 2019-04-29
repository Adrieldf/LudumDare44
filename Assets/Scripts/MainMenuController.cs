using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void OpenGameScene()
    {
        //StartCoroutine(Play());
        AudioMenuController.Instance.PlayClickSound();
        SceneManager.LoadScene("Game");
    }
    public void ExitGame()
    {
        //StartCoroutine(Exit());
        AudioMenuController.Instance.PlayClickSound();
        Application.Quit();
    }
    public void OpenCredits()
    {
        //StartCoroutine(Credits());
        AudioMenuController.Instance.PlayClickSound();
        SceneManager.LoadScene("Credits");
    }
    public void OpenMainMenuScene()
    {
        //StartCoroutine(MainMenu());
        AudioMenuController.Instance.PlayClickSound();
        SceneManager.LoadScene("MainMenu");
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
        Debug.Log("Exit");
        AudioMenuController.Instance.PlayClickSound();
        yield return new WaitForSeconds(0.5f);
        Debug.Log("ExitAfter");
        Application.Quit();

    }

    IEnumerator Credits()
    {
        Debug.Log("Credits");
        AudioMenuController.Instance.PlayClickSound();
        yield return new WaitForSeconds(0.5f);
        Debug.Log("CreditsAfter");
        SceneManager.LoadScene("Credits");
    }

}
