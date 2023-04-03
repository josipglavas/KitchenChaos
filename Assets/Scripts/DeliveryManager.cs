using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DeliveryManager : MonoBehaviour {

    public event EventHandler onRecipeSpawned;
    public event EventHandler onRecipeCompleted;

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;

    private List<RecipeSO> waitingRecipeSOList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4; 

    private void Awake()
    {
        Instance = this;

        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if(spawnRecipeTimer <= 0f) {
            spawnRecipeTimer = spawnRecipeTimerMax;
            if(waitingRecipeSOList.Count < waitingRecipesMax) {

                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);

                onRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
            
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject) {

        for(int i=0; i<waitingRecipeSOList.Count; i++) {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count) {
                //has the same number of ingredients
                bool plateContentsMatchesRecipe = true;
                foreach(KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList) {
                    // cycling through ingredients in the recipe
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()) {
                        // cycling through ingredients on the plate
                        if(recipeKitchenObjectSO == plateKitchenObjectSO) {
                            //ingredients matches
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound) {
                        //this recipe ingredient was not found on the plate
                        plateContentsMatchesRecipe = false;
                    }
                }
                if (plateContentsMatchesRecipe) {
                    //player delivered the correct recipe!
                    
                    waitingRecipeSOList.RemoveAt(i);
                    onRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        //no matching igredients found
      
    }

    public List<RecipeSO> GetWaitingRecipeSOList() {
        return waitingRecipeSOList;
    }
}