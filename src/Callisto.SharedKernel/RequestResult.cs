using Callisto.SharedKernel.Enum;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Callisto.SharedKernel
{
    /// <summary>
    /// Defines the <see cref="RequestResult" />
    /// </summary>
    public class RequestResult
    {
        /// <summary>
        /// Defines the ExceptionFriendlyMessage
        /// </summary>
        public static string ExceptionFriendlyMessage = "Oops. That was not suppose to happen";

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
        [JsonIgnore]
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
        /// The Critical
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/></param>
        /// <param name="message">The <see cref="string"/></param>
        /// <param name="result">The <see cref="string"/></param>
        /// <returns>The <see cref="RequestResult"/></returns>
        public static RequestResult Critical(Exception exception, string message = "", string result = "")
        {
            if (string.IsNullOrEmpty(message))
            {
                message = ExceptionFriendlyMessage;
            }

            return new RequestResult(RequestStatus.Exception, result: result, friendlyMessage: message, systemMessage: exception.Message);
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

        /// <summary>
        /// The From
        /// </summary>
        /// <param name="action">The <see cref="Action"/></param>
        /// <returns>The <see cref="RequestResult"/></returns>
        public static RequestResult From(Action action)
        {
            try
            {
                action();
                return Success();
            }
            catch (Exception ex)
            {
                return Critical(ex, string.Empty);
            }
        }

        /// <summary>
        /// The From
        /// </summary>
        /// <param name="action">The <see cref="Func{Task}"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        public static async Task<RequestResult> From(Func<Task> action)
        {
            try
            {
                await action();
                return Success();
            }
            catch (Exception ex)
            {
                return Critical(ex, string.Empty);
            }
        }

        /// <summary>
        /// The From
        /// </summary>
        /// <param name="action">The <see cref="Func{Task}"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        public static async Task<RequestResult> From(Func<Task<RequestResult>> action)
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                return Critical(ex, string.Empty);
            }
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
        [JsonIgnore]
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
        /// The Critical
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/></param>
        /// <param name="message">The <see cref="string"/></param>
        /// <param name="result">The <see cref="string"/></param>
        /// <returns>The <see cref="RequestResult"/></returns>
        public static RequestResult<T> Critical(Exception exception, string message, T result = default)
        {
            if (string.IsNullOrEmpty(message))
            {
                message = RequestResult.ExceptionFriendlyMessage;
            }

            return new RequestResult<T>(RequestStatus.Exception, result: result, friendlyMessage: message, systemMessage: exception.Message);
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

        /// <summary>
        /// The From
        /// </summary>
        /// <param name="action">The <see cref="Func{T}"/></param>
        /// <returns>The <see cref="RequestResult{T}"/></returns>
        public static RequestResult<T> From(Func<T> action)
        {
            try
            {
                var result = action();
                return Success(result);
            }
            catch (Exception ex)
            {
                return Critical(ex, string.Empty);
            }
        }

        /// <summary>
        /// The From
        /// </summary>
        /// <param name="action">The <see cref="Func{Task{T}}"/></param>
        /// <returns>The <see cref="Task{RequestResult{T}}"/></returns>
        public static async Task<RequestResult<T>> From(Func<Task<T>> action)
        {
            try
            {
                var result = await action();
                return Success(result);
            }
            catch (Exception ex)
            {
                return Critical(ex, string.Empty);
            }
        }
    }
}
