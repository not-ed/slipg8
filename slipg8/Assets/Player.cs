using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //###################################### VARIABLES
    #region
    private Rigidbody2D COMP_RB2D;

    public float MOVEMENT_SPEED;
    public float INITIAL_JUMP_FORCE;
    public float HELD_JUMP_FORCE;
    [SerializeField]
    private float KILL_LAUNCH_FORCE;

    [SerializeField]
    private Vector2 SLIPGATE_EXIT_POSITION;
    [SerializeField]
    private GameObject SLIPGATE_EFFECT;

    private float force_sideways_movement;

    private bool can_jump = true;

    private Timer TIMER;

    private Modifiers MODIFIER_LIST;

    private Animator COMP_ANIMATOR;

    private SpriteRenderer COMP_RENDERER;

    private BoxCollider2D COMP_BC2D;

    private ScoreCounter COMP_SCORECOUNTER;

    private SoundManager SOUND;

    private MainMenu MAIN_MENU;

    private bool slipgate_exit_cantmove = false; //stops movement until keys are released after leaving a slipgate, stops the player just running off into the spikes

    [SerializeField]
    private GameObject GAME_OVER_SCREEN;

    [HideInInspector]
    public int current_round = 0;

    private bool is_dead = false;

    [SerializeField]
    private GameObject GO_SIGN;

    [SerializeField]
    private GameObject LAG_CAMERA_OVERLAY;

    private Color TRANSPARENCY_NORMAL = new Color(1,1,1,1), TRANSPARENCY_INVISIBLE = new Color(1, 1, 1, 0.0392156862745098f * 2f);

    private int modifiers_count_multiplier = 0;
    #endregion
    //###################################### VARIABLES



    void InitializeComponents()
    {
        COMP_RB2D = gameObject.GetComponent<Rigidbody2D>();
        TIMER = FindObjectOfType<Timer>();
        MODIFIER_LIST = FindObjectOfType<Modifiers>();
        COMP_ANIMATOR = gameObject.GetComponent<Animator>();
        COMP_RENDERER = gameObject.GetComponent<SpriteRenderer>();
        COMP_BC2D = gameObject.GetComponent<BoxCollider2D>();
        COMP_SCORECOUNTER = FindObjectOfType<ScoreCounter>();
        SOUND = FindObjectOfType<SoundManager>();
        MAIN_MENU = FindObjectOfType<MainMenu>();
    }
	// Use this for initialization
	void Start () {
        InitializeComponents();
	}

    void Update()
    {
        if (is_dead == false)
        {
            COMP_BC2D.enabled = true;
            COMP_RENDERER.sortingOrder = 0;
        }
        else
        {
            COMP_BC2D.enabled = false;
            COMP_RENDERER.sortingOrder = 5;
        }

        if (is_dead == true)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -6);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }



        if (TIMER.remaining_time == 0.001f && is_dead == false) //WHEN TIME RUNS OUT
        {
            Kill();
        }

        if (!MODIFIER_LIST.HasModifier(Modifiers.MODIFIER.DISABLE_JUMP))
        {
            if (Input.GetKeyDown("z") && can_jump == true && is_dead == false)
            {
                COMP_RB2D.AddForce((transform.up * INITIAL_JUMP_FORCE));
                SOUND.PlaySound(SOUND.AS_PLAYER_JUMP);
            }
        }

        if (is_dead == false)
        { //ANIMATION CHANGES BASED ON MOVEMENT AND FALLING STATE
            if ((Input.GetKey("left") && (!Input.GetKey("right")) || (Input.GetKey("right")) && (!Input.GetKey("left"))) && COMP_RB2D.velocity.y == 0 && slipgate_exit_cantmove == false)
            {

                COMP_ANIMATOR.Play("Move");
                if (SOUND.AS_PLAYER_RUN.isPlaying == false)
                {
                    SOUND.PlaySound(SOUND.AS_PLAYER_RUN);
                }
            }
            else if (COMP_RB2D.velocity.y == 0)
            {
                COMP_ANIMATOR.Play("Idle");
            }

            if (COMP_RB2D.velocity.y != 0)
            {
                if (COMP_RB2D.velocity.y > 0) //meaning player is rising up
                {
                    COMP_ANIMATOR.Play("JumpUp");
                }
                else if (COMP_RB2D.velocity.y < 0) //meaning player is falling
                {
                    COMP_ANIMATOR.Play("JumpDown");
                }
                SOUND.StopPlayingSound(SOUND.AS_PLAYER_RUN);
            }
        }
        else
        {
            COMP_ANIMATOR.Play("Dead");
        }

    }

    void FixedUpdate()
    {

        //force_sideways_movement = 0;

        if (is_dead == false)
        {
            if (MODIFIER_LIST.HasModifier(Modifiers.MODIFIER.REVERSED_CONTROLS))
            {
                if (Input.GetKey("right") && slipgate_exit_cantmove == false)
                {
                    force_sideways_movement = -1;
                    COMP_RENDERER.flipX = true;
                    
                }

                if (Input.GetKey("left") && slipgate_exit_cantmove == false)
                {
                     force_sideways_movement = 1;
                    COMP_RENDERER.flipX = false;
                }

            }
            else
            {
                if (Input.GetKey("left") && slipgate_exit_cantmove == false)
                {
                    force_sideways_movement = -1;
                    //force_sideways_movement = Mathf.Lerp(force_sideways_movement, -1, 12f * Time.deltaTime);
                    COMP_RENDERER.flipX = true;
                }

                if (Input.GetKey("right") && slipgate_exit_cantmove == false)
                {
                    force_sideways_movement = 1;
                   // force_sideways_movement = Mathf.Lerp(force_sideways_movement, 1, 12f * Time.deltaTime);
                    COMP_RENDERER.flipX = false;
                }

            }

            if (!Input.GetKey("left") && !Input.GetKey("right") )
            {
                slipgate_exit_cantmove = false;
                if (MODIFIER_LIST.HasModifier(Modifiers.MODIFIER.FLOAT_STOP))
                {
                    force_sideways_movement = Mathf.Lerp(force_sideways_movement, 0, 4f * Time.deltaTime);
                }
                else
                {
                    force_sideways_movement = 0;
                }
            }

            if ((Input.GetKey("left") || Input.GetKey("right")) && MODIFIER_LIST.HasModifier(Modifiers.MODIFIER.INVISIBLE_MOVE) && slipgate_exit_cantmove == false) 
            {
                //COMP_RENDERER.color = TRANSPARENCY_INVISIBLE;
                COMP_RENDERER.color = Color.Lerp(COMP_RENDERER.color, TRANSPARENCY_INVISIBLE, 12f * Time.deltaTime);
            }
            else
            {
               // COMP_RENDERER.color = TRANSPARENCY_NORMAL;
                COMP_RENDERER.color = Color.Lerp(COMP_RENDERER.color, TRANSPARENCY_NORMAL, 16f * Time.deltaTime);
            }

        }

        if (!MODIFIER_LIST.HasModifier(Modifiers.MODIFIER.DISABLE_JUMP))
        {
            if (Input.GetKey("z") && COMP_RB2D.velocity.y > 0 && is_dead == false) //additional height for when the jump key is held down
            {
                COMP_RB2D.AddForce((transform.up * HELD_JUMP_FORCE));
            }
        }

        if (Physics2D.Raycast(transform.position, -transform.up, 1.03125f) || Physics2D.Raycast(transform.position + new Vector3(0.19f, 0, 0), -transform.up, 1.03125f) || Physics2D.Raycast(transform.position + new Vector3(-0.19f, 0, 0), -transform.up, 1.03125f))
        {
            can_jump = true;
        }
        else
        {
            can_jump = false;
        }


        if ((Physics2D.Raycast(transform.position, -transform.right, 0.41125f) || Physics2D.Raycast(transform.position, transform.right, 0.41125f)) && (!Input.GetKey("left") && (!Input.GetKey("right"))) ) //stopping completely when hitting a wall
        {
            force_sideways_movement = 0;
        }


        COMP_RB2D.velocity = new Vector2(force_sideways_movement * MOVEMENT_SPEED * Time.deltaTime, COMP_RB2D.velocity.y);
        
        
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (is_dead == false)
        {
            if (collision.gameObject.tag == "Slipgate")
            {
                Instantiate(SLIPGATE_EFFECT, transform.position, Quaternion.identity);
                slipgate_exit_cantmove = true;
                SOUND.PlaySound(SOUND.AS_SLIPGATE);
                transform.position = SLIPGATE_EXIT_POSITION;
                force_sideways_movement = 0;

                //adding score
                if (current_round != 0)
                {
                   // COMP_SCORECOUNTER.AddScore(((1000 * ((current_round + 1) * 10)) * (Mathf.RoundToInt(TIMER.remaining_time)) * (current_round + 1)));
                    COMP_SCORECOUNTER.AddScore((1000 * current_round) + (2500 * modifiers_count_multiplier) + (100 * (int)TIMER.remaining_time));
                }
                else
                {
                    MAIN_MENU.PlayGameMusic(MAIN_MENU.MUSIC_INGAME);
                    GO_SIGN.SetActive(false);
                }
                current_round += 1;
                if (current_round > 0 && current_round <= 5)
                {
                    MODIFIER_LIST.ReshuffleModifiers(1);
                    modifiers_count_multiplier = 1;
                }
                else if (current_round > 5 && current_round <= 10)
                {
                    MODIFIER_LIST.ReshuffleModifiers(2);
                    modifiers_count_multiplier = 2;
                }
                else
                {
                    MODIFIER_LIST.ReshuffleModifiers(3);
                    modifiers_count_multiplier = 3;
                }

                //MODIFIER_LIST.ReshuffleModifiers(3);
                TIMER.StartTimer();
                Instantiate(SLIPGATE_EFFECT, transform.position, Quaternion.identity);

                if (MODIFIER_LIST.HasModifier(Modifiers.MODIFIER.SCREEN_LAG))
                {
                    LAG_CAMERA_OVERLAY.SetActive(true);
                }
                else
                {
                    LAG_CAMERA_OVERLAY.SetActive(false);
                }

            }
            if (collision.gameObject.tag == "Hazard")
            {
                Kill();
                TIMER.PauseTimer();
            }
            if (collision.gameObject.tag == "JumpPad")
            {
                COMP_RB2D.velocity = Vector3.zero;
                SOUND.PlaySound(SOUND.AS_JUMPPAD);
                COMP_RB2D.AddForce(transform.up * 625);
            }
        }
    }

    public void Kill()
    {
        LAG_CAMERA_OVERLAY.SetActive(false);
        force_sideways_movement = 0;
        COMP_RENDERER.color = TRANSPARENCY_NORMAL;
        MAIN_MENU.StopMusic();
        SOUND.PlaySound(SOUND.AS_PLAYER_DEATH);
        is_dead = true;
        COMP_RB2D.velocity = Vector3.zero;
        COMP_RB2D.AddForce(transform.up * KILL_LAUNCH_FORCE);
        Invoke("DisplayGameOverScreen", 1.5f);
        GAME_OVER_SCREEN.GetComponent<GameOverScreen>().UpdateScore(COMP_SCORECOUNTER.current_score);
    }

    public void Respawn()
    {
        current_round = 0;
        modifiers_count_multiplier = 0;
        is_dead = false;
        SOUND.PlaySound(SOUND.AS_SLIPGATE);
        MAIN_MENU.PlayGameMusic(MAIN_MENU.MUSIC_PREGAME);
        transform.position = SLIPGATE_EXIT_POSITION;
        COMP_RB2D.velocity = Vector2.zero;
        Instantiate(SLIPGATE_EFFECT, transform.position, Quaternion.identity);
        GO_SIGN.SetActive(true);
    }

    void DisplayGameOverScreen()
    {
        GAME_OVER_SCREEN.SetActive(true);
    }

}
