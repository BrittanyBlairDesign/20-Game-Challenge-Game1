

using Microsoft.Xna.Framework;

class Movement
{
    public Movement(BaseGameObject gameObject)
    {
        this.Object = gameObject;
    }

        public Movement(BaseGameObject gameObject, float mass, Vector2 gravity)
    {
        this.Object = gameObject;
        this.mass = mass;
        this.gravity = gravity;
    }

    public virtual void calculateVelocity(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        if(gravity.Y < 1.0)
        {
            gravity.Y += deltaTime;
        }
        else
        {
            gravity.Y = 1.0f;
        }
        
        acceleration += (gravity / mass) * deltaTime;
        velocity += acceleration * deltaTime;

        Object.setPosition(Object.getPosition() + (velocity * deltaTime));
    }


    public Vector2 acceleration = Vector2.Zero;
    public Vector2 velocity = Vector2.Zero;
    public Vector2 gravity = Vector2.Zero;
    public float mass = 0.0f;
    private BaseGameObject Object;
}