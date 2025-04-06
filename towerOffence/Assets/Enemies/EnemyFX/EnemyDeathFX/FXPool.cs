using UnityEngine;
using System.Collections;

public class FXPool : MonoBehaviour
{
    public static FXPool Instance { get; set; }
    [SerializeField] GameObject[] FX;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }
    public void SpawnFX(Vector3 pos)
    {
        for (int i = 0; i < FX.Length; i++)
        {
            if (!FX[i].activeInHierarchy)
            {
                FX[i].SetActive(true);
                FX[i].transform.position = pos;
                StartCoroutine(DeactivateFX(FX[i]));
                break;
            }
        }
    }
    IEnumerator DeactivateFX(GameObject fx)
    {
        yield return new WaitForSeconds(0.5f);
        fx.SetActive(false);
    }
}

