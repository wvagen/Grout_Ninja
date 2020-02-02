using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public GameObject hole;
    public Transform xLimit, yLimit;

    public Text comboTxt, motvationalWordTxt,scoreTxt;
    public Text finalScoreTxt, finalBestScoreTxt;
    public string[] motivationWordsSet;
    public Color[] comboColors;
    public Animator canvasAnim;

    Color initComboAndMotivationColor;
    public float crackPeriodToChangeStatus = 2f;

    public List<GameObject> holesCreated = new List<GameObject>();
    public List<GameObject> waterFalls = new List<GameObject>();

    public static int orderInLayer = 0;

    int score = 0;
    int scoreAmountToBeAdded = 5;
    short comboAmount = 1;
    short whenToStartCoutingCombo = 0;
    bool isGameOver = false;
    const float minimumDistanceBetweenHoles = 1f;
    void Start()
    {
        InvokeRepeating("SpawnAHole", 0, crackPeriodToChangeStatus);
        initComboAndMotivationColor = scoreTxt.color;
        finalScoreTxt.text = "Score: ";
        finalBestScoreTxt.text = "Best Score: ";
        PlayerPrefs.DeleteAll();
    }

    



    void SpawnAHole()
    {
        if (isGameOver){
             foreach (GameObject g in holesCreated) Destroy(g);
             foreach (GameObject item in waterFalls)
             {
                 if (item != null) Destroy(item);
             }
            return;
        }
       
        Vector2 randomSpot = Vector2.zero ;
        do {
            randomSpot = new Vector2(Random.Range(-xLimit.position.x,xLimit.position.x),Random.Range(-yLimit.position.y,yLimit.position.y));
        }while (isPositionNearAnotherHole(randomSpot));

        Vector3 randomRotation = Vector3.zero;
        randomRotation.z = Random.Range(0, 360);
        GameObject tempHole = Instantiate(hole, randomSpot,Quaternion.Euler (randomRotation));
        tempHole.GetComponent<Crack>().man = this;
        orderInLayer++;
        holesCreated.Add(tempHole);
    }
    bool isPositionNearAnotherHole(Vector2 randomSpot)
    {
        foreach( GameObject h in holesCreated){
            if (Vector2.Distance(randomSpot,h.transform.position) < minimumDistanceBetweenHoles) return true;
        }
        return false;
    }

    public void GameOver()
    {
        canvasAnim.Play("GameOver",0,0);
        isGameOver = true;
        if (score > PlayerPrefs.GetInt("bestScore", 0))
        {

            finalBestScoreTxt.text += score.ToString();
            PlayerPrefs.SetInt("bestScore", score);
        }else{
            finalBestScoreTxt.text += PlayerPrefs.GetInt("bestScore", 0).ToString();
        }
        finalScoreTxt.text += score.ToString();
        
    }


    public void incrementScore()
    {
        score += scoreAmountToBeAdded * comboAmount;
        scoreTxt.text = score.ToString();
        canvasAnim.Play("ScoreBumb", 0, 0);
        if (crackPeriodToChangeStatus > 0.5f)
        crackPeriodToChangeStatus -= 0.02f;
        whenToStartCoutingCombo++;
    }

    public void boostCombot()
    {
        if (whenToStartCoutingCombo < 100) return;
        if (comboAmount < (comboColors.Length - 1))
        comboAmount++;
        comboTxt.text = "Combo x" + comboAmount.ToString();
        comboTxt.color = comboColors[comboAmount - 2];
        scoreTxt.color = comboColors[comboAmount - 2];
        motvationalWordTxt.color = comboColors[comboAmount - 2];
        motvationalWordTxt.text = motivationWordsSet[Random.Range(0, motivationWordsSet.Length)];
        canvasAnim.Play("MotivationalWord", 1, 0);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void resetCombo()
    {
        whenToStartCoutingCombo = 0;
        comboAmount = 1;
        comboTxt.color = initComboAndMotivationColor;
        motvationalWordTxt.color = initComboAndMotivationColor;
        scoreTxt.color = initComboAndMotivationColor;
    }
    

}
