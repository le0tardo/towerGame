using UnityEngine;

public class TowerAnimator : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] TowerBehaviour tower;
    [SerializeField] float triggerDelay=0f;

    // Update is called once per frame
    void Update()
    {
        if (tower.currentTarget != null)
        {
            anim.SetBool("combat",true);

            if (tower.coolDown <= triggerDelay)
            {
                anim.SetTrigger("fire");
            }
        }
        else
        {
            anim.SetBool("combat",false) ;
        }

    }
}
