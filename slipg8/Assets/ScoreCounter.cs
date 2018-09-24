using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {

    [HideInInspector]
    public int current_score;

    public Text SCORE_DISPLAY;




	
	// Update is called once per frame
	void Update () {

        SCORE_DISPLAY.text = current_score.ToString("00000000000");

	}

    public void AddScore(int amount)
    {
        current_score += amount;
    }

    public void ResetCounter()
    {
        current_score = 0;
    }
}
