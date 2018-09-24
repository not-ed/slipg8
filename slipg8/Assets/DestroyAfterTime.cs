using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {

    [SerializeField]
    private float TIME;

	// Use this for initialization
	void Start () {
        Invoke("Destroy",TIME);
	}
	
	void Destroy () {
        Destroy(gameObject);
	}
}
