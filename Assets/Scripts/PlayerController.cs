using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 1.5f;
    public float jumpPower = 180f;
    private CapsuleCollider2D m_capsuleCollider2D;
    private Rigidbody2D m_rigidbody2D;
    Animator anim;

    ScoreManager scoreManager;
    private bool InCheckFlg = false;
    private bool EscapeFlg = false;

    public LifeManager lifeManager;

    bool isGround = false;

    AudioSource SE;
    public AudioClip jumpVoice;
    public AudioClip attackVoice;
    public AudioClip damageVoice;
    public AudioClip itemGetSE;

    bool isDamege = false;

    void Awake()
    {
        m_capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        lifeManager = GameObject.Find("LifeManager").GetComponent<LifeManager>();
        SE = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        bool jump = Input.GetButtonDown("Jump");
        Move(x, jump);

        if (InCheckFlg)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                transform.position = new Vector3(GameObject.Find("WarpDokan").transform.position.x,
                    transform.position.y, transform.position.z);
                GetComponent<CapsuleCollider2D>().enabled = false;
                StartCoroutine("Warp");
            }
        }

        if (EscapeFlg)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                transform.position = new Vector3(transform.position.x,
                    GameObject.Find("EscapeDokan").transform.position.y, transform.position.z);
                GetComponent<CapsuleCollider2D>().enabled = false;
                transform.Translate(Vector2.right * 0.2f);
                StartCoroutine("EscapeWarp");
            }
        }
    }

    private void FixedUpdate()
    {
        Ray();
    }

    void Move(float move, bool jump)
    {
        if(Mathf.Abs(move) > 0)
        {
            Quaternion rot = transform.rotation;
            transform.rotation = Quaternion.Euler(rot.x, Mathf.Sign(move) == 1 ? 0f : 180f, rot.z);
        }

        m_rigidbody2D.velocity = new Vector2(move * maxSpeed, m_rigidbody2D.velocity.y);
        anim.SetFloat("run", Mathf.Abs(move));

        if (jump)
        {
            if(isGround)
            {
                m_rigidbody2D.AddForce(Vector2.up * jumpPower);
                SE.PlayOneShot(jumpVoice);
                anim.SetTrigger("jump");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string ObjectName = collision.gameObject.tag;
        if(ObjectName == "cherry" || ObjectName == "gem" || ObjectName == "biggem")
        {
            SE.PlayOneShot(itemGetSE);

            scoreManager.ScoreUp(ObjectName);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.name == "InCheckCol")
        {
            InCheckFlg = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.name == "InCheckCol")
        {
            InCheckFlg = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        string objectName = collision.gameObject.name;
        if(objectName == "EscapeDokan")
        {
            EscapeFlg = true;
        }

        string layerName = LayerMask.LayerToName(collision.gameObject.layer);
        if(layerName == "Enemy")
        {
            if (!isDamege)
            {
                SE.PlayOneShot(damageVoice);
                m_rigidbody2D.velocity = Vector2.up * 3f;
                StartCoroutine("Damage");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 接触相手が敵かしらべる
        string layerName = LayerMask.LayerToName(collision.gameObject.layer);

        if(layerName == "Enemy")
        {
            //Vector3 m_capsuleCollider2D_pos = m_capsuleCollider2D.transform.position;
            float player_ypos = transform.position.y;

            float m_capsuleCollider2D_hight = m_capsuleCollider2D.bounds.size.y;

            float player_footpos = player_ypos - (m_capsuleCollider2D_hight / 2.0f);

            float enemy_pos = collision.gameObject.transform.position.y;

            if(enemy_pos < player_footpos)
            {
                //Debug.Log("Squash Hit");
                if (collision.gameObject.GetComponent<EnemyDamage>().isDamage == false)
                {
                    SE.PlayOneShot(attackVoice);
                    m_rigidbody2D.velocity = Vector3.zero;
                    m_rigidbody2D.AddForce(Vector2.up * 110.0f);
                    collision.gameObject.GetComponent<EnemyDamage>().isDamage = true;
                    collision.gameObject.layer = LayerMask.NameToLayer("DamageEnemy");
                }
            }
            else
            {
                if (!isDamege)
                {
                    //Debug.Log("Other Hit");
                    SE.PlayOneShot(damageVoice);
                    m_rigidbody2D.velocity = Vector2.up * 3f;
                    StartCoroutine("Damage");
                }
            }
        }
    }

    IEnumerator Warp()
    {
        yield return new WaitForSeconds(1.0f);

        GameObject.Find("GameManager").GetComponent<GameManager>().changeSubScene(SceneManager.GetActiveScene().name, "Warp");
    }

    IEnumerator EscapeWarp()
    {
        yield return new WaitForSeconds(1.0f);

        GameObject.Find("GameManager").GetComponent<GameManager>().changeSubScene(SceneManager.GetActiveScene().name, "Escape");
    }

    IEnumerator Damage()
    {
        Renderer renderer;
        renderer = GetComponent<Renderer>();

        isDamege = true;

        int count = 10;

        while(count > 0)
        {
            renderer.material.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.05f);
            renderer.material.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.05f);
            count--;
        }
        lifeManager.LifeDamage();
        isDamege = false;
    }

    void Ray()
    {
        float RayDistance = 0.02f;  // Ray の長さ

        string[] MaskNameList = new string[] { "Ground", "Dokan" };
        int mask = LayerMask.GetMask(MaskNameList);

        float collider_hight = m_capsuleCollider2D.bounds.size.y;
        float collider_width = m_capsuleCollider2D.bounds.size.x;

        Vector2 collider_center_pos = m_capsuleCollider2D.bounds.center;

        float collider_left_pos = collider_center_pos.x - collider_width / 2;
        float collider_right_pos = collider_center_pos.x + collider_width / 2;
        float collider_under_pos = collider_center_pos.y - collider_hight / 2;

        // Ray 発射地点の座標を作る
        Vector2 rayOriginPoint_left = new Vector2(collider_left_pos, collider_under_pos);
        Vector2 rayOriginPoint_center = new Vector2(collider_center_pos.x, collider_under_pos);
        Vector2 rayOriginPoint_right = new Vector2(collider_right_pos, collider_under_pos);

        Ray ray_left = new Ray(rayOriginPoint_left, Vector2.down);
        Ray ray_center = new Ray(rayOriginPoint_center, Vector2.down);
        Ray ray_right = new Ray(rayOriginPoint_right, Vector2.down);

        RaycastHit2D hit_left = Physics2D.Raycast(rayOriginPoint_left, ray_left.direction, RayDistance, mask);
        RaycastHit2D hit_center = Physics2D.Raycast(rayOriginPoint_center, ray_center.direction, RayDistance, mask);
        RaycastHit2D hit_right = Physics2D.Raycast(rayOriginPoint_right, ray_right.direction, RayDistance, mask);

        // Ray のデバッグ用
        Color color = new Color(1f, 0f, 0f);
        Debug.DrawRay(rayOriginPoint_left, ray_left.direction * RayDistance, color);
        Debug.DrawRay(rayOriginPoint_center, ray_center.direction * RayDistance, color);
        Debug.DrawRay(rayOriginPoint_right, ray_right.direction * RayDistance, color);

        if (hit_left || hit_center || hit_right)
            isGround = true;
        else
            isGround = false;
    }
}
