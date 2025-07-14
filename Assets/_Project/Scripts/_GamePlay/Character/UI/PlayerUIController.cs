using UnityEngine;
using VirtueSky.Inspector;

public class PlayerUIController : MonoBehaviour
{
    [HeaderLine("Properties")] [SerializeField]
    private GameObject groupMaxItem;

    public void SetGroupMaxItem(bool isActive)
    {
        groupMaxItem.SetActive(isActive);
    }
}