using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    private Animator anim;
    private bool bitancong;
    private bool isAttacking;
    public float RangeAttack;
    public float moveSpeed;
    public float distance;
    public int HpBosshientai;
    public static int damageBoss;
    public static int coinKillBoss;
    public float lucday;

    private Rigidbody2D rb;
    PlayerController player;
    public GameObject TextDamage;
    public Text textDame;
    public Vector3 Offset;
    public GameObject ArrowNextLevel;
    public Image FillHpBar;
    // Start is called before the first frame update
    void Start()
    {
        HpBosshientai = NextLevel.Hpenemy *10;
        damageBoss = (int)(Enemy.damage * RespawnEnemy.LevelGame);
        coinKillBoss = RespawnEnemy.LevelGame * 15;
        player = GameObject.FindObjectOfType<PlayerController>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        FillHpBar.fillAmount = (float)HpBosshientai / (NextLevel.Hpenemy * 10);
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (!bitancong && distance >= 1.5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }

        if (!isAttacking && distance <= RangeAttack)
        {
            StartCoroutine(EnemyAttack());
        }
        checkAnimation();
    }
    private IEnumerator EnemyAttack()
    {
        isAttacking = true;
        anim.SetBool("Attack", true);
        //thoi gian chuyen anim attack ve idle
        yield return new WaitForSeconds(0.5f);
        if (Vector2.Distance(transform.position, player.transform.position) <= RangeAttack && HpBosshientai > 0)
        {
            player.TakeDamage(damageBoss);
        }
        anim.SetBool("Attack", false);
        //thoi gian hoi attack
        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }
    public void takeDamage(int damage)
    {
        textDame.text = "-" + damage;
        TextDamage.transform.position = Camera.main.WorldToScreenPoint(transform.position + Offset);
        TextDamage.SetActive(true);
        StartCoroutine(Destext());
        HpBosshientai -= damage;
        if (HpBosshientai <= 0)
        {
            StartCoroutine(TimeDestroyEnemy());
        }
        else
        {
            anim.SetTrigger("Hurt");
            // anim.SetBool("Attack", false);
            bitancong = true;
            Vector2 distance = (transform.position - player.transform.position).normalized;
            rb.AddForce(distance * lucday, ForceMode2D.Impulse);
            StartCoroutine(timeKnockBack());
        }
    }
    private IEnumerator TimeDestroyEnemy()
    {
        HpBosshientai = 0;
        anim.SetTrigger("Hurt");
        anim.SetBool("Attack", false);
        anim.SetTrigger("Death");
        bitancong = true;
        yield return new WaitForSeconds(0.5f);
        UpdatePlayer.Coin += coinKillBoss;
        Instantiate(ArrowNextLevel, new Vector3(player.transform.position.x + 20, player.transform.position.y, 0), Quaternion.identity);
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
    private void checkAnimation()
    {
        if (UpdatePlayer.isUpdating) anim.enabled = false;
        else anim.enabled = true;
    }
}
