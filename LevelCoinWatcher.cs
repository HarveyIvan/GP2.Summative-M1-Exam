using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCoinWatcher : MonoBehaviour
{
    [Header("References")]
    public PlayerInventory playerInventory;   // drag your Player here (the one with PlayerInventory)

    [Header("Flow")]
    [Tooltip("Scene to load when ALL coins in the current scene are collected.")]
    public string nextSceneName;              // e.g., for SampleScene put "NewScene"; for NewScene put "startscene"

    private int totalCoinsInScene;

    void Awake()
    {
        // Count all coins (active or inactive) that can be collected in this scene
        totalCoinsInScene = FindObjectsOfType<Coins>(true).Length;

        if (playerInventory == null)
        {
            playerInventory = FindObjectOfType<PlayerInventory>();
        }
    }

    void OnEnable()
    {
        if (playerInventory != null)
            playerInventory.OnCoinCollected.AddListener(OnCoinCollected);
    }

    void OnDisable()
    {
        if (playerInventory != null)
            playerInventory.OnCoinCollected.RemoveListener(OnCoinCollected);
    }

    private void OnCoinCollected(PlayerInventory inv)
    {
        // When player's coin count reaches the total in this scene, switch scene
        if (inv.NumberofCoins >= totalCoinsInScene && !string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
