using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine
{
    [System.Serializable]
    public class BallHistory
    {
        private List<Ball> _balls;

        private BallHistory()
        {
            _balls = new List<Ball>();
        }

        public List<Ball> GetList () {
            return this._balls;
        }

        private static BallHistory _instance;

        public static BallHistory GetInstance()
        {
            if (_instance == null) {
                _instance = new BallHistory();
            }
            return _instance;
        }

        public void AddBall(Ball ball)
        {
            var findBall = _balls.FindIndex(b => b.ID == ball.ID);

            if (findBall == -1)
            {
                _balls.Add(ball);
            }
            else
            {
                _balls.RemoveAt(findBall);
                _balls.Add(ball);
            }
        }

        public void Clear() {
            this._balls.Clear();
        }
    }
    
    public class StepCounter
    {
        private int _counter;
        public void Inc()
        {
            _counter++;
        }

        public int GetCounter()
        {
            return _counter;
        }
        
        private StepCounter()
        {}

        private static StepCounter _instance;

        public static StepCounter GetInstance()
        {
            return _instance ?? (_instance = new StepCounter());
        }
    }
    
    public class Game
    {
        private IList<IStepable> currentWorld;

        /// <summary>
        ///     Создать экземпляр пустого мира.
        /// </summary>
        public Game()
        {
            currentWorld = new List<IStepable>();
        }

        /// <summary>
        ///     Получить обьект на позиции, за исключением шарика.
        /// </summary>
        /// <param name="point">Точка в которой необходимо получить обьект.</param>
        /// <param name="world">Мир в котором будет выполнен поиск.</param>
        /// <returns></returns>
        public static IStepable GetByPos(Point point, IEnumerable<IStepable> world)
        {
            if (world == null) throw new ArgumentNullException("world");
            return world.FirstOrDefault(e => e.GetPosition().Equals(point) && !(e is Ball));
        }

        /// <summary>
        /// Выполнить один шаг игрового мира.
        /// </summary>
        /// <returns>Новый мир.</returns>
        public IList<IStepable> Step()
        {
            var newWorld = new List<IStepable>();

            foreach (var elem in currentWorld) elem.Step(currentWorld, newWorld);

            StepCounter.GetInstance().Inc();
            currentWorld = newWorld;

            return newWorld;
        }

        /// <summary>
        ///     Добавить новый обьект в текущий мир.
        /// </summary>
        /// <param name="obj">Добавляемый обьект.</param>
        public void AddObject(IStepable obj)
        {
            currentWorld.Add(obj);
        }

        /// <summary>
        ///     Удалить обьект из мира.
        /// </summary>
        /// <param name="obj">Удаляемый обьект.</param>
        public void RemoveObject(IStepable obj)
        {
            currentWorld.Remove(obj);
        }

        /// <summary>
        ///     Получить обьекты в позиции.
        /// </summary>
        /// <param name="pos">Позиция.</param>
        /// <returns>Массив обьектов.</returns>
        public IList<IStepable> GetObjectsByPos(Point pos)
        {
            return currentWorld.Where(x => x.GetPosition().Equals(pos)).ToList();
        }

        /// <summary>
        ///     Получить текущий мир.
        /// </summary>
        public IList<IStepable> GetCurrentWorld()
        {
            return currentWorld;
        }

        public override string ToString()
        {
            var items = GetCurrentWorld();
            var worldWidth = items.Max(x => x.GetPosition().X);
            var worldHeight = items.Max(x => x.GetPosition().Y);

            worldWidth = worldWidth == 0 ? 1 : worldWidth + 1;
            worldHeight = worldHeight == 0 ? 1 : worldHeight + 1;

            var all = "";
            for (var i = 0; i < worldHeight + 1; i++)
            {
                var line = "";
                for (var j = 0; j < worldWidth + 1; j++)
                {
                    var item = GetObjectsByPos(new Point(j, i));
                    if (item.Count == 0)
                    {
                        line += String.Format("{0},{1}", i, j).Center(10);
                        continue;
                    }

                    line += item.First().ToString();
                }

                all += String.Format("{0}\n", line);
            }

            return all;
        }
    }
}