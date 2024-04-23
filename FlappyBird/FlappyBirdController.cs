// written by Brittany Blair //
// player class, handles player input, and updates.

using System.Collections.Generic;
using System.Security;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


class Jump : Action
{
    public Jump()
    {
        name = "Jump";
        KeyCommand keyCommand = new KeyCommand(Keys.W, KeyState.Down);
        GamepadCommand GCommand = new GamepadCommand(Buttons.A, ButtonState.Pressed);
        this.setCommands(new List<Command>(){keyCommand,GCommand}); 
    }

    public override void Execute()
    {

    }
}