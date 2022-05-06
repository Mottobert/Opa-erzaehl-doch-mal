using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] questions;

    private int questionIndex = 0;

    private void Start()
    {
        HideAllQuestions();
    }

    public void StartQuiz()
    {
        DisplayQuestion(questionIndex);
    }

    public void DisplayNextQuestionDelay()
    {
        Invoke("DisplayNextQuestion", 3f);
    }

    private void DisplayNextQuestion()
    {
        HideQuestion(questionIndex);
        questionIndex++;

        if(questionIndex <= questions.Length - 1)
        {
            DisplayQuestion(questionIndex);
        }
    }

    private void DisplayQuestion(int index)
    {
        questions[index].SetActive(true);
    }

    private void HideQuestion(int index)
    {
        questions[index].SetActive(false);
    }

    private void HideAllQuestions()
    {
        foreach(GameObject q in questions)
        {
            q.SetActive(false);
        }
    }
}
