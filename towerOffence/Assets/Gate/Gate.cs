using UnityEngine;
using TMPro;
public class Gate : MonoBehaviour
{
    public int gateHealth=100;
    int gateMaxHealth;
    [SerializeField] Animator gateAnim;
    [SerializeField] TMP_Text healthText;

    private void Start()
    {
        gateMaxHealth=gateHealth;
        healthText.text = gateHealth + "/" + gateMaxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyBehaviour enemy=other.GetComponent<EnemyBehaviour>();
            if (enemy != null)
            {
                float dmg = enemy.damage;
                Hit(Mathf.RoundToInt(dmg));
            }

            other.gameObject.SetActive(false);

        }
    }

    void Hit(int damage)
    {
        gateHealth -= damage;
        gateAnim.SetTrigger("hitTrigger");
        healthText.text = gateHealth + "/" + gateMaxHealth;
    }
}
