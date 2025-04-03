using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroBehaviour : MonoBehaviour
{
    [SerializeField] HeroBase heroBase;
    [SerializeField] Canvas canvas;
    [SerializeField] Image health;

    float hp;
    float maxHp;
    float p;

    public bool inCombat = false;
    public List<GameObject> potentialTargets = new List<GameObject>();
    public GameObject currentTarget;

    SphereCollider rangeTrigger;

    private void Start()
    {
        hp = heroBase.hp;
        maxHp = hp;
        rangeTrigger = GetComponent<SphereCollider>();
        rangeTrigger.radius = heroBase.range;

    }

    private void Update()
    {

        UpdateHp();
        FindTargets();

        if (currentTarget != null)
        {
            currentTarget.GetComponent<EnemyBehaviour>().CheckCombat(true);
            MoveToTarget();

        }
        else
        {
            inCombat=false;
            StopAllCoroutines();
        }
        if (hp <= 0)
        {
            if (currentTarget != null)
            {
                currentTarget.GetComponent<EnemyBehaviour>().CheckCombat(false);
            }
            StopAllCoroutines();
        }
    }

    void UpdateHp()
    {
        p = hp / maxHp;
        health.rectTransform.localScale = new Vector3(p, health.rectTransform.localScale.y, health.rectTransform.localScale.z);
        if (hp == maxHp || hp <= 0) { canvas.enabled = false; } else { canvas.enabled = true; }
    }
    void FindTargets()
    {
        if (potentialTargets.Count > 0)
        {
            currentTarget = potentialTargets[0];
        }
        else
        {
            currentTarget = null;
        }
    }

    public void TakeDamage(float damage)
    {
        hp-=damage;
        if (hp <= 0)
        {
            //kill 
        }
    }

    void MoveToTarget()
    {
        float dist = Vector3.Distance(transform.position,currentTarget.transform.position);
        if (dist > 1.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, heroBase.speed * Time.deltaTime);
        }
        else
        {
            if (!inCombat)
            {
                StartCoroutine(AttackTarget());
            }
        }
    }

    IEnumerator AttackTarget()
    {
        inCombat = true;
        while (currentTarget != null)
        {
            currentTarget.GetComponent<EnemyBehaviour>().TakeDamage(heroBase.damage);
            float targetHp=currentTarget.GetComponent<EnemyBehaviour>().hp;
            if (targetHp <= 0)
            {
                potentialTargets.Remove(currentTarget);
                inCombat = false;
                StopAllCoroutines();
            }

            yield return new WaitForSeconds(heroBase.coolDown);
        }
        inCombat = false;
    }

    #region //Trigger
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
    #endregion
}
