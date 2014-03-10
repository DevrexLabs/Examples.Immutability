using System;
using OrigoDB.Core;
using OrigoDB.Core.Configuration;
using OrigoDB.Core.Logging;

namespace OrigoDb.Examples.Immutable
{
    class Program
    {
        static void Main(string[] args)
        {

            ConsoleLogger.MinimumLevel = LogLevel.Trace;

            EngineConfiguration config = CreateConfig();

            var engine = Engine.For<TodoModel>(config);

            try
            {
                //can only be executed once, exception will be thrown from within the model.
                engine.Execute(new AddListCommand("Todo"));
                engine.Execute(new AddListCommand("Not todo"));
            }
            catch (Exception)
            {
                Console.WriteLine("caught exception creating lists");
            }

            engine.Execute(new AddItemCommand("Todo", "Learn F#"));
            engine.Execute(new AddItemCommand("Todo", "Read James Willstrops book"));
            engine.Execute(new AddItemCommand("Todo", "pushups"));
            engine.Execute(new AddItemCommand("Not todo", "Write messy code"));
            engine.Execute(new AddItemCommand("Not todo", "Eat too much"));

            
            foreach (var item in engine.Execute(new GetListItemsQuery("Todo")))
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
        }

        private static EngineConfiguration CreateConfig()
        {
            var config = new EngineConfiguration();
            config.Kernel = Kernels.Immutability;
            config.EnsureSafeResults = false;
            config.Synchronization = SynchronizationMode.None;
            return config;
        }
    }
}
