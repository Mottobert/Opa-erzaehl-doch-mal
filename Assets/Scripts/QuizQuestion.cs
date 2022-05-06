using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuizQuestion : MonoBehaviour
{
    [SerializeField]
    private QuizManager quizManager;

    [SerializeField]
    private GameObject[] possibleAnswers;
    [SerializeField]
    private GameObject correctAnswer;

    [SerializeField]
    private GameObject nextButton;

    private bool questionAnswered = false;

    public void CheckAnswer(GameObject answer)
    {
        if (!questionAnswered)
        {
            if (answer == correctAnswer)
            {
                Debug.Log("Correct answer");
                // Color anwser text green
                correctAnswer.GetComponentInChildren<TextMeshProUGUI>().color = Color.green;
            }
            else
            {
                Debug.Log("Wrong answer");
                // Color correct answer text green
                correctAnswer.GetComponentInChildren<TextMeshProUGUI>().color = Color.green;

                // Color wrong answer text red
                answer.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
            }

            questionAnswered = true;
        }

        DisplayNextButton();
    }

    private void DisplayNextButton()
    {
        nextButton.SetActive(true);
    }
}
