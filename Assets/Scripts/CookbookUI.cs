using UnityEngine;
using TMPro;

public class CookbookUI : MonoBehaviour
{
    public GameObject stepPrefab;      // A prefab with a TMP text object
    public Transform stepContainer;    // Vertical Layout Group parent

    private TextMeshProUGUI[] stepTexts;

    void Start()
    {
        InitializeSteps();
        UpdateSteps();
    }

    void Update()
    {
        UpdateSteps();
    }

    void InitializeSteps()
    {
        int total = GameManager.Instance.AllSteps.Length;
        stepTexts = new TextMeshProUGUI[total];

        // Create one text object per step
        for (int i = 0; i < total; i++)
        {
            GameObject newStep = Instantiate(stepPrefab, stepContainer);
            TextMeshProUGUI tmp = newStep.GetComponent<TextMeshProUGUI>();
            stepTexts[i] = tmp;

            // Hide all at first
            tmp.gameObject.SetActive(false);
        }
    }

    void UpdateSteps()
    {
        var gm = GameManager.Instance;

        for (int i = 0; i < stepTexts.Length; i++)
        {
            string stepName = gm.AllSteps[i];
            TextMeshProUGUI tmp = stepTexts[i];

            // Reveal this step ONLY when reached
            if (i <= gm.CurrentStepIndex)
            {
                tmp.gameObject.SetActive(true);
                tmp.text = ConvertToReadable(stepName);

                // Completed steps turn green
                if (i < gm.CurrentStepIndex)
                    tmp.color = Color.green;
                else
                    tmp.color = Color.white; // current step stays white
            }
        }
    }

    string ConvertToReadable(string code)
    {
        switch (code)
        {
            case "GutTurkey": return "Gut the turkey";
            case "StuffTurkey": return "Stuff the turkey";
            case "Salt": return "Add salt";
            case "Pepper": return "Add pepper";
            case "Water": return "Pour water";
            default: return code;
        }
    }
}
