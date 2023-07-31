using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAInonTargeting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveLoop());   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MoveLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Move();
        }
    }

    void Move()
    {
        int randValue;
        randValue = Random.Range(0, 2);

        if(randValue == 0)
        {
            GetComponent<FrogJump>().Jump();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "enemyBlocking")
        {
            if(GetComponent<FrogJump>().direction == 0)
            {
                GetComponent<FrogJump>().direction = 1;
            }
            else if (GetComponent<FrogJump>().direction == 1)
            {
                GetComponent<FrogJump>().direction = 0;
            }
        }
    }
}
