using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public float maxShotDelay;  // 최대발사
    public float curShotDelay;  //

    public ObjectManager objectManager;

    // 따라다니기 위한 변수들
    public Vector3 followPos;
    public int followDelay;
    public Transform parent;
    public Queue<Vector3> parentPos;

    private void Awake()
    {
        parentPos = new Queue<Vector3>();
    }


    // Update is called once per frame
    void Update()
    {
        Watch();
        Follow();
        Fire();
        Reload();
    }

    void Watch()
    {
        if(!parentPos.Contains(parent.position))
            parentPos.Enqueue(parent.position);

        // 큐의 갯수가 followDelay(상수)보다 클경우 꺼낸다.
        if (parentPos.Count > followDelay)
            followPos = parentPos.Dequeue();
        else if (parentPos.Count < followDelay) //
            followPos = parent.position;

    }

    void Follow()
    {
        transform.position = followPos;
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    void Fire()
    {
        if (!Input.GetButtonDown("Fire1"))
            return;
        if (curShotDelay < maxShotDelay)
            return;

        FireBullet("BulletFollower", Vector3.zero);
        curShotDelay = 0;
    }

    void FireBullet(string obj, Vector3 delta)
    {
        GameObject bullet = objectManager.MakeObj(obj, transform.position + delta, transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
    }
}
