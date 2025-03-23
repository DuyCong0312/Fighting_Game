using UnityEngine;

public class PlayerAnimation : MonoBehaviour 
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        UpdateAnimation();
    }
    public void UpdateAnimation()
    {
        anim.SetInteger("CurrentState", PlayerStateManager.Instance.CurrentState);
    }
}