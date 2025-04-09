using UnityEngine;
using UnityEngine.Splines;

public class PortalPoint : MonoBehaviour
{
    public bool claimed = false;
    [SerializeField] MeshRenderer mesh;
    [SerializeField] Material material;
    [SerializeField] GameObject portalFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")&&!claimed)
        {
            float p = other.gameObject.GetComponent<SplineAnimate>().NormalizedTime;
            GameManager.instance.spawnSplinePoint = p;

            mesh.material = material;
            portalFX.SetActive(true);

            claimed = true;
        }
    }
}
