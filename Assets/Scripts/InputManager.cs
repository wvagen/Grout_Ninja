using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public GameObject smallGrout;

    GameObject lastGroutCreated;
    Vector2 mousePos;
    bool isMouseDown = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) OnDown();
        if (Input.GetMouseButtonUp(0)) OnUp();
        if (isMouseDown) followMyMouse();
    }

    void OnDown()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastGroutCreated = Instantiate(smallGrout, mousePos, Quaternion.identity);
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
