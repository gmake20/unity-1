using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    public Transform[] spawnPoints;

    public float nextSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;

    public Text scoreText;
    public Image[] lifeImage;
    public Image[] boomImage;
    public GameObject gameOverSet;

    public ObjectManager objectManager;


    public enum eEnemy {
        EnemyL = 0, EnemyM = 1,EnemyS=2
    }

    public List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;

    void Awake() {
        spawnList = new List<Spawn>();
        ReadSpawnFile();
    }

    void ReadSpawnFile() {
        // 변수 초기화
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        // 리스폰 File Read
        TextAsset textFile = Resources.Load("stage0") as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        while(stringReader != null) {
            string line = stringReader.ReadLine();
            if(line == null) break;
            Spawn spawnData = new Spawn();
            string[] tmp = line.Split(',');
            // Debug.Log(line);

            if(tmp[1].Length<1) break;
            spawnData.delay = float.Parse(tmp[0]);
            spawnData.type = tmp[1];
            spawnData.point = int.Parse(tmp[2]);
            spawnList.Add(spawnData);
        }


        stringReader.Close();

        nextSpawnDelay = spawnList[0].delay;
    }

    void Update() {
        curSpawnDelay += Time.deltaTime;

        // spawn이 끝나고 10초지나면 다시 처음부터 적등장 
        if(curSpawnDelay > 10.0 && spawnEnd)
        {
            spawnIndex = 0;
            spawnEnd = false;
        }
 
        // curSpawnDelay 값이 nextSpawnDelay 값보다 크면 적비행기 생성 
        if (curSpawnDelay > nextSpawnDelay && !spawnEnd) {
            SpawnEnemy();
            // nextSpawnDelay = Random.Range(0.5f,3f);
            curSpawnDelay = 0;
        }

        // UI Score Update
        Player playerLogic = player.GetComponent<Player>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score);
    }

    // 적비행기 생성 
    void SpawnEnemy() {
        int enemyIndex = 0;
        switch(spawnList[spawnIndex].type) {
            case "S":
                enemyIndex = 2;
                break;
            case "M":
                enemyIndex = 1;
                break;
            case "L":
                enemyIndex = 0;
                break;
        }

        int enemyPoint = spawnList[spawnIndex].point;

        //  int randEnemy = Random.Range(0,3);
        // int randPoint = Random.Range(0,spawnPoints.Length);

        GameObject enemy = objectManager.MakeObj(((eEnemy)enemyIndex).ToString(), spawnPoints[enemyPoint].position, spawnPoints[enemyPoint].rotation);        

        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic =  enemy.GetComponent<Enemy>();
        enemyLogic.player = player;
        enemyLogic.objectManager = objectManager;

        if(enemyPoint == 5 || enemyPoint == 6) {  // 
            enemy.transform.Rotate(Vector3.back*-90);
            rigid.velocity = new Vector2(enemyLogic.speed,-1);
        }
        else if(enemyPoint == 7 || enemyPoint == 8) { // 
            enemy.transform.Rotate(Vector3.back*90);
            rigid.velocity = new Vector2(enemyLogic.speed*(-1),-1);
        } else {
            rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));
        }

        spawnIndex++;
        if(spawnIndex >= spawnList.Count) { 
            spawnEnd = true;
            return ;
            //spawnIndex = 0;
        }

        nextSpawnDelay = spawnList[spawnIndex].delay;
    }

    public void UpdateLifeIcon(int life) {
        for(int i=0;i<3;i++) {
            lifeImage[i].color = Color.clear;
        }
        for(int i=0;i<life ;i++) {
            lifeImage[i].color = Color.white;
        }
    }

    public void UpdateBoomIcon(int boom) {
        for(int i=0;i<3;i++) {
            boomImage[i].color = Color.clear;
        }
        for(int i=0;i<boom ;i++) {
            boomImage[i].color = Color.white;
        }
    }

    public void RespawnPlayer() {
        Invoke(nameof(RespawnPlayerExe),2.0f);
    }

    void RespawnPlayerExe() {
        player.transform.position = Vector3.down * 3.5f;
        player.SetActive(true);

        Player playerLogic = player.GetComponent<Player>();
        playerLogic.isHit = false;
    }

    public void GameOver() {
        gameOverSet.SetActive(true);
    }    

    public void GameRetry() {
        SceneManager.LoadScene(0);
    }
}
