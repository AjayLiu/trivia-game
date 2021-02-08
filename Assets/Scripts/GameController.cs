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
    int questionIndex = -1;
    List<Question> list = new List<Question>();
    public GameObject obstaclePrefab;

    float score = 0;
    public Text scoreText;

    public HealthBarScript health;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        list = GetData().list;
        LoadNewQuestion();
        SetDifficulty(1);
        StartCoroutine(ObstacleSpawner());
    }


    // Update is called once per frame
    void Update()
    {
        UpdateScore();
    }

    void UpdateScore()
    {
        score += Time.deltaTime;
        scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();
    }
    public QuizPanelScript quizPanel;


    public float heartsPerCorrect = 0.2f;
    public void OnAnswer(int guessIndex)
    {
        if (answerIndex == guessIndex)
        {
            health.SetHearts(health.hearts + heartsPerCorrect);
        }
        else
        {
            SetDifficulty(difficulty + 0.5f);
        }

        LoadNewQuestion();
    }

    int answerIndex = 0;
    public void LoadNewQuestion()
    {
        if (questionIndex < list.Count - 1)
        {
            questionIndex++;
            quizPanel.questionText.text = list[questionIndex].question;

            answerIndex = UnityEngine.Random.Range(0, 4);
            quizPanel.choice.SetChoices(list[questionIndex].ShuffleChoicesAndAnswer(answerIndex).ToArray());
            ResetTimer();
        }
        else
        {
            Debug.LogError("REACHED END OF QUESTIONS LIST");
        }

    }

    void ResetTimer()
    {
        if (currentCountdown != null)
            StopCoroutine(currentCountdown);
        currentCountdown = StartCoroutine(TimerCountdown(8f));
    }

    Coroutine currentCountdown;
    IEnumerator TimerCountdown(float seconds)
    {
        float timer = 0;
        while (timer < seconds)
        {
            timer += Time.deltaTime;
            quizPanel.timeBar.fillAmount = timer / seconds;
            yield return null;
        }
        AnswerTimeOut();
    }

    void AnswerTimeOut()
    {
        SetDifficulty(difficulty + 0.5f);
        LoadNewQuestion();
    }

    float difficulty = 1f;
    public float spawnIntervalDifficultyConstant;
    void SetDifficulty(float newDifficulty)
    {
        if (newDifficulty > difficulty)
        {
            //indicate higher difficulty
        }
        difficulty = newDifficulty;
        obstacleSpawnInterval = 7f - difficulty * spawnIntervalDifficultyConstant;
    }

    public float obstacleSpawnInterval;
    IEnumerator ObstacleSpawner()
    {
        SpawnObstacle();
        yield return new WaitForSeconds(obstacleSpawnInterval);
        StartCoroutine(ObstacleSpawner());
    }

    Queue<ObstacleScript> obstacleQueue = new Queue<ObstacleScript>();
    public Transform obstacleSpawn;
    float WORLD_LEFT_BOUND = -9;
    void SpawnObstacle()
    {
        ObstacleScript obj;
        if (obstacleQueue.Count == 0 || obstacleQueue.Peek().transform.position.x > WORLD_LEFT_BOUND)
        {
            obj = Instantiate(obstaclePrefab, obstacleSpawn.position, Quaternion.identity).GetComponent<ObstacleScript>();
        }
        else
        {
            obj = obstacleQueue.Dequeue();
        }
        obj.transform.position = obstacleSpawn.position;
        obstacleQueue.Enqueue(obj);
    }

    public void OnObstacleHit()
    {
        health.SetHearts(health.hearts - 1);
        print(health.hearts);
        CheckHealthIfGameOver();
    }

    void CheckHealthIfGameOver()
    {
        if (health.hearts <= 0)
        {
            GameOver();
        }
    }

    public GameOverPanelScript gameoverPanelScript;
    void GameOver()
    {
        Time.timeScale = 0;
        gameoverPanelScript.ShowPanel(Mathf.FloorToInt(score));
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
