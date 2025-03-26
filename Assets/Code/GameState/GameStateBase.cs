using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //load/unload functionality for scenes

namespace blorbothecat.States
{
    // and abstract class. this means we cant instantiate objects directly from this class
    // instead we must derive atleast one non-abstract class from this and use that instead
    public abstract class GameStateBase
    {
        //list of legal target state types for this state
        private List<StateType> targetStates = new List<StateType>();
        //an abstract property, the get accessor has to be implemented in a child class
        public abstract string SceneName { get; }

        public abstract StateType Type { get; }

        public virtual bool IsAdditive { get { return false; } }

        //default constructor this is called when object is created without parameters
        //compiler will create this is we dont declare any constructors
        //this is safe since the class is not derived from MonoBehaviour
        protected GameStateBase()
        {
        }

        protected void AddTargetState(StateType targetStateType)
        {
            if (!targetStates.Contains(targetStateType))
            {
                targetStates.Add(targetStateType);
            }
        }

        protected void RemoveTargetState(StateType targetStateType)
        {
            targetStates.Remove(targetStateType);
        }

        /// <summary>
        /// activates the state, loads the related scene as well
        /// </summary>
        /// <param name="forceLoad">forces the scene (re)load</param>
        public virtual void Activate(bool forceLoad = false)
        {
            //The scene loading
            //load the target scene if its not loaded yet
            //the reference to currently loaded scene
            Scene currentScene = SceneManager.GetActiveScene();
            if (forceLoad || currentScene.name.ToLower() != SceneName.ToLower())
            {
                //the target scene is not loaded yet, lets load it
                //one line if-else statement+variable assignment
                //Syntax: variable = condition ? true case : false case
                LoadSceneMode mode = IsAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single;

                //handles the scene loading
                SceneManager.LoadScene(SceneName, mode);
            }
        }
        public virtual void Deactivate()
        {
            //unload the level if necessary
            if (IsAdditive)
            {
                SceneManager.UnloadSceneAsync(SceneName);
            }
        }

        /// <summary>
        /// checks if the targetStateType is valid
        /// </summary>
        /// <param name="targetStateType">type of transition target</param>
        /// <returns>true, if targetStateType is valid target, false otherwise</returns>
        public bool IsValidTarget(StateType targetStateType)
        {
            foreach(StateType stateType in targetStates)
            {
                if (stateType == targetStateType)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
