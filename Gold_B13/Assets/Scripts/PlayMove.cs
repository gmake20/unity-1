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
        if(Input.GetButtonDown("Horizontal")) {
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
            // Debug.Log("플레이어가 맞았읍니다.");
            OnDamaged(coll.transform.position);
        }
    }

    void OnDamaged(Vector2 targetPos) {
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
}
