using System;
using OrigoDB.Core;

namespace OrigoDb.Examples.Immutable
{
    [Serializable]
    public class AddListCommand : ImmutabilityCommand<TodoModel>
    {
        /// <summary>
        /// Name of the list to add
        /// </summary>
        public readonly string List;


        public AddListCommand(string list)
        {
            List = list;
        }

        public override void Execute(TodoModel model, out TodoModel result)
        {
            result = model.AddList(List);
        }
    }
}