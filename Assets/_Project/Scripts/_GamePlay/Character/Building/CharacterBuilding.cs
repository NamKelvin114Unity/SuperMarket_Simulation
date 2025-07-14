using UnityEngine;
using VirtueSky.Core;
using VirtueSky.Inspector;
using VirtueSky.Variables;

public abstract class CharacterBuilding : MonoBehaviour
{
    [HeaderLine("Properties")] [SerializeField]
    protected FloatVariable timeDelayInteractVariable;

    [SerializeField] protected CharacterStateVariable characterStateVariable;

    [SerializeField] protected Transform holderCurrency;
    private bool _isCanNotAction;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(Constant.BUILDING_AREA_TAG))
        {
            if (_isCanNotAction || characterStateVariable.Value != ECharacterState.NonCarry) return;
            var buildingArea = other.gameObject.GetComponent<BuildingManager>();
            if (buildingArea.IsBuyAvailable())
            {
                _isCanNotAction = true;
                buildingArea.OnBuying();
                ItemSpawnerController.Instance.OnSpawnItemMove(holderCurrency, buildingArea.getCenterTransform,
                    0, buildingArea.GetMaxNumberHorizontal, buildingArea.GetMaxNumberVertical,
                    buildingArea.IsArrangeVertical, buildingArea.IsArrangeHorizontal, isDeActiveWhenMoveDone: true);
                App.Delay(this, timeDelayInteractVariable.Value, (() => { _isCanNotAction = false; }));
            }
        }
    }
}