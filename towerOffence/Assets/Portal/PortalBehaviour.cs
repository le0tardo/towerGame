using UnityEngine;

public class PortalBehaviour : MonoBehaviour
{
    [SerializeField] Animator portalAnim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            SpawnAnimation();
        }
    }
    public void SpawnAnimation()
    {
        portalAnim.SetTrigger("spawnTrigger");
    }
    public void DeathAnimation()
    {
        portalAnim.SetTrigger("deathTrigger");
    }
}
