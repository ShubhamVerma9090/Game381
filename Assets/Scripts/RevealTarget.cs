using UnityEngine;

public class RevealTarget : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float revealHoldTime = 0.2f;

    private float revealTimer;

    private void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (revealTimer > 0f)
        {
            revealTimer -= Time.deltaTime;
            SetAlpha(1f);
        }
        else
        {
            SetAlpha(0.15f); 
        }
    }

    public void SetRevealed(bool revealed)
    {
        if (revealed)
        {
            revealTimer = revealHoldTime;
        }
    }

    private void SetAlpha(float alpha)
    {
        if (spriteRenderer == null) return;

        Color c = spriteRenderer.color;
        c.a = alpha;
        spriteRenderer.color = c;
    }
}