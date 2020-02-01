using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroutLogic : MonoBehaviour
{

    Transform holePos = null;

    float distance;
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name);
        holePos = col.transform;
    }

    void Update()
    {
        if (holePos != null)
        {
            QuickMaths();
        }
    }

    void QuickMaths()
    {
        distance = Vector2.Distance(transform.position, holePos.position);
        if (distance < (holePos.transform.localScale.x / 2) * Mathf.Sqrt(2))
        {
            Debug.Log("Covered");
        }
    }

}
