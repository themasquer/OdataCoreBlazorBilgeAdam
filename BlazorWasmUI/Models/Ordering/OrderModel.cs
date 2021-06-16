namespace BlazorWasmUI.Models.Ordering
{
    public class OrderModel
    {
        public string Expression { get; set; }
        public bool DirectionAscending { get; set; }

        public OrderModel(string expression, bool directionAscending = true)
        {
            Expression = expression;
            DirectionAscending = directionAscending;
        }
    }
}
