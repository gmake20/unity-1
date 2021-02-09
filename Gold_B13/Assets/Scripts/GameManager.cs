using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stageIndex;
    public int stagePoint; 
    public int health; 
    public PlayMove player;


    public void NextStage() {
        stageIndex++;
        totalPoint += stagePoint;
        stagePoint = 0;
    }

    public void HealthDown() {
        if(health > 0)
            health--;
        else {

            // Player Die effect
            player.OnDie();

            // result ui
            Debug.Log("Player Die!");

            // retry button ui
        }
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if(coll.gameObject.tag == "Player") {
            if(health>1) {
                // Player Reposition
                coll.attachedRigidbody.velocity = Vector2.zero;
                coll.transform.position = new Vector3(-7,1,0);
            }

            HealthDown();

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
