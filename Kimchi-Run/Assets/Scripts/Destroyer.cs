using UnityEngine;

public class Destroyer : MonoBehaviour
{
    ObjectPool pool;

    public void SetPool(ObjectPool p)
    {
        pool = p;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < -15)
        {
            if(pool)
            {
                pool.ReturnObject(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
