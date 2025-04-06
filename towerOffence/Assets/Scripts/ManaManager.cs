using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ManaManager : MonoBehaviour
{
    public int mana;
    public int maxMana = 100;
    [SerializeField] Image manaBar;
    [SerializeField] TMP_Text manaText;
    GameManager game;

    private void Start()
    {
        mana = maxMana;
        game=GetComponent<GameManager>();
    }
    private void Update()
    {
        mana = Mathf.Clamp(mana, 0, maxMana);
        float p = (float)mana /(float)maxMana;
        manaBar.rectTransform.localScale = new Vector3(p, manaBar.rectTransform.localScale.y, manaBar.rectTransform.localScale.z);
        manaText.text = mana + "/" + maxMana;

    }

    public void RefillManaEvent()
    {
        StartCoroutine(RefillMana());
    }

    private IEnumerator RefillMana()
    {
        while (mana < maxMana)
        {
            mana += 2;
            mana = Mathf.Min(mana, maxMana);
            yield return new WaitForSeconds(0.001f);
        }
        game.wave++;
    }
}
