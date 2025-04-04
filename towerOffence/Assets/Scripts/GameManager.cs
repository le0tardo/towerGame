using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int wave = 1;
    public int maxWaves = 3;
    public int enemyCount = 0;
    public int summonsLeft=1;

    [SerializeField] TMP_Text enemyCountText;
    [SerializeField] TMP_Text waveCountText;
    ManaManager mana;

    private void Start()
    {
        mana =GetComponent<ManaManager>();
    }

    private void Update()
    {
        CountEnemies();

        waveCountText.text = wave + "/" + maxWaves;

        if ((wave >= maxWaves) && (enemyCount == 0) && (mana.mana <=0)) //
        {
            Debug.Log("GAME OVER!!!");
        }
    }
    void CountEnemies()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemyCountText.text=enemyCount.ToString();
    }

    void CountRemainingSummons()
    {
        //find cheapest enemy available to summon in scene
        //get that enemies cost
        //if that value is < mana, and that other shit, game over. 
    }
 
}
