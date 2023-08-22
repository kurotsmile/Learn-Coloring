using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Pic : MonoBehaviour
{
    private void OnMouseDown()
    {
        SpriteRenderer sp = this.GetComponent<SpriteRenderer>();
        sp.color = GameObject.Find("App").GetComponent<Apps>().paint.get_color_select();
        GameObject.Find("App").GetComponent<Apps>().paint.add_scores();
    }
}
