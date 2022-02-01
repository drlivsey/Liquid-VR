namespace Liquid.StateMachines
{
    public interface IState
    {
        void Enter();
        void Exit();
    }
}