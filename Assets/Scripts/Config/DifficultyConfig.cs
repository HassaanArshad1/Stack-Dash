using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyConfig", menuName = "Stack Dash/Difficulty Config")]
public class DifficultyConfig : ScriptableObject
{
    [Header("Gap Settings")]
    public AnimationCurve gapWidthOverDistance = AnimationCurve.Linear(0, 2, 500, 6);
    public AnimationCurve gapSpawnChanceOverDistance = AnimationCurve.Linear(0, 0.3f, 500, 0.7f);

    [Header("Pickup Settings")]
    public AnimationCurve pickupCountOverDistance = AnimationCurve.Linear(0, 4, 500, 2);

    [Header("Speed Settings")]
    public AnimationCurve playerSpeedOverDistance = AnimationCurve.Linear(0, 8, 500, 14);

    public int GetGapWidth(float distance) => 
        Mathf.RoundToInt(gapWidthOverDistance.Evaluate(distance));

    public float GetGapSpawnChance(float distance) => 
        gapSpawnChanceOverDistance.Evaluate(distance);

    public int GetPickupCount(float distance) => 
        Mathf.RoundToInt(pickupCountOverDistance.Evaluate(distance));

    public float GetPlayerSpeed(float distance) => 
        playerSpeedOverDistance.Evaluate(distance);
}