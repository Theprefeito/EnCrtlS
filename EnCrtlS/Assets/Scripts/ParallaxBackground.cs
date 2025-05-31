using UnityEngine;
[System.Serializable]

public class BackgroundElement
{
    public SpriteRenderer srBackground;
    [Range(0, 1)] public float speedBackground;
    [HideInInspector] public Material spriteMaterial;
}

public class ParallaxBackground : MonoBehaviour

{
    private const float parallaxMultiplier = 0.1f;
    [SerializeField] private BackgroundElement [] backgroundElements;


    void Start()
    {
        foreach (BackgroundElement element in backgroundElements)
        {
            element.spriteMaterial = element.srBackground.material;
        }
    }





    void Update()
    {
        foreach (BackgroundElement element in backgroundElements)
        {
            element.spriteMaterial.mainTextureOffset = new Vector2(transform.position.x * element.speedBackground * parallaxMultiplier, 0);
        }
    }
    
}

