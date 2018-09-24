using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NailLauncher : MonoBehaviour {

    [SerializeField]
    private GameObject PROJECTILE;
    [SerializeField]
    private float COOLDOWN_TIME;
    private bool inside_object = false;
    private SafetyNetHazard COMP_SNH;

    void InitializeComponents()
    {
        COMP_SNH = gameObject.GetComponent<SafetyNetHazard>();
    }

	// Use this for initialization
	void Start () {
        InitializeComponents();
        StartCoroutine("FireProjectile");
	}

    IEnumerator FireProjectile()
    {
        while (true)
        {
            if (COMP_SNH.is_hidden == false)
            {
                Instantiate(PROJECTILE, gameObject.transform.position + -transform.up / 2f, transform.rotation);
            }
            yield return new WaitForSeconds(COOLDOWN_TIME);
        }
    }
}
