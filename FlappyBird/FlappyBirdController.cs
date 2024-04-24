// written by Brittany Blair //
// player class, handles player input, and updates.

using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using FlappyBird;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


class Jump : Action
{
    public Jump()
    {
        name = "Jump";
        KeyCommand keyCommand = new KeyCommand(Keys.W, KeyState.Down);
        GamepadCommand GCommand = new GamepadCommand(Buttons.A, ButtonState.Pressed);
        MouseCommand MCommand = new MouseCommand(MouseButtons.ClickL, ButtonState.Pressed);
        this.setCommands(new List<Command>(){keyCommand,GCommand,MCommand}); 
    }

    public override void Execute()
    {

    }
}

class Exit: Action
{
    public Exit()
    {
        name = "Exit";
        KeyCommand kCommand = new KeyCommand(Keys.Escape, KeyState.Down);
        GamepadCommand gCommand = new GamepadCommand(Buttons.Back, ButtonState.Pressed);
        this.setCommands(new List<Command>(){kCommand,gCommand});
    }

    public override void Execute()
    {

    }
}