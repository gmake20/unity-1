using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    public float minSpawnDelay;
    public float maxSpawnDelay;

    [Header("References")]
    public GameObject[] gameObjects;

    ObjectPool[] _pool;

    private void Start()
    {
        _pool = new ObjectPool[gameObjects.Length];

        for(int i=0;i<gameObjects.Length;i++)
        {
            if (gameObject != null)
            {
                _pool[i] = gameObject.AddComponent<ObjectPool>();
                _pool[i].prefab = gameObjects[i];
                _pool[i].Init(gameObject);
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        Invoke("Spawn", Random.Range(minSpawnDelay,maxSpawnDelay));
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void Spawn()
    {
        int idx = Random.Range(0, gameObjects.Length);
        GameObject obj =  _pool[idx].GetObject(transform.position);
        obj.GetComponent<Destroyer>().SetPool(_pool[idx]);

        /*
        var randomObject = gameObjects[Random.Range(0, gameObjects.Length)];
        Instantiate(randomObject, transform.position, Quaternion.identity);
        */

        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));
    }
}
