

namespace blorbothecat.States
{
    public class LevelOverState : GameStateBase
    {
        public override string SceneName
        {
            get { return "LevelOver"; }
        }

        public override StateType Type
        {
            get { return StateType.LevelOver; }
        }

        public override bool IsAdditive
        {
            get { return true; }
        }

        public LevelOverState() : base()
        {
            AddTargetState(StateType.InGame);
            AddTargetState(StateType.MainMenu);
        }
    }
}
