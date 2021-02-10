using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stageIndex;
    public int stagePoint; 
    public int health; 
    public PlayMove player;
    public GameObject[] Stages;

    public Image[] UIHelath;
    public Text UIPoint;
    public Text UIStage;

    public GameObject UIRestartBtn;

    public void NextStage() {
        // Change Stage
        if(stageIndex < Stages.Length-1) {
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            PlayerReposition();

            UIStage.text = "STAGE " + (stageIndex+1);
        } 
        else {  // Game Clear
            // Player Control Lock
            Time.timeScale = 0;

            // Result UI 
            Debug.Log("Game Clear");

            // Restart Button UI
            Text btnText = UIRestartBtn.GetComponentInChildren<Text>();
            btnText.text = "Clear!!";
            ViewBtn();
        }

        totalPoint += stagePoint;
        stagePoint = 0;
    }

    public void HealthDown() {
        if(health > 0) {
            health--;
            UIHelath[health].color = new Color(1,0,0,0.4f);
        }
        else {
            UIHelath[0].color = new Color(1,0,0,0.4f);

            // Player Die effect
            player.OnDie();

            // result ui
            Debug.Log("Player Die!");

            // retry button ui
            ViewBtn();
        }
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if(coll.gameObject.tag == "Player") {
            if(health>1) {
                // Player Reposition
                /*
                coll.attachedRigidbody.velocity = Vector2.zero;
                coll.transform.position = new Vector3(-13,5,0);
                */
                PlayerReposition();
            }

            HealthDown();

        }
    }

    void PlayerReposition() {
        player.transform.position = new Vector3(-13,5,0);
        player.VelocityZero();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UIPoint.text = (totalPoint + stagePoint).ToString();

    }

    void ViewBtn() {
        UIRestartBtn.SetActive(true);
    }

    public void Restart() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
