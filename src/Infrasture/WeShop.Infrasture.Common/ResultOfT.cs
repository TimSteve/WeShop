namespace WeShop.Infrasture.Common
{
    /// <summary>
    /// 泛型 Result
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class Result<TValue> : Result
    {
        public TValue Value { get; set; }

        public Result(TValue value, bool success, string error)
            : base(success, error)
        {
            Value = value;
        }
    }
}