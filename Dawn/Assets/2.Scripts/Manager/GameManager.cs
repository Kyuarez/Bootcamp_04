using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int beadCount = 0;

    public Action<int> OnUpdateBeadCount;

    public PlayerController player;
    public Vector3 startPos = new Vector3(-6.06f, -3.04f, 0f);

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
    }

    public void UpdateBeadCount(int amount)
    {
        beadCount += amount;
        OnUpdateBeadCount?.Invoke(beadCount);
    }
}
