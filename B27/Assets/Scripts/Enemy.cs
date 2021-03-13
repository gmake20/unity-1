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
    

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        //Move();
        Fire();
        Reload();
    }

    void FireBullet(GameObject obj,Vector3 delta, float bulletSpeed=4) {
        GameObject bullet = Instantiate(obj,transform.position+delta, transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

        Vector3 dirVec = player.transform.position - transform.position;
        rigid.AddForce(dirVec.normalized * bulletSpeed, ForceMode2D.Impulse);
    }

    void Fire() {
        if(curShotDelay < maxShotDelay)
            return;

        if(enemyName == "S") {
            FireBullet(bulletObjA,Vector3.zero, 3);
        } if(enemyName == "L") {
            FireBullet(bulletObjA,Vector3.right*0.3f);
            FireBullet(bulletObjA,Vector3.left*0.3f);
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
            Destroy(gameObject);

            // Random Item Drop
            int ran = Random.Range(0,10); 
            if(ran < 3) {

            }
            else if(ran < 6) {
                Instantiate(itemCoin, transform.position, itemCoin.transform.rotation);
            }
            else if(ran < 8) {
                Instantiate(itemPower, transform.position, itemPower.transform.rotation);
                
            }
            else if(ran < 10) {
                Instantiate(itemBoom, transform.position, itemBoom.transform.rotation);
                
            }

        }
    }

    void ReturnSprite() {
        spriteRenderer.sprite = sprites[0];
    }

    void OnTriggerEnter2D(Collider2D coll) {
        // 화면 밖으로 나가면 삭제 
        if(coll.gameObject.CompareTag("BorderBullet")) {
            Destroy(gameObject);
        }
        else if(coll.gameObject.CompareTag("PlayerBullet")) {
            Bullet bullet = coll.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);

            Destroy(coll.gameObject);
        }

    }

}
