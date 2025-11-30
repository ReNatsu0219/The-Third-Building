using TMPro;
using UnityEngine;

public class FruitCollection : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void OnEnable()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        text.text = $" {PlayerManager.Instance.collectionNum}";
    }
}
