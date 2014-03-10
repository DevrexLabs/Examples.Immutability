using System;
using OrigoDB.Core;

namespace OrigoDb.Examples.Immutable
{
    [Serializable]
    public class ReorderItemsCommand : ImmutabilityCommand<TodoModel>
    {
        /// <summary>
        /// Name of the list to reorder
        /// </summary>
        public readonly string List;

        /// <summary>
        /// The new order of the existing items, one-based indicies
        /// </summary>
        private readonly int[] NewOrder;

        public ReorderItemsCommand(string list, int[] order)
        {
            List = list;
            NewOrder = order;
        }

        public override void Execute(TodoModel model, out TodoModel result)
        {
            result = model.ReorderItems(List, NewOrder);
        }
    }
}