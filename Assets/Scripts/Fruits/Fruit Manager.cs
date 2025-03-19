using UnityEngine;
using UnityEngine.UI;

public class FruitManager : MonoBehaviour
{
    public Text fruitsCollected;
    public Text fruitsMax;
    public GameObject levelCleared;
    public GameObject tramsition;
    private int totalFruitsInLevel;

    void Start()
    {
        totalFruitsInLevel = transform.childCount;
        UpdateUI();
        ItemCollector.OnFruitCollected.AddListener(UpdateFruitCollected);
    }

    private void OnDestroy()
    {
        ItemCollector.OnFruitCollected.RemoveListener(UpdateFruitCollected);
    }

    public void AllfruitsCollected()
    {
        if (transform.childCount == 0)
        {
            Debug.LogWarning("no fruit!");
            levelCleared.SetActive(true);
            tramsition.SetActive(true);
            Invoke("ChangeScene", 1);
        }
    }

    private void UpdateFruitCollected(int bananas)
    {
        fruitsCollected.text = bananas.ToString();
    }

    private void UpdateUI()
    {
        fruitsMax.text = totalFruitsInLevel.ToString(); 
    }
}
