using UnityEngine;

public class Destroyer : MonoBehaviour
{
    ObjectPool _pool;

    public void SetPool(ObjectPool p)
    {
        _pool = p;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < -15)
        {
            if(_pool)
            {
                _pool.ReturnObject(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
