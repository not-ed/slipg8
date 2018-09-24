using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLagger : MonoBehaviour {

    private Camera SELF_CAMERA;
    [SerializeField]
    private RenderTexture OUTPUT_RENDER_TEX;
    [SerializeField]
    private float CheckRefreshRate;
    [SerializeField]
    private int MAX_LAG_COUNT_LIMIT, LAG_SEED_RANGE;
    private int consecutive_lag_count = 0;

	// Used to initialize Variables Relying on Components
	void InitializeComponents(){
        SELF_CAMERA = gameObject.GetComponent<Camera>();
	}

	// Use this for initialization
	void Start () {
        InitializeComponents();
        StartCoroutine("UpdateSeed");
	}
	
	// Update is called once per frame
	IEnumerator UpdateSeed () {

        while (true)
        {


            int lag_seed = Random.Range(0, LAG_SEED_RANGE);

            if (lag_seed != 0)
            {
                SELF_CAMERA.targetTexture = OUTPUT_RENDER_TEX;
                consecutive_lag_count += 1;
            }
            else
            {
                SELF_CAMERA.targetTexture = null;
                consecutive_lag_count = 0;
            }

            if (consecutive_lag_count > MAX_LAG_COUNT_LIMIT)
            {
                SELF_CAMERA.targetTexture = null;
                consecutive_lag_count = 0;
            }

            yield return new WaitForSeconds(CheckRefreshRate);
        }

    }
}
