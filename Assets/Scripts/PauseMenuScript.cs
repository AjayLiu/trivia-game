using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResumeButtonPress()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void PauseButtonPress()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public int gameSceneIndex = 0;
    public void RestartButtonPress()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(gameSceneIndex);
    }


    public int homeSceneIndex = 0;
    public void ReturnHomeButtonPress()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(homeSceneIndex);
    }
}
