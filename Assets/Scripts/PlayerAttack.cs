
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
    private float cooldownTimer;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        cooldownTimer = attackCooldown;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && cooldownTimer >= attackCooldown)
        {
            StartAttack();
        }
    }

    private void StartAttack()
    {
        if (anim == null || attackPoint == null) return;

        cooldownTimer = 0f;
        anim.SetTrigger("attack1");
    }

    // Called by Animation Event
    public void Attack()
    {
        // Check for enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        Debug.Log("Enemies hit: " + hitEnemies.Length);

        foreach (Collider2D enemy in hitEnemies)
        {
            // 1. Check for Goblin
            GoblinHealth goblin = enemy.GetComponent<GoblinHealth>() ?? enemy.GetComponentInParent<GoblinHealth>();
            if (goblin != null)
            {
                goblin.TakeDamage(attackDamage);
                continue;
            }

            // 2. CHECK FOR BOSSLEVEL2 (The most important one)
            BossLevel2 boss = enemy.GetComponent<BossLevel2>() ?? enemy.GetComponentInParent<BossLevel2>();
            if (boss != null)
            {
                // Explicitly pass attackDamage as a float
                boss.TakeDamage((float)attackDamage);
                Debug.Log("Hit BossLevel2 script!");
                continue;
            }

            // 3. Check for Boss2Health (Backup)
            Boss2Health bossHealth = enemy.GetComponent<Boss2Health>() ?? enemy.GetComponentInParent<Boss2Health>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(attackDamage);
                Debug.Log("Hit Boss2Health script!");
                continue;
            }

            EnemyLvl3Health lvl3 = enemy.GetComponent<EnemyLvl3Health>() ?? enemy.GetComponentInParent<EnemyLvl3Health>();
            if (lvl3 != null)
            {
                lvl3.TakeDamage(attackDamage);
                Debug.Log("Hit Enemy Level 3!");
                continue;
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

