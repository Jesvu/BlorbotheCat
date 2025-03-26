using UnityEngine;
using UnityEngine.SceneManagement;
using Bluescreen.BlorboTheCat;

namespace blorbothecat.States
{
    public class MainMenuState : GameStateBase
    {
        public override string SceneName
        {
            get { return "MainMenu"; }
        }

        public override StateType Type
        {
            get { return StateType.MainMenu; }
        }

        public MainMenuState() : base()
        {
            AddTargetState(StateType.InGame);
            AddTargetState(StateType.Options);
            AddTargetState(StateType.LevelOver);
            AddTargetState(StateType.MainMenu);
        }
        
    }
}