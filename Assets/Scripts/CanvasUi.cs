using TMPro;
using UnityEngine;

public class CanvasUi : MonoBehaviour
{
    [Header("Components")]
    public TextMeshProUGUI objectCountTxt;
    public int objectCount = 0;


    void Start()
    {
        objectCountTxt = GameObject.Find("Object Count_txt").GetComponent<TextMeshProUGUI>();


    }

    public void CountObject()
    {
        objectCount++;
        objectCountTxt.text = $"Objects: {objectCount}";
    }
}
