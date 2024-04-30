
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FlappyBird;

class FlappyPlayer : PlayerSprite
{
    public bool isJumping = false;
    public int Score = 0;
    
    
    public FlappyPlayer(Texture2D texture): base(texture)
    {
        this.move = new Movement(this, 35f, new Vector2(0, 20f));
        GenerateCollisions();
    }

    public FlappyPlayer( GraphicsDevice graphics,Texture2D texture) : base(graphics,texture)
    {
        this.move = new Movement(this, 35f, new Vector2(0, 20f));
        collisionOutline = Color.Green;
        GenerateCollisions();
        SetCollisionTexture(graphics);
    }

    public override void GenerateCollisions()
    {
        collisions.Add(new Rectangle((int)Position.X, (int)Position.Y, Width, Height));
    }

    public void Update(GameTime gameTime)
    {
        move.calculateVelocity(gameTime);
        if (move.gravity.Y >= move.startGravity.Y / 10)
        {
            isJumping = false;
        }

        if (collisions != null)
        {
            for (int i = 0; i < collisions.Count; i++)
            {
                Rectangle rect = collisions[i];
                rect.X = (int)Position.X;
                rect.Y = (int)Position.Y;

                collisions[i] = rect;
            }
        }
    }

    public override void Render(SpriteBatch spriteBatch)
    {

        base.Render(spriteBatch);
        
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