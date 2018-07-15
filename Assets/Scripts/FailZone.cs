using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider other)
    {
        // Check to see that it is an attached fruit colliding
        Fruit otherFruit = other.GetComponent<Fruit>();
        if (otherFruit && otherFruit.IsStuck())
        {
            GameController.GameOver();
            return;
        }
    }
}
