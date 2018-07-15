using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour {

    const float FRUIT_DIAMETER = 1f;
    const float FRUIT_TOUCH_RANGE = 0.1f;
    const int MATCH_AMOUNT = 3;

    public static List<Color> potentialColors = new List<Color> { Color.red, Color.blue, Color.yellow };

    private bool stuck = false;
    private Color fruitColor;
    private MeshRenderer renderer;

	// Use this for initialization
	void Start () {
        int numSelection = (int)Random.Range(0, potentialColors.Count);
        fruitColor = potentialColors[numSelection];

        setColor(potentialColors[numSelection]);
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
        Vector3 objectToSelf = (this.transform.position - other.transform.position).normalized;

        this.transform.position = other.transform.position + objectToSelf * FRUIT_DIAMETER;

        this.transform.parent = other.transform;
        this.GetComponent<Rigidbody>().isKinematic = true;
        this.gameObject.layer = other.layer;
        stuck = true;

        removeMatches();
    }

    public void removeMatches()
    {
        // get list of this object and all the matching fruits that are connected to it
        List<Fruit> matchList = new List<Fruit> { this };
        GetTouchingMatches(matchList, fruitColor);
        
        if (matchList.Count >= MATCH_AMOUNT)
        {
            // one pass to detatch all their children
            foreach(Fruit fruit in matchList)
            {
                fruit.transform.DetachChildren();
            }
            // another to destroy them all
            foreach(Fruit fruit in matchList)
            {
                fruit.DestroySelf();
            }
            GameController.ReparentFruits();
        }
    }

    public void GetTouchingMatches(List<Fruit> matchList, Color colorVal)
    {
        Collider[] touchingColliders = Physics.OverlapSphere(this.transform.position, (FRUIT_DIAMETER / 2) + FRUIT_TOUCH_RANGE);
        foreach (Collider other in touchingColliders)
        {
            // Check to see that it is an attached fruit colliding
            Fruit otherFruit = other.GetComponent<Fruit>();
            if (otherFruit && otherFruit.IsStuck() && !matchList.Contains(otherFruit) && otherFruit.fruitColor == colorVal)
            {
                matchList.Add(otherFruit);
                otherFruit.GetTouchingMatches(matchList, colorVal);
            }
        }
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

    void setColor(Color newColor)
    {
        fruitColor = newColor;

        // find material to change its color
        renderer = GetComponent<MeshRenderer>();
        renderer.material.color = fruitColor;
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
