using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundStatusIcon : MonoBehaviour {

    private Image COMP_IMAGE;
    [SerializeField]
    private Sprite ICO_SOUND_ON, ICO_SOUND_OFF, ICO_MUSIC_ON, ICO_MUSIC_OFF;

	// Used to initialize Variables Relying on Components
	void InitializeComponents(){
        COMP_IMAGE = gameObject.GetComponent<Image>();
	}

	// Use this for initialization
	void Start () {
        InitializeComponents();
	}
	
    public void DisplaySoundToggle(int new_sound_state)
    {
        if (new_sound_state == 1) //meaning it is now disabled and muted
        {
            COMP_IMAGE.sprite = ICO_SOUND_OFF;
        }
        else
        {
            COMP_IMAGE.sprite = ICO_SOUND_ON;
        }

        COMP_IMAGE.enabled = true;
        CancelInvoke("HideIcon");
        Invoke("HideIcon", 2f);
    }

    public void DisplayMusicToggle(int new_sound_state)
    {
        if (new_sound_state == 1) //meaning it is now disabled and muted
        {
            COMP_IMAGE.sprite = ICO_MUSIC_OFF;
        }
        else
        {
            COMP_IMAGE.sprite = ICO_MUSIC_ON;
        }

        COMP_IMAGE.enabled = true;
        CancelInvoke("HideIcon");
        Invoke("HideIcon", 2f);
    }

	void HideIcon()
    {
        COMP_IMAGE.enabled = false;
    }
}
