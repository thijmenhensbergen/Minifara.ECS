


using System.Diagnostics;

namespace MinafaraECF
{
    /// <summary>
    /// A Reality is where all the entities are stored
    /// </summary>
    /// <remarks>
    /// Some other names that are used in other ECS are World and Scene
    /// </remarks>
    public class Reality
    {
        private List<Entity> Entities = new();

        private CancellationTokenSource? CancelToken;
        private bool IsRunning = false;
        public List<Entity> GetEntities()
        {
            return Entities;
        }

        public void Start()
        {
            CancelToken?.Cancel();
            CancelToken = new CancellationTokenSource();
            var token = CancelToken.Token;

            Task.Run(() =>
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                double lastTime = 0;

                while (!token.IsCancellationRequested)
                {
                    double currentTime = stopwatch.Elapsed.TotalSeconds;
                    double deltaTime = currentTime - lastTime;
                    lastTime = currentTime;

                    Process(deltaTime);

                    Thread.Sleep(8);
                }
            }, token);
        }

        public void Stop()
        {
            if (CancelToken != null)
            {
                CancelToken.Cancel();
            }
        }

        private void Process(double DeltaTime)
        {
            foreach (Entity entity in Entities)
            {
                entity.ProcessComponents(DeltaTime);
            }
        }
    }
}
