using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlipping : MonoBehaviour {

    private Modifiers MODIFIER_LIST;
    [SerializeField]
    private Vector3 NONFLIPPED_POS, FLIPPED_POS, NONFLIPPED_ROT,FLIPPED_ROT;

	// Use this for initialization
	void Start () {

        MODIFIER_LIST = FindObjectOfType<Modifiers>();

	}
	
	// Update is called once per frame
	void Update () {
		
        if (MODIFIER_LIST.HasModifier(Modifiers.MODIFIER.FLIPPED_SCREEN))
        {
            transform.position = FLIPPED_POS;
            transform.rotation = Quaternion.Euler(FLIPPED_ROT);
        }
        else
        {
            transform.position = NONFLIPPED_POS;
            transform.rotation = Quaternion.Euler(NONFLIPPED_ROT);
        }

	}
}
