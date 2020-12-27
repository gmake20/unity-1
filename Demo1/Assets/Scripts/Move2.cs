using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2 : MonoBehaviour
{
    // 목적지 오브젝트
    public GameObject targetObj;
    // 목적지 좌표
    Vector3 target;

    bool isMove = false;
    // Start is called before the first frame update
    void Start()
    {
        target = targetObj.transform.position;
    }

    // [유니티 기초 - B7] 목표 지점으로 이동시키기
    // Update is called once per frame
    void Update()
    {
        // 목적지 좌표를 가져온다.
        target = targetObj.transform.position;

        if(Input.anyKeyDown)
            isMove = true;
            
        if(isMove)
            Update3();
    }

    void Update1() {
        // 단순 등속이동
        // 1. MoveTowards (현재위치 , 목표위치, 속도)
        transform.position = Vector3.MoveTowards(transform.position, target,0.1f);
    }

    void Update2() {
        Vector3 velo = Vector3.zero;
        // 2. SmoothDamp (현재위치, 목표위치, 참조속도, 속도)
        // 미끄러지듯이 감속이동, 목적지에 가까워질수록 속도가 느려진다.
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velo, 0.1f);

    }

    void Update3() {
        // 3. Lerp (선형보간)
        transform.position = Vector3.Lerp(transform.position, target, 0.003f);
    }

    void Update4() {
        // 4. SLerp(구면선형보간, 호를 그리며 이동)
        transform.position = Vector3.Slerp(transform.position, target, 0.05f);
    }
}
