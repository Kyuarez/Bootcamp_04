using UnityEngine;

public class PlayerEventObject : MonoBehaviour
{
    public PlayerEventType EventType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") == true)
        {

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {

        }
    }
}
