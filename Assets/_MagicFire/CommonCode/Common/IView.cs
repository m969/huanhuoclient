namespace MagicFire.Common
{

    using KBEngine;
    using Model = KBEngine.Model;

    public interface IView
    {
        IModel Model { get; }
        void InitializeView(IModel model);
        void OnModelDestrooy(object[] objects);
    }

}