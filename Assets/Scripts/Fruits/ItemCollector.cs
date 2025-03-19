using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    public static FruitCollectedEvent OnFruitCollected = new FruitCollectedEvent();

    private int bananas = 0;

    [SerializeField] private Text bananasText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Banana"))
        {
            collision.gameObject.SetActive(false);
            bananas++;

            if (bananasText != null)
            {
                bananasText.text = "Bananas: " + bananas;
            }
            else
            {
                Debug.LogWarning("bananasText is not assigned in the inspector!");
            }

            OnFruitCollected.Invoke(bananas);
        }
    }
}
