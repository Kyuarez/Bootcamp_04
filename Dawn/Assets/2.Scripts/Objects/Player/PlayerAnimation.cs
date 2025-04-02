using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void SetRunning(bool isMove)
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("Anim_Girl_Jump") == true || info.IsName("Anim_Girl_Fall") == true)
        {
            return;
        }

        anim.SetBool("IsMove", isMove);
    }

    public bool OnAttack()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("Anim_Girl_Jump") == true || info.IsName("Anim_Girl_Fall") == true)
        {
            return false;
        }

        anim.SetTrigger("OnAttack");
        return true;
    }
    public void OnDie()
    {
        anim.SetTrigger("OnDie");
    }
    public void OnJump()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("Anim_Girl_Jump") == true)
        {
            return;
        }

        anim.SetTrigger("OnJump");
    }
    public void OnFall()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        if(info.IsName("Anim_Girl_Fall") == true)
        {
            return;
        }

        anim.SetTrigger("OnFall");
        anim.ResetTrigger("OnJump");
    }
    public void OnGrounded()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("Anim_Girl_Fall") == false)
        {
            anim.ResetTrigger("OnGrounded");
            return;
        }

        anim.SetTrigger("OnGrounded");
        anim.ResetTrigger("OnFall");
    }
}
