using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public int NumberofCoins { get; private set; }
    public int NumberofKills { get; private set; }  

    
    public UnityEvent<PlayerInventory> OnCoinCollected;

    public void CoinsCollected()
    {
        NumberofCoins++;
        OnCoinCollected.Invoke(this);
    }

    public void RegisterKill()  
    {
        NumberofKills++;
        OnCoinCollected.Invoke(this); 
    }
}
