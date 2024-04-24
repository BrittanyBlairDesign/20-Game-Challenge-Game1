
using Microsoft.Xna.Framework.Graphics;

public class PlayerSprite : BaseGameObject
{
    public PlayerSprite(Texture2D texture)
    {
        _texture = texture;
    }

    public virtual void OnNotifyAction(Action actionType){}
}
