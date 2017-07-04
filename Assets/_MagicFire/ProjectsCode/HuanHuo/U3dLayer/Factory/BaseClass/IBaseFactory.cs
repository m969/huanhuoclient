using System;

namespace MagicFire
{
    public interface IBaseFactory
    {
        TProductType CreateProduct<TProductType>(params object[] productParameters);
        object CreateProduct(Type productType, params object[] productParameters);
    }
}