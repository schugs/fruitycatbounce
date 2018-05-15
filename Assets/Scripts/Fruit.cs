using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour {

    private bool stuck = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool isStuck()
    {
        return stuck;
    }

    public void stick()
    {
        stuck = true;
    }
}
