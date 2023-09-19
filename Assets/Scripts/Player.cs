using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [HideInInspector]public int hpCurrent;
    [HideInInspector] public int hpMax;
    [HideInInspector] public int damage;
    [HideInInspector] public int Critical;
    [HideInInspector] public int CriticalDamage;
    [HideInInspector] public int CoolDown;
    //
    [HideInInspector] public int coin;
    [HideInInspector] public int lvHp;
    [HideInInspector] public int lvDamage;
    [HideInInspector] public int lvCritical;
    [HideInInspector] public int lvCriticalDamage;
    [HideInInspector] public int lvCoolDown;
    [HideInInspector] public int coinUpgradeHp;
    [HideInInspector] public int coinUpgradeDamage;
    [HideInInspector] public int coinUpgradeCritical;
    [HideInInspector] public int coinUpgradeCriticalDamage;
    [HideInInspector] public int coinUpgradeCoolDown;

    [HideInInspector] public int damagequai;
    //
    [HideInInspector] public int damageboss;
    [HideInInspector] public int coinkillboss;
    [HideInInspector] public int Level;
    // 
    [HideInInspector] public int HpEnemy;
    public void SavePlayer()
    {
        hpCurrent = PlayerController.HpCurrent;
        hpMax = PlayerController.HpMax;
        damage = PlayerController.damage;
        Critical = PlayerController.Critical;
        CriticalDamage = PlayerController.CriticalDamage;
        CoolDown = PlayerController.CoolDown;
        coin = UpdatePlayer.Coin;
        lvHp = UpdatePlayer.LvHp;
        lvDamage = UpdatePlayer.LvDamage;
        lvCritical = UpdatePlayer.LvCritical;
        lvCriticalDamage = UpdatePlayer.LvCriticalDamage;
        lvCoolDown = UpdatePlayer.LvCoolDown;
        coinUpgradeHp = UpdatePlayer.CoinUpgradeHp;
        coinUpgradeDamage = UpdatePlayer.CoinUpgradeDamage;
        coinUpgradeCritical = UpdatePlayer.CoinUpgradeCritical;
        coinUpgradeCriticalDamage = UpdatePlayer.CoinUpgradeCriticalDamage;
        coinUpgradeCoolDown = UpdatePlayer.CoinUpgradeCoolDown;
        Level = RespawnEnemy.LevelGame;
        //
        damagequai = Enemy.damage;
        HpEnemy = NextLevel.Hpenemy;
        //
        if (RespawnEnemy.LevelGame > 0 && RespawnEnemy.LevelGame % 5 == 0)
        {
            damageboss = Boss.damageBoss;
            coinkillboss = Boss.coinKillBoss;
        }
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data.Level >= 1)
        {
            PlayerController.loadd = true;
            UpdatePlayer.loadUpdatePlayer = true;
            RespawnEnemy.loadRespawnEnemy= true;
            Enemy.load = true;
            RespawnEnemy.LevelGame = data.Level;
            PlayerController.HpCurrent = data.hpCurrent;
            PlayerController.HpMax = data.hpMax;
            PlayerController.damage = data.damage;
            PlayerController.Critical = data.Critical;
            PlayerController.CriticalDamage = data.CriticalDamage;
            PlayerController.CoolDown = data.CoolDown;
            UpdatePlayer.Coin = data.coin;
            UpdatePlayer.LvHp = data.lvHp;
            UpdatePlayer.LvDamage = data.lvDamage;
            UpdatePlayer.LvCritical = data.lvCritical;
            UpdatePlayer.LvCriticalDamage = data.lvCriticalDamage;
            UpdatePlayer.LvCoolDown = data.lvCoolDown;
            UpdatePlayer.CoinUpgradeHp = data.coinUpgradeHp;
            UpdatePlayer.CoinUpgradeDamage = data.coinUpgradeDamage;
            UpdatePlayer.CoinUpgradeCritical = data.coinUpgradeCritical;
            UpdatePlayer.CoinUpgradeCriticalDamage = data.coinUpgradeCriticalDamage;
            UpdatePlayer.CoinUpgradeCoolDown = data.coinUpgradeCoolDown;
            //
            Enemy.damage = data.damagequai;
            NextLevel.Hpenemy = data.HpEnemy;
            SceneManager.LoadScene(1);
            //
            if (RespawnEnemy.LevelGame > 0 && RespawnEnemy.LevelGame % 5 == 0)
            {
                Boss.damageBoss = data.damageboss;
                Boss.coinKillBoss = data.coinkillboss;
            }
        }
    }

    //
    public void SaveWhenNextLevelorRetry()
    {
        hpCurrent = PlayerController.HpCurrent;
        hpMax = PlayerController.HpMax;
        damage = PlayerController.damage;
        Critical = PlayerController.Critical;
        CriticalDamage = PlayerController.CriticalDamage;
        CoolDown = PlayerController.CoolDown;
        coin = UpdatePlayer.Coin;
        lvHp = UpdatePlayer.LvHp;
        lvDamage = UpdatePlayer.LvDamage;
        lvCritical = UpdatePlayer.LvCritical;
        lvCriticalDamage = UpdatePlayer.LvCriticalDamage;
        lvCoolDown = UpdatePlayer.LvCoolDown;
        coinUpgradeHp = UpdatePlayer.CoinUpgradeHp;
        coinUpgradeDamage = UpdatePlayer.CoinUpgradeDamage;
        coinUpgradeCritical = UpdatePlayer.CoinUpgradeCritical;
        coinUpgradeCriticalDamage = UpdatePlayer.CoinUpgradeCriticalDamage;
        coinUpgradeCoolDown = UpdatePlayer.CoinUpgradeCoolDown;
        //
        damagequai = Enemy.damage;
        HpEnemy = NextLevel.Hpenemy;
        //
        if (RespawnEnemy.LevelGame > 0 && RespawnEnemy.LevelGame % 5 == 0)
        {
            damageboss = Boss.damageBoss;
            coinkillboss = Boss.coinKillBoss;
        }
        SaveSystem.SavePlayer(this);
    }
    public void LoadWhenNextLevelorRetry()
    {
        PlayerController.loadd = true;
        UpdatePlayer.loadUpdatePlayer = true;
        RespawnEnemy.loadRespawnEnemy = true;
        Enemy.load = true;
        PlayerData data = SaveSystem.LoadPlayer();
        PlayerController.HpCurrent = data.hpCurrent;
        PlayerController.HpMax = data.hpMax;
        PlayerController.damage = data.damage;
        PlayerController.Critical = data.Critical;
        PlayerController.CriticalDamage = data.CriticalDamage;
        PlayerController.CoolDown = data.CoolDown;
        UpdatePlayer.Coin = data.coin;
        UpdatePlayer.LvHp = data.lvHp;
        UpdatePlayer.LvDamage = data.lvDamage;
        UpdatePlayer.LvCritical = data.lvCritical;
        UpdatePlayer.LvCriticalDamage = data.lvCriticalDamage;
        UpdatePlayer.LvCoolDown = data.lvCoolDown;
        UpdatePlayer.CoinUpgradeHp = data.coinUpgradeHp;
        UpdatePlayer.CoinUpgradeDamage = data.coinUpgradeDamage;
        UpdatePlayer.CoinUpgradeCritical = data.coinUpgradeCritical;
        UpdatePlayer.CoinUpgradeCriticalDamage = data.coinUpgradeCriticalDamage;
        UpdatePlayer.CoinUpgradeCoolDown = data.coinUpgradeCoolDown;
        //
        Enemy.damage = data.damagequai;
        NextLevel.Hpenemy = data.HpEnemy;
        //
        if (RespawnEnemy.LevelGame > 0 && RespawnEnemy.LevelGame % 5 == 0)
        {
            Boss.damageBoss = data.damageboss;
            Boss.coinKillBoss = data.coinkillboss;
        }
        SceneManager.LoadScene(1);
    }
}
