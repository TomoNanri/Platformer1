using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleAIAttack : MonoBehaviour
{
    bool playerSearch = false;
    Vector3 pos;
    Vector3 playerPosition;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerSearch)
        {
            if(transform.position.x > playerPosition.x)
            {
                GetComponent<EagleFly>().direction = 0;
            }
            else
            {
                GetComponent<EagleFly>().direction = 1;
            }

            transform.position = Vector2.MoveTowards(transform.position,
                new Vector2(playerPosition.x, playerPosition.y), 0.7f * Time.deltaTime);
            pos = transform.position;
            anim.SetBool("attack", true);
        }
        else
        {
            transform.position = new Vector3(pos.x, pos.y + Mathf.PingPong(Time.time / 2f, 0.5f), pos.x);
            anim.SetBool("attack", false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //Debug.Log("true");
            playerSearch = true;
            playerPosition = collision.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //Debug.Log("false");
            playerSearch = false;
        }
    }
}
