using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    private TextMeshProUGUI coinText;
    void Start()
    {
        coinText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateCoinText(PlayerInventory playerInventory)
    {
        coinText.text = playerInventory.NumberofCoins.ToString();
    }
}