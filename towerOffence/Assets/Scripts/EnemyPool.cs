using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] EnemyBase enemyBase;
    [SerializeField] GameObject[] enemies;
    [SerializeField] ManaManager manaManager;

    public void SpawnEnemy()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (!enemies[i].activeInHierarchy)
            {
                enemies[i].SetActive(true);
                manaManager.mana -= enemyBase.cost;
                break;
            }
        }
    }
}
