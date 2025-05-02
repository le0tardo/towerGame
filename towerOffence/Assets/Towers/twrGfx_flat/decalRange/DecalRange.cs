using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DecalRange : MonoBehaviour
{
    [SerializeField] TowerBehaviour tower;
    [SerializeField] DecalProjector decal;

    private void Start()
    {
        if (tower != null)
        {
            decal.size = new Vector3(tower.range*2,tower.range*2,decal.size.z);
        }
    }

    private void OnEnable()
    {
        decal.size = new Vector3(tower.range * 2, tower.range * 2, decal.size.z);
    }
}
