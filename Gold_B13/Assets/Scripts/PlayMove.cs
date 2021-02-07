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
            anim.SetBool("isWalking", true);
        else
            anim.SetBool("isWalking", false);

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
    }

}
