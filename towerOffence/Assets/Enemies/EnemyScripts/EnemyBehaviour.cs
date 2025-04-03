using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] EnemyBase enemyBase;
    public float hp;
    float maxHp;
    float p;

    [SerializeField] Canvas canvas;
    [SerializeField] Image health;
    SplineAnimate anim;

    private void Start()
    {
        hp = enemyBase.hp;
        maxHp = hp;
        anim = GetComponent<SplineAnimate>();
        anim.MaxSpeed = enemyBase.speed;
        //if (anim.Container == null) { anim.Container}
    }

    private void Update()
    {
        p = hp / maxHp;
        health.rectTransform.localScale = new Vector3(p, health.rectTransform.localScale.y, health.rectTransform.localScale.z);
        if (hp == maxHp || hp <= 0) {canvas.enabled = false; } else { canvas.enabled = true;}

        if (hp <= 0)
        {
            anim.Pause();
        }
    }

    public void TakeDamage(float dmg)
    {
        hp -= dmg;
    }
}
