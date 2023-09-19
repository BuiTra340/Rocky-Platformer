using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static int HpCurrent;
    public static int HpMax = 100;
    public static int damage = 5;
    public float moveSpeed;
    public float AttackSpeed =0.5f;
    public static int Critical;
    public static int CriticalDamage;
    public static int CoolDown;
    public float CoundownTimerAttackSpeed;
    private Animator anim;
    private bool canAttack;
    public static bool canMove; // false when collision with arrow 
    //
    public float thoigianno;
    //mo khoa ki nang
    public static bool unlockSkill1;
    public static bool unlockSkill2;
    public static bool unlockSkill3;
    private bool Fury; // tang damage khi fury
    public float timeToLoadSkill1;
    public float timeToLoadSkill2;
    public float timeToLoadSkill3;
    private float CountdownSkill1;
    private float CountdownSkill2;
    private float CountdownSkill3;

    public GameObject FuryPoint;
    public GameObject KnifeBullet;
    public Transform KnifePoint;
    public GameObject Skill3Prefab;
    public Image BarFillFury;
    public GameObject Shield;
    private float ShieldLife;
    public GameObject TextDamage;
    public Text textDame;
    public Image healtBar;
    public GameObject SkillNotUnlocked;
    AudioManager audio;

    public GameObject BannerPlayerDie;
    Player player;
    public static bool loadd;
    public Image Skill1FillCoolDown;
    public Image Skill2FillCoolDown;
    public Image Skill3FillCoolDown;
    public Image ShieldFillCoolDown;
    //
    public GameObject ObjecttextSkill1;
    public GameObject ObjecttextSkill2;
    public GameObject ObjecttextSkill3;
    public Text TextCoundownSkill1;
    public Text TextCoundownSkill2;
    public Text TextCoundownSkill3;
    // Start is called before the first frame update
    void Start()
    {
        if(!loadd)
        {
            CoolDown = 0;
            Critical = 1;
            CriticalDamage = 100;
            HpCurrent = 100;
            damage = 5;
            HpMax = 100;
        }
        canMove = true;
        CoundownTimerAttackSpeed = AttackSpeed;
        canAttack = true;
        anim = GetComponent<Animator>();
        audio = FindObjectOfType<AudioManager>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        healtBar.fillAmount = (float)HpCurrent / HpMax;
        if(canMove && !UpdatePlayer.isUpdating) transform.position += new Vector3(1 * moveSpeed * Time.deltaTime, 0, 0);
        CountDownSkill();
        checkAnimation();
        FillSkillLocked();
    }
    public void BtnATKNormalUp()
    {
        if (thoigianno >= 1f)
        {
            Fury = true;
            anim.SetTrigger("Fury");
        }
        CancelInvoke("ATKNormalDrag");
        FuryPoint.SetActive(false);
        thoigianno = 0;
        canAttack = true;
    }
    public void BtnATKNormalDown()
    {
        if (CoundownTimerAttackSpeed <= 0 && canAttack)
        {
            audio.PlaySFX(audio.attack);
            canAttack = false;
            anim.SetBool("Attack", true);
            CoundownTimerAttackSpeed = AttackSpeed;
            StartCoroutine(waitAttack());
        }
        InvokeRepeating("ATKNormalDrag", 0, 0.01f);
    }
    public void ATKNormalDrag()
    {
        thoigianno += Time.deltaTime;
        if (!FuryPoint.activeInHierarchy && canAttack)
        {
            thoigianno = 0;
            FuryPoint.SetActive(true);
            canAttack = false;
        }
        BarFillFury.fillAmount = (float)thoigianno / 1f;
    }
    public void BtnSkill1()
    {
        //skill 1
        if (CountdownSkill1 <= 0)
        {
            if (unlockSkill1)
            {
                CountdownSkill1 = (timeToLoadSkill1 - (timeToLoadSkill1 * CoolDown) / 100);
                anim.SetTrigger("Skill1");
                ObjecttextSkill1.SetActive(true);
            }
            else StartCoroutine(SkillnotUnlocked());
        }
    }
    public void BtnSkill2()
    {
        //Skill 2
        if (CountdownSkill2 <= 0)
        {
            if (unlockSkill2)
            {
                CountdownSkill2 = (timeToLoadSkill2 - (timeToLoadSkill2 * CoolDown) / 100);
                anim.SetTrigger("Skill2");
                Destroy(Instantiate(KnifeBullet, KnifePoint.position, Quaternion.identity),1.5f);
                ObjecttextSkill2.SetActive(true);

            }
            else StartCoroutine(SkillnotUnlocked());
        }
    }
    public void BtnSkill3()
    {
        //Skill 3
        if (CountdownSkill3 <= 0)
        {
            if (unlockSkill3)
            {
                if (RespawnEnemy.LevelGame % 5 != 0) FindClosestEnemy();
                else FindClosestBoss();
            }
            else StartCoroutine(SkillnotUnlocked());
        }
    }
    public void PlayerAttack()
    {
        //skill 1
        if (Input.GetKey(KeyCode.Alpha1) && CountdownSkill1 <= 0)
        {
            if (unlockSkill1)
            {
                CountdownSkill1 = (timeToLoadSkill1 - (timeToLoadSkill1 * CoolDown) / 100);
                anim.SetTrigger("Skill1");
                ObjecttextSkill1.SetActive(true);
            }
            else StartCoroutine(SkillnotUnlocked());
        }
        //Skill 2
        if (Input.GetKey(KeyCode.Alpha2) && CountdownSkill2 <= 0)
        {
            if (unlockSkill2)
            {
                CountdownSkill2 = (timeToLoadSkill2 - (timeToLoadSkill2 * CoolDown) / 100);
                anim.SetTrigger("Skill2");
                Destroy(Instantiate(KnifeBullet, KnifePoint.position, Quaternion.identity),1.5f);
                ObjecttextSkill1.SetActive(true);

            }
            else StartCoroutine(SkillnotUnlocked());
        }
        //Skill 3
        if (Input.GetKey(KeyCode.Alpha3) && CountdownSkill3 <= 0)
        {
            if (unlockSkill3)
            {
                if(RespawnEnemy.LevelGame % 5 != 0) FindClosestEnemy();
                else FindClosestBoss();

            }
            else StartCoroutine(SkillnotUnlocked());
        }
        // danh thuong, thêm bar nộ trên đầu player
        if (Input.GetKey(KeyCode.E) || Input.GetKeyUp(KeyCode.E))
        {
            if (Input.GetKeyDown(KeyCode.E) && CoundownTimerAttackSpeed <= 0 && canAttack)
            {
                audio.PlaySFX(audio.attack);
                canAttack = false;
                anim.SetBool("Attack", true);
                CoundownTimerAttackSpeed = AttackSpeed;
                StartCoroutine(waitAttack());
            }
            thoigianno += Time.deltaTime;
            if (thoigianno >= 1f && Input.GetKeyUp(KeyCode.E))
            {
                Fury = true;
                thoigianno = 0;
                anim.SetTrigger("Fury");
                FuryPoint.SetActive(false);
            }
            else if (thoigianno >= 0.2f)
            {
                if (!FuryPoint.activeInHierarchy) FuryPoint.SetActive(true);
                BarFillFury.fillAmount = (float)thoigianno / 1f;
            }
        }
        else
        {
            FuryPoint.SetActive(false);
            thoigianno = 0;
            CoundownTimerAttackSpeed -= Time.deltaTime * 10;
        }
    }
    private void CountDownSkill()
    {
        if (CoundownTimerAttackSpeed > 0) CoundownTimerAttackSpeed -= Time.deltaTime;

        if (ShieldLife > 0)
        {
            ShieldFillCoolDown.fillAmount = (float)ShieldLife / 3f;
            ShieldLife -= Time.deltaTime;
        }
        else Shield.SetActive(false);

        if (unlockSkill1 && CountdownSkill1 > 0)
        {
            Skill1FillCoolDown.fillAmount = (float)CountdownSkill1 / (timeToLoadSkill1 - (timeToLoadSkill1 * CoolDown) / 100);
            CountdownSkill1 -= Time.deltaTime;
            TextCoundownSkill1.text = CountdownSkill1.ToString("0.0");
        }
        else ObjecttextSkill1.SetActive(false);

        if (unlockSkill2 && CountdownSkill2 > 0)
        {
            Skill2FillCoolDown.fillAmount = (float)CountdownSkill2 / (timeToLoadSkill2 - (timeToLoadSkill2 * CoolDown) / 100);
            CountdownSkill2 -= Time.deltaTime;
            TextCoundownSkill2.text = CountdownSkill2.ToString("0.0");
        }
        else ObjecttextSkill2.SetActive(false);

        if (unlockSkill3 && CountdownSkill3 > 0)
        {
            Skill3FillCoolDown.fillAmount = (float)CountdownSkill3 / (timeToLoadSkill3 - (timeToLoadSkill3 * CoolDown) / 100);
            CountdownSkill3 -= Time.deltaTime;
            TextCoundownSkill3.text = CountdownSkill3.ToString("0.0");
        }
        else ObjecttextSkill3.SetActive(false);
    }
    IEnumerator waitAttack()
    {
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("Attack", false);
        canAttack = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int satthuong = damage;
        int rate = Random.Range(1, 101);
        Color a;
        if (rate == 100)
        {
            satthuong = damage * CriticalDamage/100;
            a = Color.red;
        }
        else a = Color.white;
        if (collision.gameObject.tag == "Enemy")
        {
            if (Fury)
            {
                collision.gameObject.GetComponent<Enemy>().textDame.fontSize = 45;
                collision.gameObject.GetComponent<Enemy>().textDame.color = a;
                collision.gameObject.GetComponent<Enemy>().takeDamage((int)(satthuong * 1.5f));
            }
            else
            {
                collision.gameObject.GetComponent<Enemy>().textDame.fontSize = 45;
                collision.gameObject.GetComponent<Enemy>().textDame.color = a;
                collision.gameObject.GetComponent<Enemy>().takeDamage(satthuong);
            }

        }
        if (collision.gameObject.tag == "Boss")
        {
            if (Fury)
            {
                collision.gameObject.GetComponent<Boss>().textDame.fontSize = 45;
                collision.gameObject.GetComponent<Boss>().textDame.color = a;
                collision.gameObject.GetComponent<Boss>().takeDamage((int)(satthuong * 1.5f));
            }
            else
            {
                collision.gameObject.GetComponent<Boss>().textDame.fontSize = 45;
                collision.gameObject.GetComponent<Boss>().textDame.color = a;
                collision.gameObject.GetComponent<Boss>().takeDamage(satthuong);
            }
        }

    }
    public void ActiveShield()
    {
        if(!Shield.activeInHierarchy && ShieldLife <=0)
        {
            ShieldLife =3f;
            Shield.SetActive(true);
        }
    }
    public void TakeDamage(int damage)
    {
        if(!Shield.activeInHierarchy)
        {
            textDame.text = "-" + damage;
            textDame.fontSize = 40;
            TextDamage.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0.8f, 0.4f, 0));
            TextDamage.SetActive(true);
            StartCoroutine(Destext());
            HpCurrent -= damage;
            if (HpCurrent > 0)
            {
                anim.SetTrigger("Hurt");
            }
            else
            {
                HpCurrent = 0;
                anim.Play("PLayer_Death");
                StartCoroutine(waitPlayerdie());
            }
        }
        else
        {
            textDame.text = "Blocked";
            textDame.fontSize = 30;
            TextDamage.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0.3f, 0f, 0));
            TextDamage.SetActive(true);
            StartCoroutine(Destext());
        }
    }
    private IEnumerator Destext()
    {
        yield return new WaitForSeconds(0.5f);
        TextDamage.SetActive(false);
    }
    public void Retry()
    {
        Time.timeScale = 1;
        if (RespawnEnemy.LevelGame > 1)
        {
            player.LoadWhenNextLevelorRetry();
        }
        else SceneManager.LoadScene(1);
        BannerPlayerDie.SetActive(false);
    }
    public void ExitPlayerDie()
    {
        Time.timeScale = 1;
        BannerPlayerDie.SetActive(false);
        SceneManager.LoadScene(0);
    }
    IEnumerator waitPlayerdie()
    {
        yield return new WaitForSeconds(0.7f);
        if (!BannerPlayerDie.activeInHierarchy)
        {
            BannerPlayerDie.SetActive(true);
        }
        Time.timeScale = 0;
    }
    private void FindClosestEnemy()
    {
        float setPostXSkill3 = 0;
        float distanceClosestEnemy = Mathf.Infinity;
        Enemy closestEnemy = null;
        Enemy[] allEnemies = GameObject.FindObjectsOfType<Enemy>();
        foreach (Enemy currentEnemy in allEnemies)
        {
            float distancetoEnemy = (currentEnemy.transform.position - transform.position).sqrMagnitude;
            //sap xep
            if (distancetoEnemy < distanceClosestEnemy)
            {
                distanceClosestEnemy = distancetoEnemy;
                closestEnemy = currentEnemy;
            }
        }
        if (distanceClosestEnemy < 10)
        {
            setPostXSkill3 = 1.5f;
        }
        if(closestEnemy !=null)
        {
            CountdownSkill3 = (timeToLoadSkill3 - (timeToLoadSkill3 * CoolDown) / 100);
            Vector3 Skill3Point = new Vector3(closestEnemy.transform.position.x + setPostXSkill3, closestEnemy.transform.position.y + 1.5f, closestEnemy.transform.position.z);
            Destroy(Instantiate(Skill3Prefab, Skill3Point, Quaternion.identity), 1f);
            ObjecttextSkill3.SetActive(true);
            //Debug.DrawLine(transform.position, closestEnemy.transform.position);
        }
    }
    private void FindClosestBoss()
    {
        float setPostXSkill3 = 0;
        float distanceClosestEnemy = Mathf.Infinity;
        Boss closestEnemy = GameObject.FindObjectOfType<Boss>();
        distanceClosestEnemy = (closestEnemy.transform.position - transform.position).sqrMagnitude;
        if (distanceClosestEnemy < 10)
        {
            setPostXSkill3 = 1.5f;
        }
        if (closestEnemy != null)
        {
            CountdownSkill3 = (timeToLoadSkill3 - (timeToLoadSkill3 * CoolDown) / 100);
            Vector3 Skill3Point = new Vector3(closestEnemy.transform.position.x + setPostXSkill3, closestEnemy.transform.position.y + 1.5f, closestEnemy.transform.position.z);
            Destroy(Instantiate(Skill3Prefab, Skill3Point, Quaternion.identity), 1f);
            ObjecttextSkill3.SetActive(true);
            //Debug.DrawLine(transform.position, closestEnemy.transform.position);
        }
    }
    //them vao animation Fury
    public void ResetDamage()
    {
        Fury = false;
    }
    private IEnumerator SkillnotUnlocked()
    {
        if(!SkillNotUnlocked.activeInHierarchy)
        {
            SkillNotUnlocked.SetActive(true);
            yield return new WaitForSeconds(1f);
            SkillNotUnlocked.SetActive(false);
        }
    }
    private void checkAnimation()
    {
        if (UpdatePlayer.isUpdating) anim.enabled = false;
        else anim.enabled = true;
    }
    void FillSkillLocked()
    {
        if (!unlockSkill1) Skill1FillCoolDown.fillAmount = 1;
        if (!unlockSkill2) Skill2FillCoolDown.fillAmount = 1;
        if (!unlockSkill3) Skill3FillCoolDown.fillAmount = 1;
    }
}
