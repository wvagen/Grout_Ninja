using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneManager : MonoBehaviour
{
    public Animator myAnim;
    bool isGameEntered = false;
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isGameEntered)
        {
            isGameEntered = true; 
            myAnim.Play("GameStartAnimation");
        }
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
