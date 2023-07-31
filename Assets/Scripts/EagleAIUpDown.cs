using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleAIUpDown : MonoBehaviour
{
    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(pos.x, pos.y + Mathf.PingPong(Time.time / 2, 0.5f), pos.z);
    }
}
