using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpossumAITargeting : MonoBehaviour
{
    private Rigidbody2D m_rigidbody2D;
    bool playerSearch = false;
    Vector3 playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(MoveLoop());
    }

    // Update is called once per frame
    void Update()
    {
        if (playerSearch)
        {
            if(transform.position.x > playerPosition.x)
            {
                GetComponent<OpossumWalkRun>().direction = 0;
            }
            else
            {
                GetComponent<OpossumWalkRun>().direction = 1;
            }
        }

        float walkForward;

        if (GetComponent<OpossumWalkRun>().walkType)
        {
            walkForward = 1.5f;
        }
        else
        {
            walkForward = 0.5f;
        }
        if (GetComponent<OpossumWalkRun>().direction == 0)
        {
            m_rigidbody2D.velocity = Vector3.left * walkForward;
        }
        else if (GetComponent<OpossumWalkRun>().direction == 1)
        {
            m_rigidbody2D.velocity = Vector3.right * walkForward;
        }
    }

    IEnumerator MoveLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if(!playerSearch)
                Move();
        }
    }

    void Move()
    {
        int randValue;
        randValue = Random.Range(0, 2);

        if (randValue == 0)
        {
            if (GetComponent<OpossumWalkRun>().direction == 0)
            {
                GetComponent<OpossumWalkRun>().direction = 1;
            }
            else if (GetComponent<OpossumWalkRun>().direction == 1)
            {
                GetComponent<OpossumWalkRun>().direction = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemyBlocking")
        {
            if (GetComponent<OpossumWalkRun>().direction == 0)
            {
                GetComponent<OpossumWalkRun>().direction = 1;
            }
            else if (GetComponent<OpossumWalkRun>().direction == 1)
            {
                GetComponent<OpossumWalkRun>().direction = 0;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerSearch = true;
            playerPosition = collision.transform.position;
            GetComponent<OpossumWalkRun>().walkType = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerSearch = false;
            GetComponent<OpossumWalkRun>().walkType = false;
        }
    }
}
