using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;

    public float maxSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;

    void Update() {
        curSpawnDelay += Time.deltaTime;

        if(curSpawnDelay > maxSpawnDelay) {
            SpawnEnemy();
            maxSpawnDelay = Random.Range(0.5f,3f);
            curSpawnDelay = 0;
        }
    }

    void SpawnEnemy() {
        int randEnemy = Random.Range(0,3);
        int randPoint = Random.Range(0,spawnPoints.Length);
        GameObject enemy = Instantiate(enemyObjs[randEnemy], spawnPoints[randPoint].position, spawnPoints[randPoint].rotation);
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic =  enemy.GetComponent<Enemy>();
        enemyLogic.player = player;

        if(randPoint == 5 || randPoint == 6) {  // 
            enemy.transform.Rotate(Vector3.back*-90);
            rigid.velocity = new Vector2(enemyLogic.speed,-1);
        }
        else if(randPoint == 7 || randPoint == 8) { // 
            enemy.transform.Rotate(Vector3.back*90);
            rigid.velocity = new Vector2(enemyLogic.speed*(-1),-1);
        } else {
            rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));
        }

    }

    public void RespawnPlayer() {
        Invoke("RespawnPlayerExe",2.0f);
    }

    void RespawnPlayerExe() {
        player.transform.position = Vector3.down * 3.5f;
        player.SetActive(true);
    }
}
