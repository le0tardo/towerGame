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
    public bool animated = false;
    public List<GameObject> enemies = new List<GameObject>();
    DamageType damageType;
    ElementType elementType;
    [SerializeField] LayerMask enemyLayer;
    public float area;

    [SerializeField] TMP_Text trapName;

    private void Start()
    {
        damage=trapBase.damage;
        coolDown=trapBase.coolDown;
        maxCoolDown = trapBase.coolDown;
        area=trapBase.area;
        damageType = trapBase.damageType;
        elementType = trapBase.elementType;
        trapName.text = trapBase.trapName;
    }
    private void Update()
    {
        if (enemies.Count > 0)
        {
            if (!animated) { coolDown -= Time.deltaTime; }
            if (coolDown <= 0)
            {
                coolDown = maxCoolDown;

                if (!singleUse)
                {
                    DealTrapDamage();
                }
                else
                {
                    Explode();
                }
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                if (!enemies[i].activeInHierarchy)
                {
                    enemies.Remove(enemies[i]);
                }
            }

        }
    }
    public void DealTrapDamage()
    {
        if (enemies.Count <= 0) { return; }
        foreach (GameObject enemy in enemies)
        {
            if (damage <= 0 || !enemy.activeInHierarchy) { continue; }

            float ehp = enemy.GetComponent<EnemyBehaviour>().enemyBase.hp;
            float dmg = ehp * trapBase.fractionDamage;
            dmg = Mathf.Max(dmg, 1f);
            dmg = Mathf.Floor(dmg);//Debug.Log("enemy max hp: " + ehp + ". Trap fraction damage: " + trapBase.fractionDamage + ". Damage dealt: " + dmg);

            enemy.GetComponent<EnemyBehaviour>().TakeDamage(dmg, damageType, elementType, 0);
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
        FXPool.Instance.SpawnFX(transform.position);
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

            if (elementType == ElementType.Ice)
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

            if (elementType == ElementType.Ice)
            {
                other.gameObject.GetComponent<EnemyBehaviour>().Unfreeze();
            }
        }
    }
}
