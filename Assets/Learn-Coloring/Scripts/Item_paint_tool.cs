using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_paint_tool : MonoBehaviour
{
    public Image img;
    public Image img_border;
    public int index;
    public bool is_tray = false;

    public void click()
    {
        if(this.is_tray)
            GameObject.Find("App").GetComponent<Apps>().paint.select_tool_color(this.index);
        else
            GameObject.Find("App").GetComponent<Apps>().paint.set_color_tool(this.img.color);

    }
}
