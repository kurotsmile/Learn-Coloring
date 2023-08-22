using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Thumb_Item : MonoBehaviour
{
    public Image img_avatar;
    private UnityAction act;

    public void click()
    {
        this.act.Invoke();
    }

    public void set_act(UnityAction act_new)
    {
        this.act = act_new;
    }
}
