using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Cell : MonoBehaviour
{
    public BoxCollider2D CellCollider;

    public int BoardX { get; private set; }

    public int BoardY { get; private set; }

    public Item Item { get; private set; }

    public Cell NeighbourUp { get; set; }

    public Cell NeighbourRight { get; set; }

    public Cell NeighbourBottom { get; set; }

    public Cell NeighbourLeft { get; set; }


    public bool IsEmpty => Item == null;

    private void OnEnable()
    {
        CellCollider.enabled = true;
    }

    public NormalItem.eNormalType GetItemType()
    {
        return IsEmpty ? NormalItem.eNormalType.NONE : Item.GetItemType();
    }

    public bool IsDistinceFrom4Around()
    {
        if (NeighbourUp != null && IsSameType(NeighbourUp))
            return false;

        if (NeighbourBottom != null && IsSameType(NeighbourBottom))
            return false;

        if (NeighbourLeft != null && IsSameType(NeighbourLeft))
            return false;

        if (NeighbourRight != null && IsSameType(NeighbourRight))
            return false;

        return true;
    }

    public void Setup(int cellX, int cellY)
    {
        this.BoardX = cellX;
        this.BoardY = cellY;
    }

    public bool IsNeighbour(Cell other)
    {
        return BoardX == other.BoardX && Mathf.Abs(BoardY - other.BoardY) == 1 ||
            BoardY == other.BoardY && Mathf.Abs(BoardX - other.BoardX) == 1;
    }


    public void Free()
    {
        Item = null;
    }

    public void Assign(Item item)
    {
        Item = item;
        Item.SetCell(this);
    }

    public void ApplyItemPosition(bool withAppearAnimation)
    {
        Item.SetViewPosition(this.transform.position);

        if (withAppearAnimation)
        {
            Item.ShowAppearAnimation();
        }
    }

    internal void Clear()
    {
        if (Item != null)
        {
            StopHintAnimation();
            Item.Clear();
            Item = null;
        }
    }

    internal bool IsSameType(Cell other)
    {
        return Item != null && other.Item != null && Item.IsSameType(other.Item);
    }

    internal void ExplodeItem()
    {
        if (Item == null) return;

        StopHintAnimation();
        Item.ExplodeView();
        Item = null;
    }

    internal void AnimateItemForHint()
    {
        Item.AnimateForHint();
    }

    internal void StopHintAnimation()
    {
        Item.StopAnimateForHint();
    }

    internal void ApplyItemMoveToPosition()
    {
        Item.AnimationMoveToPosition();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        CellCollider ??= GetComponent<BoxCollider2D>();   
    }
#endif
}
