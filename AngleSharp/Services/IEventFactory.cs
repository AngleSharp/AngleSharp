namespace AngleSharp.Services
{
    using AngleSharp.Dom.Events;
    using System;

    interface IEventFactory : IService
    {
        Event Create(String name);
    }
}
