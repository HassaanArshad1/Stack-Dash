using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyConfig", menuName = "Stack Dash/Difficulty Config")]
public class DifficultyConfig : ScriptableObject
{
    [Header("Gap Settings")]
    public AnimationCurve gapWidthOverDistance = AnimationCurve.Linear(0, 2, 500, 6);
    public AnimationCurve gapSpawnChanceOverDistance = AnimationCurve.Linear(0, 0.4f, 500, 0.7f);

    [Header("Pickup Settings")]
    public int pickupsPerSegment = 5;

    [Header("Speed Settings")]
    public AnimationCurve playerSpeedOverDistance = AnimationCurve.Linear(0, 8, 500, 14);
    
    [Header("Gap Spawn Settings")]
    public float minDistanceBeforeFirstGap = 5f;

    public int GetGapWidth(float distance) => 
        Mathf.RoundToInt(gapWidthOverDistance.Evaluate(distance));

    public float GetGapSpawnChance(float distance) => 
        gapSpawnChanceOverDistance.Evaluate(distance);

    public float GetPlayerSpeed(float distance) => 
        playerSpeedOverDistance.Evaluate(distance);
}