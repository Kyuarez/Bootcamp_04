using TMPro;
using UnityEngine;

public class UISetting : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI beadCountText;
    private bool isPaused = false;

    private void Start()
    {
        GameManager.Instance.OnUpdateBeadCount += UpdateBeadCount;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ReGame();
            }
            else
            {
                Pause();
            }
        }
    }


    public void Pause()
    {
        panel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ReGame()
    {
        panel.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
    }
    public void UpdateBeadCount(int count)
    {
        beadCountText.text = count.ToString();
    }

    #region OnClick
    public void OnClickPlay()
    {

    }

    public void OnClickPlus()
    {

    }
    #endregion
}
