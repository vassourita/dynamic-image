using System;

namespace DynamicImage
{
    public interface ICounterProvider
    {
        /// <summary>
        /// Increments the counter by 1 and returns the old value.
        /// </summary>
        /// <param name="counterId">The counter identifier.</param>
        int Increment(Guid counterId);
    }
}