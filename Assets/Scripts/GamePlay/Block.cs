using UnityEngine;
using DG.Tweening;

public class Block : MonoBehaviour
{
    public Direction direction;
    public GameObject arrow;
    public BoxCollider2D col;
    public LayerMask blockLayer;
    public int gridX, gridY;

    void Start()
    {
        if (col == null) col = GetComponent<BoxCollider2D>();
        SetupArrowRotation();
    }

    public void SetupArrowRotation()
    {
        float angle = 0f;
        switch (direction)
        {
            case Direction.Up: angle = 90f; break;
            case Direction.Down: angle = -90f; break;
            case Direction.Left: angle = 180f; break;
            case Direction.Right: angle = 0f; break;
        }
        arrow.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public Vector2 GetDirection()
    {
        switch (direction)
        {
            case Direction.Up: return Vector2.up;
            case Direction.Down: return Vector2.down;
            case Direction.Left: return Vector2.left;
            case Direction.Right: return Vector2.right;
            default: return Vector2.zero;
        }
    }

    void OnMouseDown()
    {
        GridManager.Instance.RegisterMoveUsed();
        AudioManager.Instance.PlaySFX(AudioManager.Instance.moveBlock);
        TryRemove();
    }

    void TryRemove()
    {
        Vector2 dir = GetDirection();
        int targetX = gridX;
        int targetY = gridY;
       
        while (true)
        {
            int nextX = targetX + (int)dir.x;
            int nextY = targetY + (int)dir.y;

           
            if (nextX < 0 || nextX >= GridManager.Instance.activeLevel.width ||
                nextY < 0 || nextY >= GridManager.Instance.activeLevel.height)
            {
                FlyOutAndDestroy(dir);
                return;
            }

            
            if (GridManager.Instance.grid[nextX, nextY] != null)
            {
                if (targetX != gridX || targetY != gridY) MoveTo(targetX, targetY);
                return;
            }

            targetX = nextX;
            targetY = nextY;
        }
    }

    void FlyOutAndDestroy(Vector2 dir)
    {
        GridManager.Instance.grid[gridX, gridY] = null;
        Vector3 exitPos = transform.position + (Vector3)dir * 10f; 
        transform.DOMove(exitPos, 0.4f).OnComplete(RemoveBlock);
    }
    void MoveTo(int x, int y)
    {
        GridManager.Instance.grid[gridX, gridY] = null;
        gridX = x; 
        gridY = y;
        GridManager.Instance.grid[gridX, gridY] = this;

  
        transform.DOMove(GridManager.Instance.GetWorldPos(x, y), 0.2f);
    }
    void RemoveBlock()
        {
            GridManager.Instance.RegisterBlockRemoved();
            Destroy(gameObject);
        }
    public void DestroyByGear()
    {
        GridManager.Instance.grid[gridX, gridY] = null;
        GridManager.Instance.RegisterBlockRemoved();
        Destroy(gameObject);
    }
}