using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DrawOnRawImage : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    // color we are setting pixels to
    public Color32 clr = Color.white;

    // our source UI image - it can be a raw image or sprite renderer, I just used UI image
    public Image img = null;

    [Range(1, 255)]
    [SerializeField] private int BrushSize = 1;

    // the texture we are going to manipulate
    private Texture2D tex2D = null;

    public void on_load()
    {
        this.on_reset();
    }

    public void on_reset()
    {
        Sprite imgSprite = img.sprite;

        // create a new instance of our texture to not write to it directly and overwrite it
        tex2D = new Texture2D((int)imgSprite.rect.width, (int)imgSprite.rect.height);
        var pixels = imgSprite.texture.GetPixels((int)imgSprite.textureRect.x,
                                                (int)imgSprite.textureRect.y,
                                                (int)imgSprite.textureRect.width,
                                                (int)imgSprite.textureRect.height);

        tex2D.SetPixels(pixels);
        tex2D.Apply();

        // assign this new texture to our image by creating a new sprite
        img.sprite = Sprite.Create(tex2D, img.sprite.rect, img.sprite.pivot);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Draw(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Draw(eventData);
    }

    private void Draw(in PointerEventData eventData)
    {
        Vector2 localCursor;

        // convert the position click to a local position on our rect
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(img.rectTransform, eventData.position, eventData.pressEventCamera, out localCursor))
            return;

        // convert this position to pixel coordinates on our texture
        int px = Mathf.Clamp(0, (int)((localCursor.x - img.rectTransform.rect.x) * tex2D.width / img.rectTransform.rect.width), tex2D.width);
        int py = Mathf.Clamp(0, (int)((localCursor.y - img.rectTransform.rect.y) * tex2D.height / img.rectTransform.rect.height), tex2D.height);

        // confirm we are in the bounds of our texture
        if (px >= tex2D.width || py >= tex2D.height)
            return;

        // debugging - you can remove this
        // print(px + ", " + py);

        // if our brush size is greater than 1, then we need to grab neighbors
        if (BrushSize > 1)
        {
            // bottom - left aligned, so find new bottom left coordinate then use that as our starting point
            px = Mathf.Clamp(px - (BrushSize / 2), 0, tex2D.width);
            py = Mathf.Clamp(py - (BrushSize / 2), 0, tex2D.height);

            // add 1 to our brush size so the pixels found are a neighbour search outward from our center point
            int maxWidth = Mathf.Clamp(BrushSize + 1, 0, tex2D.width - px);
            int maxHeight = Mathf.Clamp(BrushSize + 1, 0, tex2D.height - py);

            // cache our maximum dimension size
            int blockDimension = maxWidth * maxHeight;

            // create an array for our colors
            Color[] colorArray = new Color[blockDimension];

            // fill this with our color
            for (int x = 0; x < blockDimension; ++x)
                colorArray[x] = clr;

            // set our pixel colors
            tex2D.SetPixels(px, py, maxWidth, maxHeight, colorArray);
        }
        else
        {
            // set our color at our position - note this will almost never be seen as most textures are rather large, so a single pixel is not going to
            // appear most of the time
            tex2D.SetPixel(px, py, clr);
        }

        // apply the changes - this is what you were missing
        tex2D.Apply();

        // set our sprite to the new texture data
        img.sprite = Sprite.Create(tex2D, img.sprite.rect, img.sprite.pivot);
    }

    public void set_BrushSize(int size_paint)
    {
        this.BrushSize = size_paint;
    }

    public int get_BrushSize()
    {
        return this.BrushSize;
    }
}