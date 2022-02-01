namespace Liquid.StateMachines
{
    public interface IStateMachine
    {
        void MakeTransaction(IState state);
        bool TryMakeTransaction(IState state);
        bool CanMakeTransaction(IState state);
        bool IsCurrentState(IState state);
        void MakeTransaction(string stateName);
        bool TryMakeTransaction(string stateName);
        bool CanMakeTransaction(string stateName);
        bool IsCurrentState(string stateName);
    }
}