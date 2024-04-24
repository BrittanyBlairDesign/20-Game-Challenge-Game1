// written by Brittany Blair //
// player class, handles player input, and updates.


using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;


class Jump : ActionEvent
{
    public Jump():base(Action.JUMP)
    {
        
        KeyCommand keyCommand = new KeyCommand(Keys.W, KeyState.Down);
        GamepadCommand GCommand = new GamepadCommand(Buttons.A, ButtonState.Pressed);
        MouseCommand MCommand = new MouseCommand(MouseButtons.ClickL, ButtonState.Pressed);
        this.setCommands(new List<Command>(){keyCommand,GCommand,MCommand}); 
    }
}


class FlappyBirdController : ActionHandler
{
    public FlappyBirdController(List<ActionEvent> actions):base(actions){}
}

