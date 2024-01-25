using System;
using UnityEngine;

public class ProjectileTextBundleInfo : MonoBehaviour
{
    public int textBundle;
    public bool isFading;
    public TextMesh textMesh;
    public float fadeTime;

    public void Awake()
    {
        textMesh = this.gameObject.GetComponent<TextMesh>();
    }

    public void Start()
    {
        isFading = true;
    }

    public void Update()
    {
        if(isFading)
        {
            fadeTime -= Time.deltaTime;
            textMesh.color -= new Color(0, 0, 0, Time.deltaTime/fadeTime);
            if(fadeTime < 0) Destroy(this.gameObject);
        }
    }
}
