using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchRight;
    public bool isTouchLeft;
    Animator anim;
    

    public GameObject bulletObjA;
    public GameObject bulletObjB;
    
    public float maxShotDelay;  // 최대발사
    public float curShotDelay;  //

    public float power;

    public GameManager gameManager;

    public int life;
    public int score;
    public bool isHit;

    void Awake() {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
        Reload();
    }

    private void Move() {
        float h = Input.GetAxisRaw("Horizontal");
        if( (isTouchRight && h == 1) || (isTouchLeft && h == -1))  h = 0;

        float v = Input.GetAxisRaw("Vertical");
        if( (isTouchTop && v == 1) || (isTouchBottom && v == -1))  v = 0;

        Vector3 curPos = transform.position;
        Vector3 nextgPos = new Vector3(h,v,0) * speed * Time.deltaTime;

        transform.position = curPos + nextgPos;
        
        if(Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal")) {
            anim.SetInteger("Input", (int)h);
        }
    }

    // Q:Vector3는 default value를 설정할수없나? 
    void FireBullet(GameObject obj,Vector3 delta) {
        GameObject bullet = Instantiate(obj,transform.position+delta, transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
    }

    void Fire() {
        if(!Input.GetButtonDown("Fire1")) 
            return;
        if(curShotDelay<maxShotDelay)
            return;

        switch(power) {
            case 1:
                FireBullet(bulletObjA,Vector3.zero);
                break;
            case 2:
                FireBullet(bulletObjA,Vector3.right*0.1f);
                FireBullet(bulletObjA,Vector3.left*0.1f);

                break;
            case 3:
                FireBullet(bulletObjA,Vector3.right*0.35f);
                FireBullet(bulletObjB,Vector3.zero);
                FireBullet(bulletObjA,Vector3.left*0.35f);
                break;

        }

        curShotDelay = 0;
    }

    void Reload() {
        curShotDelay += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if(coll.gameObject.tag == "Border") {
            switch(coll.gameObject.name) {
                case "Top":
                    isTouchTop = true;
                    break;
                case "Bottom":
                    isTouchBottom = true;
                    break;
                case "Left":
                    isTouchLeft = true;
                    break;
                case "Right":
                    isTouchRight = true;
                    break;
            }
        }
        else if(coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "EnemyBullet") {
            if(isHit) return;
            isHit = true;
            life--;
            gameManager.UpdateLifeIcon(life);

            if(life == 0) {
                gameManager.GameOver();
            }
            else {
                gameManager.RespawnPlayer();
            }

            gameObject.SetActive(false);
            Destroy(coll.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D coll) {
        if(coll.gameObject.tag == "Border") {
            switch(coll.gameObject.name) {
                case "Top":
                    isTouchTop = false;
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
            }
        }
    }    
}
