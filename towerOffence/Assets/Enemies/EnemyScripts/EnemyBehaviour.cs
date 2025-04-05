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
    public  SplineAnimate anim;

    public bool inCombat = false;
    public GameObject myTarget = null;
    bool frozen = false;

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

        if (myTarget != null)
        {
            //turn to face target here
            Vector3 dir=myTarget.transform.position-transform.position;
            Quaternion look = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, look, 5f * Time.deltaTime);

            GameObject targetsTarget=myTarget.GetComponent<HeroBehaviour>().currentTarget;
            if (targetsTarget != this.gameObject) { myTarget=null;inCombat = false;StopAllCoroutines();}
        }
        else
        {
            //turn to face direction
            Quaternion look = Quaternion.LookRotation(Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, look, 5f * Time.deltaTime);
        }

    }

    public void GetTarget(GameObject target)
    {
        myTarget = target;
        if (myTarget!=null)
        {
            //anim.Pause();
            Invoke("StopWalking",0.25f);
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

    void StopWalking()
    {
        if (anim.IsPlaying)
        {
            anim.Pause();
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
    public void Freeze(float duration)
    {
        if (frozen) { return; }

        float progress=anim.NormalizedTime;
        anim.MaxSpeed = enemyBase.speed / 2;
        anim.NormalizedTime = progress;
        frozen = true;
        Invoke("Unfreeze", duration);
    }
    public void Unfreeze()
    {
        if (!frozen) {  return; }   
        float progress = anim.NormalizedTime;
        anim.MaxSpeed = enemyBase.speed;
        anim.NormalizedTime = progress;
        frozen = false;
    }
    IEnumerator DealDamage()
    {  
        while (myTarget != null)
        {
            yield return new WaitForSeconds(enemyBase.meleeCooldown);
           if(myTarget!=null) myTarget.GetComponent<HeroBehaviour>().TakeDamage(enemyBase.meleeDamage);
        }
        inCombat = false;
    }
}
