using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : Entity
{
    private GameObject sprite;
    private BoxCollider col;

    public Goal(float x, float y, float z) : base(x, y, z, 0)
    {
        ent = new GameObject("Goal", typeof(BoxCollider), typeof(GoalFlag));
        sprite = new GameObject("Sprite", typeof(SpriteRenderer), typeof(Animator));

        ent.GetComponent<GoalFlag>().script = this;

        sprite.transform.SetParent(ent.transform);
        sprite.transform.localPosition = VectorData.zero;

        col = ent.GetComponent<BoxCollider>();
        col.isTrigger = true;
        col.size = VectorData.Set(5, 5, 5);

        sprite.GetComponent<SpriteRenderer>().sprite = Main.game.TEXTURES.goal;

        ent.transform.position = VectorData.Set(x, y, z);
        ent.transform.localScale = VectorData.Set(2, 2, 2);
    }

    public class GoalFlag : MonoBehaviour
    {
        public Goal script;
        public Player player;

        private void LateUpdate()
        {
            if (player == null)
                player = FindObjectOfType<Player.FUNC_PLAYER>().player;
            else
            {
                script.ent.transform.LookAt(player.ent.transform);
            }
        }
    }
}
