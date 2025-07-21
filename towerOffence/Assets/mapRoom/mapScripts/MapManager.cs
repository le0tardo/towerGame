using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; set; }

    public MapMarker selectedMap=null;

    [Header("UI")]
    [SerializeField] GameObject levelBox;
    [SerializeField] TMP_Text levelName;
    [SerializeField] Animator boxAnim;
    [SerializeField] Animator barAnim;

    SceneLoader sceneLoader;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        sceneLoader = GetComponent<SceneLoader>();
    }

    public void SelectMarker(MapMarker newMarker)
    {
        // CASE: Going from instance to null
        if (newMarker == null && selectedMap != null)
        {
            selectedMap.Deselect();
            selectedMap = null;
            AnimateBox(false);
            return;
        }

        // CASE: Going from null to instance
        if (newMarker != null && selectedMap == null)
        {
            selectedMap = newMarker;
            selectedMap.Select();
            AnimateBox(true);
            return;
        }

        // CASE: Switching instance to different instance (NO callback)
        if (selectedMap != null && selectedMap != newMarker)
        {
            selectedMap.Deselect();
            selectedMap = newMarker;
            selectedMap.Select();
            return;
        }

        // CASE: Clicking same selected marker (do nothing, already handled)
    }

    public void AnimateBox(bool moveUp)
    {
        if (moveUp){ boxAnim.SetTrigger("up");barAnim.SetTrigger("in"); }
        else { boxAnim.SetTrigger("down"); barAnim.SetTrigger("out"); }
    }
    public void DrawLevelInfo()
    {
        levelName.text = selectedMap.myLevel.levelName;
    }

    public void GoToLevel()
    {
        if (selectedMap != null)
        {
            string sceneToLoad= selectedMap.myLevel.LevelToLoad.ToString();
            sceneLoader.LoadScene(sceneToLoad);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left-click
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out RaycastHit hit))
            {
                // Clicked on nothing
                if (selectedMap != null)
                {
                    selectedMap.Deselect();
                    selectedMap = null;
                    AnimateBox(false);
                }
            }
        }
    }

}
