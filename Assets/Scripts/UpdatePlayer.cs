using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpdatePlayer : MonoBehaviour
{
    public static int Coin;
    public Text TextCoin;

    public static int LvDamage;
    public static int LvHp;
    public static int LvCritical;
    public static int LvCriticalDamage;
    public static int LvCoolDown;

    public static int CoinUpgradeDamage;
    public static int CoinUpgradeHp;
    public static int CoinUpgradeCritical;
    public static int CoinUpgradeCriticalDamage;
    public static int CoinUpgradeCoolDown;

    public Text ChisoDamage;
    public Text ChisoHp;
    public Text ChisoCritical;
    public Text ChisoCriticalDamage;
    public Text ChisoCoolDown;

    public GameObject Ncapthanhcong;
    public GameObject Ncapthatbai;
    public GameObject bannerNcap;
    public GameObject BannerPause;

    public static bool loadUpdatePlayer;
    public Button BtnShowBannerNcap;
    AudioManager audio;
    public static bool isUpdating;
    // Start is called before the first frame update
    void Start()
    {
        if (!loadUpdatePlayer)
        {
            Coin = 0;
            LvDamage = 0;
            LvHp = 0;
            LvCritical = 0;
            LvCriticalDamage = 0;
            LvCoolDown = 0;
            CoinUpgradeDamage = 15;
            CoinUpgradeHp = 15;
            CoinUpgradeCritical = 20;
            CoinUpgradeCriticalDamage = 20;
            CoinUpgradeCoolDown = 15;
        }
        audio = GameObject.FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ChisoDamage.text = "Damage " + PlayerController.damage + " -> " + (PlayerController.damage + 5) +
            "\nUpgrade " + CoinUpgradeDamage + " Diamonds";
        ChisoHp.text = "Hp " + PlayerController.HpMax + " -> " + (PlayerController.HpMax + 20) +
            "\nUpgrade " + CoinUpgradeHp + " Diamonds";

        //

        if (LvCritical < 70)
        {
            ChisoCritical.text = "Critical " + PlayerController.Critical + "% -> " +(PlayerController.Critical + 1) +
                "%\nUpgrade " + CoinUpgradeCritical + " Diamonds";
        }
        else
        {
            ChisoCritical.text = "Critical " + PlayerController.Critical +
                "%\nMax";
        }

        if(LvCriticalDamage < 100)
        {
            ChisoCriticalDamage.text = "Crit Damage " + PlayerController.CriticalDamage + "% -> " + (PlayerController.CriticalDamage + 1) +
                "%\nUpgrade " + CoinUpgradeCriticalDamage + " Diamonds";
        }else
        {
            ChisoCriticalDamage.text = "Crit Damage " + PlayerController.CriticalDamage +
                "%\nMax";
        }

        if (LvCoolDown < 40)
        {
            ChisoCoolDown.text = "CoolDown Reduction " + PlayerController.CoolDown + "% -> " + (PlayerController.CoolDown + 1) +
                "%\nUpgrade " + CoinUpgradeCoolDown + " Diamonds";
        }
        else
        {
            ChisoCoolDown.text = "CoolDown Reduction " + PlayerController.CriticalDamage +
                "%\nMax";
        }

        TextCoin.text = "" + Coin;
    }

    public void NcapDamage()
    {
        Time.timeScale = 1;
        if (Coin >= CoinUpgradeDamage)
        {
            Coin -= CoinUpgradeDamage;
            LvDamage += 1;
            PlayerController.damage += 5;
            CoinUpgradeDamage += 5;
            audio.PlaySFX(audio.Ncapthanhcong);
            if (!Ncapthanhcong.activeInHierarchy)
            {
                Ncapthanhcong.SetActive(true);
                StartCoroutine(Desbannerthongbao(Ncapthanhcong));
            }
        }
        else
        {
            audio.PlaySFX(audio.Ncapthatbai);
            if (!Ncapthatbai.activeInHierarchy)
            {
                Ncapthatbai.SetActive(true);
                StartCoroutine(Desbannerthongbao(Ncapthatbai));
            }
        }
    }
    public void NcapHp()
    {
        Time.timeScale = 1;
        if (Coin >= CoinUpgradeHp)
        {
            Coin -= CoinUpgradeHp;
            LvHp += 1;
            PlayerController.HpMax += 5;
            CoinUpgradeHp += 2;
            audio.PlaySFX(audio.Ncapthanhcong);
            if (!Ncapthanhcong.activeInHierarchy)
            {
                Ncapthanhcong.SetActive(true);
                StartCoroutine(Desbannerthongbao(Ncapthanhcong));
            }
        }
        else
        {
            audio.PlaySFX(audio.Ncapthatbai);
            if (!Ncapthatbai.activeInHierarchy)
            {
                Ncapthatbai.SetActive(true);
                StartCoroutine(Desbannerthongbao(Ncapthatbai));
            }
        }
    }
    public void NcapCritical()
    {
        Time.timeScale = 1;
        if (LvCritical < 700)
        {
            if (Coin >= CoinUpgradeCritical)
            {
                Coin -= CoinUpgradeCritical;
                LvCritical += 1;
                PlayerController.Critical += 1;
                CoinUpgradeCritical += 10;
                audio.PlaySFX(audio.Ncapthanhcong);
                if (!Ncapthanhcong.activeInHierarchy)
                {
                    Ncapthanhcong.SetActive(true);
                    StartCoroutine(Desbannerthongbao(Ncapthanhcong));
                }
            }
            else
            {
                audio.PlaySFX(audio.Ncapthatbai);
                if (!Ncapthatbai.activeInHierarchy)
                {
                    Ncapthatbai.SetActive(true);
                    StartCoroutine(Desbannerthongbao(Ncapthatbai));
                }
            }
        }
    }
    public void NcapCriticalDamage()
    {
        Time.timeScale = 1;
        if (LvCriticalDamage < 100)
        {
            if (Coin >= CoinUpgradeCriticalDamage)
            {
                Coin -= CoinUpgradeCriticalDamage;
                LvCriticalDamage += 1;
                PlayerController.CriticalDamage += 1;
                CoinUpgradeCriticalDamage += 2;
                audio.PlaySFX(audio.Ncapthanhcong);
                if (!Ncapthanhcong.activeInHierarchy)
                {
                    Ncapthanhcong.SetActive(true);
                    StartCoroutine(Desbannerthongbao(Ncapthanhcong));
                }
            }
            else
            {
                audio.PlaySFX(audio.Ncapthatbai);
                if (!Ncapthatbai.activeInHierarchy)
                {
                    Ncapthatbai.SetActive(true);
                    StartCoroutine(Desbannerthongbao(Ncapthatbai));
                }
            }
        }
    }

    public void NcapCoolDown()
    {
        Time.timeScale = 1;
        if (LvCoolDown < 100)
        {
            if (Coin >= CoinUpgradeCoolDown)
            {
                Coin -= CoinUpgradeCoolDown;
                LvCoolDown += 1;
                PlayerController.CoolDown += 1;
                CoinUpgradeCoolDown += 5;
                audio.PlaySFX(audio.Ncapthanhcong);
                if (!Ncapthanhcong.activeInHierarchy)
                {
                    Ncapthanhcong.SetActive(true);
                    StartCoroutine(Desbannerthongbao(Ncapthanhcong));
                }
            }
            else
            {
                audio.PlaySFX(audio.Ncapthatbai);
                if (!Ncapthatbai.activeInHierarchy)
                {
                    Ncapthatbai.SetActive(true);
                    StartCoroutine(Desbannerthongbao(Ncapthatbai));
                }
            }
        }
    }
    public void BtnExitBannerPause()
    {
        Time.timeScale = 1;
        BannerPause.SetActive(false);
        SceneManager.LoadScene(0);
    }
    IEnumerator Desbannerthongbao(GameObject banner)
    {
        yield return new WaitForSeconds(1f);
        banner.SetActive(false);
        if (bannerNcap.activeInHierarchy)
        {
            Time.timeScale = 0;
        }
    }
    public void ShowBannerNcap()
    {
        if (!bannerNcap.activeInHierarchy)
        {
            isUpdating = true;
            bannerNcap.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            isUpdating = false;
            bannerNcap.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    public void showOrDesBannerPause()
    {
        if (!BannerPause.activeInHierarchy)
        {
            BtnShowBannerNcap.interactable = false;
            Time.timeScale = 0;
            BannerPause.SetActive(true);

        }
        else
        {
            BtnShowBannerNcap.interactable = true;
            Time.timeScale = 1;
            BannerPause.SetActive(false);
            if (bannerNcap.activeInHierarchy)
            {
                Time.timeScale = 0;
            }
        }
    }
    public void Resume()
    {
        if (BannerPause.activeInHierarchy)
        {
            Time.timeScale = 1;
            BannerPause.SetActive(false);
        }
    }
}
