using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public float TIME;
    [SerializeField]
    private Text[] TIMER_LABEL;
    [SerializeField]
    private Text TIMER_LABEL_BACKING;
    private Vector2 TIMER_LABEL_BACKING_STARTPOS;
    [HideInInspector]
    public float remaining_time;
    private bool is_active = false;

	// Use this for initialization
	void Start () {
        remaining_time = TIME;
        UpdateTimerLabels();
        TIMER_LABEL_BACKING_STARTPOS = TIMER_LABEL_BACKING.transform.position;
	}

    public void StartTimer()
    {
        remaining_time = TIME;
        is_active = true;
    }

    public void PauseTimer()
    {
        is_active = false;
        UpdateTimerLabels();
    }

    void StopTimer()
    {
        is_active = false;
        remaining_time = 0.001f;
        UpdateTimerLabels(); ;
    }

    public void UpdateTimerLabels()
    {
        foreach (var i in TIMER_LABEL)
        {
            i.text = remaining_time.ToString("0.00");
        }
    }

    // Update is called once per frame
    void Update() {

        if (is_active == true)
        {
            remaining_time -= Time.deltaTime;
            UpdateTimerLabels();
        }

        if (remaining_time < 0)
        {
            StopTimer();
        }

        
            TIMER_LABEL_BACKING.transform.position = TIMER_LABEL_BACKING_STARTPOS;
        if (is_active == true)
        {
            TIMER_LABEL_BACKING.transform.position += new Vector3(Random.Range(-16f, 16f), Random.Range(-16f, 16f), 0);
        }

    }
}
