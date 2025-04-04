using System.Collections;
using UnityEngine;

public class ScareCrowController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spr;

    private Color originColor;
    private float colorChangeDuration = 0.5f;

    private Coroutine coroutine;

    private void Awake()
    {
        originColor = spr.color;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack") == true)
        {
            CameraShake shake = Camera.main.GetComponent<CameraShake>();
            if (shake != null)
            {
                //shake.OnCameraShake(0.5f, 0.1f);
                shake.GenerateCameraImpulse();
            }


            FXManager.Instance.FXPlay(FXType.FX_Player_NormalAttack, transform.position, new Vector3(1, 1, 1));
            coroutine = StartCoroutine(ChangeColorTemporaily());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
            spr.color = originColor;
        }
    }

    IEnumerator ChangeColorTemporaily()
    {
        SoundManager.Instance.PlaySFX(SFXType.SFX_Weapon_clash1);
        spr.color = Color.red;
        yield return new WaitForSeconds(colorChangeDuration);
        spr.color = originColor;
    }
}
