using UnityEngine;

public class SpawnButtonGraphics : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ClickAnimation()
    {
        anim.Play("spawnClick",0);
    }
}
