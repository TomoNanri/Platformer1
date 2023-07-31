using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class FrogJump : MonoBehaviour
{
    public int jumpType = 0;    // 0: HighJump, 1: WideJump
    public int direction = 0;   // 0: Left, 1: Right
    private Rigidbody2D m_rigidbody2D;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jump()
    {
        float jumpPower = 120f;
        float jumpForward = 10f;
        Quaternion rot = transform.rotation;

        if(jumpType == 0)
        {
            jumpPower = 120f;
            jumpForward = 10f;
        }
        else if(jumpType == 1)
        {
            jumpPower = 80f;
            jumpForward = 50f;
        }

        m_rigidbody2D.AddForce(Vector2.up * jumpPower);

        if(direction == 0)
        {
            transform.rotation = Quaternion.Euler(rot.x, 0f, rot.x);
            m_rigidbody2D.AddForce(Vector2.left * jumpForward);
        }
        else if(direction == 1)
        {
            transform.rotation = Quaternion.Euler(rot.x, 180f, rot.x);
            m_rigidbody2D.AddForce(Vector2.right * jumpForward);

        }

        anim.SetTrigger("jump");
    } 
}
