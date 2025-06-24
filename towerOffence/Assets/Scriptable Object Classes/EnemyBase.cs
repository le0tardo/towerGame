using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Base", menuName = "Enemy Base", order = 2)]
public class EnemyBase : ScriptableObject
{
    [Header("Enemy Stats")]
    public string enemyName;
    public EnemyType enemyType;
    public float level;
    public float hp;
    public float speed;
    public float damage;
    public float magicDamage;
    public float armor;
    public float magicResist;
    public float coolDown;
    public int cost;

    [Header("Melee Stats")]
    public float meleeDamage;
    public float meleeCooldown;

    [Header("Enemy Gfx")]
    public Sprite enemyButtonIcon;
    public Sprite enemyCardIcon;
    public GameObject enemyPrefab;

    [Header("Upgrade")]
    public int xp;
    public int upgCost;

}
public enum EnemyType //even necessary?
{
    Basic,
    Demon,
    Undead,
    Swarm // insect/spider thing, fire weakness
}

