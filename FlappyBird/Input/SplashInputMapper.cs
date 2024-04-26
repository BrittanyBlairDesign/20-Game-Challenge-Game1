
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

public class SplashInputMapper : BaseInputMapper
{
    public override IEnumerable<BaseInputCommand> GetKeyboardState(KeyboardState state)
    {
        var commands = new List<SplashInputCommand>();

        if (state.IsKeyDown(Keys.Enter))
        {
            commands.Add(new SplashInputCommand.GameSelect());
        }
        if (state.IsKeyDown(Keys.Escape))
        {
            commands.Add(new SplashInputCommand.GameExit());
        }

        return commands;
    }
}