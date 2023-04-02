using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ContainerCounter : BaseCounter {

    public event EventHandler onPlayerGrabbedObject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player) {

        if (!player.HasKitchenObject()) {

            //Player not carrying anything
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

            onPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }

    }

}