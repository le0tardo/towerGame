using UnityEngine;
using UnityEngine.Splines;

public class MultiPortalBehaviour : MonoBehaviour
{
    [Header("Path Chain")]
    [SerializeField] public SplineContainer path;
    public float pathProgression = 0; 
    [SerializeField] GameObject portalMarker;
    [SerializeField] Animator portalAnim;
    [SerializeField] MultiPortalBehaviour previousPortal;

    [Header("Visuals")]
    [SerializeField] GameObject meshObject;
    MeshRenderer mesh;
    [SerializeField] Material[] materials; //red, blue, grey
    [SerializeField] GameObject vfx;

    public bool claimed=false;
    public bool dead=false;

    private void Start()
    {
        mesh=meshObject.GetComponent<MeshRenderer>();
        UpdateVisuals();
    }

    private void OnMouseDown()
    {
        if (!claimed || dead) { return;}

        GameManager.instance.levelPathContainer = path;
        GameManager.instance.spawnSplinePoint = pathProgression;

        if (portalMarker != null)
        {
            portalMarker.transform.position = this.gameObject.transform.position;
        }
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

                if (portalMarker != null)
                {
                    portalMarker.transform.position = this.gameObject.transform.position;
                }
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
            mesh.material = materials[2];
            vfx.SetActive(false);
        }
        else
        {
            if (claimed)
            {
                mesh.material = materials[0];
                vfx.SetActive(true);
            }
            else
            {
                mesh.material = materials[1];
                vfx.SetActive(false);
            }
        }
    }
}
