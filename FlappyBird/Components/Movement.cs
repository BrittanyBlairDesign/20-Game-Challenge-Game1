

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
        this.startGravity = gravity;
        this.acceleration = (gravity / mass);
    }

    public virtual void calculateVelocity(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        deltaTime = deltaTime * 5;
        float adjustedMass = 0.0f;
        if(gravity.Y < startGravity.Y)
        {
            gravity.Y += deltaTime * 10;
            adjustedMass = mass - mass /3;
        }
        else
        {
            adjustedMass = mass;
            gravity.Y = startGravity.Y;
        }
        
        acceleration += (gravity / mass);
        if (acceleration.Y < gravity.Y / mass)
        {
            acceleration = gravity /mass;
        }
        else if (acceleration.Y > (gravity.Y / mass) * 10)
        {
            acceleration = (gravity / mass) * 5;
        }

        velocity =  acceleration + (adjustedMass * gravity) * deltaTime;

        Object.setPosition(Object.getPosition() + (velocity * deltaTime));
    }


    public Vector2 acceleration = Vector2.Zero;
    public Vector2 velocity = Vector2.Zero;
    public Vector2 gravity = Vector2.Zero;
    public Vector2 startGravity = Vector2.Zero;
    public float mass = 0.0f;
    private BaseGameObject Object;
}