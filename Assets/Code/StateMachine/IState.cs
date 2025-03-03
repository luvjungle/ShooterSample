namespace Code.StateMachine
{
    public interface IState
    {
	    bool CanExit { get; }
	    
        void Enter();
        void Update(float deltaTime);
        void Exit();
    }
}