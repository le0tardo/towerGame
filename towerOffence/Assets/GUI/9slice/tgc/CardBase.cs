using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardBase : MonoBehaviour
{
    [SerializeField] public EnemyBase enemy;
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
    [SerializeField] TMP_Text coolDownText;

    [Header("Stats")]
    [SerializeField] Image atkImg;
    [SerializeField] TMP_Text atkTxt;
    [SerializeField] Image hpImg;
    [SerializeField] TMP_Text hpTxt;
    [SerializeField] Image spdImg;
    [SerializeField] TMP_Text spdTxt;
    [SerializeField] Image defImg;
    [SerializeField] TMP_Text defTxt;

    [Header("Icons")]
    [SerializeField] Sprite[] atkSprite;
    [SerializeField] Sprite hpSprite;
    [SerializeField] Sprite spdSprite;
    [SerializeField] Sprite[] defSprite;

    [Header("Level Up")]
    [SerializeField] TMP_Text xpTxt;
    [SerializeField] Image xpImg;
    [SerializeField] Sprite xpSpr;
    [SerializeField] GameObject lvlUp;
    [SerializeField] Button lvlUpBtn;

    float xpScaleFactor = 1.33f;

    private void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        if (enemy == null) { return; }

        minionName.text = enemy.enemyName;
        manaCost.text = enemy.cost.ToString();
        levelTxt.text = enemy.level.ToString();
        portrait.sprite = enemy.enemyCardIcon;
        coolDownText.text=enemy.coolDown.ToString();

        atkTxt.text=enemy.damage.ToString();
        hpTxt.text=enemy.hp.ToString();
        spdTxt.text=enemy.speed.ToString();
        defTxt.text=enemy.armor.ToString();

        switch (enemy.enemyType)
        {
            case EnemyType.Basic:
                classIcon.sprite = classIcons[0];
                cardBack.sprite = cardBacks[0];
                break;

            case EnemyType.Demon:
                classIcon.sprite = classIcons[1];
                cardBack.sprite = cardBacks[1];
                break;
        }

        xpTxt.text = enemy.xp.ToString() + "/" + enemy.upgCost.ToString();
        if (enemy.xp >= enemy.upgCost)
        {
            lvlUp.gameObject.SetActive(true);
            lvlUpBtn.interactable = true;
        }
        else
        {
            lvlUp.gameObject.SetActive(false);
            lvlUpBtn.interactable = false;
        }

        //stat sprites
        if (enemy.magicDamage == 0)
        {
            atkImg.sprite = atkSprite[0];
        }
        else
        {
            atkImg.sprite= atkSprite[1];
        }
        hpImg.sprite = hpSprite;
        spdImg.sprite= spdSprite;

        if (enemy.magicResist > 0)
        {
            defImg.sprite = defSprite[1];
        }
        else
        {
            defImg.sprite= defSprite[0];
        }

    }


    public void LevelUp()
    {
        Debug.Log("clicked upg button!");

        if (enemy == null) { return; }

        enemy.xp-=enemy.upgCost;
        enemy.level += 1;
        enemy.upgCost = Mathf.FloorToInt(enemy.upgCost * Mathf.Pow(xpScaleFactor, enemy.level));

        //stats here
        enemy.damage = Mathf.Floor(enemy.damage* (1+(enemy.level / 10)));
        enemy.hp=Mathf.Floor(enemy.hp*(1+(enemy.level/10)));

        UpdateText();
    }
}
