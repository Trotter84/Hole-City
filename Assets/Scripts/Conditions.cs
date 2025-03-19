using UnityEngine;

public class Conditions : MonoBehaviour
{
    [Header("Components")]
    public OnChangePosition onChangePositionScript;
    public CanvasUi canvasUiScript;

    [Header("Attributes")]
    public int points = 0;


    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);

        canvasUiScript.CountObject();

        CalculateProgress();
    }

    void CalculateProgress()
    {
        points++;

        if (points % 5 == 0)
        {
            StartCoroutine(onChangePositionScript.ScaleHole());
        }
    }
}
