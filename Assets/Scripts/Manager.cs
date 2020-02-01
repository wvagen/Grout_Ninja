using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject hole;
    public Transform xLimit, yLimit;

    List<GameObject> holesCreated = new List<GameObject>();
    const float minimumDistanceBetweenHoles = 1f;
    void Start()
    {
        InvokeRepeating("SpawnAHole", 0, 2);
    }

    void Update()
    {
        
    }



    void SpawnAHole()
    {
        Vector2 randomSpot = Vector2.zero ;
        do {
            randomSpot = new Vector2(Random.Range(-xLimit.position.x,xLimit.position.x),Random.Range(-yLimit.position.x,yLimit.position.x));
        }while (isPositionNearAnotherHole(randomSpot));

        GameObject tempHole = Instantiate(hole, randomSpot, Quaternion.identity);
        holesCreated.Add(tempHole);
    }
    bool isPositionNearAnotherHole(Vector2 randomSpot)
    {
        foreach( GameObject h in holesCreated){
            if (Vector2.Distance(randomSpot,h.transform.position) < minimumDistanceBetweenHoles) return true;
        }
        return false;
    }

}
