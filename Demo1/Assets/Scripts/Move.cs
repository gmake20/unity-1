using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 10.0f;

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 vec = new Vector3(
            h * Time.deltaTime * speed,
            0,
            v * Time.deltaTime * speed
        );
        transform.Translate(vec);
    }
}
