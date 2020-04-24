using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine
{
    /// <summary>
    ///     Генерирует заданое кол-во шариков определенных цветов.
    ///     Позиция шариков устанавливается в позицию генератора.
    /// </summary>
    public class BallGenerator : BaseStepable
    {
        /// <summary>
        ///     Словарь с цветом шарика и количеством.
        /// </summary>
        private readonly Dictionary<Color, int> _ballsSettings;

        /// <summary>
        ///     Массив шариков которые нужно создать.
        /// </summary>
        private readonly List<Ball> _genBalls;

        /// <summary>
        ///     Создать новый экземпляр шарика.
        ///     Шарик будет иметь начальные значения:
        ///         Цвет - Green;
        ///         Позиция - X: 0, Y: 0;
        /// </summary>
        
        /// <summary>
        ///     Создать новый экземпляр блока генератора. 
        /// </summary>
        /// <param name="ballsSettings">Настройки блока.</param>
        /// <param name="point">Позиция генератора.</param>
        public BallGenerator(Dictionary<Color, int> ballsSettings, Point point)
        {
            _ballsSettings = ballsSettings;
            _genBalls = new List<Ball>();
            _position = point;
            _actions = new List<StepAction>();

            _generateBalls();
        }

        /// <summary>
        ///     Создать новый экземпляр блока генератора. 
        /// </summary>
        /// <param name="ballsSettings">Настройки блока.</param>
        /// <param name="x">Позиция генератора по X.</param>
        /// <param name="y">Позиция генератора по Y.</param>
        public BallGenerator(Dictionary<Color, int> ballsSettings, int x, int y)
            : this(ballsSettings, new Point(x, y))
        {
        }

        /// <summary>
        ///     Выполнить случайную сортировку списка.
        /// </summary>
        /// <param name="list">Список.</param>
        /// <typeparam name="T">Тип эллементов списка.</typeparam>
        public static void Shuffle<T>(IList<T> list)
        {
            var rnd = new Random();

            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = rnd.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        /// <summary>
        ///     Произвести заполнение внутренего хранилища шариками
        ///     в случайном порядке.
        /// </summary>
        private void _generateBalls()
        {   
            int ballsCount = 0;
            foreach (var pair in _ballsSettings)
                for (var i = 0; i < pair.Value; i++)
                {
                    var ball = new Ball(ballsCount) {Color = pair.Key};
                    ball.SetPosition(GetPosition());
                    _genBalls.Add(ball);
                    BallHistory.GetInstance().AddBall(ball);
                    ballsCount++;
                }

            Shuffle(_genBalls);
        }

        public override string DebugPrint()
        {
            if (_genBalls.Count > 0) 
                return string.Format("G-{0}-{1}", _genBalls.Count, _genBalls.First().Color.ShortFormat()).Center(10);

            return string.Format("G-{0}-EM", _genBalls.Count).Center(10);
        }

        public override void Step(IList<IStepable> oldWorld, IList<IStepable> newWorld)
        {
            if (_genBalls.Count <= 0)
            {
                _actions.Add(new StepAction(ActionType.Nope, null, GetPosition()));
                newWorld.Add(this);
                return;
            }

            var ball = _genBalls.First();
            ball.GetActions().Add(new Ball.StepAction(ActionType.BallProduced, null, GetPosition(), ball.Color));
            _genBalls.Remove(ball);

            _actions.Add(new StepAction(ActionType.GenerateNewBall, null, GetPosition()));
            
            newWorld.Add(ball);
            newWorld.Add(this);
        }
    }
}