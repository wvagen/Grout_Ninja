using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject hole;
    public Transform xLimit, yLimit;

    public float crackPeriodToChangeStatus = 1f;

    public List<GameObject> holesCreated = new List<GameObject>();

    public static int orderInLayer = 0;
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
            randomSpot = new Vector2(Random.Range(-xLimit.position.x,xLimit.position.x),Random.Range(-yLimit.position.y,yLimit.position.y));
        }while (isPositionNearAnotherHole(randomSpot));

        Vector3 randomRotation = Vector3.zero;
        randomRotation.z = Random.Range(0, 360);
        GameObject tempHole = Instantiate(hole, randomSpot,Quaternion.Euler (randomRotation));
        tempHole.GetComponent<Crack>().man = this;
        orderInLayer++;
      /* GameObject waterFallTemp = Instantiate(waterFall, randomSpot, Quaternion.identity);
       waterFallTemp.transform.position = new Vector2(tempHole.transform.position.x + 0.22f, tempHole.transform.position.y + 0.57f);
       waterFallTemp.GetComponentInChildren <SpriteRenderer>().sortingOrder = orderInLayer;*/
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
