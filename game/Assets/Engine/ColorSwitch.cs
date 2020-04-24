using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine
{
    /// <summary>
    ///     Блок смены цвета.
    /// </summary>
    public class ColorSwitch : BaseStepable, IInteract
    {
        /// <summary>
        ///     Цвет, для которого будет выполнена смена.
        ///     Если цвет отличается от данного, то смена цвета не будет
        ///     произведена.
        /// </summary>
        public Color FromColor { get; set; }

        /// <summary>
        ///     Цвет замены, шарик после прохода блока сменит цвет на данный.
        /// </summary>
        public Color ToColor { get; set; }

        /// <summary>
        ///     Создать новый экземпляр блока смены цвета.
        /// </summary>
        /// <param name="fromColor">Необходимый цвет для смены.</param>
        /// <param name="toColor">Цвет после смены.</param>
        /// <param name="position">Позиция.</param>
        public ColorSwitch(Color fromColor, Color toColor, Point position)
        {
            FromColor = fromColor;
            ToColor = toColor;
            _position = position;
            _actions = new List<StepAction>();
        }

        /// <summary>
        ///     Создать новый экземпляр блока смены цвета.
        /// </summary>
        /// <param name="fromColor">Необходимый цвет для смены.</param>
        /// <param name="toColor">Цвет после смены.</param>
        /// <param name="x">Координата по X</param>
        /// <param name="y">Координата по Y</param>
        public ColorSwitch(Color fromColor, Color toColor, int x, int y)
            : this(fromColor, toColor, new Point(x, y))
        {
        }

        /// <summary>
        ///     Создать новый экземпляр блока смены цвета.
        ///     Блок смены цвета будет иметь начальные значения:
        ///         Необходимый цвет - Green;
        ///         Цвет после смены - Green;
        ///         Позиция - X: 0, Y: 0;
        /// </summary>
        public ColorSwitch()
            : this(default(Color), default(Color), default(Point))
        {
        }

        public void Interact(Ball ball)
        {
            if (false && !ball.Color.Equals(FromColor))
            {
                _actions.Add(new StepAction(ActionType.SwitcherNope, null, GetPosition()));
                return;
            }

            ball.Color = ToColor;
            ball.GetActions().Add(new Ball.StepAction(ActionType.BallChangeColor, this, ball.GetPosition(), ball.Color));
            ball.Move(0, 1);
            ball.GetActions().Add(new Ball.StepAction(ActionType.BallMove, this, ball.GetPosition(), ball.Color));
        }

        public override string DebugPrint()
        {
            return string.Format(
                "C|{0}>{1}", 
                FromColor.ShortFormat(), 
                ToColor.ShortFormat()).Center(10
            );
        }
    }
}