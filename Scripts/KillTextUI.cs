using UnityEngine;
using TMPro;

public class KillTextUI : MonoBehaviour
{
    public PlayerInventory playerInventory;   
    public TMP_Text text;                     

    int lastShown = -1;

    void Awake()
    {
        if (!text) text = GetComponent<TMP_Text>();
        if (!playerInventory) playerInventory = FindObjectOfType<PlayerInventory>();
    }

    void Update()
    {
        if (!playerInventory || !text) return;

        
        if (playerInventory.NumberofKills != lastShown)
        {
            lastShown = playerInventory.NumberofKills;
            text.text = $"{lastShown}";
        }
    }
}
