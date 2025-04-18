using System.Collections;
using Unity.Cinemachine;
using UnityEditor;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject hitArea;

    private PlayerAnimation playerAnim;
    private Animator anim;

    private bool isAttack = false;

    private Vector3 originPos = new Vector3(0.2f, 0f, 0f);
    private Vector3 flipPos = new Vector3(-0.2f, 0f, 0f);


    private void Awake()
    {
        playerAnim = GetComponent<PlayerAnimation>();
        anim = GetComponent<Animator>();
        
    }

    public void PerformAttack()
    {
        if(isAttack == true)
        {
            return;
        }
        if(playerAnim == null)
        {
            return;
        }

        bool active = playerAnim.OnAttack();
        if (active == true)
        {
            StartCoroutine(AttackDelayCo());
        }
    }

    private IEnumerator AttackDelayCo()
    {
        isAttack = true;

        yield return null;

        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);

        if (info.IsName("Anim_Girl_Attack") == true)
        {
            float animationLength = info.length;
            yield return new WaitForSeconds(animationLength);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
        }
        isAttack = false;
    }

    public void SetHitPosition(bool isFlip)
    {
        if (isFlip == true) 
        {
            hitArea.transform.localPosition = flipPos;
        }
        else
        {
            hitArea.transform.localPosition = originPos;
        }
    }
}
