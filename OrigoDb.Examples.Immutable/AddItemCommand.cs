using System;
using OrigoDB.Core;

namespace OrigoDb.Examples.Immutable
{
    [Serializable]
    public class AddItemCommand : ImmutabilityCommand<TodoModel>
    {
        /// <summary>
        /// Name of the list to add an item to
        /// </summary>
        public readonly string List;

        /// <summary>
        /// The item to append to the list
        /// </summary>
        public readonly string Item;


        public AddItemCommand(string list, string item)
        {
            List = list;
            Item = item;
        }

        public override void Execute(TodoModel model, out TodoModel result)
        {
            result = model.AddItem(List, Item);
        }
    }
}