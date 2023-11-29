using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
public class HUD : MonoSingleton<HUD>
{
    Ally player;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text HealtText;
    [SerializeField] private RectTransform healtBar;
    [SerializeField] private Transform ArmorBar;
    [SerializeField] private TMP_Text XPText;
    [SerializeField] private RectTransform XPBar;
    [SerializeField] private TMP_Text foodText;
    [SerializeField] private TMP_Text woodText;
    [SerializeField] private TMP_Text peapleText;

    private void Start()
    {
       player = GameManager.Instance.playerActor;
        ResourcesSet();
        HealtSet();
        LevelSet();
    }
    public void ResourcesSet()
    {
          foodText.text = GameManager.Instance.Food.ToString();
          woodText.text = GameManager.Instance.Wood.ToString();
          peapleText.text = GameManager.Instance.peapleCount.ToString();
    }
    public void LevelSet()
    {
        levelText.text = player.Level.ToString();
        XPBar.DOScaleX(player.xp / GameManager.Instance.xpLevelUpLimits[player.Level - 1], .5f);
        XPText.text = player.xp + "/" + GameManager.Instance.xpLevelUpLimits[player.Level - 1];
    }
    public void  HealtSet()
    {
        healtBar.DOScaleX((float)player.currentHealth / (float)player.maxHealth, .5f);
        HealtText.text = player.currentHealth + "/" + player.maxHealth;
    }
    public void ArmorBarSet()
    {

        if (player.armor > 0)
        {
            ArmorBar.gameObject.SetActive(true);
            for (int i = 0; i < ArmorBar.childCount; i++)
            {
                if (i < player.armor)
                    ArmorBar.GetChild(i).gameObject.SetActive(true);
                else
                    ArmorBar.GetChild(i).gameObject.SetActive(false);
            }
        }
        else
        {
            ArmorBar.gameObject.SetActive(false);
        }

    }
}