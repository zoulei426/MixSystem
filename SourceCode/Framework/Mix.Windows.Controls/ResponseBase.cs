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

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseBase"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="message">The message.</param>
        public ResponseBase(int code, string message = "")
        {
            StatusCode = code;
            Message = message;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseBase"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="result">The result.</param>
        public ResponseBase(int code, object result = null)
        {
            StatusCode = code;
            Result = result;
        }

        #endregion Ctor
    }
}