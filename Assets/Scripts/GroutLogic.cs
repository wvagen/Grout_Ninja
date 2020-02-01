using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroutLogic : MonoBehaviour
{
    public bool isSmallGrout = true;

    Transform holePos = null;

    float distance;
    TrailRenderer myTrail;
    Vector2 firstPos;
    bool weCanTalkNow = false;
   

    const float minDistanceToConsider = 0.1f;


    void Start()
    {
        myTrail = GetComponent<TrailRenderer>();
        StartCoroutine(fadeMe());
        myTrail.enabled = false;
        firstPos = transform.position;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.gameObject.tag == "crack" && (isSmallGrout || !isSmallGrout)) || ( col.gameObject.tag == "bigCrack" && !isSmallGrout)) ;
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
    IEnumerator fadeMe()
    {
        Color myCol = myTrail.material.GetColor("_TintColor");
        while (myCol.a > 0)
        {
            myCol.a -= Time.deltaTime;
            myTrail.material.SetColor("_TintColor", myCol);
            yield return new WaitForEndOfFrame();
        }
        Destroy(this.gameObject);

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
