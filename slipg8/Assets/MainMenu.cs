using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    [HideInInspector]
    public bool game_has_started = false;
    private bool start_pressed = false;
    private Animator SELF_ANIMATOR;
    private SoundManager SOUND;
    private AudioSource SELF_SOUND;
    private Player PLAYER;
    [SerializeField]
    private GameObject PRESS_START_LABEL;
    private int music_muted = 0;
    public AudioClip MUSIC_PREGAME, MUSIC_INGAME;
    [SerializeField]
    private Text HIGH_SCORE_COUNTER;
    private SoundStatusIcon SOUND_ICON;

    void InitializeComponents()
    {
        SELF_ANIMATOR = gameObject.GetComponent<Animator>();
        SOUND = FindObjectOfType<SoundManager>();
        SELF_SOUND = gameObject.GetComponent<AudioSource>();
        PLAYER = FindObjectOfType<Player>();
        music_muted = PlayerPrefs.GetInt("MUSIC_ISMUTED", 0);
        SOUND_ICON = FindObjectOfType<SoundStatusIcon>();
    }

	// Use this for initialization
	void Start () {
        InitializeComponents();
        PLAYER.enabled = false;
        HIGH_SCORE_COUNTER.text = PlayerPrefs.GetInt("PLAYER_HIGHSCORE",0).ToString();;
    }


	// Update is called once per frame
	void Update () {
		
        if (Input.GetKeyDown("z") && start_pressed == false) {
            start_pressed = true;
            SOUND.PlaySound(SOUND.AS_GAMESTART);
            CancelInvoke("BeginGame");
            Invoke("BeginGame",1.5f);
            SELF_SOUND.Stop();
            PRESS_START_LABEL.SetActive(false);
        }

        if (music_muted == 0) //false, meaning it should play
        {
            SELF_SOUND.mute = false;
        }
        else
        {
            SELF_SOUND.mute = true;
        }

        if (Input.GetKeyDown("n"))
        {
            if(music_muted == 0)
            {
                music_muted = 1;
                PlayerPrefs.SetInt("MUSIC_ISMUTED", music_muted);
            }
            else
            {
                music_muted = 0;
                PlayerPrefs.SetInt("MUSIC_ISMUTED", music_muted);
            }

            SOUND_ICON.DisplayMusicToggle(music_muted);
        }


	}

    void BeginGame()
    {
        game_has_started = true;
        SELF_ANIMATOR.Play("Open");
        PLAYER.enabled = true;
        PlayGameMusic(MUSIC_PREGAME);
    }

    public void PlayGameMusic(AudioClip music)
    {
        SELF_SOUND.Stop();
        SELF_SOUND.clip = music;
        SELF_SOUND.Play();
    }

    public void StopMusic()
    {
        SELF_SOUND.Stop();
    }

}
