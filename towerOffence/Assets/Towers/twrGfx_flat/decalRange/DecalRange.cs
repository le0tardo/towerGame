using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DecalRange : MonoBehaviour
{
    [SerializeField] TowerBehaviour tower;
    [SerializeField] DecalProjector decal;
    [SerializeField] GameObject rangeMesh;

    private void Start()
    {
        if (tower != null)
        {
            //decal.size = new Vector3(tower.range*2,tower.range*2,decal.size.z);
            rangeMesh.transform.localScale = new Vector3(tower.range,1,tower.range);
        }
    }

    private void OnEnable()
    {
        //decal.size = new Vector3(tower.range * 2, tower.range * 2, decal.size.z);
        rangeMesh.transform.localScale = new Vector3(tower.range, 1, tower.range);
    }
}
