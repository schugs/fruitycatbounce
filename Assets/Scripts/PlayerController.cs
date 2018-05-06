using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    const float PLAYER_SPEED_MULTIPLIER = 10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float xMovement = Input.GetAxis("Horizontal");

        this.gameObject.transform.position += new Vector3(xMovement, 0f, 0f)
            * PLAYER_SPEED_MULTIPLIER * Time.deltaTime;
	}
}
