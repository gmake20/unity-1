using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject enemyLPrefab;
    public GameObject enemyMPrefab;
    public GameObject enemySPrefab;
    public GameObject itemCoinPrefab;
    public GameObject itemPowerPrefab;
    public GameObject itemBoomPrefab;
    public GameObject bulletPlayerAPrefab;
    public GameObject bulletPlayerBPrefab;
    public GameObject bulletEnemyAPrefab;
    public GameObject bulletEnemyBPrefab;
    

    GameObject[] enemyL;
    GameObject[] enemyM;
    GameObject[] enemyS;

    GameObject[] itemCoin;
    GameObject[] itemPower;
    GameObject[] itemBoom;

    GameObject[] bulletPlayerA;
    GameObject[] bulletPlayerB;
    GameObject[] bulletEnemyA;
    GameObject[] bulletEnemyB;

    void Awake() {
        enemyL = new GameObject[10];
        enemyM = new GameObject[10];
        enemyS = new GameObject[10];

        itemCoin = new GameObject[10];
        itemPower = new GameObject[10];
        itemBoom = new GameObject[10];

        bulletPlayerA = new GameObject[20];
        bulletPlayerB = new GameObject[10];
        bulletEnemyA = new GameObject[10];
        bulletEnemyB = new GameObject[10];

        Generate();
    }

    void Generate() {
        for(int i=0;i<enemyL.Length;i++) {
            enemyL[i] = Instantiate(enemyLPrefab);
            enemyL[i].SetActive(false);
        }

        for(int i=0;i<enemyM.Length;i++) {
            enemyM[i] = Instantiate(enemyMPrefab);
            enemyM[i].SetActive(false);
        }

        for(int i=0;i<enemyS.Length;i++) {
            enemyS[i] = Instantiate(enemySPrefab);
            enemyS[i].SetActive(false);
        }

        for(int i=0;i<itemCoin.Length;i++) {
            itemCoin[i] = Instantiate(itemCoinPrefab);
            itemCoin[i].SetActive(false);
        }

        for(int i=0;i<itemPower.Length;i++) {
            itemPower[i] = Instantiate(itemPowerPrefab);
            itemPower[i].SetActive(false);
        }

        for(int i=0;i<itemBoom.Length;i++) {
            itemBoom[i] = Instantiate(itemBoomPrefab);
            itemBoom[i].SetActive(false);
        }

        for(int i=0;i<bulletPlayerA.Length;i++) {
            bulletPlayerA[i] = Instantiate(bulletPlayerAPrefab);
            bulletPlayerA[i].SetActive(false);
        }
        for(int i=0;i<bulletPlayerB.Length;i++) {
            bulletPlayerB[i] = Instantiate(bulletPlayerBPrefab);
            bulletPlayerB[i].SetActive(false);
        }

        for(int i=0;i<bulletEnemyA.Length;i++) {
            bulletEnemyA[i] = Instantiate(bulletEnemyAPrefab);
            bulletEnemyA[i].SetActive(false);
        }
        for(int i=0;i<bulletEnemyB.Length;i++) {
            bulletEnemyB[i] = Instantiate(bulletEnemyBPrefab);
            bulletEnemyB[i].SetActive(false);
        }                
    }


    public GameObject MakeObj(string type, Vector3 pos, Quaternion rot) {
        Debug.Log(type);


        GameObject[] targetPool = null;

        switch(type) {
            case "EnemyL":
                targetPool = enemyL;
                break;
            case "EnemyM":
                targetPool = enemyM;
                break;
            case "EnemyS":
                targetPool = enemyS;
                break;
            case "ItemCoin":
                targetPool = itemCoin;
                break;
            case "ItemPower":
                targetPool = itemPower;
                break;
            case "ItemBoom":
                targetPool = itemBoom;
                break;
            case "BulletPlayerA":
                targetPool = bulletPlayerA;
                break;
            case "BulletPlayerB":
                targetPool = bulletPlayerB;
                break;
            case "BulletEnemyA":
                targetPool = bulletEnemyA;
                break;
            case "BulletEnemyB":
                targetPool = bulletEnemyB;
                break;
        }


        for(int i=0;i<targetPool.Length;i++) {
            if(!targetPool[i].activeSelf) {
                targetPool[i].SetActive(true);
                targetPool[i].transform.position = pos;
                targetPool[i].transform.rotation = rot;
                return targetPool[i];
            }
        }

        Debug.Log(type + " : Free Object is Not!! ㅠㅠ ");
        return null;

    }

    public List<GameObject> GetActiveEnemyPool() {
        List<GameObject> list = new List<GameObject>();

        for(int i=0;i<enemyL.Length;i++) 
            if(enemyL[i].activeSelf) 
                list.Add(enemyL[i]);
        for(int i=0;i<enemyM.Length;i++) 
            if(enemyM[i].activeSelf) 
                list.Add(enemyM[i]);
        for(int i=0;i<enemyS.Length;i++) 
            if(enemyS[i].activeSelf) 
                list.Add(enemyS[i]);
        return list;
    }

    public List<GameObject> GetActiveEnemyBulletPool() {
        List<GameObject> list = new List<GameObject>();

        for(int i=0;i<bulletEnemyA.Length;i++) 
            if(bulletEnemyA[i].activeSelf) 
                list.Add(bulletEnemyA[i]);
        for(int i=0;i<bulletEnemyB.Length;i++) 
            if(bulletEnemyB[i].activeSelf) 
                list.Add(bulletEnemyB[i]);
        return list;
    }

    public GameObject[] GetPool(string type) {
        GameObject[] targetPool = null;
        switch(type) {
            case "EnemyL":
                targetPool = enemyL;
                break;
            case "EnemyM":
                targetPool = enemyM;
                break;
            case "EnemyS":
                targetPool = enemyS;
                break;
            case "ItemCoin":
                targetPool = itemCoin;
                break;
            case "ItemPower":
                targetPool = itemPower;
                break;
            case "ItemBoom":
                targetPool = itemBoom;
                break;
            case "BulletPlayerA":
                targetPool = bulletPlayerA;
                break;
            case "BulletPlayerB":
                targetPool = bulletPlayerB;
                break;
            case "BulletEnemyA":
                targetPool = bulletEnemyA;
                break;
            case "BulletEnemyB":
                targetPool = bulletEnemyB;
                break;
        }
        return targetPool;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
