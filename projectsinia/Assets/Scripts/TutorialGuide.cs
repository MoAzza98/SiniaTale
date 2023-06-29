using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialGuide : MonoBehaviour
{
    [TextArea(5,10)]
    public string dashText;
    [TextArea(5,10)]
    public string moveText;
    [TextArea(5,10)]
    public string rewindText;
    [TextArea(5,10)]
    public string attackText;
    [TextArea(5,10)]
    public string wallJumpText;

    [SerializeField] private GameObject infoPanel;

    [SerializeField] private TextMeshProUGUI infoPanelText;

    // Start is called before the first frame update
    void Start()
    {
        infoPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DashInfo()
    {
        infoPanelText.text = dashText;
        infoPanel.SetActive(true);
    }
    public void RewindInfo()
    {
        infoPanelText.text = rewindText;
        infoPanel.SetActive(true);
    }
    public void AttackInfo()
    {
        infoPanelText.text = attackText;
        infoPanel.SetActive(true);
    }
    public void MoveInfo()
    {
        infoPanelText.text = moveText;
        infoPanel.SetActive(true);
    }
    public void WallJumpInfo()
    {
        infoPanelText.text = wallJumpText;
        infoPanel.SetActive(true);
    }

    public void ClosePanel()
    {
        infoPanel.SetActive(false);
    }

}
