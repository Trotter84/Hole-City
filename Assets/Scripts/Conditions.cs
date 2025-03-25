using UnityEngine;

public class Conditions : MonoBehaviour
{
    [Header("Components")]
    public OnChangePosition onChangePositionScript;
    public CanvasUi canvasUiScript;


    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);

        CalculateProgress();
    }

    void CalculateProgress()
    {
        canvasUiScript.CountObject();

        if (canvasUiScript.objectCount % 10 == 0)
        {
            StartCoroutine(onChangePositionScript.ScaleHole());
        }
    }
}
