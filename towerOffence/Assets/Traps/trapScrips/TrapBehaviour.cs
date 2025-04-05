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
                    if (damage > 0) { enemy.GetComponent<EnemyBehaviour>().TakeDamage(damage); }
                    if (element == ElementType.Ice)
                    {
                       // enemy.GetComponent<EnemyBehaviour>().Freeze(1f); //moved to triggerEvents
                    }
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
                if (damage > 0) { hit.GetComponent<EnemyBehaviour>().TakeDamage(damage); }
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
