using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public GameManager manager;

    float h;
    float v;
    bool isHorizonMove; 
    Rigidbody2D rigid;

    float speed;

    Animator anim;

    Vector3 dirVec;

    GameObject scanObject; 

    ////////////////////
    // Mobile Key Value 
    ////////////////////
    public int up_value;
    public int down_value;
    public int left_value;
    public int right_value;
    public bool up_down;
    public bool down_down;
    public bool left_down;
    public bool right_down;
    public bool up_up;
    public bool down_up;
    public bool left_up;
    public bool right_up;
    ////////////////////


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        speed = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Move Key
        // PC
        h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal") + right_value + left_value;
        v = manager.isAction ? 0 : Input.GetAxisRaw("Vertical") + up_value + down_value;

        // Mobile
        // h = manager.isAction ? 0 : right_value + left_value;
        // v = manager.isAction ? 0 : up_value + down_value;


        // check button down&up
        // PC
        bool hDown = manager.isAction ? false : Input.GetButtonDown("Horizontal") || right_down || left_down;
        bool vDown = manager.isAction ? false : Input.GetButtonDown("Vertical") || up_down || down_down;
        bool hUp = manager.isAction ? false : Input.GetButtonUp("Horizontal") || right_up || left_up;
        bool vUp = manager.isAction ? false : Input.GetButtonUp("Vertical") || up_up || down_up;

        // Mobile
        // bool hDown = manager.isAction ? false : right_down || left_down;
        // bool vDown = manager.isAction ? false : up_down || down_down;
        // bool hUp = manager.isAction ? false : right_up || left_up;
        // bool vUp = manager.isAction ? false : up_up || down_up;


        // check Horizontal Move
        if(hDown)
            isHorizonMove = true;
        else if(vDown)
            isHorizonMove = false;
        else if(vUp || hUp)
            isHorizonMove = h != 0;

        // Animation 
        if(anim.GetInteger("hAxisRaw") != h) {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw",(int)h);
        }
        else if(anim.GetInteger("vAxisRaw") != v) {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw",(int)v);
        }
        else {
            anim.SetBool("isChange", false);
        }

        // Direction 
        if(vDown && v == 1) 
            dirVec = Vector3.up;
        else if(vDown && v == -1) 
            dirVec = Vector3.down;
        else if(hDown && h == -1) 
            dirVec = Vector3.left;
        else if(hDown && h == 1) 
            dirVec = Vector3.right;
        
        // Scan Object
        if(Input.GetButtonDown("Jump") && scanObject != null) {
            // Debug.Log("This is : " + scanObject.name);
            manager.Action(scanObject);
        }

        Debug.Log(v + ":" + h);

        // Mobile Var Init
        up_down = false;
        down_down = false;
        left_down = false;
        right_down = false;
        up_up = false;
        down_up = false;
        left_up = false;
        right_up = false;


    }

    void FixedUpdate() {
        // Move
        Vector2 moveVec = isHorizonMove ? new Vector2(h,0): new Vector2(0,v);
        rigid.velocity = moveVec * speed;;

        // Ray
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0,1,0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec , 0.7f, LayerMask.GetMask("Object"));

        if(rayHit.collider != null) {
            scanObject = rayHit.collider.gameObject;
        }
        else {
            scanObject = null;
        }
    }

    public void ButtonDown(string type) {
        switch(type) {
            case "U":
                up_value = 1;
                up_down = true;
                break;
            case "D":
                down_value = -1;
                down_down = true;
                break;
            case "L":
                left_value = -1;
                left_down = true;
                break;
            case "R":
                right_value = 1;
                right_down = true;
                break;
            case "A":
                if(scanObject != null)
                    manager.Action(scanObject);
                break;
            case "C":
                manager.SubMenuActive();
                break;

                
        }
    }
    public void ButtonUp(string type) {
        switch(type) {
            case "U":
                up_value= 0;
                up_up = true;
                break;
            case "D":
                down_value = 0;
                down_up = true;
                break;
            case "L":
                left_value = 0;
                left_up = true;
                break;
            case "R":
                right_value = 0;
                right_up = true;
                break;
        }
        
    }
}
