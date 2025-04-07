using Unity.VisualScripting;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    TowerBehaviour tower;
    [SerializeField] Transform spawnPos;
    public GameObject targetObject;
    [SerializeField] float speed;
    [SerializeField] bool AOE = false;
    [SerializeField] float area;
    [SerializeField] DamageType damageType;
    [SerializeField] ElementType elementType;
    [SerializeField] float effectDuration;
    [SerializeField] LayerMask enemyLayer;
    Vector3 startPos;
    Vector3 targetPos;
    Vector3 direction;
    float dist;
    float damage;
    float lerp;
    public bool arc;
    float arcSpeed;
    public float arcHeight = 5;
    private void Start()
    {
        tower= GetComponentInParent<TowerBehaviour>();
        damage = tower.damage;
        arcSpeed = speed * 0.06f;
    }

    private void OnEnable()
    {
        transform.position = spawnPos.transform.position;
        transform.rotation = spawnPos.transform.rotation;
        startPos = transform.position;
        lerp = 0f;
        if (speed == 0) { speed = 1;} //om jag glöm
        if (area == 0) {  area = 1;}
    }

    private void Update()
    {

        //if (tower.currentTarget != null && tower.currentTarget.activeInHierarchy) { targetPos = tower.currentTarget.transform.position;}
        //if (targetObject != null && targetObject.activeInHierarchy) { targetPos=targetObject.transform.position; }

        //gets targetObject from tower behaviour script on spawn
        if(targetObject!=null && targetObject.activeInHierarchy) {targetPos=targetObject.transform.position;}

        direction = targetPos - transform.position;
        transform.rotation = Quaternion.LookRotation(direction);

        if (!arc) //move in straight line to target
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
        else //move in an arc to target
        {
            if (lerp < 1)
            {
                lerp += arcSpeed * Time.deltaTime;

                Vector3 midPoint = (startPos + targetPos) / 2 + Vector3.up * arcHeight;
                Vector3 pointA = Vector3.Lerp(startPos, midPoint, lerp);
                Vector3 pointB = Vector3.Lerp(midPoint, targetPos, lerp);
                transform.position = Vector3.Lerp(pointA, pointB, lerp);
            }
        }
       
        dist = Vector3.Distance(transform.position, targetPos);
        if (dist < 0.01f)
        {
            if (!AOE) { HitEnemy(); }
            else { HitArea(); }
        }  
    }

    void HitEnemy()
    {
        if (tower.currentTarget != null)
        {
            EnemyBehaviour eh=tower.currentTarget.GetComponent<EnemyBehaviour>();
            eh.TakeDamage(damage, damageType, elementType, effectDuration);
        }
        FXPool.Instance.SpawnFX(transform.position);
        this.gameObject.SetActive(false);
    }

    void HitArea()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position,area, enemyLayer);
        foreach (Collider hit in hitEnemies)
        {
            if (hit.CompareTag("Enemy"))
            {
                hit.GetComponent<EnemyBehaviour>().TakeDamage(damage, damageType, elementType,0);
            }
        }
        FXPool.Instance.SpawnFX(transform.position);
        this.gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        if (AOE)
        {
            Gizmos.DrawWireSphere(transform.position, area);
        }
        if (targetPos != Vector3.zero)
        {   
            Gizmos.color = Color.red;
            Vector3 boxSize = new Vector3(2, 2, 2);
            Gizmos.DrawWireCube(targetPos,boxSize);
        }
    }

    private void OnDisable()
    {
        targetPos = Vector3.zero;
        transform.position = startPos;
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
