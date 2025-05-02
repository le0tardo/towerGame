using UnityEngine;
using UnityEngine.Splines;

public class MultiPortalBehaviour : MonoBehaviour
{
    [Header("Path Chain")]
    [SerializeField] public SplineContainer path;
    public float pathProgression = 0; 
    [SerializeField] MultiPortalBehaviour previousPortal;

    [Header("Visuals")]
    [SerializeField] Animator portalAnim;

    public bool claimed=false;
    public bool dead=false;

    private void Start()
    {
        UpdateVisuals();
    }

    private void OnMouseDown()
    {
        if (!claimed || dead) { return;}

        GameManager.instance.levelPathContainer = path;
        GameManager.instance.spawnSplinePoint = pathProgression;
        GameManager.instance.currentPortal = this.gameObject;
        GameManager.instance.SetPortalVFX();
        UpdateVisuals();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !claimed)
        {
            float p = other.gameObject.GetComponent<SplineAnimate>().NormalizedTime;
            pathProgression = p;

            if (GameManager.instance.levelPathContainer == path)
            {
                GameManager.instance.spawnSplinePoint = p;
            }

            if (GameManager.instance.currentPortal == previousPortal.gameObject)
            {
                GameManager.instance.currentPortal = this.gameObject;
                GameManager.instance.SetPortalVFX();
            }

            claimed = true;
            UpdateVisuals();
            if (previousPortal != null) { previousPortal.dead = true;previousPortal.UpdateVisuals(); }
        }
    }

    private void UpdateVisuals()
    {
        if (dead)
        {
            portalAnim.SetTrigger("sleep");
        }
        else
        {
            if (claimed)
            {
                portalAnim.SetTrigger("wakeUp");
            }
            else
            {
                portalAnim.SetTrigger("dead");
            }
        }
    }
}
