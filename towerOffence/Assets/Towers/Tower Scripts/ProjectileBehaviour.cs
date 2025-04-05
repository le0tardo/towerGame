using Unity.VisualScripting;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    TowerBehaviour tower;
    [SerializeField] Transform spawnPos;
    [SerializeField] float speed;
    Vector3 startPos;
    Vector3 targetPos;
    float dist;
    float damage;
    float lerp;
    public bool arc;
    float arcSpeed;
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
    }

    private void Update()
    {
        if (tower.currentTarget != null&&tower.currentTarget.activeInHierarchy) { targetPos = tower.currentTarget.transform.position;}

        //move in straight line to target
        if (!arc)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
        else
        {
            if (lerp < 1)
            {
                lerp += arcSpeed * Time.deltaTime;

                // Get arc direction using Slerp between start-forward and target direction
                Vector3 midPoint = (startPos + targetPos) / 2 + Vector3.up * 5f; // raises midpoint to create arc height
                Vector3 pointA = Vector3.Lerp(startPos, midPoint, lerp);
                Vector3 pointB = Vector3.Lerp(midPoint, targetPos, lerp);
                transform.position = Vector3.Lerp(pointA, pointB, lerp); // Double lerp = arc
            }
        }
       
        dist = Vector3.Distance(transform.position, targetPos);
        if (dist < 0.01f)
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
