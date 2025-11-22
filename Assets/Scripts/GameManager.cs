using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int currentStep = 0;
    private int score = 0;
    public string CurrentStepName => steps[currentStep];


    // ✨ Define cooking steps in order
    // These are the required actions in sequence.
    private string[] steps = new string[]
    {
        "GutTurkey",   // Step 0
        "StuffTurkey",   // Step 1
        "Salt", // Step 2
        "Pepper",  // Step 3
        "Water"  // Step 4
        






    };

    void Awake()
    {
        Instance = this;
    }

    // ✨ Call this when the player completes an action
    public void TryStep(string actionName)
    {
        // Are we on a valid step?
        if (currentStep >= steps.Length)
        {
            Debug.Log("All steps completed!");
            EndGame();
            return;
        }

        // Does player action match the required step?
        if (actionName == steps[currentStep])
        {
            score += 10;
            Debug.Log($"Correct step: {actionName}! Score: {score}");

            currentStep++;

            // If all steps finished:
            if (currentStep >= steps.Length)
                EndGame();
        }
        else
        {
            score -= 5;
            Debug.Log($"Wrong step. Expected: {steps[currentStep]}, got: {actionName}. Score: {score}");
        }
    }

    public void EndGame()
    {
        Debug.Log($"Game finished! Final Score: {score}");
    }

    // ✨ Easy method to reset (you asked for future expandability)
    public void ResetGame()
    {
        currentStep = 0;
        score = 0;
        Debug.Log("Game reset.");
    }
}
