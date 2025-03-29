using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnifyingGlassController : MonoBehaviour
{
    public Material magnifyingGlassMaterial;
    public Camera magnifyCamera;
    public RenderTexture magnifyRenderTexture;

    void Start()
    {
        if (magnifyingGlassMaterial)
        {
            magnifyingGlassMaterial.SetTexture("_MagnifyRenderTex", magnifyRenderTexture);
        }
    }

    void Update()
    {
        if (magnifyCamera)
        {
            magnifyCamera.transform.position = Camera.main.transform.position;
            magnifyCamera.transform.rotation = Camera.main.transform.rotation;
        }

        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 viewportPos = Camera.main.ScreenToViewportPoint(screenPos);
        magnifyingGlassMaterial.SetVector("_MagnifyCenter", new Vector4(viewportPos.x, viewportPos.y, 0, 0));
    }
}
