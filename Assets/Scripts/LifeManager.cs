using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public static LifeManager current;
    public int Life = 5;
    Text lifeText;

    void Awake()
    {
        if(current == null)
        {
            current = this;
            DontDestroyOnLoad(this);
        }
        else if(current != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
        lifeText.text = "Life: " + Life.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
        lifeText.text = "Life: " + Life.ToString();
        if (Life == 0)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().gameOverFlg = true;
        }
    }

    public void LifeDamage()
    {
        Life -= 1;
        lifeText.text = "Life: " + Life.ToString();
    }
}
