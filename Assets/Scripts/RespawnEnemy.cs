using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] EnemyPrefab;
    [SerializeField] private GameObject[] BossPrefab;
    [SerializeField] private float timeSpawn;
    public static int Slquai;
    public static int Slquai_max;
    public static int killquai;
    public static int LevelGame;
    public static int dotquaitancong;

    public static bool loadRespawnEnemy;
    public static bool checkdotquai;
    PlayerController player;
    public GameObject Bannerlv;
    public Text Lvtext;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        if (!loadRespawnEnemy)
        {
            LevelGame = 1;
            Slquai = 0;
            killquai = 0;
            Slquai_max = 3;
            dotquaitancong = 1;
        }
        StartCoroutine(SpawnEnemy());
        StartCoroutine(ShowBannerLv());
    }

    public void Checkdotquai()
    {
        if (killquai > 0 && killquai % Slquai_max == 0 && checkdotquai)
        {
            checkdotquai = false;
            dotquaitancong++;
            if (dotquaitancong <= 10)
            {
                StartCoroutine(SpawnEnemy());
            }
        }
    }
    private IEnumerator SpawnEnemy()
    {
        if (LevelGame > 0 && LevelGame % 5 !=0)
        {
            while (Slquai < Slquai_max)
            {
                Slquai += 1;
                int a = Random.Range(0, EnemyPrefab.Length);
                Instantiate(EnemyPrefab[a], new Vector3(player.transform.position.x + Random.Range(16, 20), player.transform.position.y, 0), Quaternion.identity);
                yield return new WaitForSeconds(timeSpawn);
            }
            Slquai = 0;
        }
        else
        {
            int a = Random.Range(0, BossPrefab.Length);
            Instantiate(BossPrefab[a], new Vector3(player.transform.position.x + Random.Range(16, 20), player.transform.position.y, 0), Quaternion.identity);
        }
    }
    IEnumerator ShowBannerLv()
    {
        Lvtext.text = "Level " + LevelGame;
        Bannerlv.SetActive(true);
        yield return new WaitForSeconds(2f);
        Bannerlv.SetActive(false);
    }
}
