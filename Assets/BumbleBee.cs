using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class BumbleBee : MonoBehaviour
{
    public float speed = 0.1f;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 pos = transform.position;
        pos.x += speed;
        transform.position = pos;
    }
}
