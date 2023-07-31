using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAITargeting : MonoBehaviour
{
    bool playerSearch = false;
    Vector3 playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveLoop());
    }

    // Update is called once per frame
    void Update()
    {
        if (playerSearch)
        {
            if(transform.position.x > playerPosition.x)
            {
                GetComponent<FrogJump>().direction = 0;
            }
            else
            {
                GetComponent<FrogJump>().direction = 1;
            }
        }
    }

    IEnumerator MoveLoop()
    {
        while(true){
            yield return new WaitForSeconds(1f);
            Move();
        }
    }

    void Move()
    {
        int randValue;
        randValue = Random.Range(0, 2);

        //Debug.Log("RND = " + randValue);

        if(randValue == 0)
        {
            GetComponent<FrogJump>().Jump();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //Debug.Log("Player found!");
            playerSearch = true;
            playerPosition = collision.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("Lost Target!");
            playerSearch = false;
        }
    }
}
