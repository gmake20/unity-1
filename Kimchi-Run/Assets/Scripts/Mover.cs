using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3.left * (GameManager.Instance.CalcualteGameSpeed() * Time.deltaTime));        
    }
}
