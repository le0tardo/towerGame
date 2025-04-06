using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrapBehaviour : MonoBehaviour
{
    [SerializeField] TrapBase trapBase;
    float damage;
    public float coolDown;
    float maxCoolDown;

    public bool singleUse = false;
    public List<GameObject> enemies = new List<GameObject>();
    DamageType damageType;
    ElementType elementType;
    [SerializeField] LayerMask enemyLayer;
    public float area;

    [SerializeField] TMP_Text trapName;

    ElementType element;

    private void Start()
    {
        damage=trapBase.damage;
        coolDown=trapBase.coolDown;
        maxCoolDown = trapBase.coolDown;
        area=trapBase.area;
        element = trapBase.elementType;
        trapName.text = trapBase.trapName;
    }
    private void Update()
    {

        if (enemies.Count > 0) { coolDown -= Time.deltaTime; }
        
        if (coolDown <= 0)
        {
            if (!singleUse) { 
                foreach (GameObject enemy in enemies)
                {
                    if (damage <= 0) { return; }if (!enemy.activeInHierarchy) { return;}
                    float spd = enemy.GetComponent<EnemyBehaviour>().enemyBase.speed; //idea, deals more damage to slower enemies!?
                    float dmg = damage;
                    dmg=(dmg-spd); if (dmg < 1) { dmg = 1;}
                    //Debug.Log("trap base damag is: "+trapBase.damage +" enemy speed is "+ spd +". new damage is "+ dmg);
                    if (dmg > 0) { enemy.GetComponent<EnemyBehaviour>().TakeDamage(dmg, damageType, elementType,0);}
                }
                 coolDown = maxCoolDown;
            }
            else
            {
                Explode();
            }
        }
    }

    void Explode()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, area, enemyLayer);
        foreach (Collider hit in hitEnemies)
        {
            if (hit.CompareTag("Enemy"))
            {
                if (damage > 0) { hit.GetComponent<EnemyBehaviour>().TakeDamage(damage,damageType, elementType,0); }
            }
        }
        this.gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,area);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Add(other.gameObject);

            if (element == ElementType.Ice)
            {
                other.gameObject.GetComponent<EnemyBehaviour>().Freeze(10f); //overkill, but resets on triggerExit
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Remove(other.gameObject);

            if (element == ElementType.Ice)
            {
                other.gameObject.GetComponent<EnemyBehaviour>().Unfreeze();
            }
        }
    }
}
