using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipPro : MonoBehaviour
{
    private static ToolTipPro instance;

    public Text text;
    public GameObject background;
    public GameObject toolTip;
    public Camera cam;

    

    Vector2 backgroundZise;

    Transform toolTipTransform;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        toolTipTransform = toolTip.GetComponent<Transform>();
    }
   
    // Update is called once per frame
    void Update()
    {
        if (toolTip.activeSelf)
        {
            Vector3 localPoint = Input.mousePosition;
            localPoint.x += backgroundZise.x - 70;
            localPoint.y += backgroundZise.y;
            toolTipTransform.position = localPoint;
        }
    }

    private void ShowToolTip(string tooltipstring)
    {
        toolTip.SetActive(true);
        text.text = tooltipstring;
        float textPadding = 4f;
        backgroundZise = new Vector2(text.preferredWidth + textPadding * 5f, text.preferredHeight + textPadding * 5f);
        background.GetComponent<RectTransform>().sizeDelta = backgroundZise;
        Vector3 pos = background.GetComponent<RectTransform>().position;
        pos.x += 0.4f;
        pos.y -= 0.7f;
        text.GetComponent<RectTransform>().position = pos;
    }

    private void HideToolTip()
    {
        toolTip.SetActive(false);
    }

    public static void  ShowToolTip_Static(string tooltipstring)
    {
        instance.ShowToolTip(tooltipstring);
    }

    public static void HideToolTip_Static()
    {
        instance.HideToolTip();
    }

}
