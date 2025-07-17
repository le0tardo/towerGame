using UnityEngine;

public class MapMarker : MonoBehaviour
{
    [SerializeField] public MapLevelObject myLevel;
    [SerializeField] GameObject[] markers;

    bool isSelected = false;


    private void OnMouseEnter()
    {
        markers[0].SetActive(true);
    }

    private void OnMouseExit()
    {
        markers[0].SetActive(false);
    }

    private void OnMouseDown()
    {
        if (!isSelected)
        {
            MapManager.Instance.SelectMarker(this);
        }
        else
        {
            MapManager.Instance.SelectMarker(null);
        }
    }
    public void Select()
    {
        markers[1].SetActive(true);
        isSelected = true;
        MapManager.Instance.DrawLevelInfo();
        if (MapManager.Instance.selectedMap == null) { MapManager.Instance.AnimateBox(true); }

    }
    public void Deselect()
    {
        markers[1].SetActive(false);
        isSelected = false;
    }
}
