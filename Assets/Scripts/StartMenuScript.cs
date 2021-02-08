using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{
    public TextAsset creditTxt, howToTxt;
    public GameObject panel;
    Text panelText;
    // Start is called before the first frame update
    void Start()
    {
        panelText = panel.GetComponentInChildren<Text>();
        panel.SetActive(false);
    }

    public int gameSceneIndex;
    public void PlayButtonPress()
    {
        SceneManager.LoadScene(gameSceneIndex);
    }
    public void CreditsButtonPress()
    {
        panel.SetActive(true);
        panelText.text = creditTxt.text;
    }
    public void HowToPlayButtonPress()
    {
        panel.SetActive(true);
        panelText.text = howToTxt.text;
    }
    public void OnXButtonPress()
    {
        panel.SetActive(false);
    }


}
