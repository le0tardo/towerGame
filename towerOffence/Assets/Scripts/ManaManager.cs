using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ManaManager : MonoBehaviour
{
    public int mana;
    int maxMana = 100;
    [SerializeField] Image manaBar;
    [SerializeField] TMP_Text manaText;

    private void Start()
    {
        mana = maxMana;
    }
    private void Update()
    {
        mana = Mathf.Clamp(mana, 0, 100);
        float p = (float)mana /(float)maxMana;
        manaBar.rectTransform.localScale = new Vector3(p, manaBar.rectTransform.localScale.y, manaBar.rectTransform.localScale.z);
        manaText.text = mana + "/" + maxMana;

        if (Input.GetKeyDown(KeyCode.M))
        {
            StartCoroutine(RefillMana());
        }
    }

    private IEnumerator RefillMana()
    {
        while (mana < maxMana)
        {
            mana += 2;
            mana = Mathf.Min(mana, maxMana);
            yield return new WaitForSeconds(0.001f);
        }
    }
}
