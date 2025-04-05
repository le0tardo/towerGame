using UnityEngine;

[CreateAssetMenu(fileName = "Trap Base", menuName = "Trap Base", order = 4)]
public class TrapBase : ScriptableObject
{
    [Header("Trap Stats")]
    public string trapName;
    public float damage;
    public float coolDown;
    public float area;
    public DamageType damageType;
    public ElementType elementType;
    public bool singleUse;
}
