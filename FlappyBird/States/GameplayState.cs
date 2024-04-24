// Ignore Spelling: Gameplay
using System;
using Microsoft.Xna.Framework.Input;
public class GameplayState : BaseGameState
{
    private const string Player = "Sprites/Player";
    private const string BackgroundTexture = "Backgrounds/Barren";
    public override void LoadContent()
    {
        AddGameObject(new SplashImage(LoadTexture(BackgroundTexture)));
        AddGameObject(new PlayerSprite(LoadTexture(Player)));
    }

    public override void HandleInput()
    {
        var state = Keyboard.GetState();

        if(state.IsKeyDown(Keys.Escape))
        {
            NotifyEvent(Event.kGAME_QUIT);
        }
    }

    public event EventHandler<Action> onActionNotification;
    protected void NotifyAction (Action actionType, object argument = null)
    {
        onActionNotification?.Invoke(this, actionType);
        foreach ( var gameObj in _gameObjects)
        {
            if (gameObj.GetType() == typeof(PlayerSprite))
            {
                PlayerSprite obj = gameObj as PlayerSprite;
                obj.OnNotifyAction(actionType);
            }
        }
    }
}
