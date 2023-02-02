using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int destroyedBuildings;
    public int numOfBuildingsToWin;

    public GameObject playPanel;
    public Image winPanel;
    public Image loosePanel;
    public GameObject winMenu;
    public GameObject looseMenu;
    public Player player;

    private Color temp;

    public int xPos1, xPos2;
    public int zPos1, zPos2;

    private bool finishedLevel;
    public GameObject pauseMenu, playMenu, menu3D;

    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        playMenu.SetActive(false);
        menu3D.SetActive(false);
    }

    public void Unpause()
    {
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Scene0()
    {
        SceneManager.LoadScene(0);
    }

    public void Scene1()
    {
        SceneManager.LoadScene(1);
    }

    public void Scene2()
    {
        SceneManager.LoadScene(2);
    }

    public void Scene3()
    {
        SceneManager.LoadScene(3);
    }

    public void Scene4()
    {
        SceneManager.LoadScene(4);
    }

    public void SwitchToScene(string nameOfScene)
    {
        SceneManager.LoadScene(nameOfScene);
    }
    private void Start()
    {
        temp = winPanel.color;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !finishedLevel)
        {
            Pause();
        }
    }
    public void DestroyedABuilding()
    {
        destroyedBuildings++;
        playPanel.SetActive(false);
        if (destroyedBuildings >= numOfBuildingsToWin)
        {
            StartCoroutine(WinLevel());
        }
    }

    public IEnumerator WinLevel()
    {
        playPanel.SetActive(false);
        finishedLevel = true;
        player.currentHealth = 100000000;
        while (temp.a < .5)
        {
            yield return new WaitForSeconds(.1f);
            temp.a = temp.a + 0.01f;
            winPanel.color = temp;
        }
        winMenu.SetActive(true);
    }

    public void loose()
    {
        if (!finishedLevel)
        {
            finishedLevel = true;
            StartCoroutine(LooseLevel());
        }
        
    }
    public IEnumerator LooseLevel()
    {
        while (temp.a < .5)
        {
            yield return new WaitForSeconds(.1f);
            temp.a = temp.a + 0.01f;
            winPanel.color = temp;
        }
        looseMenu.SetActive(true);
    }
}
