using System;
using OregoFramework.Unit;

namespace OregoFramework.App
{
    public interface IUIStatefulResource : IResource
    {
        Type stateAdapterType { get; }
    }
}