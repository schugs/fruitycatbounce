using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    const float PLAYER_SPEED_MULTIPLIER = 10f;

    private Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = gameObject.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        float xMovement = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(xMovement, 0f, 0f) * PLAYER_SPEED_MULTIPLIER * Time.deltaTime;

        Vector3 newPos = this.transform.position + movement;

        rigidBody.MovePosition(newPos);
	}
}
