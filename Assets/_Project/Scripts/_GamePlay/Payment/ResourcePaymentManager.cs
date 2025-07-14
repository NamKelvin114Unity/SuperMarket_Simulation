using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VirtueSky.Core;
using VirtueSky.Events;
using VirtueSky.Inspector;
using VirtueSky.ObjectPooling;
using VirtueSky.Variables;

public class ResourcePaymentManager : MonoBehaviour
{
    [ReadOnly] [SerializeField] private bool isReadyDoPayment;
    [HeaderLine("Event")] [SerializeField] private EventNoParam moveOneCoinDone;
    [SerializeField] private EventNoParam moveAllCoinDone;
    [HeaderLine("Data")] [SerializeField] private ItemResourcePriceListData itemResourcePriceListData;
    [SerializeField] ArrangeData arrangeData;
    [SerializeField] private FloatVariable delayTimePackageVariable;
    [SerializeField] private FloatVariable delayTimeTakeMoneyVariable;

    [HeaderLine("Properties")] [SerializeField]
    Transform holderMoney;

    [ReadOnly] [SerializeField] List<ItemResource> currentMoneyList = new List<ItemResource>();
    [SerializeField] HolderItem holderItem;
    [SerializeField] Transform holderItemPlace;

    IPayment _currentCustomer = null;
    private HolderItem _currentHolderItem = null;
    private bool _isDoingPayment = false;
    private int _currentMoneyAmount;
    private int _currentAmount = 0;

    public void OnReadyDoPayment(IPayment customer)
    {
        if (_currentCustomer == null)
            _currentCustomer = customer;
    }

    public void OnStartDoPayment(Transform playerHolderMoney)
    {
        if (_currentCustomer != null && !_isDoingPayment)
        {
            _isDoingPayment = true;
            _currentAmount += CheckOrder() / itemResourcePriceListData.GetUnitPrice();
            _currentCustomer.PaymentValue(CheckOrder(), OnCompletePayment, holderMoney,
                itemResourcePriceListData.GetUnitPrice(), arrangeData, _currentAmount);
        }

        if (currentMoneyList.Count > 0)
        {
            ItemResource item = currentMoneyList[0];
            item.OnItemMove(playerHolderMoney, 0, 1, 1, false, false, (() => { item.DeSpawnItem(); }), false);
            CoinSystem.AddCoinStatic(itemResourcePriceListData.GetUnitPrice());
            moveOneCoinDone.Raise();
            moveAllCoinDone.Raise();
            currentMoneyList.Remove(item);
            _currentAmount--;
        }

        void OnPackResource()
        {
            if (!_currentHolderItem)
            {
                OnSpawnHolderItem();
            }

            UniTask.WaitUntil((() => _currentHolderItem));
            List<ItemResource> itemResources = new List<ItemResource>();
            itemResources = _currentCustomer.ItemResources;
            ArrangeData arrangeData = _currentHolderItem.ArrangeData;
            App.Delay(this, delayTimePackageVariable.Value, (() =>
            {
                for (int i = itemResources.Count - 1; i >= 0; i--)
                {
                    itemResources[i].OnItemMove(_currentHolderItem.HolderItemPlace, i,
                        arrangeData.GetMaxNumberHorizontal,
                        arrangeData.GetMaxNumberVertical, arrangeData.IsArrangeVertical,
                        arrangeData.IsArrangeHorizontal,
                        null, arrangeData.IsArrange2D);
                }

                UniTask.WaitUntil((() => itemResources.Count <= 0));
                _currentHolderItem.PlayAnimClose();
                var itemResource = _currentHolderItem.gameObject.GetComponent<ItemResource>();
                App.Delay(this, delayTimePackageVariable.Value, (() =>
                {
                    _currentCustomer.HolderItemResource = itemResource;
                    _currentCustomer.IsCompletePayment = true;
                    _currentCustomer.OnCompletePayment();
                    _currentHolderItem = null;
                    _currentCustomer = null;
                    _isDoingPayment = false;
                }));
            }));
        }

        void OnSpawnHolderItem()
        {
            var obj = holderItem.gameObject.SpawnCustom(holderItemPlace, worldPositionStays: false).gameObject;
            obj.transform.localPosition = Vector3.zero;
            _currentHolderItem = obj.GetComponent<HolderItem>();
        }

        void OnCompletePayment()
        {
            App.Delay(this, delayTimeTakeMoneyVariable.Value, (() =>
            {
                for (int i = 0; i < holderMoney.childCount; i++)
                {
                    var itemResource = holderMoney.GetChild(i).gameObject.GetComponent<ItemResource>();
                    if (itemResource && itemResource.ItemType == EItemType.Money &&
                        !currentMoneyList.Contains(itemResource))
                    {
                        currentMoneyList.Add(itemResource);
                    }
                }

                OnPackResource();
            }));
        }

        int CheckOrder()
        {
            int sum = 0;
            for (int i = 0; i < _currentCustomer.ItemResources.Count; i++)
            {
                sum += itemResourcePriceListData.CheckOrder(_currentCustomer.ItemResources[i].ItemType);
            }

            return sum;
        }
    }
}