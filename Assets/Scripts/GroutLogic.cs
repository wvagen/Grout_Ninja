using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroutLogic : MonoBehaviour
{

    Transform holePos = null;

    float distance;
    TrailRenderer myTrail;
    Vector2 firstPos;
    bool weCanTalkNow = false;

    const float minDistanceToConsider = 0.1f;
    void Start()
    {
        myTrail = GetComponent<TrailRenderer>();
        myTrail.enabled = false;
        firstPos = transform.position;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "crack")
        holePos = col.transform;
    }

    void Update()
    {
        if (!weCanTalkNow && Vector2.Distance(firstPos,transform.position) > minDistanceToConsider)
        {
            weCanTalkNow = true;
            myTrail.enabled = true;
        }
        if (weCanTalkNow && holePos != null)
        {
            QuickMaths();
        }
    }

    void QuickMaths()
    {
       /* distance = Vector2.Distance(transform.position, holePos.position);
        if (distance < (holePos.transform.localScale.x / 2) * Mathf.Sqrt(2))
        {*/
            holePos.GetComponent<Crack>().fadeAnimation();
            holePos.GetComponent<Crack>().endLoop();
            Debug.Log("Covered");
       // }
    }

}
