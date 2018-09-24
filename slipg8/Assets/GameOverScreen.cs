using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour {

    private SoundManager SOUND;
    [SerializeField]
    private GameObject TITLE, FINAL_SCORE_TITLE, FINAL_SCORE_AMOUNT, HIGH_SCORE_PROMPT, HIGH_SCORE_TITLE, HIGH_SCORE_AMOUNT, RESET_NOTICE;
    private GeneralGameFunctions GENERAL_FUNC;

    private bool can_reset = false;
    private int players_final_score;
    private int current_high_score; //5000 is a TEMPORARY VALUE, change this later to read from some kind of file in order to get an adaptive high score system

    void InitializeComponents()
    {
        SOUND = FindObjectOfType<SoundManager>();
        GENERAL_FUNC = FindObjectOfType<GeneralGameFunctions>();
    }

    void Awake() {
        InitializeComponents();
        current_high_score = PlayerPrefs.GetInt("PLAYER_HIGHSCORE", 0);
        HIGH_SCORE_AMOUNT.GetComponent<Text>().text = current_high_score.ToString();
    }

    void OnEnable() //whenever it is re-enabled by the player
    {
        can_reset = false;
        SOUND.PlaySound(SOUND.AS_GAMEOVER);
        FINAL_SCORE_TITLE.SetActive(false);
        FINAL_SCORE_AMOUNT.SetActive(false);
        HIGH_SCORE_PROMPT.SetActive(false);
        HIGH_SCORE_TITLE.SetActive(false);
        HIGH_SCORE_AMOUNT.SetActive(false);
        RESET_NOTICE.SetActive(false);
        

        Invoke("DisplayScoreTitle", 2);
        Invoke("DisplayScoreAmount", 2.5f);

        if (players_final_score > current_high_score)
        {
            PlayerPrefs.SetInt("PLAYER_HIGHSCORE",players_final_score);
            HIGH_SCORE_AMOUNT.GetComponent<Text>().text = players_final_score.ToString();
            current_high_score = players_final_score;
            Invoke("DisplayHighScoreNotice", 3);
            Invoke("DisplayHighScoreTitle", 5);
            Invoke("DisplayHighScoreAmount", 5.5f);
            Invoke("DisplayResetNotice", 6);
        }
        else
        {
            Invoke("DisplayHighScoreTitle", 3);
            Invoke("DisplayHighScoreAmount", 3.5f);
            Invoke("DisplayResetNotice", 4);
        }
    }

    void OnDisable()
    {
        can_reset = false;
    }

    public void UpdateScore(int amount){
        players_final_score = amount;
        FINAL_SCORE_AMOUNT.GetComponent<Text>().text = players_final_score.ToString();
    }

    void DisplayScoreTitle ()
    {
        SOUND.PlaySound(SOUND.AS_GAMEOVER_BLIP);
        FINAL_SCORE_TITLE.SetActive(true);
    }

    void DisplayScoreAmount()
    {
        SOUND.PlaySound(SOUND.AS_GAMEOVER_BLIP);
        FINAL_SCORE_AMOUNT.SetActive(true);
    }

    void DisplayHighScoreNotice()
    {
        SOUND.PlaySound(SOUND.AS_GAMEOVER_HIGHSCORE);
        HIGH_SCORE_PROMPT.SetActive(true);
    }

    void DisplayHighScoreTitle()
    {
        SOUND.PlaySound(SOUND.AS_GAMEOVER_BLIP);
        HIGH_SCORE_TITLE.SetActive(true);
    }

    void DisplayHighScoreAmount()
    {
        SOUND.PlaySound(SOUND.AS_GAMEOVER_BLIP);
        HIGH_SCORE_AMOUNT.SetActive(true);
    }

    void DisplayResetNotice()
    {
        SOUND.PlaySound(SOUND.AS_GAMEOVER_BLIP);
        RESET_NOTICE.SetActive(true);
        can_reset = true;
    }

    // Update is called once per frame
    void Update () {

        if ((Input.GetKeyDown("r")) && can_reset == true)
        {
            GENERAL_FUNC.ResetGame();
            gameObject.SetActive(false);
        }

	}
}
