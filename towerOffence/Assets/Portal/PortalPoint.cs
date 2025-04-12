using System.IO;
using UnityEngine;
using UnityEngine.Splines;

public class PortalPoint : MonoBehaviour
{
    public bool claimed = false;

    [SerializeField] PortalBehaviour mainPortal;

    [SerializeField] MeshRenderer mesh;
    [SerializeField] Material material;
    [SerializeField] GameObject portalFX;

    [SerializeField] GameObject portalMarker;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")&&!claimed)
        {
            float p = other.gameObject.GetComponent<SplineAnimate>().NormalizedTime;
            
            mainPortal.pathProgression = p;

            if (GameManager.instance.levelPathContainer == mainPortal.path)
            {
                GameManager.instance.spawnSplinePoint = p;
                Debug.Log("current path updated progression.");
            }


            mesh.material = material;
            portalFX.SetActive(true);

            claimed = true;
        }
    }
    private void OnMouseDown()
    {
        if (!claimed) { return; }
        GameManager.instance.levelPathContainer = mainPortal.path;
        GameManager.instance.spawnSplinePoint = mainPortal.pathProgression;

        if (portalMarker != null)
        {
            portalMarker.transform.position = this.gameObject.transform.position;
        }
    }
}
