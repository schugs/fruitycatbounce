using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour {

    const float FRUIT_SPAWN_HEIGHT = 10f;
    const float FRUIT_SPAWN_DELAY = 5f;
    const float FRUIT_SPAWN_RANGE = 5f;

    public GameObject fruitPrefab;

    private float timer = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (timer > FRUIT_SPAWN_DELAY)
        {
            SpawnFruit();
            timer = 0f;
        }
	}

    private void SpawnFruit(){
        GameObject fruit = (GameObject)Instantiate(fruitPrefab);

        float xPos = Random.Range(-FRUIT_SPAWN_RANGE, FRUIT_SPAWN_RANGE);
        fruit.transform.position = new Vector3(xPos, FRUIT_SPAWN_HEIGHT, 0f);

        
    }
}
