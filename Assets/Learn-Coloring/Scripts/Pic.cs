using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum pic_type {paint,pour}
public class Pic : MonoBehaviour
{
    public Sprite sp_avatar;
    public Sprite sp_sketch;
    public SpriteRenderer[] sp_renderer;
    public pic_type type = pic_type.pour;

    public void on_reset()
    {
        for(int i = 0; i < this.sp_renderer.Length; i++)
        {
            this.sp_renderer[i].color = Color.white;
        }
    }
}
