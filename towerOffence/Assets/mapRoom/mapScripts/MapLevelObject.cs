using UnityEngine;

[CreateAssetMenu(fileName = "NewMapLevelObject", menuName = "Map/Level Object")]
public class MapLevelObject : ScriptableObject
{
    public string levelName;
    [TextArea]
    public string levelDescription;
    public Sprite previewImage;
    public bool defeated = false;
    public string LevelToLoad;
}
