using UnityEngine;

[CreateAssetMenu(fileName = "Tower Base", menuName = "Tower Base", order = 1)]
public class TowerBase : ScriptableObject
{
    [Header("Tower Stats")]
    public string towerName;
    public int cost; //if i wanna add defence mode
    public float range;
    public float speed;

    [Header("Tower Damage")]
    public float damage;
    public DamageType damageType;
    public ElementType elementType;

    [Header("Tower Gfx")]
    public Sprite icon; //if i wanna add defence mode
    public GameObject prefab;

}
public enum DamageType
{
    Physical,
    Magical,
    Elemental
}
public enum ElementType
{
    None,
    Fire,
    Ice,
    Poison
}
