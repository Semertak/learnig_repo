using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    ///     Условный блок, оператор условного перехода. Перемещает шарик влево или вправо,
    ///     в зависимости от цвета шарика.
    /// </summary>
    public class ColorCondition : BaseStepable, IInteract
    {
        /// <summary>
        ///     Цвет шарика, для перемещения влево.
        /// </summary>
        public Color MatchColor { get; set; }

        /// <summary>
        ///     Вектор движения в случае совпадения цвета.
        ///     Если цвет не совпал, движения будет в обратную сторону.
        /// </summary>
        public Point MoveVector { get; set; }

        /// <summary>
        ///     Создать новый условного блока.
        /// </summary>
        /// <param name="matchColor">Цвет шариков для совпадения.</param>
        /// <param name="moveVector">Вектор движения в случае совпадения цвета.</param>
        /// <param name="position">Позиция шарика.</param>
        public ColorCondition(Color matchColor, Point moveVector, Point position)
        {
            MatchColor = matchColor;
            MoveVector = moveVector;
            _position = position;
            _actions = new List<StepAction>();
        }

        /// <summary>
        ///     Создать новый условного блока.
        /// </summary>
        /// <param name="matchColor">Цвет шариков для совпадения.</param>
        /// <param name="moveVector">Вектор движения в случае совпадения цвета.</param>
        /// <param name="x">Координата по X.</param>
        /// <param name="y">Координа по Y.</param>
        public ColorCondition(Color matchColor, Point moveVector, int x, int y)
            : this(matchColor, moveVector, new Point(x, y))
        {
        }
        
        public void Interact(Ball ball)
        {
            if (!ball.Color.Equals(MatchColor))
            {
                _actions.Add(new StepAction(ActionType.ConditionFalse, null, GetPosition()));
                
                // Движение в другую сторону.
                var newPoint = new Point(MoveVector.X * -1, MoveVector.Y);
                ball.Move(newPoint);
                return;
            }
            
            _actions.Add(new StepAction(ActionType.ConditionTrue, null, GetPosition()));
            ball.Move(MoveVector);
        }

        public override string DebugPrint()
        {
            if (MoveVector.X >= 0)
            {
                return string.Format(
                    "-IF|{0}+",
                    MatchColor.ShortFormat()
                ).Center(10);
            }

            return string.Format(
                "+IF|{0}-",
                MatchColor.ShortFormat()
            ).Center(10);
        }
    }
}