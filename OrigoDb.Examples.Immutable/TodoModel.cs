using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using OrigoDB.Core;
using System.Runtime.Serialization;

namespace OrigoDb.Examples.Immutable
{
    /// <summary>
    /// An immutable model. Methods that mutate return a new instance.
    /// The new immutable collections from .NET 4.5 are not serializable
    /// so we need to add custom serialization to support snapshots.
    /// </summary>
    [Serializable]
    public class TodoModel: Model, ISerializable
    {
        private readonly ImmutableDictionary<string, ImmutableList<string>> _lists;

        public TodoModel()
        {
            _lists = ImmutableDictionary.Create<string, ImmutableList<string>>();
        }

        public TodoModel(ImmutableDictionary<string,ImmutableList<string>> lists)
        {
            _lists = lists;
        }

        public TodoModel AddList(string name)
        {
            return new TodoModel(
                _lists.Add(name, ImmutableList.Create<string>()));
        }

        public TodoModel AddItem(string listName, string item)
        {
            return new TodoModel(_lists.SetItem(listName, _lists[listName].Add(item)));
        }

        public TodoModel ReorderItems(string listName, int[] newOrder)
        {
            var list = _lists[listName];
            var builder = ImmutableList.CreateBuilder<string>();
            foreach(int oneBasedIndex in newOrder) builder.Add(list[oneBasedIndex-1]);
            return new TodoModel(_lists.SetItem(listName, builder.ToImmutable()));
        }

        public ImmutableList<string> GetItems(string listName)
        {
            return _lists[listName];
        }

        public ImmutableList<string> GetLists()
        {
            return _lists.Keys.ToImmutableList();
        }


        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            var dictionary = _lists.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToArray());
            info.AddValue("d", dictionary);
        }

        protected TodoModel(SerializationInfo info, StreamingContext context)
        {
            var dictionary = (Dictionary<string, string[]>)info.GetValue("d", typeof(Dictionary<string, string[]>));
            var builder = ImmutableDictionary.CreateBuilder<string, ImmutableList<String>>();
            foreach (var kvp in dictionary)
            {
                builder.Add(kvp.Key, kvp.Value.ToImmutableList());
            }
            _lists = builder.ToImmutable();            
        }
    }
}
