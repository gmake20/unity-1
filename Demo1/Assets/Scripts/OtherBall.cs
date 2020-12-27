using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherBall : MonoBehaviour
{
    MeshRenderer mesh;
    Material mat;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mat = mesh.material;
    }

    private void OnCollisionEnter(Collision other)
    {
        // 충돌한 객체가 Player라면 Material을 Black으로 바꾼다.
        if(other.gameObject.CompareTag("Player"))
            mat.color =  new Color(0,0,0);
    }

    private void OnCollisionStay(Collision other)
    {
        
    }

    private void OnCollisionExit(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
            mat.color =  new Color(1,1,1);        
        
    }
}
