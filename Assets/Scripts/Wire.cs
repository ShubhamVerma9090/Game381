using UnityEngine;

public class Wire : MonoBehaviour
{
    public SpriteRenderer wireEnd;
    public GameObject lightOn;

    private Vector3 startPoint;
    private Vector3 startPosition;
    private bool isDone = false;

    private void Start()
    {
        startPoint = transform.parent.position;
        startPosition = transform.position;
    }

    private void OnMouseDrag()
    {
        if (isDone) return;

        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(newPosition, 0.2f);

        foreach (Collider2D col in colliders)
        {
            if (col.gameObject == gameObject) continue;

            UpdateWire(col.transform.position);

            if (transform.parent.name == col.transform.parent.name)
            {
                var otherWire = col.GetComponent<Wire>();
                if (otherWire != null)
                    otherWire.Done();

                Done();
            }

            return;
        }

        UpdateWire(newPosition);
    }

    private void OnMouseUp()
    {
        if (!isDone)
            UpdateWire(startPosition);
    }

    public void Done()
    {
        if (isDone) return;

        isDone = true;

        if (lightOn != null)
            lightOn.SetActive(true);

        CheckIfAllWiresDone();
    }

    private void CheckIfAllWiresDone()
    {
        Wire[] wires = FindObjectsByType<Wire>(FindObjectsSortMode.None);

        foreach (Wire w in wires)
        {
            if (!w.isDone)
                return; // still not finished
        }

        // All wires completed
        if (PuzzleManager.Instance != null)
            PuzzleManager.Instance.PuzzleSolved();
    }

    public void ResetWire()
    {
        isDone = false;

        if (lightOn != null)
            lightOn.SetActive(false);

        UpdateWire(startPosition);
    }

    private void UpdateWire(Vector3 newPosition)
    {
        transform.position = newPosition;

        Vector3 direction = newPosition - startPoint;
        transform.right = direction * transform.lossyScale.x;

        float dist = Vector2.Distance(startPoint, newPosition);
        wireEnd.size = new Vector2(dist, wireEnd.size.y);
    }
}