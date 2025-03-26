using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace blorbothecat.States
{
    //controls the games global state, there should be exactly one instance of this object
    //at any given time
    public class GameStateManager : MonoBehaviour
    {
        #region Statics
        //the static variable which stores the reference to the only instance that can be created
        //from this class
        private static GameStateManager instance;
        

        public static GameStateManager Instance
        {
            get
            {
                //lazy loading, the instance is created when we need it for the first time
                if (instance == null)
                {
                    //create the instance that we can create from this state
                    //we use Resources.Load to access correct prefab runtime
                    GameStateManager prefab = 
                        Resources.Load<GameStateManager>(typeof(GameStateManager).Name);

                    instance = Instantiate(prefab);
                }
                return instance;
            }
        }
        #endregion

        #region Fields
        private List<GameStateBase> states = new List<GameStateBase>();

        #endregion

        #region Properties
        public GameStateBase CurrentState
        {
            get;
            private set;
        }

        public GameStateBase PreviousState
        {
            get;
            private set;
        }

        #endregion

        #region Unity messages
        private void Awake()
        {
            //we need to make sure theres only one intance available at any given time
            if (instance == null)
            {
                //this should be the one and only instance
                instance = this;
            }
            else if (instance != this)
            {
                //we have more than one instance from this class at the same time
                //this is illegal based on Singleton patterns definiton
                //destroy the second instance
                Debug.LogWarning($"Multiple {typeof(GameStateManager).Name} instances detected!" +
                    $"\n Destroying excess ones.");
                Destroy(this);
                return;
            }

            //by calling this unity prevents destroying this Gameobject durign scene (un)load
            DontDestroyOnLoad(gameObject);

            Initialize();
        }
        #endregion

        #region Private implementation
        private void Initialize()
        {
            //create state objects
            MainMenuState mainMenu = new MainMenuState();
            InGameState inGame = new InGameState();
            OptionsState options = new OptionsState();
            GameOverState gameOver = new GameOverState();
            LevelOverState levelOver = new LevelOverState();

            states.Add(mainMenu);
            states.Add(inGame);
            states.Add(options);
            states.Add(gameOver);
            states.Add(levelOver);

            string activeSceneName = SceneManager.GetActiveScene().name.ToLower();
            foreach(GameStateBase state in states)
            {
                if (state.SceneName.ToLower() == activeSceneName)
                {
                    ActivateFirstScene(state);
                    break; //early exit from loop
                }
            }

            if (CurrentState == null)
            {
                if (GameObject.Find("Player") == null)
                {
                    ActivateFirstScene(mainMenu);
                }
                else
                {
                    ActivateFirstScene(inGame);
                }
            }
        }

        private void ActivateFirstScene(GameStateBase first)
        {
            CurrentState = first;
            CurrentState.Activate();
        }

        private GameStateBase GetState(StateType type)
        {
            foreach (GameStateBase state in states)
            {
                if (state.Type == type)
                {
                    return state;
                }
            }

            return null;
        }
        #endregion

        #region Public API
        /// transitions from current state to the target state
        /// <param name="targetStateType"> the type of the target state </param>
        /// <returns>True, if transition is legal and can be done, false otherwise</returns>
        public bool Go(StateType targetStateType)
        {
            Debug.Log($"Transitioning to the {targetStateType}");
            //check the legality of the transition
            if (!CurrentState.IsValidTarget(targetStateType))
            {
                Debug.Log($"{targetStateType} is not valid target for {CurrentState.Type}");
                return false;
            }
            //find the state that matches the TargetStateType
            GameStateBase nextState = GetState(targetStateType);
            if (nextState == null)
            {
                Debug.Log($"No state exists that requires the {targetStateType}");
                return false;
            }
            //transition from current state to the target state
            PreviousState = CurrentState;

            CurrentState.Deactivate();
            CurrentState = nextState;
            CurrentState.Activate();
            return true;
        }

        /// <summary>
        /// transitions back to the previous state
        /// </summary>
        /// <returns>true is the transition succeeds, false otherwise</returns>
        public bool GoBack()
        {
            return Go(PreviousState.Type);
        }

        #endregion
    }
}
