using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement movement;
    private PlayerAttack attack;
    private PlayerHealth health;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        attack = GetComponent<PlayerAttack>();
        health = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        movement.HandleMovement();

        if (Input.GetButtonDown("Fire1"))
        {
            attack.PerformAttack();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Pickup") == true)
        {
            ItemData data = collision.gameObject.GetComponent<ItemData>();
            if (data != null) 
            {
                //TODO : 이거 테스트 코드임
                if (data.ItemID == 477) 
                {
                    SoundManager.Instance.PlaySFX(SFXType.SFX_UI_Accept);
                    GameManager.Instance.UpdateBeadCount(1);
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}
