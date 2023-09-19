using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuStart : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }
    public void newGame()
    {
        RespawnEnemy.LevelGame = 1;
        RespawnEnemy.loadRespawnEnemy = false;
        PlayerController.loadd = false;
        UpdatePlayer.loadUpdatePlayer = false;
        Enemy.load = false;
        StartCoroutine(wait());
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(1);
    }    
}
