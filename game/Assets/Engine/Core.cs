using System.Collections.Generic;
using System.Diagnostics;

namespace Engine
{
    /// <summary>
    ///     Интерфейс взаимодействия блока с шариком.
    /// </summary>
    public interface IInteract
    {
        /// <summary>
        ///     Производит взаимодействие с шариком, путем изменения состояния шара.
        /// </summary>
        /// <param name="ball">Шарик для изменений.</param>
        void Interact(Ball ball);
    }

    /// <summary>
    ///     Интерфейс определяющий действия, которые обьект выполняет на одном шаге.
    /// </summary>
    public interface IStepable
    {
        void Step(IList<IStepable> oldWorld, IList<IStepable> newWorld);

        /// <summary>
        ///     Получить текущую позицию.
        /// </summary>
        /// <returns>Позиция в мире.</returns>
        Point GetPosition();

        /// <summary>
        ///     Установить новую позицию.
        /// </summary>
        /// <param name="point">Новая позиция.</param>
        void SetPosition(Point point);

        /// <summary>
        ///     Установить новую позицию.
        /// </summary>
        /// <param name="x">Позиция по X.</param>
        /// <param name="y">Позиция по Y.</param>
        void SetPosition(int x, int y);

        /// <summary>
        ///     Вывести в консоль внутреннее представление обьекта.
        /// </summary>
        /// <returns></returns>
        string DebugPrint();

        IList<StepAction> GetActions();
    }

    public abstract class BaseStepable : IStepable
    {
        protected IList<StepAction> _actions;
        protected Point _position;
        
        public virtual Point GetPosition()
        {
            return _position;
        }

        public virtual void SetPosition(Point point)
        {
            _position = point;
        }

        public virtual void SetPosition(int x, int y)
        {
            _position = new Point(x, y);
        }

        public virtual IList<StepAction> GetActions()
        {
            return _actions;
        }
        
        public virtual void Step(IList<IStepable> oldWorld, IList<IStepable> newWorld)
        {
            _actions.Add(new StepAction(ActionType.Nope, null, GetPosition()));
            newWorld.Add(this);
        }

        public override string ToString()
        {
            return DebugPrint();
        }

        public abstract string DebugPrint();
    }
}