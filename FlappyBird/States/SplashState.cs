
using Microsoft.Xna.Framework.Input;

public class SplashState : BaseGameState
{
    public override void LoadContent()
    {
        AddGameObject(new SplashImage(LoadTexture("SplashScreens/Splash")));
    }

    public override void HandleInput()
    {
        var state = Keyboard.GetState();

        if(state.IsKeyDown(Keys.Enter))
        {
            SwitchState(new GameplayState());
        }
    }
}

