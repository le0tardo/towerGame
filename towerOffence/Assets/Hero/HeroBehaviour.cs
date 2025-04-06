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
    DamageType damageType;
    ElementType elementType;

    Vector3 home;
    float distanceHome=0;
    public bool isHome=true;
    Vector3 startRotation;

    private void Start()
    {
        hp = heroBase.hp;
        maxHp = hp;
        rangeTrigger = GetComponent<SphereCollider>();
        rangeTrigger.radius = heroBase.range;
        speed = heroBase.speed;
        home=transform.position;

        startRotation = Vector3.forward;
    }

    private void Update()
    {
        UpdateHp();
        FindTargets();

        distanceHome = Vector3.Distance(transform.position,home);
        if (distanceHome > 0.01f)
        {
            isHome = false;
        }
        else
        {
            isHome = true;
            if (Vector3.Angle(transform.forward,startRotation)>1)
            { 
                Quaternion toRotation = Quaternion.LookRotation(startRotation);
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.deltaTime);
            }
        }

        if (!isHome && !inCombat && potentialTargets.Count <= 0)
        {
            GoHome();
        }
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

        for (int i = 0; i < potentialTargets.Count; i++)
        {
            float enemyHp = potentialTargets[i].GetComponent<EnemyBehaviour>().hp;
            if (enemyHp <= 0)
            {
                potentialTargets.Remove(potentialTargets[i]);
            }
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

            Vector3 direction = currentTarget.transform.position - transform.position;
            if (direction != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.deltaTime);
            }

            isHome = false;
        }
        else
        {
            if (!inCombat)
            {
                StartCoroutine(AttackTarget());
            }
        }

        if (distanceHome > 10)
        {
            potentialTargets.Clear();
            currentTarget = null;
            inCombat = false;
            StopAllCoroutines();
            GoHome();
        }
    }
    void GoHome()
    {
        transform.position = Vector3.MoveTowards(transform.position, home, speed * Time.deltaTime);
        Vector3 direction = home - transform.position;
        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.deltaTime);
        }
    }

    IEnumerator AttackTarget()
    {
        inCombat = true;
        while (currentTarget != null)
        {
            currentTarget.GetComponent<EnemyBehaviour>().TakeDamage(heroBase.damage, damageType,elementType,0);
            float targetHp=currentTarget.GetComponent<EnemyBehaviour>().hp;
            if (targetHp <= 0)
            {
                potentialTargets.Remove(currentTarget);
                inCombat = false;
                foundTarget = false;
                StopAllCoroutines();
            }

            GameObject enemyTarget = currentTarget.GetComponent<EnemyBehaviour>().myTarget;
            if (enemyTarget == null) { currentTarget.GetComponent<EnemyBehaviour>().GetTarget(this.gameObject);}

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
            //if isHome?
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
