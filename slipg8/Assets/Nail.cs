using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nail : MonoBehaviour {

    [SerializeField]
    private float SPEED;
    private Rigidbody2D COMP_RB2D;
    private Vector3 TIP_LOCATION = new Vector3(0,0.234375f,0);
    private Timer TIMER;

	// Used to initialize Variables Relying on Components
	void InitializeComponents(){
        COMP_RB2D = gameObject.GetComponent<Rigidbody2D>();
        TIMER = FindObjectOfType<Timer>();
	}

	// Use this for initialization
	void Start () {
        InitializeComponents();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        COMP_RB2D.MovePosition(transform.position + transform.up * (SPEED * Time.deltaTime));

    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            collision.gameObject.GetComponent<Player>().Kill();
            TIMER.PauseTimer();
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
