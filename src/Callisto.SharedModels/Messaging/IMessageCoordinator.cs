using Callisto.SharedKernel.Messaging;
using System;
using System.Threading.Tasks;

namespace Callisto.SharedModels.Messaging
{
    /// <summary>
    /// Defines the <see cref="IMessageCoordinator" />
    /// </summary>
    public interface IMessageCoordinator : IDisposable
    {
        /// <summary>
        /// The Publish
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message">The <see cref="T"/></param>
        void Publish<T>(T message);

        /// <summary>
        /// The StopConsuming
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void StopConsuming<T>();

        /// <summary>
        /// The Consume
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="messageDelegate">The <see cref="Func{T, Task{IMessageResult}}"/></param>
        void Consume<T>(Func<T, Task<IMessageResult>> messageDelegate);
    }
}
