using System;
using UI;
using UnityEngine;
using VContainer;

namespace ATG.Session
{
    [Serializable]
    public class SessionServiceCreator
    {
        [SerializeField] private SessionsView view;

        public void Create(IContainerBuilder builder)
        {
            builder.Register<SessionsService>(Lifetime.Singleton)
                .AsSelf().AsImplementedInterfaces();
            builder.Register<SessionsPresenter>(Lifetime.Singleton)
                .WithParameter(view)
                .AsSelf().AsImplementedInterfaces();
        }
    }
}