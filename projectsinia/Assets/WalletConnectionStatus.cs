using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WalletConnectionStatus : MonoBehaviour
{
    [SerializeField] private bool walletConnected;
    [SerializeField] private TextMeshProUGUI walletStatus;
    [SerializeField] private Image walletConnectionLight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (walletConnected)
        {
            WalletConnected();
        }
        */
    }

    public void WalletConnected()
    {
        walletStatus.text = "Wallet connected";
        walletConnectionLight.color = Color.green;
    }

    public void WalletDisconnected()
    {
        walletStatus.text = "Wallet disconnected";
        walletConnectionLight.color = Color.red;
    }

}
