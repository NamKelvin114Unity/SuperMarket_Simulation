using UnityEngine;

[CreateAssetMenu(menuName = "Data/ArrangeData", fileName = "arrange_data")]
public class ArrangeData : ScriptableObject
{
    [SerializeField] int maxNumberHorizontal;
    [SerializeField] int maxNumberVertical;
    [SerializeField] bool isArrangeHorizontal;
    [SerializeField] bool isArrangeVertical;
    [SerializeField] bool isArrange2D;
    public int GetMaxNumberHorizontal => maxNumberHorizontal;
    public int GetMaxNumberVertical => maxNumberVertical;
    public bool IsArrangeHorizontal => isArrangeHorizontal;
    public bool IsArrangeVertical => isArrangeVertical;
    public bool IsArrange2D => isArrange2D;
}