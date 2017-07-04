namespace MagicFire
{

    public interface IFactorysFactory
    {
        IBaseFactory CreateFactory<TFactoryType>(params object[] factoryParameters) where TFactoryType : IBaseFactory;
    }

}