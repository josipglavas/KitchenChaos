using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            //there is nothing on top
            if (player.HasKitchenObject()) {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } else {
                //player not carrying anything
            }
        } else {
            //there is something on top
            if (player.HasKitchenObject()) {
                //player has something

                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    //Player is holding a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                    }
                } else {
                    // Player is not carrying a Plate but something else
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) {

                        //counter has a plate on top
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            } else {
                //player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

}
