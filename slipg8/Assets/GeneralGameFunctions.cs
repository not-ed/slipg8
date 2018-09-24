using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralGameFunctions : MonoBehaviour {

    private Player PLAYER;
    private ScoreCounter SCORE;
    private Modifiers MOD_LIST;
    private Timer TIMER;

    void InitializeComponents()
    {
        PLAYER = FindObjectOfType<Player>();
        SCORE = FindObjectOfType<ScoreCounter>();
        MOD_LIST = FindObjectOfType<Modifiers>();
        TIMER = FindObjectOfType<Timer>();
    }

	// Use this for initialization
	void Start () {
        InitializeComponents();
	}

    public void ResetGame()
    {
        //reset player pos and unkill them
        PLAYER.Respawn();
        //reset score
        SCORE.ResetCounter();
        //reset modifiers
        MOD_LIST.ReshuffleModifiers(0);
        //reset countdown (DO LATER)
        TIMER.remaining_time = TIMER.TIME;
        TIMER.UpdateTimerLabels();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
