﻿using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DesignPatterns.Infrastructure
{
    public class Bus
    {
        public Bus()
        {
            _listeners = new List<Action<object>>();
            _queue = new Dictionary<Type, Action<object>>();
            _topic = new Dictionary<Type, List<Action<object>>>();
        }

        private readonly List<Action<object>> _listeners;
        private readonly IDictionary<Type, Action<object>> _queue;
        private readonly IDictionary<Type, List<Action<object>>> _topic;

        public void AddListener(Action<object> listener)
        {
            _listeners.Add(listener);
        }

        public void RegisterQueue<T>(Action<T> handler)
        {
            if (_topic.ContainsKey(typeof(T)) || _queue.ContainsKey(typeof(T)))
            {
                throw new HandlerAlreadyUsedException();
            }
            _queue.Add(typeof(T), (a) => handler((T)a));
        }

        public void RegisterTopic<T>(Action<T> handler)
        {
            if (_queue.ContainsKey(typeof(T)))
            {
                throw new HandlerAlreadyUsedException();
            }
            if (!_topic.ContainsKey(typeof(T)))
            {
                _topic.Add(typeof(T), new List<Action<object>>());
            }
            _topic[typeof(T)].Add((a) => handler((T)a));
        }

        private static object Clone(object message)
        {
            var tmp = JsonConvert.SerializeObject(message);
            return JsonConvert.DeserializeObject(tmp, message.GetType());
        }

        public void Send(object message)
        {
            Execute(message, _listeners.ToArray());
            if (_topic.ContainsKey(message.GetType()))
            {
                Execute(message, _topic[message.GetType()].ToArray());
            }
            else if (_queue.ContainsKey(message.GetType()))
            {
                Execute(message, _queue[message.GetType()]);
            }
        }

        private static void Execute(object message, params Action<object>[] actions)
        {
            foreach (var action in actions)
            {
                try
                {
                    var clone = Clone(message);
                    action(clone);
                }
                catch (Exception)
                {
                    //NOOP
                }
            }
        }
    }
}
