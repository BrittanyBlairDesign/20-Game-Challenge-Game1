// Brittany Blair //
// Action handling system
//      Commands = key, gamepad, or mouse strokes
//      Actions = Jump, movement, or other game specific actions
//      ActionHandler = Checks if actions are made based on Action & allows for Action re-mapping.
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

 
 interface Command
{
    bool isActionActivated();
}
class KeyCommand : Command
{
    public KeyCommand( Keys key, KeyState state )
    {
        this.key = key;
        this.activeState = state;
    }
    
    public bool isActionActivated()
    {
        if( activeState == KeyState.Up)
        {
            return Keyboard.GetState().IsKeyDown( key );
        }
        else if ( activeState == KeyState.Down )
        {
            return Keyboard.GetState().IsKeyDown( key);
        }
        else return false;
    }
    public Keys getKey()
    {
        return this.key;
    }
    public KeyState getActiveState()
    {
        return this.activeState;
    }
    public void setKey(Keys key)
    {
        this.key = key;
    }
    public void setActiveState( KeyState state )
    {
        this.activeState = state;
    }
    protected Keys key;
    protected KeyState activeState;
}
class GamepadCommand : Command
{
    public GamepadCommand(Buttons button, ButtonState state)
    {
        this.button = button;
    }
    
    public bool isActionActivated()
    {
        return GamePadButtons.Equals(this.button, this.activeState);
    }
    public Buttons getButton()
    {
        return this.button;
    }
    public void setButton(Buttons button)
    {
        this.button = button;
    }

    public ButtonState getActiveState()
    {
        return this.activeState;
    }
    public void setActiveState(ButtonState state)
    {
        this.activeState = state;
    }
    protected Buttons button;
    protected ButtonState activeState;
}
class MouseCommand : Command
{
    public MouseCommand( MouseButtons button, ButtonState state)
    {
        MouseState mouseState;
        switch(button)
        {
            case MouseButtons.ClickL:
                mouseState = new MouseState(0,0,0,state,ButtonState.Released,ButtonState.Released,ButtonState.Released,ButtonState.Released);
                break;
            case MouseButtons.ClickR:
                 mouseState = new MouseState(0,0,0,ButtonState.Released,ButtonState.Released, state,ButtonState.Released, ButtonState.Released);
                break;
            case MouseButtons.ClickW:
                 mouseState = new MouseState(0,0,0,ButtonState.Released, state, ButtonState.Released,ButtonState.Released, ButtonState.Released);
                break;
            default:
                mouseState = new MouseState(0,0,0,state,ButtonState.Released, ButtonState.Released,ButtonState.Released, ButtonState.Released);            
                break;
        }   

        this.mouseState = mouseState;
        this.button = button;
        this.activeState = state;
    }

    public bool isActionActivated()
    {
        switch(this.button)
        {
            case MouseButtons.ClickL:
            return Mouse.GetState().LeftButton == activeState;
            case MouseButtons.ClickR:
            return Mouse.GetState().RightButton == activeState;
            case MouseButtons.ClickW:
            return Mouse.GetState().MiddleButton == activeState;
            default:
            return false;
        }
    }

    public MouseState getMouseState()
    {
        return this.mouseState;
    }
    public void setMouseState(MouseState mouseState, ButtonState state)
    {
        if(mouseState.LeftButton == state)
        {
            activeState = state;
            button = MouseButtons.ClickL;
        }
        else if (mouseState.RightButton == state)
        {
            activeState = state;
            button = MouseButtons.ClickR;
        }
        else if(mouseState.MiddleButton == state)
        {
            activeState = state;
            button = MouseButtons.ClickW;
        }

        this.mouseState = mouseState;
    }
    protected MouseState mouseState;
    protected MouseButtons button;
    protected ButtonState activeState;
}

public enum MouseButtons
{
    ClickL,
    ClickR,
    ClickW,
}
class ActionEvent
{
    public ActionEvent(Action action)
    {
        this.action = action;
        List<Command> commands= new List<Command>();
    }
    public ActionEvent(Action action,List<Command> commands)
    {
        this.action=action;
        this.commands = commands;
    }

    public bool isActionActive()
    {
        foreach (Command command in this.commands)
        {
            if (command.isActionActivated())
            {
                return true;
            }
        }
        return false;
    }
    public List<Command> getCommands()
    {
        return commands;
    }
    public Action getActionType()
    {
        return this.action;
    }
    public void setCommands(List<Command> commands)
    {
        this.commands = commands;
    }

