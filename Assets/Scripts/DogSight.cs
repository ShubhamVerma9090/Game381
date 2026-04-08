using System.Collections;
using UnityEngine;

public class DogClearSight : MonoBehaviour
{
    [Header("Clear Sight Settings")]
    public float activeDuration = 4f;
    public float cooldownDuration = 8f;
    public float revealRadius = 8f;
    public LayerMask revealLayerMask;

    public bool IsActive { get; private set; }
    public bool IsOnCooldown { get; private set; }

    public void TryActivateClearSight()
    {
        if (IsActive || IsOnCooldown)
            return;

        StartCoroutine(ClearSightRoutine());
    }

    private IEnumerator ClearSightRoutine()
    {
        IsActive = true;

        float timer = 0f;

        while (timer < activeDuration)
        {
            RevealNearbyThreats();
            timer += Time.deltaTime;
            yield return null;
        }

        IsActive = false;
        IsOnCooldown = true;

        yield return new WaitForSeconds(cooldownDuration);

        IsOnCooldown = false;
    }

    private void RevealNearbyThreats()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, revealRadius, revealLayerMask);

        foreach (Collider2D hit in hits)
        {
            RevealTarget target = hit.GetComponent<RevealTarget>();
            if (target != null)
            {
                target.SetRevealed(true);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, revealRadius);
    }
}