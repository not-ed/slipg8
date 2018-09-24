using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraLagOverlay : MonoBehaviour {

    private Modifiers MODIFIER_LIST;
    private Image COMP_IMAGE;

	// Used to initialize Variables Relying on Components
	void InitializeComponents(){
        MODIFIER_LIST = FindObjectOfType<Modifiers>();
        COMP_IMAGE = gameObject.GetComponent<Image>();
	}

	// Use this for initialization
	void Start () {
        InitializeComponents();
	}
	
	// Update is called once per frame
	void Update () {

        if (MODIFIER_LIST.HasModifier(Modifiers.MODIFIER.SCREEN_LAG))
        {
            COMP_IMAGE.enabled = true;
        }
        else
        {
            COMP_IMAGE.enabled = false;
        }
		
	}
}
