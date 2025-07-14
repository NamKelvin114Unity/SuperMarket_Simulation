using System;
using System.Collections.Generic;
using UnityEngine;
using VirtueSky.Core;
using VirtueSky.Inspector;
using VirtueSky.ObjectPooling;
using VirtueSky.Variables;

public abstract class CharacterInteractArea : MonoBehaviour
{
    [HeaderLine("Data")] [SerializeField] protected CollectorItemData collectorItem;

    [HeaderLine("Properties")] [SerializeField]
    protected ERoleInteractItem roleInteract;

    [SerializeField] protected CharacterStateVariable characterState;

    [SerializeField] protected List<ItemResource> itemResources = new List<ItemResource>();

    [SerializeField] protected Transform resourceHolder;

    [HeaderLine("ResourceSupplier")] [SerializeField]
    protected FloatVariable radiusCheckSupplierResourceVariable;

    [SerializeField] protected FloatVariable timeDelayInteractSupplierResourceVariable;

    [SerializeField] protected LayerMask resourceSupplierLayer;

    [HeaderLine("ResourceSeller")] [SerializeField]
    protected FloatVariable radiusCheckSellerResourceVariable;

    [SerializeField] protected FloatVariable timeDelayInteractSellerResourceVariable;

    [SerializeField] protected LayerMask resourceSellLayer;

    [HeaderLine("ResourcePayment")] [SerializeField]
    protected FloatVariable radiusCheckResourcePaymentVariable;

    [SerializeField] protected FloatVariable timeDelayInteractResourcePaymentVariable;

    [SerializeField] protected LayerMask resourcePaymentLayer;
    protected bool isDetectSupplier;
    protected bool isDetectSeller;
    protected bool isDetectPayment;

    public void OnEnable()
    {
        DoEnable();
        isDetectSeller = false;
        isDetectSupplier = false;
        isDetectPayment = false;
    }

    private void OnDisable()
    {
        DoDisable();
    }

    protected virtual void DoDisable()
    {
    }

    protected virtual void DoEnable()
    {
        if (itemResources.Count > 0)
        {
            foreach (var item in itemResources)
            {
                item.DeSpawnItem();
            }
        }

        itemResources.Clear();

        collectorItem.ResetData();
    }


    private void Update()
    {
        CheckAreaResourceSupplier();
        CheckAreaResourceSeller();
        CheckAreaResourcePayment();
    }

    void CheckAreaResourceSupplier()
    {
        if (isDetectSupplier) return;
        Collider[] hits = Physics.OverlapSphere(transform.position, radiusCheckSupplierResourceVariable.Value,
            resourceSupplierLayer);
        foreach (var hit in hits)
        {
            if (hit.gameObject != null)
            {
                isDetectSupplier = true;
                ResourceSupplierManager resourceSupplierManager =
                    hit.gameObject.GetComponent<ResourceSupplierManager>();
                OnDetectResourceSupplier(resourceSupplierManager);
                App.Delay(this, timeDelayInteractSupplierResourceVariable.Value,
                    (() => { isDetectSupplier = false; }));
                break;
            }
        }
    }

    void CheckAreaResourceSeller()
    {
        if (isDetectSeller) return;
        Collider[] hits = Physics.OverlapSphere(transform.position, radiusCheckSellerResourceVariable.Value,
            resourceSellLayer);
        foreach (var hit in hits)
        {
            if (hit.gameObject != null)
            {
                isDetectSeller = true;
                ResourceSellerManager resourceSellerManager =
                    hit.gameObject.GetComponent<ResourceSellerManager>();
                OnDetectResourceSeller(resourceSellerManager);
                App.Delay(this, timeDelayInteractSupplierResourceVariable.Value,
                    (() => { isDetectSeller = false; }));
                break;
            }
        }
    }

    void CheckAreaResourcePayment()
    {
        if (isDetectPayment) return;
        Collider[] hits = Physics.OverlapSphere(transform.position, radiusCheckResourcePaymentVariable.Value,
            resourcePaymentLayer);
        foreach (var hit in hits)
        {
            if (hit.gameObject != null)
            {
                isDetectPayment = true;
                ResourcePaymentManager resourceManagerPayment =
                    hit.gameObject.GetComponent<ResourcePaymentManager>();
                OnDetectResourcePayment(resourceManagerPayment);
                App.Delay(this, timeDelayInteractResourcePaymentVariable.Value,
                    (() => { isDetectPayment = false; }));
                break;
            }
        }
    }

    protected virtual void OnDetectResourceSupplier(ResourceSupplierManager resourceSupplierManager)
    {
    }

    protected virtual void OnDetectResourceSeller(ResourceSellerManager resourceSellerManager)
    {
    }

    protected virtual void OnDetectResourcePayment(ResourcePaymentManager resourceManagerPayment)
    {
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusCheckSupplierResourceVariable.Value);
        Gizmos.color = Color.indianRed;
        Gizmos.DrawWireSphere(transform.position, radiusCheckSellerResourceVariable.Value);
    }
}

public enum ERoleInteractItem
{
    Seller,
    Customer,
}