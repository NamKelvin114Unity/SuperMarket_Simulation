using UnityEngine;
using VirtueSky.Variables;

[CreateAssetMenu(menuName = "Data/ResourceData", fileName = "resource_data")]
public class ResourceData : ScriptableObject
{
    [SerializeField] private IntegerVariable maxValueResourceVariable;
    [SerializeField] private FloatVariable timeSpawnResourceVariable;
    [SerializeField] private BaseItem itemResource;
    public float GetTimeSpawnResourceValue => timeSpawnResourceVariable.Value;
    public BaseItem GetItemResourceValue => itemResource;
    public int GetMaxValueResource => maxValueResourceVariable.Value;
}