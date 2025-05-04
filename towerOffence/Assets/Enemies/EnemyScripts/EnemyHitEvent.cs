using UnityEngine;

public class EnemyHitEvent : MonoBehaviour
{
    EnemyBehaviour eb;

    private void Start()
    {
        eb=GetComponentInParent<EnemyBehaviour>();
    }

    public void HitEvent()
    {
        if (eb != null)
        {
            eb.DeadDamageEvent();
        }
    }
}
