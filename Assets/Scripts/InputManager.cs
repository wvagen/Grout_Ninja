using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public GameObject smallGrout;
    public Color[] startTrailColors,endTrailColors;

    GameObject lastGroutCreated;
    Vector2 mousePos;
    bool isMouseDown = false;
    //bool isSmallGroutSpawned = true;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.GetKey(KeyCode.LeftControl))
                OnDown(false);
            else
            OnDown(true);

        }
        if (Input.GetMouseButtonUp(0)) OnUp();
        if (isMouseDown) followMyMouse();
    }

    void OnDown(bool isSmallGrout)
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastGroutCreated = Instantiate(smallGrout, mousePos, Quaternion.identity);
        if (!isSmallGrout)
        {
            lastGroutCreated.GetComponent<TrailRenderer>().startWidth *= 1.5f;
            lastGroutCreated.GetComponent<TrailRenderer>().endWidth *= 1.5f;
        }
        int randColor = Random.Range(0, startTrailColors.Length);
        lastGroutCreated.GetComponent<TrailRenderer>().startColor = startTrailColors[randColor];
        lastGroutCreated.GetComponent<TrailRenderer>().endColor = endTrailColors[randColor];
        Manager.orderInLayer++;
        lastGroutCreated.GetComponent<TrailRenderer>().sortingOrder = Manager.orderInLayer;
        isMouseDown = true;
    }

    void OnUp()
    {
        lastGroutCreated = null;
        isMouseDown = false;
    }

    void followMyMouse()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastGroutCreated.transform.position = mousePos;
    }

}
