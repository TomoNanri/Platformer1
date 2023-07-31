using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    Animator anim;
    public bool isDamage = false;
    public GameObject damageFX;
    ScoreManager scoreManager;
    bool isScoring = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDamage & !isScoring)
        {
            Damage();
            isScoring = true;
        }
    }
    public void Damage()
    {
        GameObject FX_obj;
        isDamage = true;
        StartCoroutine("DamageAnim");

        FX_obj = (GameObject)Instantiate(damageFX, transform.position, Quaternion.identity);
        FX_obj.transform.parent = transform;
        StartCoroutine("enemyDestroy");
    }

    IEnumerator DamageAnim()
    {
        Renderer renderer = GetComponent<Renderer>();

        for(int loop=0; loop < 5; loop++)
        {
            renderer.material.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.05f);
            renderer.material.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator enemyDestroy()
    {
        scoreManager.ScoreUp(transform.name);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
