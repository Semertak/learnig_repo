namespace Engine
{
    public enum ActionType
    {
        /// <summary>
        /// Ничего...
        /// </summary>
        Nope,
        
        /// <summary>
        /// Простое движение.
        /// </summary>
        BallMove,

        /// <summary>
        /// Взаимодействие с блоком.
        /// </summary>
        BallInteract,

        /// <summary>
        /// Появление из генератора.
        /// </summary>
        BallProduced,

        /// <summary>
        /// Шарик упал в корзину.
        /// </summary>
        BallConsumed,
        
        /// <summary>
        /// Шарик изменил цвет.
        /// </summary>
        BallChangeColor,
        
        /// <summary>
        /// Генератор создал новый шарик.
        /// </summary>
        GenerateNewBall,
        
        /// <summary>
        /// Условный блок пропустил ширик через себя.
        /// </summary>
        ConditionNope,
        
        /// <summary>
        /// Условный блок толкнул шарик влево.
        /// </summary>
        ConditionTrue,
        
        /// <summary>
        /// Условный блок толкнул шарик право.
        /// </summary>
        ConditionFalse,
        
        /// <summary>
        /// Блок смены цветы не поменял цвет шарику.
        /// </summary>
        SwitcherNope,
        
        /// <summary>
        /// В корзину попал нужный цвет.
        /// </summary>
        BascketColorMatched,
        
        /// <summary>
        /// В корзину попал шарик не того цвета.
        /// </summary>
        BascketColorMissing
    }
    
    public class StepAction
    {
        public StepAction(ActionType type, IInteract subject, Point position)
        {
            Type = type;
            Subject = subject;
            Position = position;
            CurrentStep = StepCounter.GetInstance().GetCounter();
        }

        /// <summary>
        ///     Номер текущего шага.
        /// </summary>
        public int CurrentStep { get; set; }
        
        /// <summary>
        ///     Тип действия.
        /// </summary>
        public ActionType Type { get; set; }

        /// <summary>
        ///     Если тип действия не взаимодействие, то поля не будет (null).
        /// </summary>
        public IInteract Subject { get; set; }
        
        /// <summary>
        ///     Позиция в которой произшло событие.
        /// </summary>
        public Point Position { get; set; }
    }
}