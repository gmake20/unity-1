using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public float speed;
    float hAxis;
    float vAxis;
    Vector3 moveVec;

    Animator anim;
    Rigidbody rigid;

    bool wDown;
    bool jDown;
    bool isJump;

    public int jumpPower = 15;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();

        GameAwake();
    }

    void Update()
    {
        GetInput();
        Move();
        Turn();
        Jump();
    }


    void GetInput() {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
    }

    void Move() {
        moveVec = new Vector3(hAxis,0,vAxis).normalized;
        
        transform.position += moveVec * speed * (wDown?0.3f:1f) * Time.deltaTime;

        anim.SetBool("isRun",moveVec != Vector3.zero);
        anim.SetBool("isWalk",wDown);
    }

    void Turn() {
        transform.LookAt(transform.position + moveVec);
    }

    void Jump() {
        if(jDown && !isJump) {
            rigid.AddForce(Vector3.up * jumpPower,ForceMode.Impulse);
            isJump = true;

            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
        }
    }

    void OnCollisionEnter(Collision coll) {
        if(coll.gameObject.tag == "Floor") {
            anim.SetBool("isJump",false);
            isJump = false;
        }

        GameEventCollisionEnter(coll);
    }    


    ///////////////////////////////////////////////////////////////////////////
    public int itemCount;
    SoundManager soundMgr;
    public GameManager gameMgr;

    void GameAwake() {
        soundMgr = GameObject.Find("SoundManager").GetComponent<SoundManager>();

    }

    void GameEventCollisionEnter(Collision coll) {
        if(coll.gameObject.tag == "Floor") 
            isJump = false;
    }    


    void OnTriggerEnter(Collider other) {
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