    protected Action action;
    protected List<Command> commands;
}

class ActionHandler
{
// Constructor
    public ActionHandler(List<ActionEvent> actions)
    {
        this.actions = actions;
    }
// Methods
    public virtual Action HandleAction()
    {
        foreach (ActionEvent a in actions)
        {
            if(a.isActionActive())
            {
                return a.getActionType();
            }
        }

        return Action.NONE;       
    }
    
    public virtual bool BindButton(ActionEvent action, Buttons button, ButtonState state)
    {
        if ( actions.Contains(action))
        {
            int actionIndex = -1;
            int commandIndex = -1;
            GamepadCommand commandEdit = null;
            bool isInUse = false;

            for (int i = 0; i < actions.Count; i++)
            {
                for (int c = 0; c < actions[i].getCommands().Count; c++)
                {
                    Command command = actions[i].getCommands()[c];
                    if (command.GetType() == typeof(GamepadCommand))
                    {
                        GamepadCommand buttonC = (GamepadCommand)command;
                        if (buttonC.getButton() == button && buttonC.getActiveState() == state)
                        {
                            isInUse = true;
                            break;
                        }
                        else if (action == actions[i])
                        {
                            actionIndex = i;
                            commandIndex = c;
                            commandEdit = buttonC;
                            commandEdit.setButton(button);
                            commandEdit.setActiveState(state);
                        }
                    }
                }
                if (isInUse)
                {
                    break;
                }
            }

            if (!isInUse && actionIndex != -1 && commandIndex != -1 && commandEdit != null)
            {
                List<Command> clist = actions[actionIndex].getCommands();
                clist[commandIndex] = commandEdit;

                actions[actionIndex].setCommands(clist);
                return true;
            }
            else return false;
        }
        else return false;
    }
    public virtual bool BindKey(ActionEvent action, Keys key, KeyState state)
    {
        if( actions.Contains(action) )
        {
            int actionIndex = -1;
            int commandIndex = -1;
            KeyCommand commandEdit = null;
            bool isInUse = false;

            for (int i = 0; i < actions.Count; i++)
            {
                for (int c = 0; c < actions[i].getCommands().Count; c++)
                {
                    if (actions[i].getCommands()[c].GetType() == typeof(KeyCommand))
                    {
                        KeyCommand keyCommand = actions[i].getCommands()[c] as KeyCommand;
                        if (keyCommand.getKey() == key && keyCommand.getActiveState() == state)
                        {
                            isInUse = true;
                            break;
                        }
                        else if (action == actions[i])
                        {
                            actionIndex = i;
                            commandIndex = c;
                            commandEdit = keyCommand;
                            commandEdit.setKey(key);
                            commandEdit.setActiveState(state);
                        }
                    }
                }

                if (isInUse)
                {
                    break;
                }
            }

            if (actionIndex != -1 && commandIndex != -1 && commandEdit != null && !isInUse)
            {
                List<Command> commands = actions[actionIndex].getCommands();
                commands[commandIndex] = commandEdit;
                actions[actionIndex].setCommands(commands);
                return true;
            }
            else return false;
        }
        else return false;
    }
    public virtual bool BindMouseState(ActionEvent action, MouseState mouseState, ButtonState state)
    {
        if (actions.Contains(action))
        {
            int actionIndex = -1;
            int commandIndex = -1;
            MouseCommand commandEdit = null;

            bool isInUse = false;
            
            for (int i = 0; i < actions.Count; i++)
            {
                for(int c = 0; c < actions[i].getCommands().Count; c++)
                {
                    if( actions[i].getCommands()[c].GetType() == typeof(MouseState) )
                    {
                        MouseCommand mc = actions[i].getCommands()[c] as MouseCommand;
                        if(mc.getMouseState() == mouseState)
                        {
                            isInUse = true;
                            break;
                        }
                        else if(action == actions[i])
                        {
                            actionIndex = i;
                            commandIndex = c;
                            commandEdit = mc;
                            commandEdit.setMouseState(mouseState , state);

                        }
                    }
                }

                if (isInUse)
                {
                    break;
                }
            }

            if (actionIndex  != -1 && commandIndex != -1 && commandEdit != null && !isInUse)
            {
                return true;
            }
            else return false;
        }
        else return false;
    }
// Getters
    public List<ActionEvent
>   getActions()
    {
        return this.actions;
    }

// Variables
    protected List<ActionEvent> actions;
}