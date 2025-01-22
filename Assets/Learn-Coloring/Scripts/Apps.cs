using UnityEngine;
using UnityEngine.UI;

public class Apps : MonoBehaviour
{
    [Header("Obj App")]
    public Carrot.Carrot carrot;
    public Paint_Manager paint;
    public GameObject obj_pic;
    public Transform tr_menu_lits_pic;

    [Header("Panel Ui")]
    public GameObject panel_home;
    public GameObject panel_play;
    public Text txt_level;

    [Header("Asset Pic")]
    public GameObject thumb_menu_prefab;
    public Pic[] pic;

    [Header("Sounds")]
    public AudioSource sound_bk;

    private int level = 0;

    void Start()
    {
        this.carrot.Load_Carrot(this.check_exit_app);
        this.carrot.game.load_bk_music(this.sound_bk);

        this.panel_home.SetActive(true);
        this.panel_play.SetActive(false);
        this.obj_pic.SetActive(false);
        this.create_list_menu_thumb();
        this.level = PlayerPrefs.GetInt("level",0);
        this.txt_level.text = this.level.ToString();
    }

    private void check_exit_app()
    {
        if (this.panel_play.activeInHierarchy)
        {
            this.btn_back_home();
            this.carrot.set_no_check_exit_app();
        }
    }

    private void create_list_menu_thumb()
    {
        this.carrot.clear_contain(this.tr_menu_lits_pic);
        
        for(int i = 0; i < this.pic.Length; i++)
        {
            var index_pic = i;
            GameObject obj_thumb = Instantiate(this.thumb_menu_prefab);
            obj_thumb.transform.SetParent(this.tr_menu_lits_pic);
            obj_thumb.transform.position = Vector3.zero;
            obj_thumb.transform.localScale = new Vector3(1f, 1f, 1f);

            Thumb_Item data_thumb = obj_thumb.GetComponent<Thumb_Item>();
            data_thumb.img_avatar.sprite = this.pic[i].sp_avatar;
            data_thumb.set_act(() => btn_play(index_pic));
        }
    }

    public void btn_play(int index_pic)
    {
        this.carrot.ads.show_ads_Interstitial();
        for(int i = 0; i < this.pic.Length; i++)
        {
            this.pic[i].gameObject.SetActive(false);
        }

        this.pic[index_pic].gameObject.SetActive(true);
        this.carrot.play_sound_click();
        this.panel_play.SetActive(true);
        this.panel_home.SetActive(false);
        this.obj_pic.SetActive(true);
        this.paint.on_load(this.pic[index_pic]);
    }

    public void btn_back_home()
    {
        this.carrot.ads.show_ads_Interstitial();
        this.carrot.play_sound_click();
        this.panel_play.SetActive(false);
        this.panel_home.SetActive(true);
        this.obj_pic.SetActive(false);
    }

    public void btn_rate()
    {
        this.carrot.ads.show_ads_Interstitial();
        this.carrot.show_rate();
    }

    public void btn_share()
    {
        this.carrot.ads.show_ads_Interstitial();
        this.carrot.show_share();
    }

    public void btn_setting()
    {
        this.carrot.ads.show_ads_Interstitial();
        Carrot.Carrot_Box box_setting=this.carrot.Create_Setting();
    }

    public void btn_user()
    {
        this.carrot.user.show_login();
    }

    public void btn_top_player()
    {
        this.carrot.game.Show_List_Top_player();
    }

    public void add_level()
    {
        this.level++;
        PlayerPrefs.SetInt("level", this.level);
        this.txt_level.text = this.level.ToString();
        this.carrot.game.update_scores_player(this.level);
    }
}
