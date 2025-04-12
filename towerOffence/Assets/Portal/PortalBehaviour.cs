using UnityEngine;
using UnityEngine.Splines;

public class PortalBehaviour : MonoBehaviour
{

    [SerializeField] public SplineContainer path;
    public float pathProgression=0;
    [SerializeField] GameObject portalMarker;
    [SerializeField] Animator portalAnim;



    private void OnMouseDown()
    {
        Debug.Log("I got clicked on!");
        GameManager.instance.levelPathContainer = path;
        GameManager.instance.spawnSplinePoint = pathProgression;

        if (portalMarker != null)
        {
            portalMarker.transform.position=this.gameObject.transform.position;
        }
    }

    public void SetPathProgression()
    {

    }

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
