using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VirtueSky.Core;
using VirtueSky.Inspector;

public class NPCUIController : MonoBehaviour
{
    [HeaderLine("GroupMission")] [SerializeField]
    private Image icon;

    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] GameObject groupUI;

    public void OnShowUIMission(bool isUsingText, Sprite iconSprite, string content, float timeDeActive = 0)
    {
        icon.sprite = iconSprite;
        text.gameObject.SetActive(isUsingText);
        text.text = content;
        groupUI.SetActive(true);
        if (timeDeActive > 0)
        {
            App.Delay(this, timeDeActive, (() => { groupUI.SetActive(false); }));
        }
    }
}