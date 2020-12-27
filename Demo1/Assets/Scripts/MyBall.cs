using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBall : MonoBehaviour
{
    Rigidbody rig;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        // rig.velocity = new Vector3(3,3,3); // Vector3.right;
    }

    // Update is called once per frame
    void Update()
    {
        Update3();
    }

    void Update1() {        
        // 1. 속력바꾸기
        rig.velocity = Vector3.right;
    }

    void Update2() {
        Vector3 vec = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        rig.AddForce(vec,ForceMode.Impulse);

        // 2. 힘을 가하기, (Space, 상하좌우)
        if(Input.GetButtonDown("Jump")) {
            // 해당 방향으로 힘을 준다. 
            rig.AddForce(Vector3.up * 50,ForceMode.Impulse);
        }        
    }

    void Update3() {
        // 3. 회전력
        rig.AddTorque(Vector3.up);
    }


    void FixedUpdate() {  
        // rig.velocity = new Vector3(3,3,3); // Vector3.right;
    }
}
