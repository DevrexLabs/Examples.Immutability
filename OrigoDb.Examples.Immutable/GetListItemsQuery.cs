using System.Collections.Immutable;
using OrigoDB.Core;

namespace OrigoDb.Examples.Immutable
{
    public class GetListItemsQuery : Query<TodoModel, ImmutableList<string>>
    {

        /// <summary>
        /// Name of the list to retrieve items for
        /// </summary>
        public readonly string List;

        public GetListItemsQuery(string list)
        {
            List = list;
        }

        protected override ImmutableList<string> Execute(TodoModel model)
        {
            return model.GetItems(List);
        }
    }
}