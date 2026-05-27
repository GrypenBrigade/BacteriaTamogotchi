using UnityEngine;
using UnityEngine.UIElements;

public class Tamogotchi : MonoBehaviour
{
    private ProgressBar hungerBar;
    private Button feedButton;
    private Button buyButton;
    private Label foodAmountLabel;
    
    private float hunger = 100f; // Current hunger level (0-100, 100 = very hungry)
    private float hungerIncreaseRate = 5f; // Hunger increases by this amount per minute
    private float feedAmount = 40f; // Amount hunger decreases when fed
    private float maxHunger = 100f;
    private float minHunger = 0f;
    private int foodCount = 0;

    void Start()
    {
        // Get UI elements
        var root = GetComponent<UIDocument>().rootVisualElement;
        hungerBar = root.Q<ProgressBar>();
        feedButton = root.Q<Button>("Feed");
        buyButton = root.Q<Button>("Buy");
        foodAmountLabel = root.Q<Label>("Amount");
        
        // Wire up the feed button
        feedButton.clicked += OnFeedClicked;
        buyButton.clicked += OnBuyClicked;
        
        // Set initial hunger value
        if (hungerBar != null)
        {
            hungerBar.value = hunger;
        }

        UpdateFoodAmountLabel();
    }

    void Update()
    {
        // Increase hunger over time
        hunger += (hungerIncreaseRate / 60f) * Time.deltaTime;
        hunger = Mathf.Clamp(hunger, minHunger, maxHunger);
        
        // Update the progress bar
        if (hungerBar != null)
        {
            hungerBar.value = hunger;
        }
    }
    
    private void OnFeedClicked()
    {
        if (foodCount <= 0)
        {
            Debug.Log("No food left. Buy more.");
            return;
        }

        // Decrease hunger when fed
        hunger -= feedAmount;
        hunger = Mathf.Clamp(hunger, minHunger, maxHunger);
        foodCount -= 1;
        UpdateFoodAmountLabel();
        
        Debug.Log($"Fed! Hunger is now: {hunger}");
    }

    private void OnBuyClicked()
    {
        foodCount += 1;
        UpdateFoodAmountLabel();

        Debug.Log($"Bought food. Food count is now: {foodCount}");
    }

    private void UpdateFoodAmountLabel()
    {
        if (foodAmountLabel != null)
        {
            foodAmountLabel.text = foodCount.ToString();
        }
    }
}
