using Callisto.SharedKernel.Enum;
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
        public RequestResult(RequestStatus status, string systemMessage = "", string friendlyMessage = "")
        {
            Status = status;
            SystemMessage = systemMessage;
            FriendlyMessage = friendlyMessage;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestResult"/> class.
        /// </summary>
        /// <param name="ex">The <see cref="Exception"/></param>
        /// <param name="friendlyMessage">The <see cref="string"/></param>
        public RequestResult(Exception ex, string friendlyMessage = "")
        {
            Status = RequestStatus.Exception;
            SystemMessage = ex.Message;
            FriendlyMessage = string.IsNullOrEmpty(friendlyMessage) ? "Oops, we did not expect that!" : friendlyMessage;
        }

        /// <summary>
        /// Defines the _friendlyMessage
        /// </summary>
        private string _friendlyMessage;

        /// <summary>
        /// Defines the _systemMessage
        /// </summary>
        private string _systemMessage;

        /// <summary>
        /// Gets or sets the Status
        /// </summary>
        public RequestStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the FriendlyMessage
        /// </summary>
        public string FriendlyMessage
        {
            get { return string.IsNullOrEmpty(_friendlyMessage) ? _systemMessage : _friendlyMessage; }
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
        public static RequestResult Success => new RequestResult();

        /// <summary>
        /// The Failed
        /// </summary>
        /// <param name="message">The <see cref="string"/></param>
        /// <returns>The <see cref="RequestResult"/></returns>
        public static RequestResult Failed(string message)
        {
            return new RequestResult(RequestStatus.Failed, message);
        }
    }

    /// <summary>
    /// Defines the <see cref="RequestResult{T}" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RequestResult<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestResult"/> class.
        /// </summary>
        public RequestResult(T objectResult)
        {
            ObjectResult = objectResult;
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
        public RequestResult(RequestStatus status, T objectResult = default, string systemMessage = "", string friendlyMessage = "")
        {
            ObjectResult = objectResult;
            Status = status;
            SystemMessage = systemMessage;
            FriendlyMessage = friendlyMessage;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestResult"/> class.
        /// </summary>
        /// <param name="ex">The <see cref="Exception"/></param>
        /// <param name="friendlyMessage">The <see cref="string"/></param>
        public RequestResult(Exception ex, T objectResult = default, string friendlyMessage = "")
        {
            ObjectResult = objectResult;
            Status = RequestStatus.Exception;
            SystemMessage = ex.Message;
            FriendlyMessage = string.IsNullOrEmpty(friendlyMessage) ? "Oops, we did not expect that!" : friendlyMessage;
        }

        /// <summary>
        /// Defines the _friendlyMessage
        /// </summary>
        private string _friendlyMessage;

        /// <summary>
        /// Defines the _systemMessage
        /// </summary>
        private string _systemMessage;

        /// <summary>
        /// Gets or sets the Status
        /// </summary>
        public RequestStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the FriendlyMessage
        /// </summary>
        public string FriendlyMessage
        {
            get { return string.IsNullOrEmpty(_friendlyMessage) ? _systemMessage : _friendlyMessage; }
            set { _friendlyMessage = value; }
        }

        /// <summary>
        /// Gets or sets the SystemMessage
        /// </summary>
        public string SystemMessage { get; set; }

        /// <summary>
        /// Gets the ObjectResult
        /// </summary>
        public T ObjectResult { get; }

        /// <summary>
        /// The AsResult
        /// </summary>
        /// <returns>The <see cref="RequestResult"/></returns>
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
        public static RequestResult<T> Failed(string message, T objectResult = default)
        {
            return new RequestResult<T>(RequestStatus.Failed, objectResult, message);
        }

      
    }
}
