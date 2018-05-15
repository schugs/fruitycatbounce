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

    public bool IsStuck()
    {
        return stuck;
    }

    public void Stick(GameObject other)
    {
        this.transform.parent = other.transform;
        this.GetComponent<Rigidbody>().isKinematic = true;
        this.gameObject.layer = other.layer;
        stuck = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (this.IsStuck()){
            return;
        }

        GameObject obj = collision.gameObject;

        PlayerController player = obj.GetComponent<PlayerController>();
        if (player && !this.IsStuck()){
            this.Stick(collision.gameObject);
            return;
        }

        Fruit otherFruit = obj.GetComponent<Fruit>();
        if (otherFruit && otherFruit.IsStuck() && !this.IsStuck())
        {
            this.Stick(otherFruit.gameObject);
            return;
        }
    }
}
