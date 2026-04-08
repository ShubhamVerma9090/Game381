using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private float attackRange = 1.2f;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayers;

    private Animator anim;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && cooldownTimer >= attackCooldown)
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (anim == null)
        {
            Debug.LogError("Animator not found!");
            return;
        }

        anim.SetTrigger("attack1");

      
        cooldownTimer = 0f;

        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange,
            enemyLayers
        );

        
        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyAI enemyScript = enemy.GetComponent<EnemyAI>();
            if (enemyScript != null)
            {
                enemyScript.TakeHit(attackDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}