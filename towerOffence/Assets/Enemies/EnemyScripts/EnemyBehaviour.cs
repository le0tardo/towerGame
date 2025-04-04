using System.Collections;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] EnemyBase enemyBase;
    public float hp;
    float maxHp;
    float p;
    public float damage;

    [SerializeField] Canvas canvas;
    [SerializeField] Image health;
    SplineAnimate anim;

    public bool inCombat = false;
    public GameObject myTarget = null;

    private void Start()
    {
        anim = GetComponent<SplineAnimate>();
        anim.MaxSpeed = enemyBase.speed;
        //if (anim.Container == null) { anim.Container}
    }

    private void OnEnable()
    {
        hp = enemyBase.hp;
        maxHp = hp;
        damage = enemyBase.damage;
        inCombat = false;
        anim?.Play();
    }

    private void Update()
    {
        p = hp / maxHp;
        health.rectTransform.localScale = new Vector3(p, health.rectTransform.localScale.y, health.rectTransform.localScale.z);
        if (hp == maxHp || hp <= 0) {canvas.enabled = false; } else { canvas.enabled = true;}

        if (hp <= 0)
        {
            DeathFXPool.Instance.SpawnDeathFX(transform.position);
            anim.Restart(true);
            gameObject.SetActive(false);
        }
    }

    public void GetTarget(GameObject target)
    {
        myTarget = target;
        if (myTarget!=null)
        {
            anim.Pause();
            if (!inCombat)
            {
                StartCoroutine(DealDamage());
                inCombat = true;
            }
        }
        else
        {
            Invoke("ResumeWalking", 0.5f);
            inCombat = false;
            StopAllCoroutines();
        }
    }

    void ResumeWalking()
    {
        if (!anim.IsPlaying)
        {
            anim.Play();
        }
    }

    public void TakeDamage(float dmg)
    {
        hp -= dmg;
    }

    IEnumerator DealDamage()
    {  
        while (myTarget != null)
        {
            yield return new WaitForSeconds(enemyBase.meleeCooldown);
            myTarget.GetComponent<HeroBehaviour>().TakeDamage(enemyBase.meleeDamage);
        }
        inCombat = false;
    }
}
