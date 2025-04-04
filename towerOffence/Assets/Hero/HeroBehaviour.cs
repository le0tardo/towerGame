using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroBehaviour : MonoBehaviour
{
    [SerializeField] HeroBase heroBase;
    [SerializeField] Canvas canvas;
    [SerializeField] Image health;
    [SerializeField] GameObject deathFX;

    float hp;
    float maxHp;
    float p;
    float speed;
    public bool inCombat = false;
    bool foundTarget = false;
    bool isDead = false;
    public List<GameObject> potentialTargets = new List<GameObject>();
    public GameObject currentTarget;
    SphereCollider rangeTrigger;

    private void Start()
    {
        hp = heroBase.hp;
        maxHp = hp;
        rangeTrigger = GetComponent<SphereCollider>();
        rangeTrigger.radius = heroBase.range;
        speed = heroBase.speed;
    }

    private void Update()
    {
        UpdateHp();
        FindTargets();

        if (currentTarget != null)
        {
            if(!foundTarget)currentTarget.GetComponent<EnemyBehaviour>().GetTarget(this.gameObject);
            foundTarget = true;
            MoveToTarget();
        }
        else
        {
            inCombat=false;
            foundTarget = false;
            StopAllCoroutines();
        }

        if (hp <= 0)
        {
            if (!isDead) { Death();} 
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

    }
    void Death()
    {
        isDead = true;
        if (currentTarget != null)
        {
            currentTarget.GetComponent<EnemyBehaviour>().GetTarget(null);
            currentTarget = null;
        }
        potentialTargets.Clear();
        StopAllCoroutines();
        speed = 0;
        deathFX.SetActive(true);
        MeshRenderer mesh=GetComponentInChildren<MeshRenderer>(); mesh.enabled = false;
        Invoke("Kill", 1f);
    }
    void Kill()
    {
        this.gameObject.SetActive(false);
    }
    void MoveToTarget()
    {
        float dist = Vector3.Distance(transform.position,currentTarget.transform.position);
        if (dist > 1.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, speed * Time.deltaTime);
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
                foundTarget = false;
                StopAllCoroutines();
            }

            yield return new WaitForSeconds(heroBase.coolDown);
        }
        foundTarget = false;
        inCombat = false;
    }

    #region //Trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")&&hp>0)
        {
            potentialTargets.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy")&&hp>0)
        {
            potentialTargets.Remove(other.gameObject);
        }
    }
    #endregion
}
