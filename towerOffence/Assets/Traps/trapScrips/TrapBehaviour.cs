using System.Collections.Generic;
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

    private void Start()
    {
        damage=trapBase.damage;
        coolDown=trapBase.coolDown;
        maxCoolDown = trapBase.coolDown;
        area=trapBase.area;
    }
    private void Update()
    {

        if (enemies.Count > 0) { coolDown -= Time.deltaTime; }
        
        if (coolDown <= 0)
        {
            if (!singleUse) { 
                foreach (GameObject enemy in enemies)
                {
                    enemy.GetComponent<EnemyBehaviour>().TakeDamage(damage);
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
        Debug.Log("boom");
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, area, enemyLayer);
        foreach (Collider hit in hitEnemies)
        {
            if (hit.CompareTag("Enemy"))
            {
                hit.GetComponent<EnemyBehaviour>().TakeDamage(damage);
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
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Remove(other.gameObject);
        }
    }
}
