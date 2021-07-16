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

    Animator anim;

    public int patternIndex;
    public int curPatternCount;
    public int[] maxPatternCount;
    
    public ObjectManager objectManager;


    private enum eEnemyBullet {
        BulletEnemyA = 0, BulletEnemyB = 1, BulletBossA = 2, BulletBossB = 3
    }
    private enum eItem {
        ItemCoin = 0, ItemPower = 1,ItemBoom=2
    }

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (enemyName == "B")
            anim = GetComponent<Animator>();
    }

    void OnEnable() {
        switch(enemyName) {
            case "B":
                health = 3000;
                Invoke("Stop", 2);
                break;

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

    void Stop()
    {
        if (!gameObject.activeSelf)
            return;
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;

        Invoke("Think", 2);
    }

    void Think()
    {
        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
        curPatternCount = 0;

        switch(patternIndex)
        {
            case 0:
                FireFoward();
                break;
            case 1:
                FireShot();
                break;
            case 2:
                FireArc();
                break;
            case 3:
                FireAround();
                break;
        }
    }

    void FireFoward()
    {
        // 앞으로 4발 
        FireBullet(eEnemyBullet.BulletBossA.ToString(), Vector3.right * 0.3f, 8,Vector3.zero, Vector3.down);
        FireBullet(eEnemyBullet.BulletBossA.ToString(), Vector3.right * 0.45f, 8, Vector3.zero, Vector3.down);
        FireBullet(eEnemyBullet.BulletBossA.ToString(), Vector3.left * 0.3f, 8, Vector3.zero, Vector3.down);
        FireBullet(eEnemyBullet.BulletBossA.ToString(), Vector3.left * 0.45f, 8, Vector3.zero, Vector3.down);

        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireFoward", 2);
        else
            Invoke("Think", 3);
    }

    void FireShot()
    {
        // 플레이어 방향으로 샷건
        for (int i=0;i<5;i++)
        {
            FireBullet(eEnemyBullet.BulletBossB.ToString(), Vector3.zero, 6, new Vector3(Random.Range(-0.5f,0.5f),Random.Range(0f,2f),0));

        }


        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireShot", 3.5f);
        else
            Invoke("Think", 3);
    }

    void FireArc()
    {
        // 부채모양
        Vector3 dirVec = new Vector3(Mathf.Cos(Mathf.PI * 10 * curPatternCount / maxPatternCount[patternIndex]),-1,0);
        FireBullet(eEnemyBullet.BulletBossB.ToString(), Vector3.zero, 5,Vector3.zero, dirVec);

        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireArc", 0.15f);
        else
            Invoke("Think", 3);
    }

    void FireAround()
    {
        // 원형태 공격
        int roundNumA = 38 + Random.Range(0,4);
        for(int i=0;i<roundNumA;i++)
        {
            Vector3 dirVec = new Vector3(Mathf.Cos(Mathf.PI * 2 * i / roundNumA),
                Mathf.Sin(Mathf.PI * 2 * i / roundNumA), 0);


            GameObject obj = FireBullet(eEnemyBullet.BulletBossB.ToString(), Vector3.zero, 3, Vector3.zero, dirVec);
            Vector3 rotVec = Vector3.forward * 360 * i / roundNumA + Vector3.forward * 90;
            obj.transform.Rotate(rotVec);

        }

        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireAround", 0.7f);
        else
            Invoke("Think", 3);
    }

    void Update()
    {
        if (enemyName == "B")
            return;
                
        Fire();
        Reload();
    }

    // delta : 초기값에 delta값만큼 위치지정됨 
    // 진행방향 
    // dirVec2값은 dirVec에 더해서 진행됨 
    // dirTarget 값이 있을경우 해당 값으로 AddForce됨
    GameObject FireBullet(string obj, Vector3 delta, float bulletSpeed = 4, Vector3 dirVec2 = default(Vector3),Vector3 dirTarget=default(Vector3)) {
        // GameObject bullet = Instantiate(obj,transform.position+delta, transform.rotation);
        GameObject bullet = objectManager.MakeObj(obj,transform.position+delta, transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

        Vector3 dirVec = player.transform.position - transform.position;
        dirVec += dirVec2;


        if(dirTarget.sqrMagnitude > 0.0f) dirVec = dirTarget;

        rigid.AddForce(dirVec.normalized * bulletSpeed, ForceMode2D.Impulse);
        return bullet;
    }

    void Fire() {
        if(curShotDelay < maxShotDelay)
            return;

        if(enemyName == "S") {
            FireBullet(eEnemyBullet.BulletEnemyA.ToString(),Vector3.zero, 3);
        } if(enemyName == "L") {
            FireBullet(eEnemyBullet.BulletEnemyB.ToString(),Vector3.right*0.3f);
            FireBullet(eEnemyBullet.BulletEnemyB.ToString(),Vector3.left*0.3f);
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

        if(enemyName == "B")
        {
            anim.SetTrigger("OnHit");
        }
        else
        {
            spriteRenderer.sprite = sprites[1];
            Invoke(nameof(ReturnSprite), 0.1f);
        }


        if (health<=0) {
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enemyScore;
            gameObject.SetActive(false);

            // Random Item Drop
            int ran = enemyName == "B" ? 0 : Random.Range(0,10); 
            if(ran < 3) {

            }
            else if(ran < 6) {
                GameObject obj = objectManager.MakeObj(eItem.ItemCoin.ToString(), transform.position, itemCoin.transform.rotation);
            }
            else if(ran < 8) {
                GameObject obj = objectManager.MakeObj(eItem.ItemPower.ToString(), transform.position, itemCoin.transform.rotation);       
            }
            else if(ran < 10) {
                GameObject obj = objectManager.MakeObj(eItem.ItemBoom.ToString(), transform.position,itemCoin.transform.rotation);
            }

        }
    }

    void ReturnSprite() {
        spriteRenderer.sprite = sprites[0];
    }

    void OnTriggerEnter2D(Collider2D coll) {
        // 화면 밖으로 나가면 삭제 
        if(coll.gameObject.CompareTag("BorderBullet") && enemyName != "B") {
            gameObject.SetActive(false);
        }
        else if(coll.gameObject.CompareTag("PlayerBullet")) {
            Bullet bullet = coll.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);

            coll.gameObject.SetActive(false);            
        }
    }


}
