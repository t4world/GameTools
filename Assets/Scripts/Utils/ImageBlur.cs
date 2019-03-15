using UnityEngine;
using Foundation;
using UnityEngine.UI;

public sealed class ImageBlur : MonoBehaviour
{
    private const string texName = "Assets/_RS2Art/UI/UISprites/loading_back.png";
    private float avgR = 0;
    private float avgG = 0;
    private float avgB = 0;
    private float avgA = 0;
    private float blurPixelCount = 0;
    private bool texLoaded;

    public int radius = 2;
    public int iterations = 2;

    public Texture2D tex;
    public Image m_sourceImage;
    public Image image;
    private bool m_isInit;
    private Color[] m_sourcePixels = null;
    private Color m_destColro = Color.white;

    public void OnInit()
    {
        m_isInit = true;
        tex = m_sourceImage.sprite.texture;
        if (tex == null)
        {
            Log.Info("Reload tex");
            Mod.Resource.LoadAsset(texName, new AssetLoadCallbacks(AssetLoadSuccess, null), this);
            return;
        }

        image.sprite = GameTools.Instacne.CreateSpriteByTexture2D(FastBlur(tex, radius, iterations));
    }


    private void OnDestroy()
    {
        m_isInit = false;
        if (texLoaded && tex != null)
        {
            Mod.Resource.UnloadAsset(tex);
            tex = null;
            texLoaded = false;
        }
        m_sourcePixels = null;
    }

    Texture2D FastBlur(Texture2D image, int radius, int iterations)
    {
        var tex = image;

        for (var i = 0; i < iterations; i++)
        {
            tex = BlurImage(tex, radius, true);
            if (tex == null)
            {
                Log.Error("FastBlur tex 1 is null");
            }

            tex = BlurImage(tex, radius, false);
            if (tex == null)
            {
                Log.Error("FastBlur tex 2 is null");
            }
        }

        return tex;
    }


    Texture2D BlurImage(Texture2D image, int blurSize, bool horizontal)
    {
        Texture2D blurred = new Texture2D(image.width, image.height);
        if (blurred == null)
        {
            Log.Error("blurred is null");
            return null;
        }
        m_sourcePixels = image.GetPixels();
        int _W = image.width;
        int _H = image.height;
        int xx, yy, x, y;
        int index = 0;

        if (horizontal)
        {
            for (yy = 0; yy < _H; yy++)
            {
                for (xx = 0; xx < _W; xx++)
                {
                    //ResetPixel();
                    avgR = 0.0f;
                    avgG = 0.0f;
                    avgB = 0.0f;
                    blurPixelCount = 0;

                    //Right side of pixel
                    for (x = xx; (x < xx + blurSize && x < _W); x++)
                    {
                        index = yy * _W + x;
                        //AddPixel(image.GetPixel(x, yy));
                        //AddPixel(sourcePixels[index]);
                        avgR += m_sourcePixels[index].r;
                        avgG += m_sourcePixels[index].g;
                        avgB += m_sourcePixels[index].b;
                        blurPixelCount++;
                    }

                    //Left side of pixel
                    for (x = xx; (x > xx - blurSize && x > 0); x--)
                    {
                        index = yy * _W + x;
                        //AddPixel(image.GetPixel(x, yy));
                        //AddPixel(sourcePixels[index]);
                        avgR += m_sourcePixels[index].r;
                        avgG += m_sourcePixels[index].g;
                        avgB += m_sourcePixels[index].b;
                        blurPixelCount++;
                    }


                    //CalcPixel();
                    avgR = avgR / blurPixelCount;
                    avgG = avgG / blurPixelCount;
                    avgB = avgB / blurPixelCount;

                    for (x = xx; x < xx + blurSize && x < _W; x++)
                    {
                        index = yy * _W + x;
                        m_destColro = new Color(avgR, avgG, avgB, 1.0f);
                        m_sourcePixels[index] = m_destColro;
                        //blurred.SetPixel(x, yy, new Color(avgR, avgG, avgB, 1.0f));
                    }
                }
            }
            blurred.SetPixels(m_sourcePixels);
        }

        else
        {
            for (xx = 0; xx < _W; xx++)
            {
                for (yy = 0; yy < _H; yy++)
                {
                    //ResetPixel();
                    avgR = 0.0f;
                    avgG = 0.0f;
                    avgB = 0.0f;
                    blurPixelCount = 0;

                    //Over pixel
                    for (y = yy; (y < yy + blurSize && y < _H); y++)
                    {
                        index = y * _W + xx;
                        //AddPixel(image.GetPixel(xx, y));
                        //AddPixel(sourcePixels[index]);
                        avgR += m_sourcePixels[index].r;
                        avgG += m_sourcePixels[index].g;
                        avgB += m_sourcePixels[index].b;
                        blurPixelCount++;
                    }

                    //Under pixel
                    for (y = yy; (y > yy - blurSize && y > 0); y--)
                    {
                        index = y * _W + xx;
                        //AddPixel(image.GetPixel(xx, y));
                        //AddPixel(sourcePixels[index]);
                        avgR += m_sourcePixels[index].r;
                        avgG += m_sourcePixels[index].g;
                        avgB += m_sourcePixels[index].b;
                        blurPixelCount++;
                    }

                    //CalcPixel();
                    avgR = avgR / blurPixelCount;
                    avgG = avgG / blurPixelCount;
                    avgB = avgB / blurPixelCount;
                    for (y = yy; y < yy + blurSize && y < _H; y++)
                    {
                        index = y * _W + xx;
                        m_destColro = new Color(avgR, avgG, avgB, 1.0f);
                        m_sourcePixels[index] = m_destColro;
                        //blurred.SetPixel(xx, y, new Color(avgR, avgG, avgB, 1.0f));
                    }
                }
            }
            blurred.SetPixels(m_sourcePixels);
        }

        blurred.Apply();
        return blurred;
    }

    void AddPixel(Color pixel)
    {
        avgR += pixel.r;
        avgG += pixel.g;
        avgB += pixel.b;
        blurPixelCount++;
    }

    void ResetPixel()
    {
        avgR = 0.0f;
        avgG = 0.0f;
        avgB = 0.0f;
        blurPixelCount = 0;
    }

    void CalcPixel()
    {
        avgR = avgR / blurPixelCount;
        avgG = avgG / blurPixelCount;
        avgB = avgB / blurPixelCount;
    }

    private void AssetLoadSuccess(string assetName, object asset, float duration, object userData)
    {
        if (m_isInit == false)
        {
            Mod.Resource.UnloadAsset(asset);
            return;
        }
        else
        {

            tex = asset as Texture2D;
            if (tex == null)
            {
                Log.Error("tex is null");
                return;
            }

            texLoaded = true;
            image.sprite = GameTools.Instacne.CreateSpriteByTexture2D(FastBlur(tex, radius, iterations));
        }
    }
}