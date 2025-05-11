using UnityEngine;

public class ClickTower : MonoBehaviour
{
    TowerBehaviour tb;

    private void Start()
    {
        tb = GetComponentInChildren<TowerBehaviour>();
    }

    private void OnMouseDown()
    {
        tb.MouseClick();
    }
}
