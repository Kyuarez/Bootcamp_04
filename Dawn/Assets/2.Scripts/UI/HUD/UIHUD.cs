using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI beadCountText;

    private void Start()
    {
        GameManager.Instance.OnUpdateBeadCount += UpdateBeadCount;
    }

    public void UpdateBeadCount(int amount)
    {
        beadCountText.text = amount.ToString();
    }
}
