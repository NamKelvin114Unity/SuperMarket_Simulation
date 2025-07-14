using System.Collections.Generic;
using UnityEngine;
using VirtueSky.Inspector;
using VirtueSky.Variables;

[CreateAssetMenu(menuName = "Data/NPCData", fileName = "npc_data")]
public class NPCData : ScriptableObject
{
    [HeaderLine("MissionData")] [SerializeField]
    NPCMissionData missionData;

    [HeaderLine("CharacterState")] [SerializeField]
    CharacterStateVariable characterStateVariable;

    [HeaderLine("MissionTypeVariable")] [SerializeField]
    NPCMissionTypeVariable missionTypeVariable;

    [HeaderLine("DataCollector")] [SerializeField]
    CollectorItemData collectorItemData;

    [HeaderLine("ResourceSupplier")] [SerializeField]
    FloatVariable radiusCheckResourceSupplierVariable;

    [SerializeField] FloatVariable timeDelayCheckResourceSupplierVariable;

    [HeaderLine("ResourceSeller")] [SerializeField]
    FloatVariable radiusCheckResourceSellerVariable;

    [SerializeField] FloatVariable timeDelayCheckResourceSellerVariable;

    [HeaderLine("ResourcePayment")] [SerializeField]
    FloatVariable radiusCheckResourcePaymentVariable;

    [SerializeField] FloatVariable timeDelayCheckResourcePaymentVariable;


    [HeaderLine("Material")] [SerializeField]
    List<Material> materials = new List<Material>();

    [HeaderLine("MoveSpeed")] [SerializeField]
    FloatVariable moveSpeedVariable;

    [HeaderLine("MoveMinDistace")] [SerializeField]
    FloatVariable moveMinDistaceVariable;

    [HeaderLine("RotationSpeed")] [SerializeField]
    FloatVariable rotationSpeedVariable;

    public NPCMissionData MissionData => this.missionData;
    public CharacterStateVariable CharacterState => this.characterStateVariable;
    public NPCMissionTypeVariable MissionType => this.missionTypeVariable;
    public CollectorItemData CollectorItemData => this.collectorItemData;

    public FloatVariable RadiusCheckResourceSupplier => this.radiusCheckResourceSupplierVariable;

    public FloatVariable TimeDelayCheckResourceSupplier => this.timeDelayCheckResourceSupplierVariable;

    public FloatVariable RadiusCheckResourceSeller => this.radiusCheckResourceSellerVariable;

    public FloatVariable TimeDelayCheckResourceSeller => this.timeDelayCheckResourceSellerVariable;

    public FloatVariable RadiusCheckResourcePayment => this.radiusCheckResourcePaymentVariable;

    public FloatVariable TimeDelayCheckResourcePayment => this.timeDelayCheckResourcePaymentVariable;

    public Material Material => materials[Random.Range(0, materials.Count)];

    public FloatVariable MoveSpeed => this.moveSpeedVariable;

    public FloatVariable MoveMinDistace => this.moveMinDistaceVariable;

    public FloatVariable RotationSpeed => this.rotationSpeedVariable;
}