using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Level Stats")]
    public int waves;
    public int gateHp;
    
    [Header("Object references")]
    ManaManager mana;
    [SerializeField] Gate gate;
    [SerializeField] PortalBehaviour portal;

    [Header("Object Variables")]
    public int wave = 1;
    public int maxWaves = 3;
    public int enemyCount = 0;
    public int cheapestUnit;
    public bool levelComplete = false;

    [SerializeField] TMP_Text waveCountText;
    [SerializeField] GameObject loseObject;
    [SerializeField] GameObject winObject;
    [SerializeField] GameObject levelHud;

    private void Awake()
    {
        instance = this;
        if (waves != 0) maxWaves = waves;
        if (gateHp!=0)gate.gateHealth = gateHp;
        Time.timeScale = 1.0f;
    }
    private void Start()
    {
        mana =GetComponent<ManaManager>();
    }

    private void Update()
    {
        CountEnemies();
        if (!levelComplete)
        {
            CheckLoss();
            CheckWin();
        }

        //debug shit
        if (Input.GetKeyDown(KeyCode.R))
        {
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }
    }
    void CountEnemies()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        waveCountText.text = "Wave " +wave + "/" + maxWaves;
    }

    void CheckLoss()
    {
        if (cheapestUnit<1){Debug.Log("No loss condition!");}

        if ((wave >= maxWaves) && (enemyCount == 0) && (mana.mana < cheapestUnit)) //lose condition
        {
           loseObject.SetActive(true);
            levelComplete = true;
            levelHud.SetActive(false);
        }
    }

    void CheckWin()
    {
        if (gate.gateHealth <= 0)
        {
            winObject.SetActive(true);
            levelComplete= true;
            levelHud.SetActive(false);
            if (loseObject.activeInHierarchy) { loseObject.SetActive(false); }
            Time.timeScale = 0;
        }

    }
 
}
