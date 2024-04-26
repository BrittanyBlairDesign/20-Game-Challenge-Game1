

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


public class FlappyObstacle : PlayerSprite
{
    public FlappyObstacle(Texture2D texture, Vector2 StartPosition, float speed, Vector2 direction) : base(texture)
    {
        Position = StartPosition;
        this.speed = speed;
        this.direction = direction;
    }

    public void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        Vector2 velocity = speed * direction;
        Position += velocity * deltaTime;
    }


    public float speed;
    Vector2 direction;
}
