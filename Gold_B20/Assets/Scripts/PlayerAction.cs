using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    float h;
    float v;
    Rigidbody2D rigid;

    float speed;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        speed = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal") * speed;
        v = Input.GetAxisRaw("Vertical") * speed;
    }

    void FixedUpdate() {
        rigid.velocity = new Vector2(h,v);
    }
}
