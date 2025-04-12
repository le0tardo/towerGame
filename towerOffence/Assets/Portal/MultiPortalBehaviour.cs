using UnityEngine;
using UnityEngine.Splines;

public class MultiPortalBehaviour : MonoBehaviour
{
    [Header("Path Chain")]
    [SerializeField] public SplineContainer path;
    public float pathProgression = 0; 
    [SerializeField] GameObject portalMarker;
    [SerializeField] Animator portalAnim;
    [SerializeField] GameObject previousPortal;

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
