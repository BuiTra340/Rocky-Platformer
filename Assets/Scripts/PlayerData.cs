using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int hpCurrent;
    public int hpMax;
    public int damage;
    public int Critical;
    public int CriticalDamage;
    public int CoolDown;
    //
    public int coin;
    public int lvHp;
    public int lvDamage;
    public int lvCritical;
    public int lvCriticalDamage;
    public int lvCoolDown;
    public int coinUpgradeHp;
    public int coinUpgradeDamage;
    public int coinUpgradeCritical;
    public int coinUpgradeCriticalDamage;
    public int coinUpgradeCoolDown;
    //
    public int damagequai;
    public int HpEnemy;
    //
    public int damageboss;
    public int coinkillboss;

    public int Level;
    public PlayerData(Player player)
    {
        hpCurrent = player.hpCurrent;
        hpMax = player.hpMax;
        damage = player.damage;
        Critical = player.Critical;
        CriticalDamage = player.CriticalDamage;
        CoolDown = player.CoolDown;
        coin = player.coin;
        lvHp = player.lvHp;
        lvDamage = player.lvDamage;
        lvCritical = player.lvCritical;
        lvCriticalDamage = player.lvCriticalDamage;
        lvCoolDown = player.lvCoolDown;
        coinUpgradeHp = player.coinUpgradeHp;
        coinUpgradeDamage = player.coinUpgradeDamage;
        coinUpgradeCritical = player.coinUpgradeCritical;
        coinUpgradeCriticalDamage = player.coinUpgradeCriticalDamage;
        coinUpgradeCoolDown = player.coinUpgradeCoolDown;
        Level = player.Level;
        //
        damagequai = player.damagequai;
        HpEnemy = player.HpEnemy;
        if (Level > 0 && Level % 5 == 0)
        {
            damageboss = player.damageboss;
            coinkillboss = player.coinkillboss;
        }
    }
}
