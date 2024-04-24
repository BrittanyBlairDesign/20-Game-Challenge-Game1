

using System.Collections.Generic;

namespace FlappyBird;
class FlappyBirdGameplayState : GameplayState
{
    public override void HandleInput()
    {
        controller.HandleAction();
        base.HandleInput();
    }

    FlappyBirdController controller = new FlappyBirdController((new List<ActionEvent>(){new Jump()}));
}