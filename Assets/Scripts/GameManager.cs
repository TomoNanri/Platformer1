using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager current;
    private bool SceneEscape = false;
    public bool gameClearFlg = false;
    public bool gameOverFlg = false;

    void Awake()
    {
        if (current == null) 
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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameClearFlg)
        {
            GameClear();
            return;

        }

        if (gameOverFlg)
        {
            // ScoreManager の DontDestroyOnLoad を解除
            SceneManager.MoveGameObjectToScene(GameObject.Find("ScoreManager"), SceneManager.GetActiveScene());

            // Manager の DontDestroyOnLoad を解除
            SceneManager.MoveGameObjectToScene(GameObject.Find("LifeManager"), SceneManager.GetActiveScene());

            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());

            SceneManager.LoadScene("GameOver");
        }

        if(SceneManager.GetActiveScene().name == "Stage1" && SceneEscape == true)
        {
            // Stage1 に戻った時に Player を ExitDokan の位置から飛び出させる
            GameObject Player = GameObject.Find("UnityChan");
            Rigidbody2D PlayerRigid = Player.GetComponent<Rigidbody2D>();
            Player.transform.position = GameObject.Find("ExitDokan").transform.position;
            PlayerRigid.AddForce(Vector2.up * 180.0f);

            SceneEscape = false;

        }
    }

    public void OnStartClick()      // Game 開始の Start ボタンの処理
    {
        SceneManager.LoadScene("Stage1");
    }

    public void changeSubScene(string beforeName, string DokanCategory)
    {
        if (beforeName == "Stage1" && DokanCategory == "Warp")  // 本来 State pattern で作るべき
        {
            SceneManager.LoadScene("Stage1-sub");
        }
        else if (beforeName == "Stage1-sub" && DokanCategory == "Escape")
        {
            SceneEscape = true;
            SceneManager.LoadScene("Stage1");
        }
    }

    public void GameClear()
    {
        Debug.Log("Game Clear");
    }
}
