using UnityEngine;

public class ButtonAnimation : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void ClickButton()
    {
        anim.SetTrigger("click");
    }
}
