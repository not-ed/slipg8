using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafetyNetHazard : MonoBehaviour {

    private enum DIRECTION {
        UP,LEFT,RIGHT,DOWN
    }

    [SerializeField]
    private DIRECTION DESIRED_LOCAL_DIRECTION = DIRECTION.DOWN;

    private Player PLAYER;

    private Vector3 EXTENDED_POS, HIDDEN_POS;

    [SerializeField]
    private float OFFSET_AMOUNT;
    [SerializeField]
    private bool DISABLE_COLLIDERS;
    private BoxCollider2D COLLIDER;
    [SerializeField]
    private int ROUND_TO_SHOW;

    [HideInInspector]
    public bool is_hidden = false;

    void InitializeComponents()
    {
        PLAYER = FindObjectOfType<Player>();
        EXTENDED_POS = transform.position;
        switch (DESIRED_LOCAL_DIRECTION)
        {
            case DIRECTION.UP:
                HIDDEN_POS = transform.position + (transform.up * OFFSET_AMOUNT);
                break;
            case DIRECTION.LEFT:
                HIDDEN_POS = transform.position - (transform.right * OFFSET_AMOUNT);
                break;
            case DIRECTION.RIGHT:
                HIDDEN_POS = transform.position + (transform.right * OFFSET_AMOUNT);
                break;
            case DIRECTION.DOWN:
                HIDDEN_POS = transform.position - (transform.up * OFFSET_AMOUNT);
                break;
        }
        COLLIDER = gameObject.GetComponent<BoxCollider2D>();
    }

	// Use this for initialization
	void Start () {
        InitializeComponents();
	}
	
	// Update is called once per frame
	void Update () {
		
        if (PLAYER.current_round < ROUND_TO_SHOW)
        {
            is_hidden = true;
            transform.position = Vector3.MoveTowards(transform.position,HIDDEN_POS, 1f * Time.deltaTime);
            if(DISABLE_COLLIDERS == true)
            {
                COLLIDER.enabled = false;
            }
            else
            {
                COLLIDER.enabled = true;
            }

        }
        else
        {
            is_hidden = false;
            transform.position = Vector3.MoveTowards(transform.position, EXTENDED_POS, 1f * Time.deltaTime);
            COLLIDER.enabled = true;
        }

	}
}
