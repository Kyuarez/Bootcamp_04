using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerEvent : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI descriptionText;
   
    public void OnUIPlayerEvent(PlayerEventType eventType)
    {
        Sprite icon = GetSpriteByPlayerEventType(eventType);
        string desc = GetDescriptionByPlayerEventType(eventType);

        if (icon == null || desc == null)
        {
            if (panel.activeSelf == true)
            {
                panel.SetActive(false);
            }
            return;
        }

        iconImage.sprite = icon;
        descriptionText.text = desc;
        panel.SetActive(true);
    }

    public Sprite GetSpriteByPlayerEventType(PlayerEventType eventType)
    {


        return null;
    }

    public string GetDescriptionByPlayerEventType(PlayerEventType eventType)
    {


        return null;
    }
    
}
