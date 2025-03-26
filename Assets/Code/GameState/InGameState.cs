using UnityEngine;
using UnityEngine.SceneManagement;
using Bluescreen.BlorboTheCat;

namespace blorbothecat.States
{
    public class InGameState : GameStateBase
    {
        public override string SceneName
        {
            get { return "Level" + LatestCompletionInfo.levelNumber; }
        }

        public override StateType Type
        {
            get { return StateType.InGame; }
        }


        public InGameState() : base()
        {
            AddTargetState(StateType.Options);
            AddTargetState(StateType.GameOver);
            AddTargetState(StateType.InGame);
            AddTargetState(StateType.LevelOver);
            AddTargetState(StateType.MainMenu);
        }

        public override void Activate(bool forceLoad = false)
        {
            //calls the base class's implementation
            base.Activate(forceLoad);

            //unpause the game
            Time.timeScale = 1;

            PlayerActions.actionsFrozen = false;
            PlayerController.movementFrozen = false;
        }

        public override void Deactivate()
        {
            base.Deactivate();

            PlayerActions.actionsFrozen = true;
            PlayerController.movementFrozen = true;

            Time.timeScale = 0;
        }
    }
}
