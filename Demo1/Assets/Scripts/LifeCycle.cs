using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCycle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 
        if(Input.anyKeyDown)
            Debug.Log("아무키나 눌렀습니다.");

        // enter키
        if(Input.GetKeyDown(KeyCode.Return))
            Debug.Log("Return Key");
        // 왼쪽키 눌렀을때
        if(Input.GetKey(KeyCode.LeftArrow)) 
            Debug.Log("Left Key");
        // 오른쪽키 눌렀을때
        if(Input.GetKeyUp(KeyCode.RightArrow)) 
            Debug.Log("Right Key");

        // 마우스 버튼 관련 이벤트
        if(Input.GetMouseButtonDown(0))
            Debug.Log("MouseButtonDown");
        if(Input.GetMouseButton(0))
            Debug.Log("MouseButton");
        if(Input.GetMouseButtonUp(0))
            Debug.Log("MouseButtonUp");


        // Input Manager의 Fire1 
        if(Input.GetButtonDown("Fire1"))
            Debug.Log("ButtonDown");
        if(Input.GetButton("Fire1"))
            Debug.Log("Button");
        if(Input.GetButtonUp("Fire1"))
            Debug.Log("ButtonUp");
        
        // 좌우 화살표를 누르면 -1 ~ 1까지의 값을 가져온다.
        if(Input.GetButton("Horizontal")) {
            Debug.Log(Input.GetAxis("Horizontal"));
        }

        // 좌우 화살표를 누르면 -1 또는 1까지의 값을 가져온다.(중간값이 없음)
        if(Input.GetButton("Horizontal")) {
            Debug.Log(Input.GetAxisRaw("Horizontal"));
        }
    }
}
