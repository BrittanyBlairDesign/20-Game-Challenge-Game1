
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
class FlappyPlayer : PlayerSprite
{
    bool isJumping = false;
    public FlappyPlayer(Texture2D texture): base(texture)
    {
        this.move = new Movement(this, 35f, new Vector2(0, 20f));
    }

    public void Update(GameTime gameTime)
    {
        move.calculateVelocity(gameTime);
        if (move.gravity.Y >= move.startGravity.Y / 10)
        {
            isJumping = false;
        }
    }

    public void Jump()
    {
        if (!isJumping)
        {
            isJumping = true;
            move.gravity.Y = -30;

        } 
    }
    Movement move;
}