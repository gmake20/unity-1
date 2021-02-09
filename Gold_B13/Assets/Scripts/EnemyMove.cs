using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove; 

    Animator anim;
    SpriteRenderer spriteRenderer;

    CapsuleCollider2D coll; 


    void Awake() {
        rigid = GetComponent<Rigidbody2D>();  
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<CapsuleCollider2D>();
    }

    void Start() {
        // Invoke() : 주어진 시간이 지난뒤 , 지정된 함수를 실행하는 함수
        Invoke("Think", 2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove,rigid.velocity.y);

        // Platform Check
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove*0.3f, rigid.position.y);

        Debug.DrawRay(frontVec, Vector3.down, new Color(0,1,0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if(rayHit.collider == null) 
            Turn();
    }

    void Think() {
        nextMove = Random.Range(-1,2);
        anim.SetInteger("WalkSpeed", nextMove);
        
        if(nextMove != 0)
            spriteRenderer.flipX = nextMove == 1;

        float nextThinkTime = Random.Range(2f,5f);
        // Debug.Log(nextThinkTime);
        Invoke("Think", nextThinkTime);

    }

    void Turn() {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == 1; 
        CancelInvoke();
        Invoke("Think", 2);          
    }

    public void OnDamaged() {
        // Sprite Alpha
        spriteRenderer.color = new Color(1,1,1,0.4f);

        // Sprite Flip Y
        spriteRenderer.flipY = true;

        // Collider Disable
        coll.enabled = false;

        // Die Effect Jump
        rigid.AddForce(Vector2.up * 5 , ForceMode2D.Impulse);

        // Destroy 
        Invoke("DeActive",5);
    }

    void DeActive() {
        gameObject.SetActive(false);
    }
}
