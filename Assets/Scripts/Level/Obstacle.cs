using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Obstacle : MonoBehaviour
{
    [SerializeField] private int stackPenalty = 2;
    private bool _hit;

    public void Initialise()
    {
        _hit = false;
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_hit) return;

        var collector = other.GetComponentInParent<StackCollector>();
        if (collector == null) return;

        _hit = true;

        bool survived = collector.ConsumeStack(stackPenalty);
        if (!survived)
            GameManager.Instance.TriggerGameOver();
    }

    public void Recycle()
    {
        gameObject.SetActive(false);
    }
}