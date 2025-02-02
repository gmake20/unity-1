using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize = 10;

    private Queue<GameObject> _pool = new Queue<GameObject>();

    public void Init(GameObject p)
    {
        for(int i=0;i<poolSize;i++)
        {
            GameObject obj = Instantiate(prefab,p.transform);
            obj.SetActive(false);
            _pool.Enqueue(obj);
        }
    }

    public GameObject GetObject(Vector3 pos)
    {
        GameObject obj;

        if(_pool.Count > 0)
        {
            obj = _pool.Dequeue();
        }
        else
        {
            obj = Instantiate(prefab);
        }

        obj.transform.position = pos;
        obj.SetActive(true);

        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        _pool.Enqueue(obj);
    }

}
