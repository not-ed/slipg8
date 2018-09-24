using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideHazard : MonoBehaviour {

    private Modifiers MODIFIER_LIST;
    private SpriteRenderer SELF_RENDERER;
    private Color TRANSPARENCY_NORMAL = new Color(1, 1, 1, 1), TRANSPARENCY_INVISIBLE = new Color(1, 1, 1, 0.0392156862745098f * 2f);
    private Vector3 LAST_POS;

    // Use this for initialization
    void Start () {

        MODIFIER_LIST = FindObjectOfType<Modifiers>();
        SELF_RENDERER = GetComponent<SpriteRenderer>();
        LAST_POS = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        if (LAST_POS == gameObject.transform.position) //meaning that the hazard is currently moving out the wall
        {
            if (MODIFIER_LIST.HasModifier(Modifiers.MODIFIER.HIDDEN_HAZARDS))
            {
                SELF_RENDERER.color = Color.Lerp(SELF_RENDERER.color, TRANSPARENCY_INVISIBLE, 7f * Time.deltaTime);
            }
            else
            {
                SELF_RENDERER.color = Color.Lerp(SELF_RENDERER.color, TRANSPARENCY_NORMAL, 20f * Time.deltaTime);
            }
        }
        else
        {
            SELF_RENDERER.color = TRANSPARENCY_NORMAL;
        }

        LAST_POS = gameObject.transform.position;

	}
}
