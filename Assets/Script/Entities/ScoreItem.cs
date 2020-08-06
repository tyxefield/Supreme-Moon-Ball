using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItem : Entity
{
    private GameObject sprite;

    public ScoreItem(Level level, int id, float x, float y, float z) : base(x, y, z, 0)
    {
        ent = new GameObject("ScoreItem", typeof(BoxCollider));
        sprite = new GameObject("Sprite", typeof(SpriteRenderer));

        sprite.transform.SetParent(ent.transform);
        sprite.transform.localPosition = VectorData.zero;

        sprite.GetComponent<SpriteRenderer>().sprite = Main.game.TEXTURES.score;

        var col = ent.GetComponent<BoxCollider>();
        col.isTrigger = true;

        ent.transform.position = VectorData.Set(x, y, z);

    }

    public class ScoreTag : MonoBehaviour
    {

    }
}