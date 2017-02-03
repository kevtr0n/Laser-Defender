using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    private Text myText;
    public static int score = 0;

    void Start()
    {
        myText = GetComponent<Text>();
    }
    public void Score(int points)
    {
        Debug.Log("Scored points");
        score += points;
        myText.text = score.ToString();
    }
    public static void Reset()
    {
        score = 0;
    }
}
