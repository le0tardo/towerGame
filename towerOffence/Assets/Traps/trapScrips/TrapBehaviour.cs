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
            coolDown -= Time.deltaTime;
            if (coolDown <= 0)
            {
                coolDown = maxCoolDown;

                if (!singleUse)
                {
                    foreach (GameObject enemy in enemies)
                    {
                        if (damage <= 0 || !enemy.activeInHierarchy) { continue; } // Skip invalid enemies

                        float spd = enemy.GetComponent<EnemyBehaviour>().enemyBase.speed;
                        spd *= 2; // Deals more damage to slower enemies

                        float dmg = damage - spd; // Calculate damage reduction
                        if (dmg < 1) { dmg = 1; } // Minimum damage

                        enemy.GetComponent<EnemyBehaviour>().TakeDamage(dmg, damageType, elementType, 0);
                    }
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
