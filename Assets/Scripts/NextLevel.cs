using System.Collections;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    Player player;
    FadeScreen fadeScreen;
    Enemy enemy;
    public static int Hpenemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = FindObjectOfType<Enemy>();
        player = FindObjectOfType<Player>();
        fadeScreen = FindObjectOfType<FadeScreen>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(NextMap());
        }
    }
    IEnumerator NextMap()
    {
        if(RespawnEnemy.LevelGame % 5 == 0)
        {
            RespawnEnemy.Slquai_max++;
        }
        PlayerController.canMove = false;
        checkHpEnemies();
        fadeScreen.FadeOutScreen();
        yield return new WaitForSeconds(1f);
        RespawnEnemy.killquai = 0;
        RespawnEnemy.LevelGame += 1;
        RespawnEnemy.Slquai = 0;
        RespawnEnemy.dotquaitancong = 1;
        if (RespawnEnemy.LevelGame == 5)
        {
            PlayerController.unlockSkill1 = true;
        }
        else if (RespawnEnemy.LevelGame == 10)
        {
            PlayerController.unlockSkill2 = true;
        }
        else if (RespawnEnemy.LevelGame == 15)
        {
            PlayerController.unlockSkill3 = true;
        }
        player.SaveWhenNextLevelorRetry();
        player.LoadWhenNextLevelorRetry();
        PlayerController.HpCurrent = PlayerController.HpMax;
    }
    private void checkHpEnemies()
    {
        if (PlayerController.unlockSkill1) Hpenemy = PlayerController.damage * 3;
        else if (PlayerController.unlockSkill2) Hpenemy = PlayerController.damage * 4;
        else if (PlayerController.unlockSkill3) Hpenemy = PlayerController.damage * 5;

        if (RespawnEnemy.LevelGame <= 4) Hpenemy = PlayerController.damage * 2;
    }
}
