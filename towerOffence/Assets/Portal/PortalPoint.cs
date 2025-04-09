using UnityEngine;
using UnityEngine.Splines;

public class PortalPoint : MonoBehaviour
{
    public bool claimed = false;
    [SerializeField] MeshRenderer mesh;
    [SerializeField] Material material;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")&&!claimed)
        {
            float p = other.gameObject.GetComponent<SplineAnimate>().NormalizedTime;
            Debug.Log("Enemys new spawn splaine position is: "+p);
            GameManager.instance.spawnSplinePoint = p;
            claimed = true;
        }
    }
}
