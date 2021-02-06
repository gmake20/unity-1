using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerBall : MonoBehaviour
{
    
    Rigidbody rigid;
    bool isJump;

    public int itemCount;

    SoundManager soundMgr;
    public GameManager gameMgr;

    void Awake() {
        rigid = GetComponent<Rigidbody>();
        isJump = false;

        soundMgr = GameObject.Find("SoundManager").GetComponent<SoundManager>();

    }

    void FixedUpdate() {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        rigid.AddForce(new Vector3(h,0,v),ForceMode.Impulse);
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float jumpPower = 30.0f;

        if(!isJump && Input.GetButtonDown("Jump")) {
            isJump = true;
            rigid.AddForce(new Vector3(0,jumpPower,0),ForceMode.Impulse);
        }
                
    }

    void OnCollisionEnter(Collision coll) {
        if(coll.gameObject.tag == "Floor") 
            isJump = false;
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log(other.tag);
        if(other.tag == "Item") {
            // PlayerBall player = other.GetComponent<PlayerBall>();
            itemCount++;
            soundMgr.playItemSound();
            other.gameObject.SetActive(false);
            gameMgr.UpdateItemCount(itemCount);
        }

        if(other.tag == "finish") {
            if(itemCount == gameMgr.totalItemCount) {        
                Debug.Log("Next Level : " + (gameMgr.stage +1).ToString());
                // game clear
                SceneManager.LoadScene("Level"+(gameMgr.stage +1).ToString());
            }
            else {
                // restart 
                // SceneManager.LoadScene("Game"+gameMgr.stage.ToString());
            }
        }
    }    
}
