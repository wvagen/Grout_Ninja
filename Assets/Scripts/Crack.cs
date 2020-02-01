using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crack : MonoBehaviour
{

    public GameObject waterFall;
    public GameObject explosionEffect;
    public Transform waterFallPosition;
    public Manager man;

    GameObject tempWaterFall;
    Animator myAnim;
    short animationStep = 0;

    bool isHoleCovered = false;

    void Start()
    {
        myAnim = GetComponent<Animator>();
        StartCoroutine(changeMyStatus());
    }

    IEnumerator changeMyStatus()
    {
        yield return new WaitForSeconds(man.crackPeriodToChangeStatus);
        animationStep++;
        if (!isHoleCovered) myAnim.SetInteger("crackStatus",animationStep);
        yield return new WaitForSeconds(man.crackPeriodToChangeStatus);
        animationStep++;
       if (!isHoleCovered) myAnim.SetInteger("crackStatus", animationStep);
        yield return new WaitForSeconds(man.crackPeriodToChangeStatus);
        CrackLevel3();

    }

    void CrackLevel3()
    {
     Destroy(Instantiate(explosionEffect, transform.position, Quaternion.identity),3);
     tempWaterFall.transform.localScale *= 2;
     transform.localScale *= 2;
    }

    public void spawnWaterFall() 
    {
      tempWaterFall =  Instantiate(waterFall, waterFallPosition.position, Quaternion.identity);
      Manager.orderInLayer++;
      tempWaterFall.GetComponentInChildren<SpriteRenderer>().sortingOrder = Manager.orderInLayer;

    }

    public void fadeAnimation()
    {
        StartCoroutine(fade());
    }

    IEnumerator fade()
    {

        Color realCol = this.GetComponent<SpriteRenderer>().color;
        while (realCol.a > 0)
        {
            realCol.a -= Time.deltaTime ;
            this.GetComponent<SpriteRenderer>().color = realCol;
            yield return new WaitForEndOfFrame();
        }
        man.holesCreated.Remove(this.gameObject);
        Destroy(this.gameObject);
        if (tempWaterFall != null)
            Destroy(tempWaterFall);
    }

    public void endLoop()
    {
        isHoleCovered = true;
        if (tempWaterFall != null) 
        tempWaterFall.GetComponentInChildren<Animator>().Play("endLoop");
    }

}
