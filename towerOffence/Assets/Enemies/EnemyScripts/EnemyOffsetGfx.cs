using UnityEngine;

public class EnemyOffsetGfx : MonoBehaviour
{
    private void OnEnable()
    {
        float r = Random.Range(-0.5f, 0.5f);
        transform.position = new Vector3(transform.position.x + r, transform.position.y, transform.position.z);

        //add animator random offset
    }
}
