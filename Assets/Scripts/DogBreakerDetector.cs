//using UnityEngine;

//public class DogBreakerDetector : MonoBehaviour
//{
//    [SerializeField] private bool isBroken = true;

//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        if (!isBroken) return;

//        if (other.CompareTag("Dog"))
//        {
//            DogController dog = other.GetComponent<DogController>();
//            if (dog != null)
//            {
//                dog.BarkAtTarget(transform);
//            }
//        }
//    }
//}

using UnityEngine;

public class DogBreakerDetector : MonoBehaviour
{
    public enum TargetType
{
    Breaker,
    Enemy
}

public TargetType targetType;

[Header("Breaker only")]
public bool breakerCompleted = false;

[Header("Enemy only")]
public bool enemyStillRelevant = true;

public bool CanAlert()
{
    if (targetType == TargetType.Breaker)
        return !breakerCompleted;

    if (targetType == TargetType.Enemy)
        return enemyStillRelevant;

    return false;
}

public bool IsCompleted()
{
    if (targetType == TargetType.Breaker)
        return breakerCompleted;

    if (targetType == TargetType.Enemy)
        return !enemyStillRelevant;

    return true;
}

public void MarkBreakerCompleted()
{
    breakerCompleted = true;
}

public void MarkEnemyCleared()
{
    enemyStillRelevant = false;
}
}