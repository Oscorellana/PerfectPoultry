using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int step = 0;
    private int score = 0;

    void Awake() => Instance = this;

    public void NextStep(bool success)
    {
        step++;
        if (success) score += 10;
        else score -= 5;

        Debug.Log($"Step {step} complete. Score: {score}");
    }

    public void EndGame()
    {
        Debug.Log($"Game finished! Final Score: {score}");
    }
}
