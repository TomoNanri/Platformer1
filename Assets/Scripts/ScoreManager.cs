using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int Score;
    Text scoreText;
    public static ScoreManager current;

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
        Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        scoreText.text = "Score: " + Score.ToString("#,0");
    }

    public void ScoreUp(string name)
    {
        if (name == "cherry")
            Score += 200;
        else if (name == "gem")
            Score += 300;
        else if (name == "biggem")
            Score += 1000;
        else if (name == "frogHighJumpNonTargeting")
            Score += 400;
        else if (name == "frogWideJumpNonTargeting")
            Score += 400;
        else if (name == "frogHighJumpTargeting")
            Score += 500;
        else if (name == "frogWideJumpTargeting")
            Score += 500;
        else if (name == "eagleUpDown")
            Score += 500;
        else if (name == "eagleAttack")
            Score += 700;
        else if (name == "opossumWalkNonTargeting")
            Score += 500;
        else if (name == "opossumRunNonTargeting")
            Score += 700;
        else if (name == "opossumTargeting")
            Score += 1500;
    }
}
