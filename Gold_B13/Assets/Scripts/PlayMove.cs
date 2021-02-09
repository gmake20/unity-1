using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Rigidbody2D
    // Linear Drag : 공기저항, 이동시 속도를 느리게해줌

    // Constraints / Freeze Rotation / z 체크 : z를 Freese해줘야만 플레이어가 넘어지지 않음.


// Sprite Renderer 
    // Flip : x,y방향으로 반전시킨다.    
public class PlayMove : MonoBehaviour
{
    public GameManager gameManager;

    public float maxSpeed;

    public float jumpPower; 

    Rigidbody2D rigid;    

    SpriteRenderer spriteRenderer;

    Animator anim;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update() {
        // Jump
        if(Input.GetButtonDown("Jump") && anim.GetBool("isJumping") == false) {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
        }

        // Stop Speed 
        if(Input.GetButtonUp("Horizontal")) {
            // normalized : 벡터크기를 1로 만든상태
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f,rigid.velocity.y);
        }

        // Directiopn Sprite
        if(Input.GetButton("Horizontal")) {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

        if(Mathf.Abs(rigid.velocity.x) < 0.3)
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Move Speed 
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if(rigid.velocity.x > maxSpeed) {
            rigid.velocity = new Vector2(maxSpeed,rigid.velocity.y);
        }
        else if(rigid.velocity.x < maxSpeed*(-1)) {
            rigid.velocity = new Vector2(maxSpeed*(-1),rigid.velocity.y);
        }

        // Landing Platform
        if(rigid.velocity.y < 0) {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0,1,0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if(rayHit.collider != null) {
                // Debug.Log(rayHit.collider.name);
                if(rayHit.distance < 0.5f)
                    anim.SetBool("isJumping", false);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if(coll.gameObject.tag == "Enemy") {
            // 몬스터보다 위에 있고, 낙하중임
            if(rigid.velocity.y < 0 && transform.position.y > coll.transform.position.y) {
                // Attack
                OnAttack(coll.transform); 

            } else {
                // Debug.Log("플레이어가 맞았읍니다.");
                OnDamaged(coll.transform.position);
            }

        }
    }

    void OnTriggerEnter2D(Collider2D coll) {
        Debug.Log(coll.gameObject.tag);
        if(coll.gameObject.tag == "item") {
            // Point
            bool isBronze = coll.gameObject.name.Contains("Bronze");
            bool isSilver = coll.gameObject.name.Contains("Silver");
            bool isGold = coll.gameObject.name.Contains("Gold");
            int point = 0;
            if(isBronze) point = 50;
            if(isSilver) point = 100;
            if(isGold) point = 300;
            gameManager.stagePoint += point;

            // Deactive Item
            coll.gameObject.SetActive(false);
        }
        else if(coll.gameObject.tag == "Finish") {
            // Next Stage
            gameManager.NextStage();
        }
    }

    void OnAttack(Transform enemy) {
        //
        gameManager.stagePoint += 100;


        // Reaction Force
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        // Enemy Die
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemyMove.OnDamaged();
    }

    void OnDamaged(Vector2 targetPos) {
        // Health Down
        gameManager.HealthDown();

        // LayerMask.NameToLayer("PlayerDamaged");
        gameObject.layer = 11;
        
        // Alpha 
        spriteRenderer.color = new Color(1,1,1,0.4f);

        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc,1)*7 , ForceMode2D.Impulse);

        anim.SetTrigger("damaged");

        Invoke("OffDamaged",3);
    }

    void OffDamaged() {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1,1,1,1);
    }

    public void OnDie() {
        // Sprite Alpha
        spriteRenderer.color = new Color(1,1,1,0.4f);

        // Sprite Flip Y
        spriteRenderer.flipY = true;

        // Collider Disable
        // coll.enabled = false;

        // Die Effect Jump
        rigid.AddForce(Vector2.up * 5 , ForceMode2D.Impulse);

     
    }
}
