using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemView : MonoBehaviour
{
    public SpriteRenderer sp;

    public void SetSprite(Sprite sprite)
    {
        sp.sprite = sprite;
    }
}
