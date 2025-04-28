using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DecalRange : MonoBehaviour
{
    [SerializeField] TowerBehaviour tower;
    [SerializeField] DecalProjector decal;

    private void Start()
    {
        if (tower == null)
        {
            decal.size = new Vector3(tower.range,tower.range,decal.size.z);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            decal.size = new Vector3(tower.range, tower.range, decal.size.z);
        }
    }
}
