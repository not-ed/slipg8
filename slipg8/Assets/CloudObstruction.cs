using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudObstruction : MonoBehaviour {

    private Modifiers MODIFIER_LIST;
    private Renderer SELF_RENDERER;

	// Use this for initialization
	void Start () {
        SELF_RENDERER = gameObject.GetComponent<Renderer>();
        MODIFIER_LIST = FindObjectOfType<Modifiers>();
	}
	
	// Update is called once per frame
	void Update () {
		
        if (MODIFIER_LIST.HasModifier(Modifiers.MODIFIER.CLOUDED_VIEW))
        {
            SELF_RENDERER.enabled = true;
        }
        else
        {
            SELF_RENDERER.enabled = false;
        }

	}
}
