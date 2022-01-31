using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Animator MenuAnim;
    public GameObject menu;
    public void startAnim()
    {
        MenuAnim.SetBool("StartAnim", true);
        StartCoroutine(switchScene());
    }

    private IEnumerator switchScene()
    {
        yield return new WaitForSeconds(1f);
        menu.SetActive(false);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }

    public void SwitchToScene(string nameOfScene)
    {
        SceneManager.LoadScene(nameOfScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
