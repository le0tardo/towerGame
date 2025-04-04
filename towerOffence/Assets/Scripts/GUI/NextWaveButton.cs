using UnityEngine;
using UnityEngine.UI;

public class NextWaveButton : MonoBehaviour
{
    [SerializeField] ManaManager mana;
    [SerializeField] GameManager game;
    Button button;
    private void Start()
    {
        button = GetComponent<Button>();
    }
    private void Update()
    {

        if ((mana.mana < mana.maxMana) && (game.enemyCount == 0) &&(game.wave<game.maxWaves))
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }


}
