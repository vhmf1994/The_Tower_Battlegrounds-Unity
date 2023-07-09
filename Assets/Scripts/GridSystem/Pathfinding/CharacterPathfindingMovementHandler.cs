using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CharacterPathfindingMovementHandler : MonoBehaviour
{
    private const float speed = 1f;

    [SerializeField] private List<PathNode> path;

    private Vector3 targetPosition;

    private void Start()
    {
        path = PathfindingGrid.Instance.FindPath();

        if (path != null)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {
                Debug.DrawLine(path[i].worldPosition, path[i + 1].worldPosition, Color.blue, 10f);
            }
        }

        SetTargetPosition();
    }

    private void OnDisable()
    {
        StopMoving();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (HasReached())
            return;

        if (Vector3.Distance(transform.position, targetPosition) > .01f)
        {
            Vector3 moveDir = (targetPosition - transform.position).normalized;

            transform.position = transform.position + moveDir * speed * Time.deltaTime;
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * 20f);
        }
        else
        {
            path.RemoveAt(0);

            if (path.Count <= 0)
            {
                StopMoving();
                return;
            }

            targetPosition = path.First().worldPosition;
        }
    }

    private void StopMoving()
    {
        path = null;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public bool HasReached()
    {
        return path == null;
    }

    public void SetTargetPosition()
    {
        if (path != null && path.Count > 1)
        {
            transform.position = path.First().worldPosition;
            transform.forward = (path.First().worldPosition - transform.position).normalized;

            targetPosition = path.First().worldPosition;
            path.RemoveAt(0);
        }
    }
}