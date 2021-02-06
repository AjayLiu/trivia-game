using System.Collections;
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
        LoadNewQuestion();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public QuizPanelScript quizPanel;

    public void OnAnswer(int index)
    {
        if (answerIndex == index)
        {
            print("CORRECT");
        }


        questionIndex++;
        LoadNewQuestion();
    }

    int answerIndex = 0;
    public void LoadNewQuestion()
    {
        quizPanel.questionText.text = list[questionIndex].question;

        answerIndex = UnityEngine.Random.Range(0, 4);
        quizPanel.choice.SetChoices(list[questionIndex].ShuffleChoicesAndAnswer(answerIndex).ToArray());
    }












    [Serializable]
    public class Question
    {
        public string question;
        public List<string> choices;
        public string answer;

        //shuffles the choices, but ensuring the answer is at answer index (between 0 and 4[exclusive])
        public List<String> ShuffleChoicesAndAnswer(int answerIndex)
        {
            List<string> temp = new List<string>(choices);
            System.Random rng = new System.Random();
            rng.Shuffle(temp);
            temp.Insert(answerIndex, answer);
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
        return q;
    }
}


static class RandomExtensions
{
    public static void Shuffle<T>(this System.Random rng, List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            int k = rng.Next(n--);
            T temp = list[n];
            list[n] = list[k];
            list[k] = temp;
        }
    }
}
