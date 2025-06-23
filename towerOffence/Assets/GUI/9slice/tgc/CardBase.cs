using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardBase : MonoBehaviour
{
    [SerializeField] EnemyBase enemy;
    [SerializeField] Sprite[] cardBacks;
    [SerializeField] Sprite[] classIcons;

    [Header("GUI")]
    [SerializeField] TMP_Text manaCost;
    [SerializeField] TMP_Text minionName;
    [SerializeField] Image classIcon;
    [SerializeField] Image cardBack;
    [SerializeField] TMP_Text classTxt;
    [SerializeField] Image portrait;
    [SerializeField] TMP_Text levelTxt;

    [Header("Stats")]
    [SerializeField] Image atkImg;
    [SerializeField] TMP_Text atkTxt;
    [SerializeField] Image hpImg;
    [SerializeField] TMP_Text hpTxt;
    [SerializeField] Image spdImg;
    [SerializeField] TMP_Text spdTxt;
    [SerializeField] Image defImg;
    [SerializeField] TMP_Text defTxt;

    [Header("Level Up")]
    [SerializeField] TMP_Text xpTxt;
    [SerializeField] Image xpImg;
    [SerializeField] Sprite xpSpr;
    [SerializeField] GameObject lvlUp;

    private void Start()
    {
        if (enemy == null) { return; }

        minionName.text = enemy.enemyName;
        manaCost.text=enemy.cost.ToString();

        switch (enemy.enemyType)
        {
            case EnemyType.Basic:
                classIcon.sprite = classIcons[0];
                cardBack.sprite = cardBacks[0];
            break;

            case EnemyType.Demon:
                classIcon.sprite = classIcons[1];
                cardBack.sprite= cardBacks[1];
            break;
        }

        //if(xp>=upgCost) - activate upgButton.
    }



    public void LevelUp()
    {
        if (enemy == null) { return; }

        //xp-=upgCost, 
    }
}
