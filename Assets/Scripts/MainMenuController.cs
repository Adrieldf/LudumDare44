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

    IEnumerator Play()
    {
        AudioMenuController.Instance.PlayClickSound();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Game");
    }

    IEnumerator Exit()
    {
        AudioMenuController.Instance.PlayClickSound();
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }
}
