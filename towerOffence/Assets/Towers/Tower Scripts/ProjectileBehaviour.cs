using Unity.VisualScripting;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    TowerBehaviour tower;
    [SerializeField] Transform spawnPos;
    [SerializeField] float speed;
    Vector3 targetPos;
    float dist;
    float damage;
    private void Start()
    {
        tower= GetComponentInParent<TowerBehaviour>();
        damage = tower.damage;
    }

    private void OnEnable()
    {
        transform.position = spawnPos.transform.position;
        transform.rotation = spawnPos.transform.rotation;
    }

    private void Update()
    {
        if (tower.currentTarget != null) { targetPos = tower.currentTarget.transform.position;}
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
       
        dist = Vector3.Distance(transform.position, targetPos);
        if (dist < 0.1f)
        {
            HitEnemy();
        }  
    }

    void HitEnemy()
    {
        if (tower.currentTarget != null)
        {
            EnemyBehaviour eh=tower.currentTarget.GetComponent<EnemyBehaviour>();
            eh.TakeDamage(damage);
        }
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        targetPos = Vector3.zero;
    }
}
