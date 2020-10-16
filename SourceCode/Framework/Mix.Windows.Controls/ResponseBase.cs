namespace Mix.Service.Core
{
    /// <summary>
    /// 响应基类
    /// </summary>
    public class ResponseBase
    {
        #region Properties

        /// <summary>
        /// 状态码
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 返回结果
        /// </summary>
        public object Result { get; set; }

        #endregion Properties

        #region Ctor

        public ResponseBase(int code, string message = "")
        {
            StatusCode = code;
            Message = message;
        }

        public ResponseBase(int code, object result = null)
        {
            StatusCode = code;
            Result = result;
        }

        #endregion Ctor
    }
}