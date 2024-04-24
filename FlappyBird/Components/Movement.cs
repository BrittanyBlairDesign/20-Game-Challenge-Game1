

using Microsoft.Xna.Framework;

class Movement
{
    public Movement(BaseGameObject gameObject)
    {
        this.Object = gameObject;
    }

    public virtual void calculateVelocity(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        if(gravity.Y < 1.0)
        {
            gravity.Y += deltaTime;
        }
        
        acceleration += (gravity / mass) * deltaTime;
        velocity += acceleration * deltaTime;

        Object.setPosition(Object.getPosition() + (velocity * deltaTime));
    }

    float speed;
    public Vector2 direction;
    public Vector2 acceleration;
    public Vector2 velocity;
    public Vector2 gravity;
    public float mass;
    private BaseGameObject Object;
}