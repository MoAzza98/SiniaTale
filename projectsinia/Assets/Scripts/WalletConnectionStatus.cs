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
    [SerializeField] private GameObject connectedPanel;

    // Start is called before the first frame update
    void Start()
    {
        walletConnected = (PlayerPrefs.GetInt("Name") != 0);
    }

    // Update is called once per frame
    void Update()
    {
        WalletSetup();
    }

    public void WalletConnected()
    {
        walletConnected = true;

        PlayerPrefs.SetInt("WalletConnection", (walletConnected ? 1 : 0));
        PlayerPrefs.Save();
        walletConnected = (PlayerPrefs.GetInt("Name") != 1);
        connectedPanel.SetActive(true);
    }

    public void WalletDisconnected()
    {
        walletConnected = false;

        PlayerPrefs.SetInt("WalletConnection", (walletConnected ? 1 : 0));
        PlayerPrefs.Save();
        walletConnected = (PlayerPrefs.GetInt("Name") != 0);
        connectedPanel.SetActive(false);
    }

    public void WalletStatusDisconnected()
    {

        walletStatus.text = "Wallet disconnected";
        walletConnectionLight.color = Color.red;
    }

    public void WalletStatusConnected()
    {
        walletStatus.text = "Wallet connected";
        walletConnectionLight.color = Color.green;
    }

    public void WalletSetup()
    {
        if (walletConnected)
        {
            WalletStatusConnected();
        }
        else
        {
            WalletStatusDisconnected();
        }
    }

}
