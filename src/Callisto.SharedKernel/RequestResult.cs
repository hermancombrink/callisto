using Callisto.SharedKernel.Enum;
using Newtonsoft.Json;
using System;

namespace Callisto.SharedKernel
{
    /// <summary>
    /// Defines the <see cref="RequestResult" />
    /// </summary>
    public class RequestResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestResult"/> class.
        /// </summary>
        public RequestResult()
        {
            Result = string.Empty;
            Status = RequestStatus.Success;
            SystemMessage = string.Empty;
            FriendlyMessage = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestResult"/> class.
        /// </summary>
        public RequestResult(string result)
        {
            Result = result;
            Status = RequestStatus.Success;
            SystemMessage = string.Empty;
            FriendlyMessage = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestResult"/> class.
        /// </summary>
        /// <param name="status">The <see cref="RequestStatus"/></param>
        /// <param name="systemMessage">The <see cref="string"/></param>
        /// <param name="friendlyMessage">The <see cref="string"/></param>
        public RequestResult(RequestStatus status, string result = "", string systemMessage = "", string friendlyMessage = "")
        {
            Result = result;
            Status = status;
            SystemMessage = systemMessage;
            FriendlyMessage = friendlyMessage;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestResult"/> class.
        /// </summary>
        /// <param name="ex">The <see cref="Exception"/></param>
        /// <param name="friendlyMessage">The <see cref="string"/></param>
        public RequestResult(Exception ex, string result = "", string friendlyMessage = "")
        {
            Result = result;
            Status = RequestStatus.Exception;
            SystemMessage = ex.Message;
            FriendlyMessage = string.IsNullOrEmpty(friendlyMessage) ? "Oops, we did not expect that!" : friendlyMessage;
        }

        /// <summary>
        /// Gets the Result
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// Defines the _friendlyMessage
        /// </summary>
        private string _friendlyMessage;

        /// <summary>
        /// Gets or sets the Status
        /// </summary>
        public RequestStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the FriendlyMessage
        /// </summary>
        public string FriendlyMessage
        {
            get { return string.IsNullOrEmpty(_friendlyMessage) ? SystemMessage : _friendlyMessage; }
            set { _friendlyMessage = value; }
        }

        /// <summary>
        /// Gets or sets the SystemMessage
        /// </summary>
        public string SystemMessage { get; set; }

        /// <summary>
        /// The IsSuccess
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        public bool IsSuccess()
        {
            return Status == RequestStatus.Success;
        }

        /// <summary>
        /// The Success
        /// </summary>
        /// <returns>The <see cref="RequestResult"/></returns>
        public static RequestResult Success(string result = "") => new RequestResult(result);

        /// <summary>
        /// The Failed
        /// </summary>
        /// <param name="message">The <see cref="string"/></param>
        /// <returns>The <see cref="RequestResult"/></returns>
        public static RequestResult Failed(string message, string result = "")
        {
            return new RequestResult(RequestStatus.Failed, result: result, friendlyMessage: message);
        }

        /// <summary>
        /// The Validation
        /// </summary>
        /// <param name="message">The <see cref="string"/></param>
        /// <returns>The <see cref="RequestResult"/></returns>
        public static RequestResult Validation(string message, string result = "")
        {
            return new RequestResult(RequestStatus.Warning, result: result, friendlyMessage: message);
        }
    }

    /// <summary>
    /// Defines the <see cref="RequestResult{T}" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RequestResult<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestResult{T}"/> class.
        /// </summary>
        public RequestResult()
        {
            Result = default;
            Status = RequestStatus.Success;
            SystemMessage = string.Empty;
            FriendlyMessage = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestResult"/> class.
        /// </summary>
        public RequestResult(T result)
        {
            Result = result;
            Status = RequestStatus.Success;
            SystemMessage = string.Empty;
            FriendlyMessage = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestResult"/> class.
        /// </summary>
        /// <param name="status">The <see cref="RequestStatus"/></param>
        /// <param name="systemMessage">The <see cref="string"/></param>
        /// <param name="friendlyMessage">The <see cref="string"/></param>
        public RequestResult(RequestStatus status, T result = default, string systemMessage = "", string friendlyMessage = "")
        {
            Result = result;
            Status = status;
            SystemMessage = systemMessage;
            FriendlyMessage = friendlyMessage;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestResult"/> class.
        /// </summary>
        /// <param name="ex">The <see cref="Exception"/></param>
        /// <param name="friendlyMessage">The <see cref="string"/></param>
        public RequestResult(Exception ex, T result = default, string friendlyMessage = "")
        {
            Result = result;
            Status = RequestStatus.Exception;
            SystemMessage = ex.Message;
            FriendlyMessage = string.IsNullOrEmpty(friendlyMessage) ? "Oops, we did not expect that!" : friendlyMessage;
        }

        /// <summary>
        /// Defines the _friendlyMessage
        /// </summary>
        private string _friendlyMessage;

        /// <summary>
        /// Gets or sets the Status
        /// </summary>
        public RequestStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the FriendlyMessage
        /// </summary>
        public string FriendlyMessage
        {
            get { return string.IsNullOrEmpty(_friendlyMessage) ? SystemMessage : _friendlyMessage; }
            set { _friendlyMessage = value; }
        }

        /// <summary>
        /// Gets or sets the SystemMessage
        /// </summary>
        public string SystemMessage { get; set; }

        /// <summary>
        /// Gets the ObjectResult
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        /// The AsResult
        /// </summary>
        /// <returns>The <see cref="RequestResult"/></returns>
        [JsonIgnore]
        public RequestResult AsResult
        {
            get
            {
                return new RequestResult(this.Status, this.SystemMessage, this.FriendlyMessage);
            }
        }

        /// <summary>
        /// The IsSuccess
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        public bool IsSuccess()
        {
            return Status == RequestStatus.Success;
        }

        /// <summary>
        /// The Success
        /// </summary>
        /// <returns>The <see cref="RequestResult"/></returns>
        public static RequestResult<T> Success(T obj) => new RequestResult<T>(obj);

        /// <summary>
        /// The Failed
        /// </summary>
        /// <param name="message">The <see cref="string"/></param>
        /// <returns>The <see cref="RequestResult"/></returns>
        public static RequestResult<T> Failed(string message, T result = default)
        {
            return new RequestResult<T>(RequestStatus.Failed, result, message);
        }

        /// <summary>
        /// The Validation
        /// </summary>
        /// <param name="message">The <see cref="string"/></param>
        /// <param name="objectResult">The <see cref="T"/></param>
        /// <returns>The <see cref="RequestResult{T}"/></returns>
        public static RequestResult<T> Validation(string message, T result = default)
        {
            return new RequestResult<T>(RequestStatus.Warning, result, message);
        }
    }
}
