using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int currentScore = 0;



    void ResetScore()
    {
        currentScore = 0;
    }

    void AddScore(int score)
    {
        currentScore += score;
    }


}
