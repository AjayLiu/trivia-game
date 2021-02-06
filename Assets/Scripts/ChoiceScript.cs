using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceScript : MonoBehaviour
{
    GameController game;
    Button[] buttons = new Button[4];
    Text[] buttonTexts = new Text[4];

    // Start is called before the first frame update
    void Start()
    {
        buttons = GetComponentsInChildren<Button>();
        //add event and link text for each button
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.AddListener(delegate { OnClick(buttons[i].name); });
            buttonTexts[i] = buttons[i].GetComponentInChildren<Text>();
        }
    }

    public void OnClick(string name)
    {
        //NAME MUST MATCH INDEX (ex: 1st button choice must be named 0)
        game.OnAnswer(int.Parse(name));
    }

    public void SetChoices(string[] choices)
    {
        for (int i = 0; i < buttonTexts.Length; i++)
        {
            buttonTexts[i].text = choices[i];
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
