using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleFly : MonoBehaviour
{
    public bool flyType = false;    // false:Normal true:Attack
    public int direction = 0;       // 0:Left 1:Right
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rot = transform.rotation;
        if(direction == 0)
        {
            transform.rotation = Quaternion.Euler(rot.x, 0f, rot.z);
        }
        else if(direction == 1)
        {
            transform.rotation = Quaternion.Euler(rot.x, 180f, rot.z);
        }
        anim.SetBool("attack", flyType);
    }
}
