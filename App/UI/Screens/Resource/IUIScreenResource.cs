using System;
using OregoFramework.Unit;

namespace OregoFramework.App
{
    public interface IUIScreenResource : IResource
    {
        Type screenType { get; }
    }
}