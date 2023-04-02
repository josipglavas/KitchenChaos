using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlateKitchenObject : KitchenObject {

    public event EventHandler<OnIgredientAddedEventArgs> OnIngredientAdded;
    public class OnIgredientAddedEventArgs: EventArgs {
        public KitchenObjectSO kitchenObjectSO;
    }
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;

    private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake() {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO) {
        if (!validKitchenObjectSOList.Contains(kitchenObjectSO)) {
            return false;
            //not a valid ingredient
        }
        if (kitchenObjectSOList.Contains(kitchenObjectSO)) {
            return false; // already has an igredient
        } else {
            kitchenObjectSOList.Add(kitchenObjectSO);
            OnIngredientAdded?.Invoke(this, new OnIgredientAddedEventArgs {
                kitchenObjectSO = kitchenObjectSO
            });
            return true;
        }
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList() {
        return kitchenObjectSOList;
    }

}