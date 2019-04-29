using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public GameObject pauseMenu;
    public GameObject PlayerObject;
    public GameObject HeartContainer;
    public GameObject HeartPrefab;
    private List<GameObject> HeartList = new List<GameObject>();
    private Vector3 currentHeartPosition = new Vector3(0, 0, 0);
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        pauseMenu.SetActive(false);
    }
    public void CreateHearts()
    {
        float amount = Character.Instance.Health;
        currentHeartPosition = HeartContainer.transform.position;
        AddHeart(amount);
    }
    public void AddHeart(float amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject go = Instantiate(HeartPrefab, HeartContainer.transform);
            go.transform.position = currentHeartPosition;
            currentHeartPosition = go.transform.position + new Vector3(33, 0, 0);
            HeartList.Add(go);
        }
    }
    public void RemoveHearts(float amount)
    {
        Destroy(HeartList[HeartList.Count - 1]);
        HeartList.RemoveAt(HeartList.Count - 1);
        currentHeartPosition -=  new Vector3(33, 0, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);

    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
    public void OpenMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
