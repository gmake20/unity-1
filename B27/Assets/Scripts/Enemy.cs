using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName; 
    public float speed;
    public int health;
    public Sprite[] sprites;

    SpriteRenderer spriteRenderer;
    
    public float maxShotDelay;  // 최대발사
    public float curShotDelay;  //
    public GameObject bulletObjA;
    public GameObject bulletObjB;

    public GameObject itemCoin;
    public GameObject itemPower;
    public GameObject itemBoom;


    public GameObject player;
    public int enemyScore;
    
    public ObjectManager objectManager;
    public string[] bulletObjs;
    public string[] itemObjs;

    void Awake() {
        bulletObjs = new string[] { "BulletEnemyA", "BulletEnemyB"  };
        itemObjs = new string[] { "ItemCoin", "ItemPower", "ItemBoom"  };

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable() {
        switch(enemyName) {
            case "L":
                health = 40;
                break;
            case "M":
                health = 10;
                break;
            case "S":
                health = 3;
                break;

        }
    }

    void Update()
    {
        //Move();
        Fire();
        Reload();
    }

    void FireBullet(string obj,Vector3 delta, float bulletSpeed=4) {
        // GameObject bullet = Instantiate(obj,transform.position+delta, transform.rotation);
        GameObject bullet = objectManager.MakeObj(obj,transform.position+delta, transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

        Vector3 dirVec = player.transform.position - transform.position;
        rigid.AddForce(dirVec.normalized * bulletSpeed, ForceMode2D.Impulse);
    }

    void Fire() {
        if(curShotDelay < maxShotDelay)
            return;

        if(enemyName == "S") {
            FireBullet(bulletObjs[0],Vector3.zero, 3);
        } if(enemyName == "L") {
            FireBullet(bulletObjs[1],Vector3.right*0.3f);
            FireBullet(bulletObjs[1],Vector3.left*0.3f);
        }


        curShotDelay = 0;
    }


    void Reload() {
        curShotDelay += Time.deltaTime;
    }

    public void OnHit(int dmg) {
        if(health <= 0)
            return;
            
        health -= dmg;
        spriteRenderer.sprite = sprites[1];
        Invoke(nameof(ReturnSprite), 0.1f);

        if(health<=0) {
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enemyScore;
            // Destroy(gameObject);
            gameObject.SetActive(false);

            // Random Item Drop
            int ran = Random.Range(0,10); 
            if(ran < 3) {

            }
            else if(ran < 6) {
                GameObject obj = objectManager.MakeObj(itemObjs[0], transform.position, itemCoin.transform.rotation);
                // Instantiate(itemCoin, transform.position, itemCoin.transform.rotation);
            }
            else if(ran < 8) {
                GameObject obj = objectManager.MakeObj(itemObjs[1], transform.position, itemCoin.transform.rotation);
                //Instantiate(itemPower, transform.position, itemPower.transform.rotation);
                
            }
            else if(ran < 10) {
                GameObject obj = objectManager.MakeObj(itemObjs[2], transform.position, itemCoin.transform.rotation);
                //Instantiate(itemBoom, transform.position, itemBoom.transform.rotation);
                
            }

        }
    }

    void ReturnSprite() {
        spriteRenderer.sprite = sprites[0];
    }

    void OnTriggerEnter2D(Collider2D coll) {
        // 화면 밖으로 나가면 삭제 
        if(coll.gameObject.CompareTag("BorderBullet")) {
            gameObject.SetActive(false);
            // Destroy(gameObject);
        }
        else if(coll.gameObject.CompareTag("PlayerBullet")) {
            Bullet bullet = coll.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);

            // Destroy(coll.gameObject);
            coll.gameObject.SetActive(false);
            
        }

    }



}
