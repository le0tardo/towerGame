using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class SpawnButton : MonoBehaviour
{
    [SerializeField] EnemyBase enemyToSpawn;
    [SerializeField] ManaManager manaManager;
    [SerializeField] KeyCode key;
    Button button;
    TMP_Text buttonText;

    bool canAfford;
    float coolDown;
    float coolDownTimer = 0f;
    bool hasCooledDown;

    [SerializeField] Image coolDownCircle;

    private void Start()
    {
        button = GetComponent<Button>();
        buttonText = GetComponentInChildren<TMP_Text>();
        buttonText.text=enemyToSpawn.enemyName;
        coolDown = enemyToSpawn.coolDown;
        hasCooledDown = true;
    }

    private void Update()
    {
        //key input
        if (Input.GetKeyDown(key) && button.interactable)
        {
            button.onClick.Invoke();
        }

        //mana check
        if (enemyToSpawn.cost <= manaManager.mana){ canAfford = true;}else{canAfford = false;}

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
        }
        else
        {
            coolDownCircle.fillAmount = 0;
        }
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
