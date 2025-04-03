using UnityEngine;

[CreateAssetMenu(fileName = "Hero Base", menuName = "Hero Base", order = 3)]
public class HeroBase : ScriptableObject
{
    [Header("Hero Stats")]
    public string heroName;
    public float hp;
    public float speed;
    public float damage;
    public float armor;
    public float magicResist;
    public float coolDown;
    public float range;

    [Header("Hero Gfx")]
    public Sprite heroIcon;
    public GameObject heroPrefab;

}