using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScore : MonoBehaviour
{
    public FB_SetScore FB_SetScore;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Score.score++;
        FB_SetScore.WriteNewScore(Score.score++);
    }

}
