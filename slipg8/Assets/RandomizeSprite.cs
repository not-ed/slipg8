using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeSprite : MonoBehaviour {

    private SpriteRenderer COMP_RENDERER;
    [SerializeField]
    private Sprite[] SPRITES;

	// Used to initialize Variables Relying on Components
	void InitializeComponents(){
        COMP_RENDERER = gameObject.GetComponent<SpriteRenderer>();
	}

	// Use this for initialization
	void Start () {
        InitializeComponents();

        COMP_RENDERER.sprite = SPRITES[Random.Range(0, SPRITES.Length)];

	}

}
