using UnityEngine;

public class DeathFXPool : MonoBehaviour
{
    public static DeathFXPool Instance { get; set; }
    [SerializeField] GameObject[] deathFX;

    private void Awake()
    {
        if (Instance == null){Instance = this;}
        else{Destroy(gameObject);}
    }
    public void SpawnDeathFX(Vector3 pos)
    {
        for (int i = 0; i < deathFX.Length; i++)
        {
            if (!deathFX[i].activeInHierarchy)
            {
                deathFX[i].SetActive(true);
                deathFX[i].transform.position = pos;
                break;
            }
        }
    }
}
