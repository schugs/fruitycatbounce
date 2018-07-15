using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public static float MAX_FRUIT_STACK_HEIGHT = 5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void GameOver ()
    {
        Debug.Log("FFFAWWWK game over");
    }

    public static void ReparentFruits()
    {
        Fruit[] allFruits = (Fruit[])FindObjectsOfType(typeof(Fruit));

        // for fruits that are stuck and have no parent, try to find them a parent
        foreach(Fruit fruit in allFruits)
        {
            if (fruit.IsStuck() && fruit.transform.parent == null)
            {
                Collider[] touchingColliders = Physics.OverlapSphere(fruit.transform.position, 1.1f);
                GameObject newParent = null;

                // try to find a suitable parent
                foreach(Collider touchingCollider in touchingColliders)
                {
                    // only check for a new parent when parent has not yet been found
                    if (!newParent)
                    {
                        if (IsSuitableParent(touchingCollider)) {
                            newParent = touchingCollider.gameObject;
                        }
                    }
                }
                
                if (newParent)
                {
                    // this is your new daddy now
                    fruit.Stick(newParent);
                } else
                {
                    //no parent? perish
                    fruit.DestroySelf();
                }
            }
        }
    }

    private static bool IsSuitableParent(Collider potentialParent)
    {
        Fruit fruit = potentialParent.GetComponent<Fruit>();
        PlayerController player = potentialParent.GetComponent<PlayerController>();
        if (player)
        {
            return true;
        } else if (fruit)
        {
            // orphans can't have children
            // them's the rules
            if (fruit.IsStuck() && fruit.transform.parent != null)
            {
                return true;
            }
        }
        return false;
    }
}
