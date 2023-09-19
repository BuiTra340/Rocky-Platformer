using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Animator anim;
    private bool bitancong;
    private bool isAttacking;
    public float RangeAttack;
    public float moveSpeed;
    public float distance;
    public int Hphientai;
    public static int damage;
    private int coinKillquai;
    public static bool load;
    public float lucday;

    private Rigidbody2D rb;
    PlayerController player;
    public GameObject TextDamage;
    public Text textDame;
    public Vector3 Offset;
    RespawnEnemy respawnEnemy;
    public GameObject ArrowNextLevel;
    // Start is called before the first frame update
    void Start()
    {
        if (!load)
        {
            Hphientai = 10;
            damage = 10;
        }
        else Hphientai = NextLevel.Hpenemy;
        player = GameObject.FindObjectOfType<PlayerController>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        respawnEnemy = GameObject.FindObjectOfType<RespawnEnemy>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (!bitancong && distance >= 1.5f && !UpdatePlayer.isUpdating)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }

        if (!isAttacking && distance <= RangeAttack && !UpdatePlayer.isUpdating)
        {
            StartCoroutine(EnemyAttack());
        }
        checkAnimation();
    }
    private void checkAnimation()
    {
        if (UpdatePlayer.isUpdating) anim.enabled = false;
        else anim.enabled = true;
    }
    private IEnumerator EnemyAttack()
    {
        isAttacking = true;
        anim.SetBool("Attack", true);
        //thoi gian chuyen anim attack ve idle
        yield return new WaitForSeconds(0.5f);
        if (Vector2.Distance(transform.position, player.transform.position) <= RangeAttack && Hphientai > 0)
        {
            player.TakeDamage((int)(damage * RespawnEnemy.LevelGame) / 2);
        }
        anim.SetBool("Attack", false);
        //thoi gian hoi attack
        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }
    public void takeDamage(int damage)
    {
        if(!bitancong)
        {
            textDame.text = "-" + damage;
            TextDamage.transform.position = Camera.main.WorldToScreenPoint(transform.position + Offset);
            TextDamage.SetActive(true);
            StartCoroutine(Destext());
            Hphientai -= damage;
            if (Hphientai <= 0)
            {
                StartCoroutine(TimeDestroyEnemy());
            }
            else
            {
                anim.SetTrigger("Hurt");
                // anim.SetBool("Attack", false);
                bitancong = true;
                Vector2 Distance = (transform.position - player.transform.position).normalized;
                rb.AddForce(Distance * lucday, ForceMode2D.Impulse);
                StartCoroutine(timeKnockBack());
            }
        }
    }
    private IEnumerator TimeDestroyEnemy()
    {
        anim.SetTrigger("Hurt");
        anim.SetBool("Attack", false);
        anim.SetTrigger("Death");
        bitancong = true;
        yield return new WaitForSeconds(0.5f);
        IncreaseCoins();
        RespawnEnemy.killquai++;
        RespawnEnemy.checkdotquai = true;
        respawnEnemy.Checkdotquai();
        if(RespawnEnemy.killquai == RespawnEnemy.Slquai_max*10)
        {
            Instantiate(ArrowNextLevel,new Vector3(player.transform.position.x + 20,player.transform.position.y , 0), Quaternion.identity);
        }
        Destroy(this.gameObject);
    }
    private IEnumerator timeKnockBack()
    {
        yield return new WaitForSeconds(0.2f);
        rb.velocity = Vector2.zero;
        bitancong = false;
    }
    private IEnumerator Destext()
    {
        yield return new WaitForSeconds(0.5f);
        TextDamage.SetActive(false);
    }
    private void IncreaseCoins()
    {
        coinKillquai = Random.Range(5, 9);
        coinKillquai = coinKillquai * RespawnEnemy.LevelGame;
        UpdatePlayer.Coin += coinKillquai;
    }
}
