using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grout : MonoBehaviour
{

    TrailRenderer myTrail;

    void Start()
    {
        myTrail = GetComponent<TrailRenderer>();
        StartCoroutine(fadeMe());
    }

    IEnumerator fadeMe()
    {
        Color myCol = myTrail.material.GetColor("_TintColor");
        while (myCol.a > 0)
        {
            myCol.a -= Time.deltaTime * 0.1f;
            myTrail.material.SetColor( "_TintColor", myCol) ;
            yield return new WaitForEndOfFrame();
        }
        Destroy(this.gameObject);

    }

}
