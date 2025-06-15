using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class SpawnButton : MonoBehaviour
{
    [SerializeField] EnemyBase enemyToSpawn;
    [SerializeField] Image enemyPortrait;
    [SerializeField] EnemyPool enemyPool;
    [SerializeField] ManaManager manaManager;
    [SerializeField] KeyCode key;
    [SerializeField] Button button;
    [SerializeField]TMP_Text buttonText;
    [SerializeField] GameObject buttonGfx;
    [SerializeField] Image coolDownBar;
    [SerializeField] Image coolDownCircle;
    [SerializeField] Animator anim;
    [SerializeField] Image fill;
    [SerializeField] Sprite[] fills;

    bool canAfford;
    float coolDown;
    float coolDownTimer = 0f;
    bool hasCooledDown;
    Color cantAffordColor;

    private void Start()
    {
        if (enemyToSpawn == null) { buttonGfx.SetActive(false);button.gameObject.SetActive(false);return; }
        if (manaManager == null) { manaManager=GameManager.instance.GetComponent<ManaManager>(); }
        anim= GetComponent<Animator>();
        buttonText.text=enemyToSpawn.enemyName;
        coolDown = enemyToSpawn.coolDown;
        hasCooledDown = true;
        enemyPortrait.sprite = enemyToSpawn.enemyIcon;
        cantAffordColor = new Color(233f / 255f, 33f / 255f, 33f / 255f);

        //assign color
        switch (enemyToSpawn.enemyType)
        {
            case EnemyType.Basic:
                fill.sprite = fills[0];
            break;
            case EnemyType.Demon:
                fill.sprite = fills[1];
            break;
        }

    }

    private void Update()
    {
        if (enemyToSpawn == null) { return; }

        //key input
        if (Input.GetKeyDown(key) && button.interactable)
        {
            button.onClick.Invoke();
        }

        //mana check
        if (enemyToSpawn.cost <= manaManager.mana){ canAfford = true;}else{canAfford = false;}
        if (!canAfford) { buttonText.color = cantAffordColor; } else { buttonText.color = Color.white;}

        //set interactable
        if (canAfford && hasCooledDown)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }

        //coolDown
        if (!hasCooledDown)
        {
            coolDownTimer-=Time.deltaTime;

            float fill = Mathf.Lerp(0,1,coolDownTimer/coolDown);
            coolDownCircle.fillAmount = fill;
            coolDownBar.fillAmount = fill;
        }
        else
        {
            coolDownCircle.fillAmount = 0;
            coolDownBar.fillAmount = 0;
        }
    }

    public void SpawnFromPool()
    {
        if (anim != null) { anim.SetTrigger("click");}
        enemyPool.SpawnEnemy();
        CoolDown();
    }
    public void CoolDown()
    {
        hasCooledDown = false;
        Invoke("CooledDown",coolDown);
        coolDownTimer = coolDown;
    }

    void CooledDown()
    {
        hasCooledDown=true;
        coolDownTimer = 0f;
    }

}
