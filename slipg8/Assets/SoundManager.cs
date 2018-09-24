using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource AS_SLIPGATE, AS_PLAYER_DEATH, AS_PLAYER_JUMP, AS_PLAYER_RUN, AS_JUMPPAD, AS_GAMEOVER, AS_GAMEOVER_BLIP, AS_GAMEOVER_HIGHSCORE,AS_GAMESTART;
    private int sound_muted = 0;
    private AudioSource[] SOUND_EFFECTS;
    private SoundStatusIcon SOUND_ICON;

    // Use this for initialization
    void Start () {
        sound_muted = PlayerPrefs.GetInt("SOUND_ISMUTED", 0);
        SOUND_EFFECTS = gameObject.GetComponents<AudioSource>();
        SOUND_ICON = FindObjectOfType<SoundStatusIcon>();
        SetMute();
    }

    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            if (sound_muted == 0)
            {
                sound_muted = 1;
                PlayerPrefs.SetInt("SOUND_ISMUTED", sound_muted);
            }
            else
            {
                sound_muted = 0;
                PlayerPrefs.SetInt("SOUND_ISMUTED", sound_muted);
            }

            SetMute();
            SOUND_ICON.DisplaySoundToggle(sound_muted);
        }
    }

    void SetMute()
    {
        if (sound_muted == 0) //false, meaning it should play
        {
            foreach (var i in SOUND_EFFECTS)
            {
                i.mute = false;
            }
            //SELF_SOUND.mute = false;
        }
        else
        {
            foreach (var i in SOUND_EFFECTS)
            {
                i.mute = true;
            }
            // SELF_SOUND.mute = true;
        }
    }

    public void PlaySound(AudioSource desired_as)
    {
        desired_as.PlayOneShot(desired_as.clip);
    }


    public void StopPlayingSound(AudioSource desired_as)
    {
        desired_as.Stop();
    }

}
