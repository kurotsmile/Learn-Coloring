using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paint_Manager : MonoBehaviour
{
    [Header("Obj Apps")]
    public Apps app;
    public DrawOnRawImage Draw;
    public GameObject paint_tool_Prefab;
    public Transform tr_arean_all_tool;

    [Header("Ui emp")]
    public Sprite sp_icon_color;
    public Sprite sp_icon_view;
    public Sprite sp_icon_no_view;
    public GameObject panel_draw;
    public Image img_draw;
    public Image img_sketch;
    public Image img_btn_view;
    public GameObject obj_btn_paint_brush;
    public GameObject obj_btn_paint_bucket;
    public GameObject obj_btn_add_size_paint;
    public GameObject obj_btn_minus_size_paint;
    public GameObject obj_panel_size_paint;
    public Image img_icon_paint_brush;
    public Image img_icon_paint_bucket;
    public Image img_icon_paint_color;
    public Text txt_size_paint;
    public Text txt_size_zoom;
    public GameObject panel_menu_color;
    public GameObject panel_menu_right;
    public GameObject panel_menu_left;
    public Slider slider_done;

    [Header("Colors")]
    public Color32[] color_tool_paint;
    public Color32 color_nomal;
    public Color32 color_select;
    public Color32 color_tool;

    private List<Item_paint_tool> list_tool_color;
    private pic_type mode_type;
    private Pic p;
    private Carrot.Carrot_Box box_color;
    private bool is_show_pic = false;
    private int scores = 0;

    public void on_load(Pic pic)
    {
        this.list_tool_color = new List<Item_paint_tool>();
        this.app.carrot.clear_contain(this.tr_arean_all_tool);
        for(int i = 0; i < this.color_tool_paint.Length; i++)
        {
            GameObject obj_color = Instantiate(this.paint_tool_Prefab);
            obj_color.transform.SetParent(this.tr_arean_all_tool);
            obj_color.transform.position = Vector3.zero;
            obj_color.transform.localScale = new Vector3(1f, 1f, 1f);
            Item_paint_tool item_tool = obj_color.GetComponent<Item_paint_tool>();
            item_tool.is_tray = true;
            item_tool.img.color = this.color_tool_paint[i];
            item_tool.index = i;
            this.list_tool_color.Add(item_tool);
        }
        this.select_tool_color(0);

        if (pic.type == pic_type.paint)
        {
            this.panel_draw.SetActive(true);
            this.obj_btn_paint_brush.SetActive(false);
            this.obj_btn_paint_bucket.SetActive(false);
        }
        else
        {
            this.panel_draw.SetActive(false);
            this.obj_btn_paint_brush.SetActive(true);
            this.obj_btn_paint_bucket.SetActive(true);
        }

        this.mode_type = pic.type;
        this.img_draw.sprite = pic.sp_avatar;
        this.img_sketch.sprite = pic.sp_sketch;
        this.Draw.on_load();
        this.check_icon_model();
        this.p = pic;
        this.update_emp_size_ui();
        this.is_show_pic = false;
        this.check_show_pic();
        this.scores = 0;
        this.slider_done.value = 0;
        this.slider_done.maxValue = pic.sp_renderer.Length;
    }

    public void btn_paint_brush()
    {
        this.GetComponent<Apps>().carrot.play_sound_click();
        this.panel_draw.SetActive(true);
        this.GetComponent<Apps>().obj_pic.SetActive(false);
        this.Draw.on_load();
        this.mode_type = pic_type.paint;
        this.check_icon_model();
    }

    public void btn_pain_bucket()
    {
        this.GetComponent<Apps>().carrot.play_sound_click();
        this.panel_draw.SetActive(false);
        this.GetComponent<Apps>().obj_pic.SetActive(true);
        this.mode_type = pic_type.pour;
        this.check_icon_model();
    }

    public void select_tool_color(int index)
    {
        this.on_reset_stats_all_tool_color();
        if (index > -1)
        {
            this.list_tool_color[index].img_border.color = this.color_tool_paint[index];
            this.Draw.clr = this.color_tool_paint[index];
            this.color_tool = this.color_tool_paint[index];
        }
    }

    private void on_reset_stats_all_tool_color()
    {
        for (int i = 0; i < this.list_tool_color.Count; i++) this.list_tool_color[i].img_border.color = Color.black;
    }

    public Color32 get_color_select()
    {
        return this.color_tool;
    }

    public void set_color_tool(Color32 color_set)
    {
        this.on_reset_stats_all_tool_color();
        this.GetComponent<Apps>().carrot.play_sound_click();
        this.Draw.clr = color_set;
        this.color_tool = color_set;
        this.img_icon_paint_color.color = color_set;
        if(this.box_color!=null) this.box_color.close();
    }

    private void check_icon_model()
    {
        this.img_icon_paint_brush.color = this.color_nomal;
        this.img_icon_paint_bucket.color = this.color_nomal;
        if (this.mode_type == pic_type.paint) this.img_icon_paint_brush.color = this.color_select;
        if (this.mode_type == pic_type.pour) this.img_icon_paint_bucket.color = this.color_select;

        if (this.mode_type == pic_type.paint)
            this.obj_panel_size_paint.SetActive(true);
        else
            this.obj_panel_size_paint.SetActive(false);
    }

    public void add_paint_size()
    {
        this.GetComponent<Apps>().carrot.play_sound_click();
        this.Draw.set_BrushSize(this.Draw.get_BrushSize()+5);
        this.update_emp_size_ui();
    }

    public void minus_paint_size()
    {
        this.GetComponent<Apps>().carrot.play_sound_click();
        this.Draw.set_BrushSize(this.Draw.get_BrushSize()-5);
        this.update_emp_size_ui();
    }

    private void update_emp_size_ui()
    {
        this.txt_size_paint.text = this.Draw.get_BrushSize().ToString();
        if (this.Draw.get_BrushSize() >= 225)
            this.obj_btn_add_size_paint.SetActive(false);
        else
            this.obj_btn_add_size_paint.SetActive(true);

        if (this.Draw.get_BrushSize() <= 10)
            this.obj_btn_minus_size_paint.SetActive(false);
        else
            this.obj_btn_minus_size_paint.SetActive(true);

        this.txt_size_zoom.text = this.Draw.transform.localScale.x.ToString("f2");
    }

    public void btn_show_box_color()
    {
        if (this.mode_type == pic_type.pour)
            this.GetComponent<Apps>().obj_pic.SetActive(false);
        else
            this.Draw.gameObject.SetActive(false);

        box_color=this.GetComponent<Apps>().carrot.show_grid();
        box_color.set_item_size(new Vector2(60f, 60f));
        box_color.set_title("Color");
        box_color.set_icon(this.sp_icon_color);

        for(int i = 0; i <= 255; i += 10)
        {
            GameObject obj_c=box_color.add_item(this.paint_tool_Prefab);
            obj_c.GetComponent<Item_paint_tool>().img.color = new Color32((byte)i, 255, 255, 255);
        }

        for (int i = 0; i <= 255; i += 10)
        {
            GameObject obj_c = box_color.add_item(this.paint_tool_Prefab);
            obj_c.GetComponent<Item_paint_tool>().img.color = new Color32(255, 255, (byte)i, 255);
        }

        for (int i = 0; i <= 255; i += 10)
        {
            GameObject obj_c = box_color.add_item(this.paint_tool_Prefab);
            obj_c.GetComponent<Item_paint_tool>().img.color = new Color32(255, (byte)i, 255, 255);
        }

        for (int i = 0; i <= 255; i += 10)
        {
            GameObject obj_c = box_color.add_item(this.paint_tool_Prefab);
            obj_c.GetComponent<Item_paint_tool>().img.color = new Color32((byte)i, (byte)i, 255, 255);
        }

        for (int i = 0; i <= 255; i += 10)
        {
            GameObject obj_c = box_color.add_item(this.paint_tool_Prefab);
            obj_c.GetComponent<Item_paint_tool>().img.color = new Color32(255, (byte)i, (byte)i, 255);
        }

        for (int i = 0; i <= 255; i += 10)
        {
            GameObject obj_c = box_color.add_item(this.paint_tool_Prefab);
            obj_c.GetComponent<Item_paint_tool>().img.color = new Color32((byte)i,255, (byte)i, 255);
        }

        /* color 2*/
        for (int i = 0; i <= 255; i += 10)
        {
            GameObject obj_c = box_color.add_item(this.paint_tool_Prefab);
            obj_c.GetComponent<Item_paint_tool>().img.color = new Color32((byte)i, 255, 0, 255);
        }

        for (int i = 0; i <= 255; i += 10)
        {
            GameObject obj_c = box_color.add_item(this.paint_tool_Prefab);
            obj_c.GetComponent<Item_paint_tool>().img.color = new Color32(0, 255, (byte)i, 255);
        }

        for (int i = 0; i <= 255; i += 10)
        {
            GameObject obj_c = box_color.add_item(this.paint_tool_Prefab);
            obj_c.GetComponent<Item_paint_tool>().img.color = new Color32(255, (byte)i, 0, 255);
        }

        for (int i = 0; i <= 255; i += 10)
        {
            GameObject obj_c = box_color.add_item(this.paint_tool_Prefab);
            obj_c.GetComponent<Item_paint_tool>().img.color = new Color32((byte)i, (byte)i, 0, 255);
        }

        for (int i = 0; i <= 255; i += 10)
        {
            GameObject obj_c = box_color.add_item(this.paint_tool_Prefab);
            obj_c.GetComponent<Item_paint_tool>().img.color = new Color32(0, (byte)i, (byte)i, 255);
        }

        for (int i = 0; i <= 255; i += 10)
        {
            GameObject obj_c = box_color.add_item(this.paint_tool_Prefab);
            obj_c.GetComponent<Item_paint_tool>().img.color = new Color32((byte)i, 0, (byte)i, 255);
        }
        box_color.set_act_before_closing(this.act_close_box_color);
    }

    private void act_close_box_color()
    {
        if (this.mode_type == pic_type.pour)
            this.GetComponent<Apps>().obj_pic.SetActive(true);
        else
            this.Draw.gameObject.SetActive(true);
    }

    public void btn_zoom_in()
    {
        this.GetComponent<Apps>().obj_pic.transform.localScale = new Vector3(this.Draw.transform.localScale.x + 0.2f, this.Draw.transform.localScale.y + 0.2f, 1f);
        this.Draw.transform.localScale = new Vector3(this.Draw.transform.localScale.x+0.2f, this.Draw.transform.localScale.y + 0.2f, 1f);
        this.GetComponent<Apps>().carrot.play_sound_click();
        this.update_emp_size_ui();
    }

    public void btn_zoom_out()
    {
        this.GetComponent<Apps>().obj_pic.transform.localScale = new Vector3(this.Draw.transform.localScale.x-0.2f, this.Draw.transform.localScale.y-0.2f, 1f);
        this.Draw.transform.localScale = new Vector3(this.Draw.transform.localScale.x - 0.2f, this.Draw.transform.localScale.y - 0.2f, 1f);
        this.GetComponent<Apps>().carrot.play_sound_click();
        this.update_emp_size_ui();
    }

    public void btn_zoom_center()
    {
        this.GetComponent<Apps>().obj_pic.transform.localScale = new Vector3(1f,1f, 1f);
        this.Draw.transform.localScale = new Vector3(1f, 1f, 1f);
        this.GetComponent<Apps>().carrot.play_sound_click();
        this.update_emp_size_ui();
    }

    public void btn_reset()
    {
        if (mode_type == pic_type.pour)
        {
            this.p.on_reset();
        }
        else
        {
            this.Draw.img.sprite = this.p.sp_avatar;
            this.Draw.on_reset();
        }  
        this.GetComponent<Apps>().carrot.play_sound_click();
        this.scores = 0;
        this.slider_done.value = 0;
    }

    public void btn_view_pic()
    {
        this.GetComponent<Apps>().carrot.play_sound_click();
        if (this.is_show_pic)
            this.is_show_pic = false;
        else
            this.is_show_pic = true;
        this.check_show_pic();
    }

    private void check_show_pic()
    {
        if (this.is_show_pic)
        {
            this.img_btn_view.sprite = this.sp_icon_no_view;
            this.panel_menu_color.SetActive(false);
            this.panel_menu_left.SetActive(false);
            this.panel_menu_right.SetActive(false);
            this.slider_done.gameObject.SetActive(false);
        }
        else
        {
            this.img_btn_view.sprite = this.sp_icon_view;
            this.panel_menu_color.SetActive(true);
            this.panel_menu_left.SetActive(true);
            this.panel_menu_right.SetActive(true);
            this.slider_done.gameObject.SetActive(true);
        }
    }

    public void add_scores()
    {
        this.scores++;
        this.slider_done.value = this.scores;
        if (this.scores == this.slider_done.maxValue)
        {
            this.GetComponent<Apps>().add_level();
        }
    }
}
