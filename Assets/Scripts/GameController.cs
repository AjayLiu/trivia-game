﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

using System.Net;
using System;
using System.IO;

public class GameController : MonoBehaviour
{
    int questionIndex = 0;
    List<Question> list = new List<Question>();

    // Start is called before the first frame update
    void Start()
    {
        list = GetData().list;
        quizPanel.questionText.text = list[questionIndex].question;
        quizPanel.choice.SetChoices(list[questionIndex].ChoicesAndAnswer().ToArray());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public QuizPanelScript quizPanel;

    public void OnAnswer(int index)
    {

    }

    [Serializable]
    public class Question
    {
        public string question;
        public List<string> choices;
        public string answer;

        public List<string> ChoicesAndAnswer()
        {
            List<string> temp = new List<string>(choices);
            temp.Add(answer);
            return temp;
        }

    }

    [Serializable]
    public class QuestionList
    {
        public List<Question> list;
    }

    private QuestionList GetData()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://troygamedev.github.io/trivia-game-data/questions.json");
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        QuestionList q = JsonUtility.FromJson<QuestionList>(jsonResponse);
        print(q.list.Count);
        return q;
    }
}