using System;

namespace MethodsLoadingPlugins
{
    public interface Calculate
    {
        bool CanCalculate(Order o);
        double NewAmount(Order o);
    }


}
