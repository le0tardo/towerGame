using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class TowerBehaviour : MonoBehaviour
{
    [SerializeField] TowerBase towerBase;
    float range;
    float speed;
    float coolDown;
    public float damage;
    public GameObject[] projectiles;

    public List<GameObject> potentialTargets=new List<GameObject>();
    public GameObject currentTarget;

    CapsuleCollider rangeTrigger;

    [Header("Tower Canvas")]
    [SerializeField] TMP_Text towerName;
    [SerializeField] Image coolDownFill;


    private void Start()
    {
        //sync stats to scriptable
        range=towerBase.range;
        speed=towerBase.speed;
        damage=towerBase.damage;
        coolDown = towerBase.speed;

        rangeTrigger = GetComponent<CapsuleCollider>();
        rangeTrigger.radius = range;
        towerName.text = towerBase.towerName;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            potentialTargets.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            potentialTargets.Remove(other.gameObject);
        }
    }

    private void Update()
    {
        if (potentialTargets.Count > 0)
        {
            currentTarget = potentialTargets[0];
        }
        else
        {
            currentTarget=null;
        }

        if (currentTarget != null)
        {
            //rotate towards target
            Vector3 direction = currentTarget.transform.position - transform.position;
            direction.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10);

            //reset target if current enemy dies
            EnemyBehaviour eh=currentTarget.GetComponent<EnemyBehaviour>();
            if (eh.hp <= 0) {potentialTargets.Remove(currentTarget);}

            //cool down counter
            coolDown-=Time.deltaTime;
            coolDownFill.fillAmount = coolDown/towerBase.speed;
            if (coolDown <= 0)
            {
                Fire();
                coolDown = speed;
            }
        }
    }

    void Fire()
    {
        if (currentTarget != null)
        {
            for (int i = 0; i < projectiles.Length; i++)
            {
                if (!projectiles[i].activeInHierarchy)
                {
                    projectiles[i].SetActive(true);
                    //ProjectileBehaviour pb = projectiles[i].GetComponent<ProjectileBehaviour>();

                    break;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position,towerBase.range);
    }
}
