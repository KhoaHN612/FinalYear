using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GhostTrail : MonoBehaviour
{
    public float delay = 0.1f;
    public float fadeTime = 1f;
    SpriteRenderer spriteRenderer;
    public SpriteRenderer playerSpriteRenderer;
    public Color color = new Color(1,1,1,0.3f);
    public Material material = null;
    [SerializeField]
    private float delta = 0;
    private void Awake() {
        if (playerSpriteRenderer ==null) {
            playerSpriteRenderer = GetComponent<SpriteRenderer>();
        }
        if (playerSpriteRenderer ==null) {
            playerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

    }
    void Update()
    {
        if (delta > 0)
            {delta -= Time.deltaTime;}
        else 
            {delta = delay; createGhost();}
    }

    private void createGhost()
    {
        GameObject ghostObject = new GameObject("DuplicatedObject");

        ghostObject.transform.position = transform.position;
        ghostObject.transform.rotation = transform.rotation;
        ghostObject.transform.localScale = transform.localScale;
        ghostObject.AddComponent<SpriteRenderer>();
        spriteRenderer = ghostObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = playerSpriteRenderer.sprite;
        spriteRenderer.sortingOrder = -1;
        spriteRenderer.color = color;
        if (material != null)
            spriteRenderer.material = material;

        Destroy(ghostObject, fadeTime);
    }
}
