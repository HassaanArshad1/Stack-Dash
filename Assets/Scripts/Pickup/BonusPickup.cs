using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BonusPickup : MonoBehaviour
{
    private int _bonusAmount;
    private bool _collected;

    public void Initialise(StackCollector collector)
    {
        _bonusAmount = Random.Range(2, 6); // +2 to +5
        _collected = false;
        gameObject.SetActive(true);
        UpdateLabel();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_collected) return;
        Debug.Log("Bonus Collision");
        var collector = other.GetComponentInParent<StackCollector>();
        if (collector == null) return;

        _collected = true;
        collector.AddToStack(_bonusAmount);
        gameObject.SetActive(false);
    }

    private void UpdateLabel()
    {
        var label = GetComponentInChildren<TMPro.TextMeshPro>();
        if (label != null)
            label.text = $"+{_bonusAmount}";
    }

    public void Recycle()
    {
        gameObject.SetActive(false);
    }
}