using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Base", menuName = "Enemy Base", order = 2)]
public class EnemyBase : ScriptableObject
{
    [Header("Enemy Stats")]
    public string enemyName;
    public float hp;
    public float speed;
    public float damage;
    public float armor;
    public float magicResist;
    public float coolDown;
    public int cost;

    [Header("Melee Stats")]
    public float meleeDamage;
    public float meleeCooldown;

    [Header("Enemy Gfx")]
    public Sprite enemyIcon;
    public GameObject enemyPrefab;

}
public enum EnemyType //even necessary?
{
    Demon,
    Undead,
    Swarm // insect/spider thing
}

