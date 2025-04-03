using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") == true)
        {
            //TODO �״°�.
            PlayerController player = GameManager.Instance.player;

            if (player != null) 
            {
                player.transform.position = GameManager.Instance.startPos;
                SoundManager.Instance.PlaySFX(SFXType.SFX_UI_Return);
            }
        }
    }
}
