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
    Animator _anim;
    

    public GameObject bulletObjA;
    public GameObject bulletObjB;

    public GameObject boomEffect;
    
    public float maxShotDelay;  // 최대발사
    public float curShotDelay;  //

    public int power;
    public int maxpower;

    public int boom;
    public int maxboom;

    public GameManager gameManager;
    public ObjectManager objectManager;

    public int life;
    public int score;
    public bool isHit;

    public bool isBoomTime;

    public enum ePlayerBullet {
        BulletPlayerA = 0, BulletPlayerB = 1
    }

    void Awake() {
        _anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
        Boom();
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
            _anim.SetInteger("Input", (int)h);
        }
    }

    // Q:Vector3는 default value를 설정할수없나? 
    void FireBullet(string obj,Vector3 delta) {
        GameObject bullet = objectManager.MakeObj(obj,transform.position+delta, transform.rotation);
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
                FireBullet(ePlayerBullet.BulletPlayerA.ToString(),Vector3.zero);
                break;
            case 2:
                FireBullet(ePlayerBullet.BulletPlayerA.ToString(),Vector3.right*0.1f);
                FireBullet(ePlayerBullet.BulletPlayerA.ToString(),Vector3.left*0.1f);

                break;
            case 3:
                FireBullet(ePlayerBullet.BulletPlayerA.ToString(),Vector3.right*0.35f);
                FireBullet(ePlayerBullet.BulletPlayerB.ToString(),Vector3.zero);
                FireBullet(ePlayerBullet.BulletPlayerA.ToString(),Vector3.left*0.35f);
                break;

        }

        curShotDelay = 0;
    }

    void Reload() {
        curShotDelay += Time.deltaTime;
    }

    void Boom() {
        if(!Input.GetButtonDown("Fire2")) 
            return;    
        if(isBoomTime)
            return;
        if(boom == 0)
            return;

        boom--;
        isBoomTime = true;
        gameManager.UpdateBoomIcon(boom);


        boomEffect.SetActive(true);
        Invoke(nameof(OffBoomEffect), 4f);

        List<GameObject> enemyList = objectManager.GetActiveEnemyPool();
        foreach(GameObject obj in enemyList) {
            Enemy enemyLogic = obj.GetComponent<Enemy>();
            enemyLogic.OnHit(1000);
        }

        List<GameObject> enemyBulletList = objectManager.GetActiveEnemyBulletPool();
        foreach(GameObject obj in enemyBulletList) {
            obj.SetActive(false);
        }
              
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if(coll.gameObject.CompareTag("Border")) {
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
        else if(coll.gameObject.CompareTag("Enemy") || coll.gameObject.CompareTag("EnemyBullet")) {
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
            coll.gameObject.SetActive(false);
        }
        else if(coll.gameObject.CompareTag("Item")) {
            Item item = coll.gameObject.GetComponent<Item>();
            switch(item.type) {
                case "coin":
                    score += 1000;
                    break;
                case "power":
                    if(power < maxpower)
                        power++;
                    else 
                        score += 500;

                    break;
                case "boom":
                    if(boom < maxboom) {
                        boom++;
                        gameManager.UpdateBoomIcon(boom);
                    }
                    else  {
                        score += 500;
                    }
                    break;    
            }

            coll.gameObject.SetActive(false);
        }
    }

    void OffBoomEffect() {
        boomEffect.SetActive(false);
        isBoomTime = false;
    }

    void OnTriggerExit2D(Collider2D coll) {
        if(coll.gameObject.CompareTag("Border")) {
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
