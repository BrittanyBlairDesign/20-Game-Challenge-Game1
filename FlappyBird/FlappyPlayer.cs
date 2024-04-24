
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
class FlappyPlayer : PlayerSprite
{
    public FlappyPlayer(Texture2D texture): base(texture)
    {
        this.move = new Movement(this, .5f, new Vector2(0, 1));
    }

    public void Update(GameTime gameTime)
    {
        move.calculateVelocity(gameTime);
    }

    public override void OnNotifyAction(Action actionType)
    {
        switch (actionType)
        {
            case Action.JUMP:
                move.gravity.Y = -1;
                break;
        }
    }
    Movement move;
}